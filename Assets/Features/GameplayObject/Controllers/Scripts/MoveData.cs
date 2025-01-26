using Features.Extensions;
using UnityEngine;

namespace Features.GameplayObjects.Components
{
    [CreateAssetMenu(fileName = nameof(MoveData), menuName = "Features/GameplayObjects/Components/" + nameof(MoveData))]
    public sealed class MoveData : ScriptableObject
    {
        public float TopPosition => _topPosition;
        [SerializeField]
        private float _topPosition = 12;

        public float MaxXPosition => _maxXPosition;
        [SerializeField]
        private float _maxXPosition = 3.8f;

        public float MinXPosition => _minXPosition;
        [SerializeField]
        private float _minXPosition = -3.8f;

        public float VerticalSpeed => _verticalSpeed;
        [Space, Header("Speed in units per second")]
        [SerializeField]
        private float _verticalSpeed = 1.75f;

        public float HorizontalSpeed => _horizontalSpeed;
        [SerializeField]
        private float _horizontalSpeed = 0.75f;

        
        public RandomPair HorizontalMoveTime => _horizontalMoveTime;
        [Space]
        [SerializeField]
        private RandomPair _horizontalMoveTime = new RandomPair()
        {
            MinValue = 1,
            MaxValue = 2.5f
        };
        
        public RandomPair HorizontalAwait => _horizontalAwait;
        [Space, Header("Await seconds before new horizontal movement")]
        [SerializeField]
        private RandomPair _horizontalAwait = new RandomPair()
        {
            MinValue = 0,
            MaxValue = 3f
        };

        public float GetVerticalMoveDuration(float distance) => Mathf.Abs(distance) / VerticalSpeed;

        public float GetHorizontalMoveDuration(float distance) => Mathf.Abs(distance) / HorizontalSpeed;
    }
}
