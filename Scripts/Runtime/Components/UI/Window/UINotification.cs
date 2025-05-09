using UnityAnimation.Runtime.Projects.unity_animation.Scripts.Runtime.Types;
using UnityAnimation.Runtime.Projects.unity_animation.Scripts.Runtime.Utils;
using UnityEngine;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Notification")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UINotification : UIPopup
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