using System;
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
            // GamepadAvailable = Gamepad.current.IsAvailable() && gamepadSupport == UIInputSupport.Yes && UIShortcutInputSettings.Singleton.CurrentInput == UIShortcutInput.Gamepad;
            // KeyboardAvailable = Keyboard.current.IsAvailable() && keyboardSupport == UIInputSupport.Yes && UIShortcutInputSettings.Singleton.CurrentInput == UIShortcutInput.Keyboard;
            
            UpdateVisual();
        }

        protected abstract void LateUpdate();

        #endregion

        protected abstract void UpdateVisual();

        protected void UpdateIcon(Key keyButton, GamepadButton gamepadButton, Image icon, GameObject iconObject) => 
            UpdateIcon(keyButton, gamepadButton, GamepadAvailable && showGamepadIcon, KeyboardAvailable && showKeyboardIcon, icon, iconObject);

        protected void UpdateIcon(KeyAxis keyAxis, GamepadAxis gamepadAxis, Image icon, GameObject iconObject) => 
            UpdateIcon(keyAxis, gamepadAxis, GamepadAvailable && showGamepadIcon, KeyboardAvailable && showKeyboardIcon, icon, iconObject);

#if UNITY_EDITOR
        protected void UpdateIconOnValidate(Key keyButton, GamepadButton gamepadButton, Image icon, GameObject iconObject) {}
            // UpdateIcon(keyButton, gamepadButton, UIShortcutInputSettings.DisplayedShortcut == UIShortcutInput.Gamepad, 
            //     UIShortcutInputSettings.DisplayedShortcut == UIShortcutInput.Keyboard, icon, iconObject);
        
        protected void UpdateIconOnValidate(KeyAxis keyAxis, GamepadAxis gamepadAxis, Image icon, GameObject iconObject)
        {
        }
            // UpdateIcon(keyAxis, gamepadAxis, UIShortcutInputSettings.DisplayedShortcut == UIShortcutInput.Gamepad, 
            //     UIShortcutInputSettings.DisplayedShortcut == UIShortcutInput.Keyboard, icon, iconObject);
#endif

        private void UpdateIcon(Key keyButton, GamepadButton gamepadButton, bool showGamepadIcon, bool showKeyboardIcon, Image icon, GameObject iconObject)
        {
        }
        // UpdateIcon(icon, iconObject, showGamepadIcon, showKeyboardIcon, 
            //     () => UIShortcutInputSettings.Singleton.GamepadButtonShortcutImageItems.First(x => x.Identifier == gamepadButton).Icon,
            //     () => UIShortcutInputSettings.Singleton.KeyboardButtonShortcutImageItems.First(x => x.Identifier == keyButton).Icon);

        private void UpdateIcon(KeyAxis keyAxis, GamepadAxis gamepadAxis, bool showGamepadIcon, bool showKeyboardIcon, Image icon, GameObject iconObject)
        {
        }
        // UpdateIcon(icon, iconObject, showGamepadIcon, showKeyboardIcon, 
            //     () => UIShortcutInputSettings.Singleton.GamepadAxisShortcutImageItems.First(x => x.Identifier == gamepadAxis).Icon,
            //     () => UIShortcutInputSettings.Singleton.KeyboardAxisShortcutImageItems.First(x => x.Identifier == keyAxis).Icon);

        private static void UpdateIcon(Image icon, GameObject iconObject, bool showGamepadIcon, bool showKeyboardIcon, Func<Sprite> gamepadIconUpdater, Func<Sprite> keyIconUpdater)
        {
            if (iconObject == null || icon == null)
                return;
            
            iconObject.SetActive(showGamepadIcon || showKeyboardIcon);
            if (showGamepadIcon)
            {
                icon.sprite = gamepadIconUpdater();
            }
            else if (showKeyboardIcon)
            {
                icon.sprite = keyIconUpdater();
            }
        }
    }

    public enum UIInputSupport
    {
        Yes,
        No
    }
}