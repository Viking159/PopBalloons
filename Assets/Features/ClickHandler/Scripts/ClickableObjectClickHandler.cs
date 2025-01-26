using Features.ClickSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using Features.GameplayObjects;

namespace Features.ClickHandler
{
    public class ClickableObjectClickHandler : AbstractComponentClickHandler<BaseClickableObject>
    {
        public ClickableObjectClickHandler(IClickSystem clickSystem, Camera camera, EventSystem eventSystem, int layerMask, int distance)
            : base(clickSystem, camera, eventSystem, layerMask, distance)
        { }

        protected override void ClickComponentHandler()
        {
            if (clickedComponent.IsClickable)
            {
                clickedComponent.ClickHandler();
                NotifyOnClick();
            }
        }
    }
}
