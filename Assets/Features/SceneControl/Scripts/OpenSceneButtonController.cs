using System;
using Zenject;

namespace Features.SceneControl
{
    public class OpenSceneButtonController : IInitializable, IDisposable
    {
        private readonly ISceneController _sceneController = default;
        private readonly OpenSceneButtonData _openSceneButtonData = default;

        public OpenSceneButtonController(ISceneController sceneController, OpenSceneButtonData openSceneButtonData)
        {
            _sceneController = sceneController;
            _openSceneButtonData = openSceneButtonData;
        }

        void IInitializable.Initialize() 
            => _openSceneButtonData.Button.onClick.AddListener(ClickHandler);

        private void ClickHandler() => _sceneController.OpenScene(_openSceneButtonData.SceneName);

        void IDisposable.Dispose() 
            => _openSceneButtonData.Button.onClick.RemoveListener(ClickHandler);
    }
}
