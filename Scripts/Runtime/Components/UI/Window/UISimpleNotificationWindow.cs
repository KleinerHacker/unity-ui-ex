using System;
using TMPro;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Simple Notification Window")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UINotification), typeof(CanvasGroup))]
    public sealed class UISimpleNotificationWindow : UINotificationWindow
    {
        #region Inspector Data

        [Header("References")]
        [SerializeField]
        private TextMeshProUGUI titleText;

        [SerializeField]
        private TextMeshProUGUI messageText;

        #endregion

        private Color _titleColor;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _titleColor = titleText.color;
        }

        #endregion

        public void Show(long identifier, string message, UINotificationType type = UINotificationType.None) => Show(identifier, "", message, type);

        public void Show(long identifier, string title, string message, UINotificationType type = UINotificationType.None)
        {
            if (titleText != null)
            {
                titleText.text = title;
                titleText.color = type switch
                {
                    UINotificationType.None => _titleColor,
                    UINotificationType.Info => UISettings.Singleton.Notification.InfoColor,
                    UINotificationType.Warning => UISettings.Singleton.Notification.WarningColor,
                    UINotificationType.Error => UISettings.Singleton.Notification.ErrorColor,
                    _ => throw new NotImplementedException(type.ToString())
                };
            }

            messageText.text = message;
            Show(identifier);
        }
    }
}