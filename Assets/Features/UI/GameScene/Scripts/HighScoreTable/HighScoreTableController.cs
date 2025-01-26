using Features.Score;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.UI.GameScene
{
    public sealed class HighScoreTableController : IInitializable, IDisposable
    {
        private List<UserResultController> _userResults = new List<UserResultController>();

        private readonly HighScoreController _highScoreController = default;
        private readonly DiContainer _diContainer = default;
        private readonly UserResultController _userResultPrefab = default;
        private readonly Transform _userResultParent = default;

        public HighScoreTableController(HighScoreController highScoreController, DiContainer diContainer, UserResultController userResultPrefab, Transform userResultParent)
        {
            _highScoreController = highScoreController;
            _diContainer = diContainer;
            _userResultPrefab = userResultPrefab;
            _userResultParent = userResultParent;
        }

        void IInitializable.Initialize()
        {
            InitScores();
            _highScoreController.onDataChanged += InitScores;
        }

        private void InitScores()
        {
            DisableOverflow();
            for (int i = 0; i < _highScoreController.Scores.Count; i++)
            {
                if (i >= _userResults.Count)
                {
                    _userResults.Add(Spawn());
                }
                _userResults[i].InitData(new UserResultData()
                {
                    Number = i + 1,
                    Name = _highScoreController.Scores[i].Id,
                    Score = _highScoreController.Scores[i].Score
                });
                _userResults[i].gameObject.SetActive(true);
            }
        }

        private void DisableOverflow()
        {
            for (int i = _highScoreController.Scores.Count; i < _userResults.Count; i++)
            {
                _userResults[i].gameObject.SetActive(false);
            }
        }

        private UserResultController Spawn() 
            => _diContainer.InstantiatePrefabForComponent<UserResultController>(_userResultPrefab, _userResultParent);

        void IDisposable.Dispose() => _highScoreController.onDataChanged -= InitScores;
    }
}
