using UnityEngine;
using System;
using Features.ClickSystem;

namespace Features.ClickHandler
{
    public abstract class AbstractClickHandler : IClickHandler
    {
        public event Action onObjectClicked = delegate { };

        protected readonly IClickSystem clickSystem = default;
        protected readonly Camera camera = default;
        protected readonly int layerMask = default;
        protected readonly int distance = default;

        public AbstractClickHandler(IClickSystem clickSystem, Camera camera, int layerMask, int distance)
        {
            this.clickSystem = clickSystem;
            this.camera = camera;
            this.layerMask = layerMask;
            this.distance = distance;
        }

        public virtual void Initialize() => clickSystem.onClick += CheckClick;

        protected abstract void CheckClick(Vector2 position);

        protected virtual void NotifyOnClick() => onObjectClicked();

        public virtual void Dispose() => clickSystem.onClick -= CheckClick;
    }
}
