using DG.Tweening;
using Features.UI.Components;
using System;

namespace Features.UI.GameScene
{
    public class CanvasGroupAnimator : IDisposable
    {
        protected Tween tween = default;
        protected bool isVisible = false;

        protected readonly CanvasGroupAnimationData canvasGroupAnimationData = default;

        public CanvasGroupAnimator(CanvasGroupAnimationData canvasGroupAnimationData) => this.canvasGroupAnimationData = canvasGroupAnimationData;

        protected virtual void HideCanvasGroup()
        {
            if (isVisible)
            {
                isVisible = false;
                canvasGroupAnimationData.CanvasGroup.alpha = CanvasGroupAnimationData.MAX_ALPHA_VALUE;
                KillTween();
                tween = canvasGroupAnimationData.CanvasGroup.DOFade(CanvasGroupAnimationData.MIN_ALPHA_VALUE, canvasGroupAnimationData.Duration);
                tween.onComplete += SetInactiveInteractable;
            }

        }

        protected virtual void ShowCanvasGroup()
        {
            if (!isVisible)
            {
                isVisible = true;
                KillTween();
                canvasGroupAnimationData.CanvasGroup.alpha = CanvasGroupAnimationData.MIN_ALPHA_VALUE;
                SetInteractable(true);
                tween = canvasGroupAnimationData.CanvasGroup.DOFade(CanvasGroupAnimationData.MAX_ALPHA_VALUE, canvasGroupAnimationData.Duration);
            }
        }

        protected virtual void SetInactiveInteractable() => SetInteractable(false);

        protected virtual void SetInteractable(bool state)
        {
            canvasGroupAnimationData.CanvasGroup.interactable = state;
            canvasGroupAnimationData.CanvasGroup.blocksRaycasts = state;
        }

        protected virtual void KillTween()
        {
            if (tween != null)
            {
                tween.onComplete -= SetInactiveInteractable;
                tween.Kill();
                tween = null;
            }
        }

        public virtual void Dispose() => KillTween();
    }
}
