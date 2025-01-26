using System;
using UnityEngine;
using Zenject;
using Features.Extensions;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Features.GameplayObjects.Components
{
    public sealed class FlyUpController : IInitializable, IDisposable
    {
        private bool _isMoving = default;
        private float _duration = default;
        private Vector2 _moveVector = Vector2.zero;
        private CancellationTokenSource _cancellationTokenSource = default;

        private readonly IBalloonStateMachine _stateMachine = default;
        private readonly IMovable _movable = default;
        private readonly Transform _transform = default;
        private readonly MoveData _moveData = default;

        public FlyUpController(IBalloonStateMachine stateMachine, IMovable movable, Transform transform, MoveData moveData)
        {
            _stateMachine = stateMachine;
            _movable = movable;
            _transform = transform;
            _moveData = moveData;
        }

        void IInitializable.Initialize()
        {
            StateChangeHandler();
            _stateMachine.onStateChanged += StateChangeHandler;
        }

        private void StateChangeHandler()
        {
            if (_isMoving != (_stateMachine.State == BalloonState.Move))
            {
                _isMoving = _stateMachine.State == BalloonState.Move;
                if (_isMoving)
                {
                    Move();
                }
                else
                {
                    Stop();
                }
            }
        }

        private void Move()
        {
            VerticalMove();
            HorizontalMove().Forget();
        }

        private void VerticalMove()
        {
            _moveVector = (_moveData.TopPosition - _transform.position.y) * Vector2.up;
            _movable.Move(_moveVector, _moveData.GetVerticalMoveDuration(_moveVector.y));
        }

        private async UniTask HorizontalMove()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (_isMoving)
                {
                    if (RandomExtensions.RandomBool())
                    {
                        _moveVector = _moveData.HorizontalSpeed * RandomExtensions.RandomPair(_moveData.HorizontalMoveTime) 
                            * (RandomExtensions.RandomBool() ? Vector2.right : Vector2.left);
                        if (_transform.position.x + _moveVector.x < _moveData.MaxXPosition
                            && _transform.position.x + _moveVector.x > _moveData.MinXPosition)
                        {
                            _duration = _moveData.GetHorizontalMoveDuration(_moveVector.x);
                            _movable.Move(_moveVector, _duration);
                            await UniTask.Delay(TimeSpan.FromSeconds(_duration), cancellationToken: _cancellationTokenSource.Token);
                        }
                    }
                    await UniTask.Delay(TimeSpan.FromSeconds(RandomExtensions.RandomPair(_moveData.HorizontalAwait)), cancellationToken: _cancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{nameof(FlyUpController)}: HorizontalMove ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void Stop()
        {
            _movable.Stop();
            CancelToken();
        }

        private void CancelToken()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        void IDisposable.Dispose()
        {
            _isMoving = false;
            Stop();
            _stateMachine.onStateChanged -= StateChangeHandler;
        }
    }
}
