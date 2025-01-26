using Features.SceneControl;
using Features.UI.Components;
using Features.UI.GameScene;
using UnityEngine;
using Zenject;

namespace Features.Installers.GameObjects
{
    public sealed class ResultScreenInstaller : MonoInstaller
    {
        [SerializeField]
        private CanvasGroupAnimationData _canvasGroupAnimationData = default;
        [SerializeField]
        private StateButton _restartButton = default;
        [SerializeField]
        private OpenSceneButtonData _openMenuButtonData = default;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<OpenSceneButtonController>().AsSingle().WithArguments(_openMenuButtonData);
            Container.BindInterfacesTo<ResultAnimationController>().AsSingle().WithArguments(_canvasGroupAnimationData);
            Container.BindInterfacesTo<GameplayStateButtonController>().AsSingle().WithArguments(_restartButton);
        }
    }
}