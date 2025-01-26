using System;
using Zenject;
using Features.DataContainer;
using Cysharp.Threading.Tasks;

namespace Features.DataController
{
    public sealed class UserDataController : GenericDataController<string, UserData>, IInitializable, IDisposable
    {
        public string Name =>dataContainer.Data != null ? dataContainer.Data.Name : string.Empty;

        public UserDataController(GenericDataContainer<string, UserData> userDataContainer)
            : base(userDataContainer) 
            => InitData();

        public void SetName(string name)
        {
            dataContainer.Data.Name = name;
            SaveData().Forget();
            NotifyOnDataChange();
        }

        void IInitializable.Initialize() => LoadData().Forget();

        protected override bool IsDataNull()
            => base.IsDataNull() || string.IsNullOrEmpty(dataContainer.Data.Name);

        void IDisposable.Dispose() => CancelToken();
    }
}
