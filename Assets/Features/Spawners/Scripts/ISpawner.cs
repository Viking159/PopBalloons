using UnityEngine;

namespace Features.Spawners
{
    public interface ISpawner<T> where T : MonoBehaviour
    {
        T Spawn();
    }
}
