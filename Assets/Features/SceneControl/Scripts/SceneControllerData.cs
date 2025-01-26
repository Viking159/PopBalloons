using System;
using UnityEngine;

namespace Features.SceneControl
{
    [Serializable]
    public class SceneControllerData
    {
        public string FirstSceneName = "Menu";
        public string LoadingSceneName = "Loading";
        [Min(0)]
        public float MinAwaitTime = 2.5f;
        [Range(0, 1f)]
        public float AwaitTime = 0.1f; 
    }
}
