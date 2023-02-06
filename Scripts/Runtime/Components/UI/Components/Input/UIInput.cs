#if PCSOFT_SHORTCUT && PCSOFT_ENV
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input
{
    public abstract class UIInput : UIBehaviour
    {
        #region Inspector Data

        [Header("Supported Inputs")]
        [SerializeField]
        protected UIInputSupport gamepadSupport = UIInputSupport.Yes;

        [SerializeField]
        private bool showGamepadIcon = true;

        [SerializeField]
        protected UIInputSupport keyboardSupport = UIInputSupport.Yes;

        [SerializeField]
        private bool showKeyboardIcon = true;

        #endregion

        #region Properties
        
        /// <summary>
        /// Returns all supported actions that must be assigned to a shortcut action
        /// </summary>
        protected abstract string[] AssignedShortcutActions { get; }

        #endregion

        private readonly IDictionary<string, UIShortcutSchemeItem> _shortcutSchemeItems = new Dictionary<string, UIShortcutSchemeItem>();
        private bool _noScheme;

        #region Builtin Methods

        protected override void Awake()
        {
            Refresh();
        }

        protected override void Start()
        {
            UpdateVisual();
        }

        /// <summary>
        /// Implement to check for device input
        /// </summary>
        protected abstract void LateUpdate();

        #endregion

        public void Refresh()
        {
            var currentInputScheme = UIShortcutInputSystem.CurrentInputScheme;
            if (currentInputScheme == null)
            {
                Debug.Log("[UI-INPUT] No shortcut input scheme found", this);
                _noScheme = true;
                UpdateVisual();
                
                return;
            }
            
            _shortcutSchemeItems.Clear();
            foreach (var assignedShortcutAction in AssignedShortcutActions)
            {
                var shortcutSchemeItem = currentInputScheme.FindByAction(assignedShortcutAction);
                if (shortcutSchemeItem == null)
                {
                    Debug.LogError("[UI-INPUT] Unable to find shortcut action " + assignedShortcutAction + " in scheme " + currentInputScheme.Name, this);
                    continue;
                }
                
                _shortcutSchemeItems.Add(assignedShortcutAction, shortcutSchemeItem);
            }

            _noScheme = false;
            UpdateVisual();
        }

        /// <summary>
        /// Implement to update visuals
        /// </summary>
        protected abstract void UpdateVisual();

        /// <summary>
        /// Update icon at runtime for a specific action 
        /// </summary>
        /// <param name="icon">Image within icon</param>
        /// <param name="iconObject">Game object of icon</param>
        /// <param name="action">Associated shortcut action</param>
        protected void UpdateIcon(Image icon, GameObject iconObject, string action) =>
            UpdateIcon(showGamepadIcon && gamepadSupport == UIInputSupport.Yes || showKeyboardIcon && keyboardSupport == UIInputSupport.Yes, icon, iconObject, action);

#if UNITY_EDITOR
        /// <summary>
        /// Update icon at editor time for a specific action 
        /// </summary>
        /// <param name="icon">Image within icon</param>
        /// <param name="iconObject">Game object of icon</param>
        /// <param name="action">Associated shortcut action</param>
        protected void UpdateIconOnValidate(Image icon, GameObject iconObject, string action) =>
            UpdateIcon(showGamepadIcon && gamepadSupport == UIInputSupport.Yes || showKeyboardIcon && keyboardSupport == UIInputSupport.Yes, icon, iconObject, action);
#endif

        private void UpdateIcon(bool showIcon, Image icon, GameObject iconObject, string action) =>
            UpdateIcon(icon, iconObject, showIcon, () => !_shortcutSchemeItems.ContainsKey(action) ? null : _shortcutSchemeItems[action].Icon);

        private void UpdateIcon(Image icon, GameObject iconObject, bool showIcon, Func<Sprite> iconUpdater)
        {
            if (iconObject == null || icon == null)
                return;
            if (_noScheme)
            {
                iconObject.SetActive(false);
                return;
            }

            iconObject.SetActive(showIcon);
            icon.sprite = iconUpdater();
        }

        /// <summary>
        /// Find the fit shortcut scheme item for given action. <b>Can be <c>null</c></b>
        /// </summary>
        /// <param name="action">Action to search for</param>
        /// <returns></returns>
        protected UIShortcutSchemeItem FindShortcutSchemeItemByAction(string action) => !_shortcutSchemeItems.ContainsKey(action) ? null : _shortcutSchemeItems[action];

        /// <summary>
        /// Check that the given shortcut action was pressed this frame.<br/>
        /// <br/>
        /// This method checks on <c>null</c> and returns <c>false</c> if action was not found. It results in an error log message.
        /// </summary>
        /// <param name="action">Action to check for</param>
        /// <returns></returns>
        protected bool WasShortcutPressedThisFrame(string action)
        {
            if (_noScheme)
                return false;
            
            var shortcutSchemeItem = FindShortcutSchemeItemByAction(action);
            if (shortcutSchemeItem == null)
            {
                Debug.LogError("[UI-INPUT] Unable to handle shortcut: action " + action + " not available", this);
                return false;
            }
            
            return shortcutSchemeItem.WasKeyPressed(keyboardSupport == UIInputSupport.Yes, gamepadSupport == UIInputSupport.Yes);
        }

        /// <summary>
        /// Returns the axis value from given action. <br/>
        /// <br/>
        /// This method checks on <c>null</c> and returns <c>0</c> if action was not found. It results in an error log message.
        /// </summary>
        /// <param name="action">Action to get axis value for</param>
        /// <returns></returns>
        protected Vector2 GetShortcutAxis(string action)
        {
            if (_noScheme)
                return Vector2.zero;
            
            var shortcutSchemeItem = FindShortcutSchemeItemByAction(action);
            if (shortcutSchemeItem == null)
            {
                Debug.LogError("[UI-INPUT] Unable to handle shortcut: action " + action + " not available", this);
                return Vector2.zero;
            }

            return shortcutSchemeItem.GetAxis(keyboardSupport == UIInputSupport.Yes, gamepadSupport == UIInputSupport.Yes);
        }
    }

    public enum UIInputSupport
    {
        Yes,
        No
    }
}
#endif