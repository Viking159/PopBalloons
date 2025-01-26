using Features.DataController;
using System;
using Zenject;

namespace Features.UI.MenuScene
{

    public class UsernameInputController : IInitializable, IDisposable
    {
        private readonly UsernameInputData _inputData = default;
        private readonly UserDataController _userDataController = default;

        public UsernameInputController(UsernameInputData inputData, UserDataController userDataController)
        {
            _inputData = inputData;
            _userDataController = userDataController;
        }

        void IInitializable.Initialize()
        {
            _inputData.InputField.text = _userDataController.Name;
            InputFieldHandler(_inputData.InputField.text);
            _inputData.InputField.onValueChanged.AddListener(InputFieldHandler);
        }

        private void InputFieldHandler(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _inputData.PlayButton.interactable = true;
                _userDataController.SetName(text);
                return;
            }
            _inputData.PlayButton.interactable = false;
        }

        void IDisposable.Dispose() => _inputData.InputField.onValueChanged.RemoveListener(InputFieldHandler);
    }
}
