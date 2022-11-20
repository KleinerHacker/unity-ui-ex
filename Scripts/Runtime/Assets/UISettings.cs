using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Assets;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UISettings : ProviderAsset<UISettings>
    {
        #region Static Area

            public static UISettings Singleton => GetSingleton("UI", "ui.asset");

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => GetSerializedSingleton("UI", "ui.asset");
#endif

        #endregion

        #region Inspector Data

        [SerializeField]
        private UIHover hover = new UIHover();

        [SerializeField]
        private UINotification notification = new UINotification();

        #endregion

        #region Properties

        public UIHover Hover => hover;

        public UINotification Notification => notification;

        #endregion
    }
}