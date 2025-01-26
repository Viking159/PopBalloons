using Cysharp.Threading.Tasks;
using System.Threading;

namespace Features.SaveSystem
{

    /// <summary>
    /// Save system interface
    /// </summary>
    /// <typeparam name="T">saving data type</typeparam>
    public interface ISaveSystem<T>
    {
        UniTask<T> LoadData(CancellationToken token = default);

        UniTask SaveData(T data, CancellationToken token = default);
    }
}
