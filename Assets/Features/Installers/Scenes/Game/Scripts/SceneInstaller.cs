using Features.Gameplay;
using Features.GameplayObjects.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Features.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Camera _camera = default;
        [SerializeField]
        private EventSystem _eventSystem = default;
        [SerializeField]
        private Canvas _baseCanvas = default;
        [SerializeField]
        private BalloonFinishTrigger _finishTrigger = default;
        [SerializeField]
        private int _maxLifes = 5;

        public override void InstallBindings()
        {
            BindGameplay();
            BindSceneObjects();
        }

        private void BindGameplay()
        {
            Container.BindInterfacesTo<GameplayController>().AsSingle().NonLazy();
            Container.Bind<BalloonFinishTrigger>().FromInstance(_finishTrigger).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LifesController>().AsSingle().WithArguments(_maxLifes);
        }

        private void BindSceneObjects()
        {
            Container.Bind<Canvas>().FromComponentInNewPrefab(_baseCanvas).AsSingle().NonLazy();
            Container.Bind<EventSystem>().FromInstance(_eventSystem).AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        }
    }
}