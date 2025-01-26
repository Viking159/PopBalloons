using System;
using Zenject;

namespace Features.Gameplay
{
    public class GameplayController : IGameplayStateMachine, IInitializable
    {
        public event Action onStateChanged = delegate { };

        public GameplayState State => _state;
        private GameplayState _state = GameplayState.Idle;

        public void SetState(GameplayState state)
        {
            if (_state != state)
            {
                _state = state;
                onStateChanged();
                if (state == GameplayState.Idle)
                {
                    StartGame();
                }
            }
        }

        void IInitializable.Initialize() => StartGame();

        private void StartGame() => SetState(GameplayState.Active);
    }
}
