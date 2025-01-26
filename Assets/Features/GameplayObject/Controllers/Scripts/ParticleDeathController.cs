using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Features.GameplayObjects.Components
{
    public sealed class ParticleDeathController : IInitializable, IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource = default;

        private readonly IKillable _killable = default;
        private readonly SpriteRenderer _spriteRenderer = default;
        private readonly ParticleSystem _particleSystem = default;
        private readonly Color _color = default;
        private readonly float _partickleAwaitSeconds = default;

        public ParticleDeathController(IKillable killable, SpriteRenderer spriteRenderer, ParticleSystem particleSystem, Color color)
        {
            _killable = killable;
            _spriteRenderer = spriteRenderer;
            _particleSystem = particleSystem;
            _color = color;
            _partickleAwaitSeconds = _particleSystem.main.startLifetimeMultiplier;
        }

        void IInitializable.Initialize()
        {
            ParticleSystem.MainModule main = _particleSystem.main;
            main.startColor = _color;
            _killable.onKill += KillHandler;
        }

        private async void KillHandler()
        {
            CancelToken();
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                _spriteRenderer.enabled = false;
                _particleSystem.Play();
                await UniTask.Delay(TimeSpan.FromSeconds(_partickleAwaitSeconds), cancellationToken: _cancellationTokenSource.Token);
                if (_spriteRenderer != null)
                {
                    Object.Destroy(_spriteRenderer.gameObject);
                }
            }
            catch (Exception ex)
            {
                if (ex is not OperationCanceledException)
                {
                    Debug.LogError($"{nameof(ParticleDeathController)}: KillHandler ex: {ex.Message}\n{ex.StackTrace}");
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

        void IDisposable.Dispose()
        {
            CancelToken();
            _killable.onKill -= KillHandler;
        }
    }
}
