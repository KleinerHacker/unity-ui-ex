using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils.Extensions;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Types;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Scroll View Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScrollRect))]
    public sealed class UIScrollViewInput : UIInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private GamepadAxis gamepad = GamepadAxis.RightStick;

        [SerializeField]
        private KeyAxis key = KeyAxis.Arrows;
        
        [Space]
        [SerializeField]
        private GameObject iconObject;
        
        [SerializeField]
        private Image icon;

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
                velocity += key switch
                {
                    KeyAxis.Arrows => Keyboard.current.GetArrows(),
                    KeyAxis.Numpad => Keyboard.current.GetNumpad(),
                    KeyAxis.WASD => Keyboard.current.GetWASD(),
                    _ => throw new NotImplementedException()
                } * velocityMultiplier;
            }

            _scrollRect.velocity = velocity;
        }

        #endregion

        protected override void UpdateVisual()
        {
            UpdateIcon(key, gamepad, icon, iconObject);
        }
    }
}