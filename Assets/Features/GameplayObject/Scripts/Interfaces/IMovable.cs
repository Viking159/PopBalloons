using System;
using UnityEngine;

namespace Features.GameplayObjects
{
    public interface IMovable
    {
        void Move(Vector3 delta, float duration);

        void Stop();
    }
}
