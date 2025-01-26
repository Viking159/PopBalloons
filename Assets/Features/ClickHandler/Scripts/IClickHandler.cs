using System;
using Zenject;

namespace Features.ClickHandler
{
    public interface IClickHandler : IInitializable, IDisposable
    {
        event Action onObjectClicked;
    }
}
