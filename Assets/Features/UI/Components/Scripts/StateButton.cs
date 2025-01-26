using Features.Gameplay;
using System;
using UnityEngine.UI;

namespace Features.UI.Components
{
    [Serializable]
    public class StateButton
    {
        public Button Button = default;
        public GameplayState State = GameplayState.Idle;
    }
}
