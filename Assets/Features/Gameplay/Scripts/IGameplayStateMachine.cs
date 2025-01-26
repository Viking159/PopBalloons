using System;

namespace Features.Gameplay
{
    public interface IGameplayStateMachine
    {
        event Action onStateChanged;

        GameplayState State { get; }

        void SetState(GameplayState state);
    }
}
