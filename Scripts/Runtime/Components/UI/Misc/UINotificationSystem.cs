using System;
using System.Collections.Generic;
using System.Linq;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.MiscMenu + "/Notification System")]
    [DisallowMultipleComponent]
    public sealed class UINotificationSystem : UIBehaviour
    {
        #region Static Area

        internal static UINotificationSystem Get(string identifier) => FindObjectsOfType<UINotificationSystem>()
            .FirstOrThrow(x => x.Identifier == identifier, () => new ArgumentException(identifier + " not found for notification system"));

        #endregion
        
        #region Inspector Data

        [SerializeField]
        private string identifier;

        [SerializeField]
        private float notificationHeight = 150f;

        [Header("Animation")]
        [SerializeField]
        private AnimationCurve shiftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [SerializeField]
        private float shiftSpeed = 1f;

        [Header("Prefabs")]
        [SerializeField]
        private UINotificationWindow notificationPrefab;

        #endregion

        #region Properties

        internal string Identifier => identifier;

        #endregion

        private readonly IList<UINotificationWindow> _instances = new List<UINotificationWindow>();
        
        internal void ShowIconNotification(long identifier, string text, Sprite icon, UINotificationType type = UINotificationType.None) => 
            ShowNotification(identifier, (UIIconNotificationWindow window) => window.Show(identifier, text, icon, type));
        
        internal void ShowIconNotification(long identifier, string title, string text, Sprite icon, UINotificationType type = UINotificationType.None) => 
            ShowNotification(identifier, (UIIconNotificationWindow window) => window.Show(identifier, title, text, icon, type));

        internal void ShowSimpleNotification(long identifier, string text, UINotificationType type = UINotificationType.None) => 
            ShowNotification(identifier, (UISimpleNotificationWindow window) => window.Show(identifier, text, type));
        
        internal void ShowSimpleNotification(long identifier, string title, string text, UINotificationType type = UINotificationType.None) => 
            ShowNotification(identifier, (UISimpleNotificationWindow window) => window.Show(identifier, title, text, type));

        private void ShowNotification<T>(long identifier, Action<T> show) where T : UINotificationWindow
        {
            if (_instances.Any(x => x.Identifier == identifier))
            {
                Debug.Log("[NOT-SYS] Notification already shown", this);
                return;
            }
            
            Debug.Log("[NOT-SYS] Show notification " + identifier, this);

            var y = _instances.Count * notificationHeight;

            var go = Instantiate(notificationPrefab.gameObject, transform.position + Vector3.down * y, Quaternion.identity, transform);

            var instance = go.GetComponent<UINotificationWindow>();
            instance.OnHiding += OnHiding;
            instance.OnHidden += OnHidden;
            show((T) instance);

            _instances.Add(instance);
        }

        private void OnHiding(object sender, EventArgs e)
        {
            var instance = (UINotificationWindow)sender;
            instance.OnHiding -= OnHiding;
            instance.OnHidden -= OnHidden;

            var indexOf = _instances.IndexOf(instance);
            if (indexOf < 0)
                return;

            _instances.Remove(instance);

            Debug.Log("[NOT-SYS] Shifting notifications", this);
            for (var i = indexOf; i < _instances.Count; i++)
            {
                var item = _instances[i];
                var yStart = item.transform.position.y;
                var yEnd = yStart + notificationHeight;
                AnimationBuilder.Create(this)
                    .Animate(shiftCurve, shiftSpeed, v =>
                    {
                        var itemTransform = item.transform;
                        var itemPosition = itemTransform.position;
                        itemTransform.position = new Vector3(itemPosition.x, Mathf.Lerp(yStart, yEnd, v), itemPosition.z);
                    })
                    .Start();
            }
        }

        private void OnHidden(object sender, EventArgs e)
        {
            Debug.Log("[NOT-SYS] Destroy notification", this);

            var instance = (UINotificationWindow)sender;
            Destroy(instance.gameObject);
        }
    }
}