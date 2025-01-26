using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Features.SceneControl
{
    public sealed class ScenesController : ISceneController, IInitializable, IDisposable
    {
        public event Action onNextSceneNameChanged = delegate { };

        public string NextSceneName => _nextSceneName;
        private string _nextSceneName = string.Empty;

        private CancellationTokenSource _cancellationTokenSource = default;
        private AsyncOperation _asyncOperation = default;

        private readonly SceneControllerData _sceneControllerData;

        public ScenesController(SceneControllerData sceneControllerData) 
            => _sceneControllerData = sceneControllerData;

        public void OpenScene(string sceneName)
        {
            SetNextSceneName(sceneName);
            if (SceneManager.GetActiveScene().name != _sceneControllerData.LoadingSceneName)
            {
                SceneManager.LoadScene(_sceneControllerData.LoadingSceneName);
            }
            OpenSceneAsync().Forget();
        }

        void IInitializable.Initialize()
        {
            if (SceneManager.GetActiveScene().name != _sceneControllerData.FirstSceneName)
            {
                OpenScene(_sceneControllerData.FirstSceneName);
            }            
        }

        private void SetNextSceneName(string name)
        {
            if (_nextSceneName != name)
            {
                _nextSceneName = name;
                onNextSceneNameChanged();
            }
        }

        private async UniTask OpenSceneAsync()
        {
            CancelToken();
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token);
                _asyncOperation = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);
                _asyncOperation.allowSceneActivation = false;
                await UniTask.Delay(TimeSpan.FromSeconds(_sceneControllerData.MinAwaitTime), cancellationToken: _cancellationTokenSource.Token);
                while (_asyncOperation.progress < 0.9f)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_sceneControllerData.AwaitTime), cancellationToken: _cancellationTokenSource.Token);
                }
                _asyncOperation.allowSceneActivation = true;
                while (!_asyncOperation.isDone)
                {
                    await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token);
                }
                _asyncOperation = null;
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextSceneName));
                await SceneManager.UnloadSceneAsync(_sceneControllerData.LoadingSceneName).ToUniTask(cancellationToken: _cancellationTokenSource.Token);
                await Resources.UnloadUnusedAssets().ToUniTask(cancellationToken: _cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{nameof(ScenesController)}: Open scene ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void CancelToken()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        void IDisposable.Dispose() => CancelToken();
    }
}