using System;

namespace Features.GameplayObjects
{
    public interface IKillable
    {
        event Action onKill;

        void Kill();
    }
}
