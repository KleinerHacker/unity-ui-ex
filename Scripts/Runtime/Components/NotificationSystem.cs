using System;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Misc;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components
{
    public static class NotificationSystem
    {
        public static NotificationSystemAccess Get(string identifier)
        {
            var notificationSystem = UINotificationSystem.Get(identifier);
            if (notificationSystem == null)
                throw new InvalidOperationException(identifier + " is unknown!");

            return new NotificationSystemAccess(notificationSystem);
        }

        public sealed class NotificationSystemAccess
        {
            private readonly UINotificationSystem _notificationSystem;

            internal NotificationSystemAccess(UINotificationSystem notificationSystem)
            {
                _notificationSystem = notificationSystem;
            }

            public void ShowIconNotification(long identifier, string text, Sprite icon, UINotificationType type = UINotificationType.None) => 
                _notificationSystem.ShowIconNotification(identifier, text, icon, type);
        
            public void ShowIconNotification(long identifier, string title, string text, Sprite icon, UINotificationType type = UINotificationType.None) => 
                _notificationSystem.ShowIconNotification(identifier, title, text, icon, type);

            public void ShowSimpleNotification(long identifier, string text, UINotificationType type = UINotificationType.None) => 
                _notificationSystem.ShowSimpleNotification(identifier, text, type);

            public void ShowSimpleNotification(long identifier, string title, string text, UINotificationType type = UINotificationType.None) => 
                _notificationSystem.ShowSimpleNotification(identifier, title, text, type);
        }
    }
}