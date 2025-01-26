using Features.UI.Components;
using Features.UI.GameScene;
using UnityEngine;
using Zenject;

namespace Features.Installers.GameObjects
{
    public sealed class ScoreCanvasInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _resultPanelPrefab = default;
        [SerializeField]
        private TextComponentData _scoreTextData = default;
        [SerializeField]
        private TextToggleData _pauseToggleData = default;
        [SerializeField]
        private LifesViewData _lifesViewData = default;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ResultScreenController>().AsSingle().WithArguments(_resultPanelPrefab);
            Container.BindInterfacesTo<ScoreTextView>().AsSingle().WithArguments(_scoreTextData);
            Container.BindInterfacesTo<GamePauseToggle>().AsSingle().WithArguments(_pauseToggleData);
            Container.BindInterfacesTo<LifesView>().AsSingle().WithArguments(_lifesViewData);
        }
    }
}
