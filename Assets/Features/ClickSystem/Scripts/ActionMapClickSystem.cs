using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Features.ClickSystem
{
    /// <summary>
    /// Click system based on Unity ActionMap
    /// </summary>
    public sealed class ActionMapClickSystem : IClickSystem, IInitializable, IDisposable
    {
        public event Action<Vector2> onClick = delegate { };

        private DefaultInputActions _inputActionMap = default;

        public ActionMapClickSystem(DefaultInputActions inputActionMap)
            => _inputActionMap = inputActionMap;

        void IInitializable.Initialize()
        {            
            _inputActionMap = new DefaultInputActions();
            _inputActionMap.UI.Click.Enable();
            _inputActionMap.UI.Click.performed += ClickHandler;
        }

        private void ClickHandler(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                onClick(Input.mousePosition);
            }
        }

        void IDisposable.Dispose()
        {
            _inputActionMap.UI.Click.Disable();
            _inputActionMap.UI.Click.performed -= ClickHandler;
        }
    }
}
