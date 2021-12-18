using System;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.InputMenu + "/Toggle Group Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ToggleGroup))]
    public sealed class UIToggleGroupInput : UIInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private Toggle[] toggles;
        
        [Space]
        [SerializeField]
        private GamepadButton negativeGamepadButton = GamepadButton.LeftShoulder;

        [SerializeField]
        private GamepadButton positiveGamepadButton = GamepadButton.RightShoulder;

        [Space]
        [SerializeField]
        private Key negativeKey = Key.LeftArrow;

        [SerializeField]
        private Key positiveKey = Key.RightArrow;

        [Space]
        [Tooltip("Behavior to define what is happen if first or last toggle was reached")]
        [SerializeField]
        private UIToggleGroupInputBehavior behavior = UIToggleGroupInputBehavior.Circle;

        #endregion

        private int _index;

        #region Builtin Methods

        protected override void Start()
        {
            _index = toggles.IndexOf(x => x.isOn);
        }

        protected override void LateUpdate()
        {
            if ((GamepadAvailable && Gamepad.current[negativeGamepadButton].isPressed) ||
                (KeyboardAvailable && Keyboard.current[negativeKey].isPressed))
            {
                GotoPrev();
            }
            else if ((GamepadAvailable && Gamepad.current[positiveGamepadButton].isPressed) ||
                     (KeyboardAvailable && Keyboard.current[positiveKey].isPressed))
            {
                GotoNext();
            }
        }

        #endregion

        private void GotoNext()
        {
            _index++;
            if (_index >= toggles.Length)
            {
                _index = behavior switch
                {
                    UIToggleGroupInputBehavior.Stays => toggles.Length - 1,
                    UIToggleGroupInputBehavior.Circle => 0,
                    _ => throw new NotImplementedException(behavior.ToString())
                };
            }

            toggles[_index].isOn = true;
        }

        private void GotoPrev()
        {
            _index--;
            if (_index < 0)
            {
                _index = behavior switch
                {
                    UIToggleGroupInputBehavior.Stays => 0,
                    UIToggleGroupInputBehavior.Circle => toggles.Length - 1,
                    _ => throw new NotImplementedException(behavior.ToString())
                };
            }

            toggles[_index].isOn = true;
        }
    }

    public enum UIToggleGroupInputBehavior
    {
        Stays,
        Circle,
    }
}