using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.GameScene
{
    public sealed class UserResultController : MonoBehaviour
    {
        public int Index { get; private set; } = -1;

        private UserResultTexts _resultTexts = default;
        private UserResultData _resultData = default;

        [Inject]
        public void Construct(UserResultTexts resultTexts) => _resultTexts = resultTexts;

        public void InitData(UserResultData resultData)
        {
            _resultData = resultData;
            SetTexts();
        }

        private void SetTexts()
        {
            SetText(_resultTexts.NumberText, _resultData.Number.ToString());
            SetText(_resultTexts.NameText, _resultData.Name);
            SetText(_resultTexts.ScoreText, _resultData.Score.ToString());
        }

        private void SetText(Text textComponent, string text) => textComponent.text = text;
    }
}
