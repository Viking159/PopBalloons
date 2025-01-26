using Features.GameplayObjects;
using Zenject;

namespace Features.Spawners
{
    public sealed class BalloonSpawner : ISpawner<Balloon>
    {
        private readonly Balloon _balloonPrefab = default;
        private readonly DiContainer _diContainer = default;

        public BalloonSpawner(Balloon balloonPrefab, DiContainer diContainer)
        {
            _balloonPrefab = balloonPrefab;
            _diContainer = diContainer;
        }

        public Balloon Spawn() => _diContainer.InstantiatePrefabForComponent<Balloon>(_balloonPrefab);
    }
}
