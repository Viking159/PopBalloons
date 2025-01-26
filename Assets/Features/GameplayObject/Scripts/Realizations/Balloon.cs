using System;
using UnityEngine;
using Zenject;
using DG.Tweening;
using Features.Score;
using Features.Gameplay;

namespace Features.GameplayObjects
{
    public sealed class Balloon : BaseClickableObject, IBalloonStateMachine, IMovable, IKillable
    {
        private class MoveAxisData : IDisposable
        {
            public float Position = default;
            public float Duration = default;
            public Tween Tween = default;

            public void Dispose()
            {
                if (Tween != null)
                {
                    Tween.Kill();
                    Tween = null;
                }
            }
        }

        public event Action onStateChanged = delegate { };

        public event Action onKill = delegate { };

        public BalloonState State => _state;
        private BalloonState _state = BalloonState.Idle;

        private MoveAxisData _moveXData = new MoveAxisData();
        private MoveAxisData _moveYData = new MoveAxisData();
        private MoveAxisData _moveZData = new MoveAxisData();

        private IGameplayStateMachine _gameplayStateMachine = default;

        private float _newDuraction = default;

        [Inject]
        public void Construct(IGameplayStateMachine gameplayStateMachine)
        {
            _gameplayStateMachine = gameplayStateMachine;
            GameplayStateHandler();
            _gameplayStateMachine.onStateChanged += GameplayStateHandler;
        }

        public override void ClickHandler()
        {
            if (_state != BalloonState.Death)
            {
                base.ClickHandler();
                SetState(BalloonState.Death);
            }
        }

        public void Kill()
        {
            if (_state != BalloonState.Death)
            {
                isClickable = false;
                onKill();
            }
        }

        public void Move(Vector3 delta, float duration)
        {
            _newDuraction = duration;
            if (delta.x != 0)
            {
                MoveAxis(_moveXData, transform.position.x + delta.x, transform.DOMoveX);
            }
            if (delta.y != 0)
            {
                MoveAxis(_moveYData, transform.position.y + delta.y, transform.DOMoveY);
            }
            if (delta.z != 0)
            {
                MoveAxis(_moveZData, transform.position.z + delta.z, transform.DOMoveZ);
            }
        }

        public void Stop()
        {
            DisposeMoveData();
            SetState(BalloonState.Idle);
        }

        public void SetState(BalloonState state)
        {
            if (_state != state)
            {
                if (state == BalloonState.Death)
                {
                    Kill();
                }
                _state = state;
                onStateChanged();
            }
        }

        private void MoveAxis(MoveAxisData moveAxisData, float endValue, Func<float, float, bool, Tween> moveFunc)
        {
            if (moveAxisData != null && moveFunc != null && (moveAxisData.Position != endValue || moveAxisData.Duration != _newDuraction))
            {
                moveAxisData.Dispose();
                moveAxisData.Position = endValue;
                moveAxisData.Duration = _newDuraction;
                moveAxisData.Tween = moveFunc(moveAxisData.Position, moveAxisData.Duration, false).SetEase(Ease.Linear);
            }
        }

        private void GameplayStateHandler()
        {
            isClickable = _gameplayStateMachine.State == GameplayState.Active;
            switch (_gameplayStateMachine.State)
            {
                case GameplayState.Active:
                    SetState(BalloonState.Move);
                    break;
                case GameplayState.Idle:
                case GameplayState.Paused:
                    SetState(BalloonState.Idle);
                    break;
                case GameplayState.End:
                default:
                    SetState(BalloonState.Death);
                    break;
            }
        }

        private void DisposeMoveData()
        {
            _moveXData?.Dispose();
            _moveYData?.Dispose();
            _moveZData?.Dispose();
        }

        private void OnDestroy()
        {
            _gameplayStateMachine.onStateChanged -= GameplayStateHandler;
            DisposeMoveData();
        }
    }
}
