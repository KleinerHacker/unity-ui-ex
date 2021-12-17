using System;
using System.Linq;
#if PLATFORM_ANDROID
using UnityAndroidEx.Runtime.android_ex.Scripts.Runtime.Utils;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils.Extensions;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions
{
    internal static class UIInputExtensions
    {
        public static bool Requires(this UIInputPreset preset, UIInputDevice inputDevice) => preset.RequiredInputDevices.Contains(inputDevice);
        
        public static bool IsUsable(this UIInputPreset preset) => 
            preset.RequiredInputDevices.All(x => x.IsAvailable()) && preset.Constraints.CheckConstraints();

        public static bool IsAvailable(this UIInputDevice type)
        {
            switch (type)
            {
                case UIInputDevice.Touch:
                    return Touchscreen.current.IsAvailable();
                case UIInputDevice.Keyboard:
                    return Keyboard.current.IsAvailable();
                case UIInputDevice.Mouse:
                    return Mouse.current.IsAvailable();
                case UIInputDevice.Gamepad:
                    return Gamepad.current.IsAvailable();
                default:
                    throw new NotImplementedException(type.ToString());
            }
        }
        
        public static bool CheckConstraints(this UIInputPresetConstraint[] constraints)
        {
            if (constraints == null || constraints.Length <= 0)
                return true;

            var constraint = constraints.FirstOrDefault(x => x.SupportedPlatform == Application.platform);
            if (constraint == null)
                return false;

            if (constraint.RequiresTV)
            {
#if PLATFORM_ANDROID
                if (!AndroidUtils.IsOnTV)
                    return false;
#endif
            }

            return true;
        }
    }
}