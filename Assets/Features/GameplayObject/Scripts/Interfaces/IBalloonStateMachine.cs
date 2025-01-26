using System;

namespace Features.GameplayObjects
{
    public interface IBalloonStateMachine
    {
        event Action onStateChanged;

        BalloonState State { get; }

        void SetState(BalloonState state);
    }
}
