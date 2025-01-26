using Features.Gameplay;
using System;
using UnityEngine;

namespace Features.Spawners
{
    public class BaseSpawnController<T>
        where T : MonoBehaviour
    {
        public event Action onSpawn = delegate { };

        public bool IsSpawning => isSpawning;
        protected bool isSpawning = false;

        protected T spawnedObject = default;

        protected readonly ISpawner<T> spawner = default;

        public BaseSpawnController(ISpawner<T> spawner) => this.spawner = spawner;

        public virtual void StartSpawn() => isSpawning = true;

        public virtual void StopSpawn() => isSpawning = false;

        protected virtual void Spawn()
        {
            spawnedObject = spawner.Spawn();
            NotifyOnSpawn();
        }

        protected virtual void NotifyOnSpawn() => onSpawn();
    }
}
