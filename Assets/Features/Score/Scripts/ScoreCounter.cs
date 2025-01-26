using Features.ClickHandler;
using Features.DataController;
using Features.Gameplay;
using System;
using System.Linq;
using Zenject;

namespace Features.Score
{
    public class ScoreCounter : IClickCounter, IInitializable, IDisposable
    {
        public event Action onCount = delegate { };

        public int Score => _score;
        private int _score = default;
        private int _highScore = -1;

        private readonly IGameplayStateMachine _gameplayStateMachine = default;
        private readonly IClickHandler _clickHandler = default;
        private readonly HighScoreController _highScoreController = default;
        private readonly UserDataController _userDataController = default;

        public ScoreCounter(IGameplayStateMachine gameplayStateMachine, IClickHandler clickHandler, HighScoreController highScoreController, UserDataController userDataController)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _clickHandler = clickHandler;
            _highScoreController = highScoreController;
            _userDataController = userDataController;
        }

        public void CountClick() => SetScore(_score + 1);

        void IInitializable.Initialize()
        {
            InitHighScore();
            GameplayStateHandler();
            _gameplayStateMachine.onStateChanged += GameplayStateHandler;
        }

        private void SetScore(int scoreValue)
        {
            _score = scoreValue;
            onCount();
        }

        private void GameplayStateHandler()
        {
            switch (_gameplayStateMachine.State)
            {
                case GameplayState.Idle:
                    SetScore(0);
                    break;
                case GameplayState.Active:
                    _clickHandler.onObjectClicked += CountClick;
                    break;
                case GameplayState.Paused:
                    _clickHandler.onObjectClicked -= CountClick;
                    break;
                case GameplayState.End:
                default:
                    _clickHandler.onObjectClicked -= CountClick;
                    CheckHighScore();
                    break;
            }
        }

        private void CheckHighScore()
        {
            if (_highScore == -1 || _score > _highScore)
            {
                _highScoreController.AddScore(_userDataController.Name, _score);
                _highScore = _score;
            }
        }

        private void InitHighScore()
        {
            _highScore = -1;
            if (_highScoreController.Scores != null && _highScoreController.Scores.Any(el => el.Id == _userDataController.Name))
            {
                _highScore = _highScoreController.Scores.First(el => el.Id == _userDataController.Name).Score;
            }
        }

        void IDisposable.Dispose()
        {
            _clickHandler.onObjectClicked -= CountClick;
            _gameplayStateMachine.onStateChanged -= GameplayStateHandler;
        }
    }
}
