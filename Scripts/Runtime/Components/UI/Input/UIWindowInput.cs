using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.InputMenu + "/Window Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIWindow))]
    public sealed class UIWindowInput : UIInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private GamepadButton gamepadButton = GamepadButton.Start;

        [SerializeField]
        private Key key = Key.Escape;

        #endregion
        
        private UIWindow _window;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _window = GetComponent<UIWindow>();
        }

        protected override void LateUpdate()
        {
            if ((GamepadAvailable && Gamepad.current[gamepadButton].wasPressedThisFrame) ||
                (KeyboardAvailable && Keyboard.current[key].wasPressedThisFrame))
            {
                _window.ToggleVisibility();
            }
        }

        #endregion
    }
}