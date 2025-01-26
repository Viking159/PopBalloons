using System;
using UnityEngine;
using Zenject;

namespace Features.Gameplay
{
    public sealed class LifesController : IInitializable, IDisposable
    {
        public event Action onLifesCountChanged = delegate { };

        public int Lifes => _lifes;
        private int _lifes = 3;

        private readonly IGameplayStateMachine _gameplayStateMachine = default;
        private readonly int _maxLifes = default;

        public LifesController(IGameplayStateMachine gameplayStateMachine, int maxLifes)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _maxLifes = maxLifes;
            _lifes = _maxLifes;
        }

        public void SetLifes(int lifesCount)
        {
            if (lifesCount >= 0 && lifesCount <= _maxLifes)
            {
                _lifes = lifesCount;
                onLifesCountChanged();
                if (_lifes == 0)
                {
                    _gameplayStateMachine.SetState(GameplayState.End);
                }
            }
        }

        void IInitializable.Initialize()
        {
            GameplayStateHandler();
            _gameplayStateMachine.onStateChanged += GameplayStateHandler;
        }

        private void GameplayStateHandler()
        {
            if (_gameplayStateMachine.State == GameplayState.Idle)
            {
                SetLifes(_maxLifes);
            }
        }

        void IDisposable.Dispose()
            => _gameplayStateMachine.onStateChanged -= GameplayStateHandler;
    }
}
