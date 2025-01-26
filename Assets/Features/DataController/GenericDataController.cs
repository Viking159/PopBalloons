using Cysharp.Threading.Tasks;
using Features.DataContainer;
using System;
using System.Threading;
using UnityEngine;

namespace Features.DataController
{
    public class GenericDataController<T, G> where G : new()
    {
        public event Action onDataChanged = delegate { };

        protected CancellationTokenSource cancellationTokenSource = default;

        protected readonly GenericDataContainer<T, G> dataContainer = default;

        public GenericDataController(GenericDataContainer<T, G> dataContainer) 
            => this.dataContainer = dataContainer;

        public virtual void ClearData()
        {
            InitData();
            SaveData().Forget();
        }

        protected virtual void InitData() => dataContainer.InitData();

        protected virtual bool IsDataNull() => dataContainer.Data == null;

        protected virtual async UniTask LoadData()
        {
            CancelToken();
            cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await dataContainer.LoadData(cancellationTokenSource.Token);
                if (IsDataNull())
                {
                    InitData();
                }
                NotifyOnDataChange();
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{GetType().Name}: Load data ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        protected virtual async UniTask SaveData()
        {
            CancelToken();
            cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await dataContainer.SaveData(cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{GetType().Name}: Save data ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        protected virtual void CancelToken()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }

        protected virtual void NotifyOnDataChange() => onDataChanged();
    }
}
