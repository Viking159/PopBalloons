using UnityEngine;
using Zenject;

namespace Features.GameplayObjects.Components
{
    public sealed class SpriteRendererColorView : IInitializable
    {
        private readonly SpriteRenderer _spriteRenderer = default;
        private readonly Color _color = default;

        public SpriteRendererColorView(SpriteRenderer spriteRenderer, Color color)
        {
            _spriteRenderer = spriteRenderer;
            _color = color;
        }

        void IInitializable.Initialize() => SetColor();

        private void SetColor() => _spriteRenderer.color = _color;
    }
}
