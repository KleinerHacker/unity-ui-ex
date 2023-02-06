#if PCSOFT_SHORTCUT && PCSOFT_ENV
using UnityEngine;
using UnityEngine.UI;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Extras;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Scroll View Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScrollRect))]
    public sealed class UIScrollViewInput : UIInput
    {
        #region Inspector Data

        [Header("Actions")]
        [UIShortcutSchemeAction(UIShortcutInputType.Axis)]
        [SerializeField]
        private string scrollAction;
        
        [Header("Behaviors")]
        [SerializeField]
        private float velocityMultiplier = 100f;
        
        [Header("References")]
        [SerializeField]
        private GameObject iconObject;

        [SerializeField]
        private Image icon;

        #endregion

        #region Properties

        protected override string[] AssignedShortcutActions => new[] { scrollAction };

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
            var velocity = GetShortcutAxis(scrollAction) * velocityMultiplier;
            _scrollRect.velocity = velocity;
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateIconOnValidate(icon, iconObject, scrollAction);
        }
#endif

        #endregion

        protected override void UpdateVisual() => UpdateIcon(icon, iconObject, scrollAction);
    }
}
#endif