using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils.Extensions;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Types;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    public abstract class UIInput : UIBehaviour
    {
        #region Inspector Data

        [SerializeField]
        private UIInputSupport gamepadSupport = UIInputSupport.Yes;

        [SerializeField]
        private bool showGamepadIcon = true;

        [SerializeField]
        private UIInputSupport keyboardSupport = UIInputSupport.Yes;
        
        [SerializeField]
        private bool showKeyboardIcon = true;

        #endregion

        protected bool GamepadAvailable { get; private set; }
        protected bool KeyboardAvailable { get; private set; }

        #region Builtin Methods

        protected override void Awake()
        {
        }

        protected override void Start()
        {
            GamepadAvailable = Gamepad.current.IsAvailable() && gamepadSupport == UIInputSupport.Yes && UIShortcutInputSettings.Singleton.CurrentInput == UIShortcutInput.Gamepad;
            KeyboardAvailable = Keyboard.current.IsAvailable() && keyboardSupport == UIInputSupport.Yes && UIShortcutInputSettings.Singleton.CurrentInput == UIShortcutInput.Keyboard;
            
            UpdateVisual();
        }

        protected abstract void LateUpdate();

        #endregion

        protected abstract void UpdateVisual();

        protected void UpdateIcon(Key keyButton, GamepadButton gamepadButton, Image icon, GameObject iconObject)
        {
            if (iconObject == null || icon == null)
                return;
            
            iconObject.SetActive(GamepadAvailable && showGamepadIcon || KeyboardAvailable && showKeyboardIcon);
            if (GamepadAvailable && showGamepadIcon)
            {
                icon.sprite = UIShortcutInputSettings.Singleton.GamepadButtonShortcutImageItems.First(x => x.Identifier == gamepadButton).Icon;
            }
            else if (KeyboardAvailable && showKeyboardIcon)
            {
                icon.sprite = UIShortcutInputSettings.Singleton.KeyboardButtonShortcutImageItems.First(x => x.Identifier == keyButton).Icon;
            }
        }
        
        protected void UpdateIcon(KeyAxis keyAxis, GamepadAxis gamepadAxis, Image icon, GameObject iconObject)
        {
            if (iconObject == null || icon == null)
                return;
            
            iconObject.SetActive(GamepadAvailable && showGamepadIcon || KeyboardAvailable && showKeyboardIcon);
            if (GamepadAvailable && showGamepadIcon)
            {
                icon.sprite = UIShortcutInputSettings.Singleton.GamepadAxisShortcutImageItems.First(x => x.Identifier == gamepadAxis).Icon;
            }
            else if (KeyboardAvailable && showKeyboardIcon)
            {
                icon.sprite = UIShortcutInputSettings.Singleton.KeyboardAxisShortcutImageItems.First(x => x.Identifier == keyAxis).Icon;
            }
        }
    }

    public enum UIInputSupport
    {
        Yes,
        No
    }
}