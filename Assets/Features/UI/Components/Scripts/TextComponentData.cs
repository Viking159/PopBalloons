using System;
using UnityEngine.UI;

namespace Features.UI.Components
{
    [Serializable]
    public class TextComponentData
    {
        public Text Text = default;
        public string Mask = "{0}";
    }
}
