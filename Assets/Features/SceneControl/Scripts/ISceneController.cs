using System;

namespace Features.SceneControl
{
    public interface ISceneController
    {
        event Action onNextSceneNameChanged;

        string NextSceneName { get; }

        /// <summary>
        /// Opens scene through loading scene
        /// </summary>
        void OpenScene(string sceneName);
    }
}
