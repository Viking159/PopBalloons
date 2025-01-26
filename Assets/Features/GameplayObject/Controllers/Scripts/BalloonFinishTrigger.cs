using Features.Gameplay;
using UnityEngine;
using Zenject;

namespace Features.GameplayObjects.Components
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class BalloonFinishTrigger : MonoBehaviour
    {
        private Balloon _balloon = default;
        private LifesController _lifesController = default;

        [Inject]
        public void Contruct(LifesController lifesController) 
            => _lifesController = lifesController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out _balloon))
            {
                _balloon.SetState(BalloonState.Death);
                _lifesController.SetLifes(_lifesController.Lifes - 1);
            }
        }
    }
}
