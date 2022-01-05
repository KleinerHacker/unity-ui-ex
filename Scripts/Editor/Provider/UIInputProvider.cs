using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIInputProvider : SettingsProvider
    {
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateGameSettingsProvider()
        {
            return new UIInputProvider();
        }

        #endregion
        
        private SerializedObject _settings;
        private SerializedProperty _presetsProperty;
        
        public UIInputProvider() : base("Project/Input System Package/UI Input", SettingsScope.Project, new[] { "UI", "Input", "Input System" })
        {
        }

        #region Builtin Methods

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = UIInputSettings.SerializedSingleton;
            if (_settings == null)
                return;

            _presetsProperty = _settings.FindProperty("inputPresets");
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            EditorGUILayout.PropertyField(_presetsProperty, new GUIContent("Presets"));
            if (GUILayout.Button("Re-Generate GUIDs"))
            {
                if (EditorUtility.DisplayDialog("Re-Generate GUIDs", "You are sure? All created links in scenes will be lost!", "yes", "no"))
                {
                    UIInputSettings.Singleton.UpdateGuids();
                }
            }

            _settings.ApplyModifiedProperties();
        }

        #endregion
    }
}