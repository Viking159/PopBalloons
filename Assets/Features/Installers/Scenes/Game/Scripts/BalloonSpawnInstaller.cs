using Features.GameplayObjects.Components;
using Features.GameplayObjects;
using Features.Spawners;
using UnityEngine;
using Zenject;

namespace Features.Installers
{
    [CreateAssetMenu(fileName = nameof(BalloonSpawnInstaller), menuName = "Features/Installers/" + nameof(BalloonSpawnInstaller))]
    public sealed class BalloonSpawnInstaller : ScriptableObjectInstaller<BalloonSpawnInstaller>
    {
        [SerializeField]
        private BalloonSpawnData _spawnData = default;
        [SerializeField]
        private Balloon _balloon = default;

        public override void InstallBindings()
        {
            Container.Bind<BalloonSpawnData>().FromInstance(_spawnData).AsSingle();
            Container.BindInterfacesAndSelfTo<Balloon>().FromInstance(_balloon).AsSingle();
            Container.BindInterfacesAndSelfTo<BalloonSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<BalloonSpawnController>().AsSingle().NonLazy();
        }
    }
}