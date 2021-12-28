using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    public abstract class UIListInput : UIInput
    {
        #region Inspector Data

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
        private UIListInputBehavior behavior = UIListInputBehavior.Circle;

        #endregion

        #region Properties

        protected abstract int CurrentIndex { get; }
        protected abstract int ListLength { get; }
        protected abstract bool Interactable { get; }

        #endregion
        
        private int _index;

        #region Builtin Methods

        protected override void Start()
        {
            base.Start();
            _index = CurrentIndex;
        }

        protected override void LateUpdate()
        {
            if ((GamepadAvailable && Gamepad.current[negativeGamepadButton].wasPressedThisFrame) ||
                (KeyboardAvailable && Keyboard.current[negativeKey].wasPressedThisFrame))
            {
                if (Interactable)
                {
                    GotoPrev();
                }
            }
            else if ((GamepadAvailable && Gamepad.current[positiveGamepadButton].wasPressedThisFrame) ||
                     (KeyboardAvailable && Keyboard.current[positiveKey].wasPressedThisFrame))
            {
                if (Interactable)
                {
                    GotoNext();
                }
            }
        }

        #endregion
        
        private void GotoNext()
        {
            _index++;
            if (_index >= ListLength)
            {
                _index = behavior switch
                {
                    UIListInputBehavior.Stays => ListLength - 1,
                    UIListInputBehavior.Circle => 0,
                    _ => throw new NotImplementedException(behavior.ToString())
                };
                
                if (behavior == UIListInputBehavior.Stays)
                    return;
            }

            if (!ActivateItem(_index))
            {
                GotoNext();
            }
        }

        private void GotoPrev()
        {
            _index--;
            if (_index < 0)
            {
                _index = behavior switch
                {
                    UIListInputBehavior.Stays => 0,
                    UIListInputBehavior.Circle => ListLength - 1,
                    _ => throw new NotImplementedException(behavior.ToString())
                };
                
                if (behavior == UIListInputBehavior.Stays)
                    return;
            }

            if (!ActivateItem(_index))
            {
                GotoPrev();
            }
        }

        protected abstract bool ActivateItem(int index);
    }
    
    public enum UIListInputBehavior
    {
        Stays,
        Circle,
    }
}