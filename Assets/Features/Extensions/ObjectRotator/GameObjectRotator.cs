using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Features.Extensions
{
    public class GameObjectRotator : IInitializable, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource = default;
        private Tween _tween = default;

        private readonly GameObjectRotatorData _rotatorData = default;

        public GameObjectRotator(GameObjectRotatorData rotatorData) 
            => _rotatorData = rotatorData;

        void IInitializable.Initialize() => Rotate().Forget();

        private async UniTask Rotate()
        {
            StopRotate();
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (_rotatorData.Tranform.gameObject.activeInHierarchy)
                {
                    _tween = _rotatorData.Tranform.DORotate(_rotatorData.Tranform.rotation.eulerAngles + _rotatorData.Rotation, _rotatorData.Duration).SetEase(_rotatorData.Ease);
                    await UniTask.Delay(TimeSpan.FromSeconds(_rotatorData.Duration), cancellationToken: _cancellationTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{nameof(GameObjectRotator)}: Rotation ex: {ex.Message}\n{ex.StackTrace}");
                }
            }
        }

        private void KillTween()
        {
            if (_tween != null)
            {
                _tween.Kill();
                _tween = null;
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

        private void StopRotate()
        {
            CancelToken();
            KillTween();
        }

        void IDisposable.Dispose() => StopRotate();
    }
}
