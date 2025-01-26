using Features.Score;
using System;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.MenuScene
{
    public sealed class ClearScoresButton : IInitializable, IDisposable
    {
        private readonly HighScoreController _highScoreController = default;
        private readonly Button _button = default;

        public ClearScoresButton(HighScoreController highScoreController, Button button)
        {
            _highScoreController = highScoreController;
            _button = button;
        }

        void IInitializable.Initialize() => _button.onClick.AddListener(ClickHandler);

        private void ClickHandler() => _highScoreController.ClearData();

        void IDisposable.Dispose() => _button.onClick.RemoveListener(ClickHandler);
    }
}
