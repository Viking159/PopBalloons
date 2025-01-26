using Features.Extensions;
using UnityEngine;

namespace Features.GameplayObjects.Components
{
    [CreateAssetMenu(fileName = nameof(BalloonSpawnData), menuName = "Features/GameplayObjects/Components/" + nameof(BalloonSpawnData))]
    public sealed class BalloonSpawnData : ScriptableObject
    {
        public RandomPair XPosition => _xPosition;
        [SerializeField]
        private RandomPair _xPosition = new RandomPair()
        {
            MinValue = -4,
            MaxValue = 4
        };

        public RandomPair SpawnAwaitSeconds => _spawnAwaitSeconds;
        [SerializeField]
        private RandomPair _spawnAwaitSeconds = new RandomPair()
        {
            MinValue = 1,
            MaxValue = 5
        };
    }
}