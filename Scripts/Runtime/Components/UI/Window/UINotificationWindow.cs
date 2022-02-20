using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Notification Window")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UINotification), typeof(CanvasGroup))]
    public abstract class UINotificationWindow : UIBehaviour
    {
        #region Properties

        public long Identifier { get; private set; }

        #endregion

        #region Events

        public event EventHandler OnHiding;
        public event EventHandler OnHidden;

        #endregion

        private UINotification _notification;

        #region Builtin Methods

        protected override void Awake()
        {
            _notification = GetComponent<UINotification>();
        }

        protected override void OnEnable()
        {
            _notification.Hiding += NotificationOnHiding;
            _notification.Hidden += NotificationOnHidden;
        }

        protected override void OnDisable()
        {
            _notification.Hiding -= NotificationOnHiding;
            _notification.Hidden -= NotificationOnHidden;
        }

        #endregion

        protected void Show(long identifier)
        {
            Identifier = identifier;
            _notification.Show();
        }

        private void NotificationOnHiding(object sender, EventArgs e)
        {
            OnHiding?.Invoke(this, EventArgs.Empty);
        }

        private void NotificationOnHidden(object sender, EventArgs e)
        {
            OnHidden?.Invoke(this, EventArgs.Empty);
        }
    }

    
}