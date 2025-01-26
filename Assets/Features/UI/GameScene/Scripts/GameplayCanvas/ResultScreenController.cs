using Features.Gameplay;
using System;
using UnityEngine;
using Zenject;

namespace Features.UI.GameScene
{
    public class ResultScreenController : IInitializable, IDisposable
    {
        private GameObject _resultScreen = default;

        private readonly IGameplayStateMachine _gameplayStateMachine = default;
        private readonly DiContainer _diContainer = default;
        private readonly GameObject _resultScreenPrefab = default;

        public ResultScreenController(IGameplayStateMachine gameplayStateMachine, DiContainer diContainer, GameObject resultScreenPrefab)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _diContainer = diContainer;
            _resultScreenPrefab = resultScreenPrefab;
        }

        void IInitializable.Initialize() => _gameplayStateMachine.onStateChanged += SpawnResultScreen;

        private void SpawnResultScreen()
        {
            if (_resultScreen == null && _gameplayStateMachine.State == GameplayState.End)
            {
                _resultScreen = _diContainer.InstantiatePrefab(_resultScreenPrefab);
            }
        }

        void IDisposable.Dispose() => _gameplayStateMachine.onStateChanged -= SpawnResultScreen;
    }
}
