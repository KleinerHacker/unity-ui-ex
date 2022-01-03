using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.InputMenu + "/Scroll View Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScrollRect))]
    public sealed class UIScrollViewInput : UIInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private GamepadAxis gamepad = GamepadAxis.RightStick;

        [SerializeField]
        private KeyboardAxis keyboard = KeyboardAxis.Arrows;

        [Space]
        [SerializeField]
        private float velocityMultiplier = 100f;

        #endregion

        private ScrollRect _scrollRect;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _scrollRect = GetComponent<ScrollRect>();
        }

        protected override void LateUpdate()
        {
            var velocity = Vector2.zero;
            if (GamepadAvailable)
            {
                velocity += gamepad switch
                {
                    GamepadAxis.LeftStick => Gamepad.current.leftStick.ReadValue(),
                    GamepadAxis.RightStick => Gamepad.current.rightStick.ReadValue(),
                    GamepadAxis.DPad => Gamepad.current.dpad.ReadValue(),
                    _ => throw new NotImplementedException()
                } * velocityMultiplier;
            }
            
            if (KeyboardAvailable)
            {
                velocity += keyboard switch
                {
                    KeyboardAxis.Arrows => Keyboard.current.GetArrows(),
                    KeyboardAxis.Numpad => Keyboard.current.GetNumpad(),
                    KeyboardAxis.WASD => Keyboard.current.GetWASD(),
                    _ => throw new NotImplementedException()
                } * velocityMultiplier;
            }

            _scrollRect.velocity = velocity;
        }

        #endregion
    }

    public enum GamepadAxis
    {
        LeftStick,
        RightStick,
        DPad,
    }

    public enum KeyboardAxis
    {
        Arrows,
        Numpad,
        WASD,
    }
}