using UnityEngine;
using System;

namespace Features.UI.Components
{
 
    [Serializable]
    public class CanvasGroupAnimationData
    {
        public const int MIN_ALPHA_VALUE = 0;
        public const int MAX_ALPHA_VALUE = 1;

        public CanvasGroup CanvasGroup = default;
        public float Duration = 0.5f;
    }
}
