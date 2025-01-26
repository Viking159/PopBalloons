using Features.GameplayObjects;
using Features.GameplayObjects.Components;
using Features.Extensions;
using System;
using UnityEngine;
using Zenject;
using Features.Gameplay;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Features.Spawners
{
    public sealed class BalloonSpawnController : BaseSpawnController<Balloon>, IInitializable, IDisposable
    {
        private float _awaitTime = default;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private readonly BalloonSpawnData _ballonSpawnData = default;
        private readonly IGameplayStateMachine _gameplayStateMachine = default;

        public BalloonSpawnController(ISpawner<Balloon> spawner, IGameplayStateMachine gameplayStateMachine, BalloonSpawnData ballonSpawnData) 
            : base(spawner)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _ballonSpawnData = ballonSpawnData;
        }

        
        void IInitializable.Initialize()
        {
            GameplayStateHandler();
            _gameplayStateMachine.onStateChanged += GameplayStateHandler;
        }

        public override void StartSpawn()
        {
            base.StartSpawn();
            SpawnCycle().Forget();
        }

        public override void StopSpawn()
        {
            base.StopSpawn();
            CanclelToken();
        }

        private void GameplayStateHandler()
        {
            if (_gameplayStateMachine.State == GameplayState.Active)
            {
                StartSpawn();
                return;
            }
            StopSpawn();
        }

        private async UniTask SpawnCycle()
        {
            CanclelToken();
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (isSpawning)
                {
                    Spawn();
                    await UniTask.Delay(TimeSpan.FromSeconds(_awaitTime), cancellationToken: _cancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{nameof(BalloonSpawnController)}: spawn ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        protected override void Spawn()
        {
            if (_gameplayStateMachine.State == GameplayState.Active)
            {
                base.Spawn();
                spawnedObject.transform.position += Vector3.right * RandomExtensions.RandomPair(_ballonSpawnData.XPosition);
                _awaitTime = RandomExtensions.RandomPair(_ballonSpawnData.SpawnAwaitSeconds);
            }
        }

        private void CanclelToken()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        void IDisposable.Dispose()
        {
            _gameplayStateMachine.onStateChanged -= GameplayStateHandler;
            StopSpawn();
        }
    }
}
