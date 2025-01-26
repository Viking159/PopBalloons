using Features.UI.GameScene;
using UnityEngine;
using Zenject;

namespace Features.Installers.GameObjects
{
    public sealed class HighScoreTableInstaller : MonoInstaller
    {
        [SerializeField]
        private UserResultController _userResultControllerPrefab = default;
        [SerializeField]
        private Transform _userDataParent = default;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Transform>().FromInstance(_userDataParent).AsSingle();
            Container.BindInterfacesAndSelfTo<UserResultController>().FromInstance(_userResultControllerPrefab).AsSingle();
            Container.BindInterfacesTo<HighScoreTableController>().AsSingle();
        }
    }
}
