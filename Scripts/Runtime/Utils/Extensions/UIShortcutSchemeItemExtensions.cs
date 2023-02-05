using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils.Extensions;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions
{
    public static class UIShortcutSchemeItemExtensions
    {
        public static bool WasKeyPressed(this UIShortcutSchemeItem item, bool supportsKeyboard = true, bool supportsGamepad = true)
        {
            if (item.InputType != UIShortcutInputType.Button)
            {
                Debug.LogError("[UI-INPUT] Unable to call this method with input type " + item.InputType + " for action " + item.AssignedAction +
                               ". Requires " + nameof(UIShortcutInputType.Button));
                return false;
            }

            return item.InputDevice switch
            {
                UIShortcutInput.Gamepad => supportsGamepad && InputUtils.GetValueFromDevice(Gamepad.current, gamepad => gamepad[item.InputGamepadButton].wasPressedThisFrame),
                UIShortcutInput.Keyboard => supportsKeyboard && InputUtils.GetValueFromDevice(Keyboard.current, keyboard => keyboard[item.InputKeyButton].wasPressedThisFrame),
                _ => throw new NotImplementedException(item.InputDevice.ToString())
            };
        }

        public static Vector2 GetAxis(this UIShortcutSchemeItem item, bool supportsKeyboard = true, bool supportsGamepad = true)
        {
            if (item.InputType != UIShortcutInputType.Axis)
            {
                Debug.LogError("[UI-INPUT] Unable to call this method with input type " + item.InputType + " for action " + item.AssignedAction +
                               ". Requires " + nameof(UIShortcutInputType.Axis));
                return Vector2.zero;
            }

            return item.InputDevice switch
            {
                UIShortcutInput.Gamepad => !supportsGamepad ? Vector2.zero : InputUtils.GetValueFromDevice(Gamepad.current, gamepad => gamepad.GetAxis(item.InputGamepadAxis)),
                UIShortcutInput.Keyboard => !supportsKeyboard ? Vector2.zero : InputUtils.GetValueFromDevice(Keyboard.current, keyboard => keyboard.GetAxis(item.InputKeyAxis)),
                _ => throw new NotImplementedException(item.InputType.ToString())
            };
        }
    }
}