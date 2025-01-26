using System;
using UnityEngine;

namespace Features.ClickSystem
{
    public interface IClickSystem
    {
        /// <summary>
        /// Click event with position
        /// </summary>
        event Action<Vector2> onClick;
    }
}
