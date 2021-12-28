using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.InputMenu + "/Toggle Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public sealed class UIToggleInput : UIInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private GamepadButton gamepadButton = GamepadButton.A;

        [SerializeField]
        private Key key = Key.Digit0;

        [Space]
        [SerializeField]
        private bool allowDisableToggle = true;

        #endregion
        
        private Toggle _toggle;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _toggle = GetComponent<Toggle>();
        }

        protected override void LateUpdate()
        {
            if ((GamepadAvailable && Gamepad.current[gamepadButton].wasPressedThisFrame) ||
                (KeyboardAvailable && Keyboard.current[key].wasPressedThisFrame))
            {
                if (_toggle.interactable)
                {
                    _toggle.isOn = !allowDisableToggle || !_toggle.isOn;
                }
            }
        }

        #endregion
    }
}