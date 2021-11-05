using UnityAnimation.Runtime.animation.Scripts.Types;
using UnityAnimation.Runtime.animation.Scripts.Utils;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.Ui.WindowMenu + "/Notification")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UiNotification : UiPopup
    {
        #region Inspector Data

        [Header("Behavior")]
        [SerializeField]
        [Range(0.5f, 10f)]
        private float showTime = 1f;

        #endregion

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();

            if (initialState == ViewableState.Shown)
            {
                AutoHide();
            }
        }

        #endregion

        protected override void OnShown()
        {
            AutoHide();
        }

        private void AutoHide()
        {
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Wait(showTime, Hide)
                .Start();
        }
    }
}