using Features.GameplayObjects;
using Features.GameplayObjects.Components;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Installers.GameObjects
{
    public sealed class BalloonInstaller : MonoInstaller
    {
        [SerializeField]
        private Balloon _balloon = default;
        [SerializeField]
        private MoveData _moveData = default;
        [SerializeField]
        private SpriteRenderer _spriteRenderer = default;
        [SerializeField]
        private ParticleSystem _particleSystem = default;
        [SerializeField]
        private List<Color> _colors = new List<Color>();

        private Color _balloonColor = Color.white;

        public override void InstallBindings()
        {
            _balloonColor = _colors[Random.Range(0, _colors.Count)];
            Container.BindInterfacesTo<SpriteRendererColorView>().AsSingle().WithArguments(_balloonColor);
            Container.BindInterfacesAndSelfTo<MoveData>().FromInstance(_moveData).AsSingle();
            BindComponents();
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<ParticleDeathController>().AsSingle().WithArguments(_balloonColor);
            Container.BindInterfacesAndSelfTo<FlyUpController>().AsSingle();
        }

        private void BindComponents()
        {
            Container.BindInterfacesAndSelfTo<Balloon>().FromInstance(_balloon).AsSingle();
            Container.BindInterfacesAndSelfTo<Transform>().FromInstance(_balloon.transform).AsSingle();
            Container.BindInterfacesAndSelfTo<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();
            Container.BindInterfacesAndSelfTo<ParticleSystem>().FromInstance(_particleSystem).AsSingle();
        }
    }
}