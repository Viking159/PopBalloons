using System;

namespace Features.Score
{
    public interface IClickCounter
    {
        event Action onCount;

        int Score { get; }

        void CountClick();
    }
}
