using Features.SceneControl;
using UnityEngine;
using Zenject;

namespace Features.Installers
{
    [CreateAssetMenu(fileName = nameof(SceneControlInstaller), menuName = "Features/Installers/" + nameof(SceneControlInstaller))]
    public sealed class SceneControlInstaller : ScriptableObjectInstaller<SceneControlInstaller>
    {
        [SerializeField]
        private SceneControllerData _sceneControllerData = default;
        public override void InstallBindings()
        {
            Container.Bind<SceneControllerData>().FromInstance(_sceneControllerData).AsSingle();
            Container.BindInterfacesTo<ScenesController>().AsSingle().NonLazy();
        }
    }
}
