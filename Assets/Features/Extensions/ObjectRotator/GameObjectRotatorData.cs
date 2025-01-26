using DG.Tweening;
using System;
using UnityEngine;

namespace Features.Extensions
{
    [Serializable]
    public class GameObjectRotatorData
    {
        public Transform Tranform = default;
        public Vector3 Rotation = new Vector3(0, 0, 360);
        [Min(0f)]
        public float Duration = 1;
        public Ease Ease = Ease.Linear;
    }
}
