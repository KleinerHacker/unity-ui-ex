using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed partial class UIProvider : SettingsProvider
    {
        private static readonly GUIStyle RightLabelStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleRight
        };
        
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new UIProvider();
        }

        #endregion
        
        private SerializedObject _settings;
        
        
        public UIProvider() : base("Project/UI", SettingsScope.Project, new []{"UI", "Hover", "Tooling"})
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = UISettings.SerializedSingleton;
            if (_settings == null)
                return;

            LoadHoverProperties();
            LoadNotificationProperties();
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            LayoutHover();
            LayoutNotification();

            _settings.ApplyModifiedProperties();
        }
    }
}