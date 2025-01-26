using Features.SceneControl;
using Features.UI.MenuScene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Installers
{
    public sealed class MenuInstaller : MonoInstaller
    {
        [SerializeField]
        private UsernameInputData _usernameInputData = default;
        [SerializeField]
        private OpenSceneButtonData _openGameButtonData = default;
        [SerializeField]
        private Button _clearDataButton = default;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UsernameInputData>().FromInstance(_usernameInputData).AsSingle();
            Container.BindInterfacesTo<OpenSceneButtonController>().AsSingle().WithArguments(_openGameButtonData);
            Container.BindInterfacesTo<UsernameInputController>().AsSingle();
            Container.BindInterfacesTo<ClearScoresButton>().AsSingle().WithArguments(_clearDataButton);
        }
    }
}