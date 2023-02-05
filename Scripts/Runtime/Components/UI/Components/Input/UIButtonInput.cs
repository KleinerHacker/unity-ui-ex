using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Extras;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Button Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButtonInput : UIInput
    {
        #region Inspector Data

        [Header("Actions")]
        [UIShortcutSchemeAction(UIShortcutInputType.Button)]
        [SerializeField]
        private string clickAction;

        [Header("References")]
        [SerializeField]
        private GameObject iconObject;

        [SerializeField]
        private Image icon;

        #endregion

        #region Properties

        protected override string[] AssignedShortcutActions => new[] { clickAction };

        #endregion

        private Button _button;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _button = GetComponent<Button>();
        }

        protected override void LateUpdate()
        {
            if (WasShortcutPressedThisFrame(clickAction))
            {
                if (_button.interactable)
                {
                    _button.OnPointerClick(new PointerEventData(EventSystem.current));
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