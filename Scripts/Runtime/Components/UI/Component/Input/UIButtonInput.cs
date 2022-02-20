using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Button Input")]
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

        [Space]
        [SerializeField]
        private GameObject iconObject;

        [SerializeField]
        private Image icon;

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

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            Debug.LogError("***");
            UpdateIconOnValidate(key, gamepadButton, icon, iconObject);
        }
#endif

        #endregion

        protected override void UpdateVisual() => UpdateIcon(key, gamepadButton, icon, iconObject);
    }
}