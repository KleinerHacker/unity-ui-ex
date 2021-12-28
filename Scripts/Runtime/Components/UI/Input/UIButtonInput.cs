using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.InputMenu + "/Button Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButtonInput : UIInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private GamepadButton gamepadButton = GamepadButton.A;

        [SerializeField]
        private Key key = Key.Digit0;

        #endregion

        private Button _button;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _button = GetComponent<Button>();
        }

        protected override void LateUpdate()
        {
            if ((GamepadAvailable && Gamepad.current[gamepadButton].wasPressedThisFrame) ||
                (KeyboardAvailable && Keyboard.current[key].wasPressedThisFrame))
            {
                if (_button.interactable)
                {
                    _button.OnPointerClick(new PointerEventData(EventSystem.current));
                }
            }
        }

        #endregion
    }
}