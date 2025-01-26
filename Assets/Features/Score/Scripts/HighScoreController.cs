using Cysharp.Threading.Tasks;
using Features.DataContainer;
using Features.DataController;
using System;
using System.Collections.Generic;
using Zenject;

namespace Features.Score
{
    public sealed class HighScoreController : GenericDataController<string, ScoresData>, IInitializable, IDisposable
    {
        public IReadOnlyList<ScoreData> Scores
            => IsDataNull() ? null : dataContainer.Data.ScoreDatas; 

        private int _index = default;

        public HighScoreController(GenericDataContainer<string, ScoresData> scoresContainer)
            : base(scoresContainer) 
            => InitData();

        public void AddScore(string id, int score)
        {
            _index = dataContainer.Data.ScoreDatas.FindIndex(el => el.Id == id);
            if (_index != -1 && dataContainer.Data.ScoreDatas[_index].Score < score)
            {
                dataContainer.Data.ScoreDatas[_index].Score = score;
            }
            else
            {
                dataContainer.Data.ScoreDatas.Add(new ScoreData() { Id = id, Score = score });
            }
            dataContainer.Data.ScoreDatas.Sort(new ScoreComparer());
            SaveData().Forget();
            NotifyOnDataChange();
        }

        void IInitializable.Initialize() => LoadData().Forget();

        protected override bool IsDataNull()
            => base.IsDataNull() || dataContainer.Data.ScoreDatas == null;

        void IDisposable.Dispose() => CancelToken();
    }
}
