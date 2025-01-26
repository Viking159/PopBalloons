using Features.Extensions;
using UnityEngine;
using Zenject;

namespace Features.Installers
{
    public sealed class LoaderInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObjectRotatorData _gameObjectRotatorData = default;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameObjectRotator>().AsSingle().WithArguments(_gameObjectRotatorData).NonLazy();
        }
    }
}
