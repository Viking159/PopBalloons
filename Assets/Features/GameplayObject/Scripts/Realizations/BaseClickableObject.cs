using System;
using UnityEngine;

namespace Features.GameplayObjects
{
    public class BaseClickableObject : MonoBehaviour, IClickable
    {
        public event Action onClick = delegate { };

        public bool IsClickable => isClickable;
        protected bool isClickable = false;

        public virtual void ClickHandler() => onClick();
    }
}
