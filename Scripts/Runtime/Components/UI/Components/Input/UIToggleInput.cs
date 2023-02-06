#if PCSOFT_SHORTCUT && PCSOFT_ENV
using UnityEngine;
using UnityEngine.UI;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Extras;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Toggle Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public sealed class UIToggleInput : UIInput
    {
        #region Inspector Data

        [Header("Actions")]
        [UIShortcutSchemeAction(UIShortcutInputType.Button)]
        [SerializeField]
        private string clickAction;
        
        [Header("Behaviors")]
        [SerializeField]
        private bool allowDisableToggle = true;
        
        [Header("References")]
        [SerializeField]
        private GameObject iconObject;

        [SerializeField]
        private Image icon;

        #endregion

        #region Properties

        protected override string[] AssignedShortcutActions => new[] { clickAction };

        #endregion

        private Toggle _toggle;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _toggle = GetComponent<Toggle>();
        }

        protected override void LateUpdate()
        {
            if (WasShortcutPressedThisFrame(clickAction))
            {
                if (_toggle.interactable)
                {
                    _toggle.isOn = !allowDisableToggle || !_toggle.isOn;
                }
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateIconOnValidate(icon, iconObject, clickAction);
        }
#endif

        #endregion
        
        protected override void UpdateVisual() => UpdateIcon(icon, iconObject, clickAction);
    }
}
#endif