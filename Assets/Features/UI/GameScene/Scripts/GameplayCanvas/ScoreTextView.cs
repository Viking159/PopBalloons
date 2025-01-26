using Features.Score;
using Features.UI.Components;
using System;
using Zenject;

namespace Features.UI.GameScene
{
    public sealed class ScoreTextView : IInitializable, IDisposable
    {
        private readonly ScoreCounter _scoreCounter = default;
        private readonly TextComponentData _textComponentData;

        public ScoreTextView(ScoreCounter scoreCounter, TextComponentData textComponentData)
        {
            _scoreCounter = scoreCounter;
            _textComponentData = textComponentData;
        }

        void IInitializable.Initialize()
        {
            SetText();
            _scoreCounter.onCount += SetText;
        }

        private void SetText() => _textComponentData.Text.text = string.Format(_textComponentData.Mask, _scoreCounter.Score);

        void IDisposable.Dispose() => _scoreCounter.onCount -= SetText;
    }
}
