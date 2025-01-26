using System;
using UnityEngine.UI;

namespace Features.UI.Components
{
    [Serializable]
    public class TextToggleData
    {
        public Toggle Toggle = default;
        public Text Text = default;
        public string ActiveText = "Active";
        public string InactiveText = "Inctive";
    }

}
