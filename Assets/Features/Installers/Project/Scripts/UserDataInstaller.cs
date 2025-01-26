using Features.DataContainer;
using Features.DataController;
using Features.Extensions;
using Features.SaveSystem;
using UnityEngine;
using Zenject;

namespace Features.Installers
{
    [CreateAssetMenu(fileName = nameof(UserDataInstaller), menuName = "Features/Installers/" + nameof(UserDataInstaller))]
    public class UserDataInstaller : ScriptableObjectInstaller<UserDataInstaller>
    {
        [SerializeField]
        private SaveSystemInfo _saveSystemInfo = new SaveSystemInfo()
        {
            SaveInfo = "UserDataKey",
            Password = "rtfOrdc"
        };

        public override void InstallBindings()
        {
            InstallSaveSystem();
            Container.BindInterfacesAndSelfTo<UserDataController>().AsSingle().NonLazy();
        }

        private void InstallSaveSystem()
        {
            Container.Bind<IConverter<string, UserData>>().To<UserDataConverter>().AsSingle();
            Container.Bind<GenericDataContainer<string, UserData>>().AsSingle()
                .WithArguments(new StringPrefsSaveSystem(_saveSystemInfo));
        }
    }
}
