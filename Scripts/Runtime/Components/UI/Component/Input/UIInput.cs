using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils.Extensions;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    public abstract class UIInput : UIBehaviour
    {
        #region Inspector Data

        [SerializeField]
        private UIInputSupport gamepadSupport = UIInputSupport.Yes;
        
        [SerializeField]
        private UIInputSupport keyboardSupport = UIInputSupport.Yes;

        [Space]
        [SerializeField]
        private UIInputPresenter inputPresenter;

        #endregion
        
        protected bool GamepadAvailable { get; private set; }
        protected bool KeyboardAvailable { get; private set; }

        #region Builtin Methods

        protected override void Awake()
        {
            if (inputPresenter == null && (gamepadSupport == UIInputSupport.FromPresenter || keyboardSupport == UIInputSupport.FromPresenter))
                throw new InvalidOperationException("Input Presenter is required");
        }

        protected override void Start()
        {
            GamepadAvailable = Gamepad.current.IsAvailable() && (gamepadSupport == UIInputSupport.Yes || (gamepadSupport == UIInputSupport.FromPresenter && inputPresenter.CurrentInputPreset.Requires(UIInputDevice.Gamepad)));
            KeyboardAvailable = Keyboard.current.IsAvailable() && (keyboardSupport == UIInputSupport.Yes || (keyboardSupport == UIInputSupport.FromPresenter && inputPresenter.CurrentInputPreset.Requires(UIInputDevice.Keyboard)));
        }

        protected abstract void LateUpdate();

        #endregion
    }

    public enum UIInputSupport
    {
        Yes,
        No,
        FromPresenter
    }
}