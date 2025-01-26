using UnityEngine;
using Features.ClickSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Features.ClickHandler
{
    public abstract class AbstractComponentClickHandler<T> : AbstractClickHandler
        where T : Component
    {
        public T ClickedComponent => clickedComponent;
        protected T clickedComponent = default;

        protected PointerEventData eventData = default;
        protected List<RaycastResult> raycastResults = new List<RaycastResult>();
        protected RaycastHit2D hit = default;
        protected EventSystem eventSystem = default;

        protected AbstractComponentClickHandler(IClickSystem clickSystem, Camera camera, EventSystem eventSystem, int layerMask, int distance)
            : base(clickSystem, camera, layerMask, distance)
        {
            this.eventSystem = eventSystem;
        }

        public override void Initialize()
        {
            eventData = new PointerEventData(eventSystem);
            base.Initialize();
        }

        protected override void CheckClick(Vector2 position)
        {
            eventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(eventData, raycastResults);
            if (raycastResults.Count == 0)
            {
                hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, distance, layerMask);
                if (hit.collider != null && hit.collider.TryGetComponent(out clickedComponent))
                {
                    ClickComponentHandler();
                }
            }
        }

        protected abstract void ClickComponentHandler();
    }
}
