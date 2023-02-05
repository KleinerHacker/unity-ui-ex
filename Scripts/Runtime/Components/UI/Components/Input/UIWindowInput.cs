using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Extras;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Window Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIWindow))]
    public sealed class UIWindowInput : UIInput
    {
        #region Inspector Data

        [Header("Actions")]
        [UIShortcutSchemeAction(UIShortcutInputType.Button)]
        [SerializeField]
        private string toggleAction;

        #endregion

        #region Properties

        protected override string[] AssignedShortcutActions => new[] { toggleAction };

        #endregion
        
        private UIWindow _window;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _window = GetComponent<UIWindow>();
        }

        protected override void LateUpdate()
        {
            if (WasShortcutPressedThisFrame(toggleAction))
            {
                _window.ToggleVisibility();
            }
        }

        #endregion

        protected override void UpdateVisual()
        {
        }
    }
}