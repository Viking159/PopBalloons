using DG.Tweening;
using Features.Gameplay;
using Features.UI.Components;
using Features.UI.MenuScene;
using Zenject;

namespace Features.UI.GameScene
{
    public sealed class ResultAnimationController : CanvasGroupAnimator, IInitializable
    {
        private readonly IGameplayStateMachine _gameplayStateMachine = default;

        public ResultAnimationController(IGameplayStateMachine gameplayStateMachine, CanvasGroupAnimationData canvasGroupAnimationData)
            : base(canvasGroupAnimationData)
        {
            _gameplayStateMachine = gameplayStateMachine;
        }

        void IInitializable.Initialize()
        {
            StateHandler();
            _gameplayStateMachine.onStateChanged += StateHandler;
        }

        private void StateHandler()
        {
            if (_gameplayStateMachine.State == GameplayState.End)
            {
                ShowCanvasGroup();
                return;
            }
            HideCanvasGroup();
        }

        public override void Dispose()
        {
            _gameplayStateMachine.onStateChanged -= StateHandler;
            base.Dispose();
        }
    }
}
