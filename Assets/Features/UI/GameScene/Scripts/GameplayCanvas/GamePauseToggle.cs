using Features.Gameplay;
using Features.UI.Components;
using System;
using Zenject;

namespace Features.UI.GameScene
{
    public sealed class GamePauseToggle : IInitializable, IDisposable
    {
        private readonly TextToggleData _textToggleData = default;
        private readonly IGameplayStateMachine _gameplayStateMachine = default;

        public GamePauseToggle(IGameplayStateMachine gameplayStateMachine, TextToggleData textToggleData)
        {
            _textToggleData = textToggleData;
            _gameplayStateMachine = gameplayStateMachine;
        }

        void IInitializable.Initialize()
        {
            ToggleHandler(_textToggleData.Toggle.isOn);
            _textToggleData.Toggle.onValueChanged.AddListener(ToggleHandler);
        }

        private void ToggleHandler(bool isOn)
        {
            _textToggleData.Text.text = isOn ? _textToggleData.ActiveText : _textToggleData.InactiveText;
            if (isOn)
            {
                _gameplayStateMachine.SetState(GameplayState.Paused);
                _textToggleData.Text.text = _textToggleData.ActiveText;
            }
            else
            {
                _gameplayStateMachine.SetState(GameplayState.Active);
                _textToggleData.Text.text = _textToggleData.InactiveText;
            }
        }

        void IDisposable.Dispose() => _textToggleData.Toggle.onValueChanged.RemoveListener(ToggleHandler);
    }

}
