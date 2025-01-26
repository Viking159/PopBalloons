using Features.DataContainer;
using Features.Extensions;
using Features.SaveSystem;
using Features.Score;
using UnityEngine;
using Zenject;

namespace Features.Installers
{
    [CreateAssetMenu(fileName = nameof(HighScoreInstaller), menuName = "Features/Installers/" + nameof(HighScoreInstaller))]
    public sealed class HighScoreInstaller : ScriptableObjectInstaller<HighScoreInstaller>
    {
        [SerializeField]
        private SaveSystemInfo _saveSystemInfo = new SaveSystemInfo()
        {
            SaveInfo = "ScoreData/ScoreData.txt",
            Password = "Password"
        };

        public override void InstallBindings()
        {
            InstallSaveSystem();
            Container.BindInterfacesAndSelfTo<HighScoreController>().AsSingle().NonLazy();
        }

        private void InstallSaveSystem()
        {
            Container.Bind<IConverter<string, ScoresData>>().To<ScoresConverter>().AsSingle();
            Container.Bind<GenericDataContainer<string, ScoresData>>().AsSingle()
                .WithArguments(new CrypredJsonSaveSystem(_saveSystemInfo));
        }
    }
}