using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Icon Notification Window")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UINotification), typeof(CanvasGroup))]
    public sealed class UIIconNotificationWindow : UINotificationWindow
    {
        #region Inspector Data

        [Header("References")]
        [SerializeField]
        private TextMeshProUGUI titleText;

        [SerializeField]
        private TextMeshProUGUI messageText;

        [SerializeField]
        private Image iconImage;

        #endregion

        private Color _titleColor;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _titleColor = titleText.color;
        }

        #endregion

        public void Show(long identifier, string message, Sprite icon, UINotificationType type) => Show(identifier, "", message, icon, type);

        public void Show(long identifier, string title, string message, Sprite icon, UINotificationType type)
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
            iconImage.sprite = icon;

            Show(identifier);
        }
    }
}