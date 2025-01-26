using Features.Gameplay;
using Features.UI.Components;
using System;
using Zenject;

namespace Features.UI.GameScene
{
    public class GameplayStateButtonController : IInitializable, IDisposable
    {
        protected readonly IGameplayStateMachine _gameplayStateMachine = default;
        private readonly StateButton _stateButton = default;

        public GameplayStateButtonController(IGameplayStateMachine gameplayStateMachine, StateButton stateButton)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _stateButton = stateButton;
        }

        void IInitializable.Initialize() => _stateButton.Button.onClick.AddListener(ClickHandler);

        protected virtual void ClickHandler() => _gameplayStateMachine.SetState(_stateButton.State);

        void IDisposable.Dispose() => _stateButton.Button.onClick.RemoveListener(ClickHandler);
    }
}
