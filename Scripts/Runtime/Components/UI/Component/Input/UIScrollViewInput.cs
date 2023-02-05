using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityInputEx.Runtime.input_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Scroll View Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScrollRect))]
    public sealed class UIScrollViewInput : UIInput
    {
        #region Inspector Data

        [SerializeField]
        private string scrollAction;
        
        [SerializeField]
        private GameObject iconObject;

        [SerializeField]
        private Image icon;

        [Space]
        [SerializeField]
        private float velocityMultiplier = 100f;

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