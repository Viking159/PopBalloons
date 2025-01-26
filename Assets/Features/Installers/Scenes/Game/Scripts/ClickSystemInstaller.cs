using Features.ClickHandler;
using Features.ClickSystem;
using Features.Score;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Features.Installers
{
    [CreateAssetMenu(fileName = nameof(ClickSystemInstaller), menuName = "Features/Installers/" + nameof(ClickSystemInstaller))]
    public sealed class ClickSystemInstaller : ScriptableObjectInstaller<ClickSystemInstaller>
    {
        [SerializeField]
        private LayerMask _clickLayerMask = default;
        [SerializeField, Min(0)]
        private int _distance = 10;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ActionMapClickSystem>().AsSingle().WithArguments(new DefaultInputActions());
            Container.BindInterfacesTo<ClickableObjectClickHandler>().AsSingle().WithArguments((int)_clickLayerMask, _distance);
            Container.BindInterfacesAndSelfTo<ScoreCounter>().AsSingle().NonLazy();
        }
    }
}