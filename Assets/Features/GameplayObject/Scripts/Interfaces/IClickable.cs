using System;

namespace Features.GameplayObjects
{
    public interface IClickable
    {
        event Action onClick;

        bool IsClickable { get; }

        void ClickHandler();
    }
}
