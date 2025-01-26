using Features.SaveSystem;
using Features.Extensions;
using System.Threading;
using System;
using Cysharp.Threading.Tasks;

namespace Features.DataContainer
{
    /// <summary>
    /// Generic data container
    /// </summary>
    /// <typeparam name="T">Data save type</typeparam>
    /// <typeparam name="G">Stored data type</typeparam>
    public class GenericDataContainer<T, G> where G : new()
    {
        public event Action onDataInit = delegate { };

        public bool IsInited
        {
            get => IsInited;
            set
            {
                isInited = value;
                onDataInit();
            }
        }
        protected bool isInited = false;

        public G Data => data;
        protected G data = default;

        protected ISaveSystem<T> saveSystem = default;
        protected IConverter<T, G> converter = default;

        public GenericDataContainer(ISaveSystem<T> saveSystem, IConverter<T, G> converter)
        {
            this.saveSystem = saveSystem;
            this.converter = converter;
        }

        public virtual void InitData() => data = new G();

        public virtual async UniTask LoadData(CancellationToken token = default) 
            => data = converter.ConvertTo(await saveSystem.LoadData(token));

        public virtual async UniTask SaveData(CancellationToken token = default) 
            => await saveSystem.SaveData(converter.ConvertFrom(data), token);
    }
}
