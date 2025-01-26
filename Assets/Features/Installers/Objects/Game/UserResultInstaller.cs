using Features.UI.GameScene;
using UnityEngine;
using Zenject;

namespace Features.Installers.GameObjects
{
    public sealed class UserResultInstaller : MonoInstaller
    {
        [SerializeField]
        private UserResultTexts userResultTexts = default;

        public override void InstallBindings() 
            => Container.BindInterfacesAndSelfTo<UserResultTexts>().FromInstance(userResultTexts).AsSingle();
    }
}