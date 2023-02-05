using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Toggle Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public sealed class UIToggleInput : UIInput
    {
        #region Inspector Data

        [SerializeField]
        private string clickAction;
        
        [SerializeField]
        private GameObject iconObject;

        [SerializeField]
        private Image icon;

        [Space]
        [SerializeField]
        private bool allowDisableToggle = true;

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