using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils.Extensions;
using UnityEngine;
using UnityEngine.UIElements;
using UnityExtension.Runtime.extension.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIShortcutInputProvider : SettingsProvider
    {
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new UIShortcutInputProvider();
        }

        #endregion

        private SerializedObject _settings;
        private SerializedProperty _useShortcutProperty;
        private SerializedProperty _shortcutInputProperty;
        private SerializedProperty[] _constraintItemsProperties;

        public UIShortcutInputProvider() : base("Project/UI/Shortcut Input", SettingsScope.Project, new[] { "UI", "Input", "Input System", "Short", "Key" })
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = UIShortcutInputSettings.SerializedSingleton;
            if (_settings == null)
                return;

            _useShortcutProperty = _settings.FindProperty("useShortcut");
            _shortcutInputProperty = _settings.FindProperty("shortcutInput");
            _constraintItemsProperties = _settings.FindProperties("constraintItems");
        }

        public override void OnInspectorUpdate()
        {
            base.OnInspectorUpdate();
            _constraintItemsProperties = _settings.FindProperties("constraintItems");
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            EditorGUILayout.LabelField("Environment Dependent Shortcuts", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Fallback (Default)", EditorStyles.boldLabel, GUILayout.Width(350f));
            EditorGUILayout.LabelField("Shortcut Input Usage: ", GUILayout.Width(125f));
            var isActive = _useShortcutProperty.boolValue;
            isActive = EditorGUILayout.Toggle(isActive, GUILayout.Width(20f));
            _useShortcutProperty.boolValue = isActive;
            EditorGUI.BeginDisabledGroup(!isActive);
            EditorGUILayout.PropertyField(_shortcutInputProperty, GUIContent.none, GUILayout.ExpandWidth(true));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            for (var i = 0; i < _constraintItemsProperties.Length; i++)
            {
                var constraintItemsProperty = _constraintItemsProperties[i];
                var useShortcutProperty = constraintItemsProperty.FindPropertyRelative("useShortcut");
                var inputProperty = constraintItemsProperty.FindPropertyRelative("shortcutInput");

                var name = EnvironmentDetectionSettings.Singleton.Items
                    .FirstOrDefault(x => string.Equals(x.Guid, constraintItemsProperty.FindPropertyRelative("environmentGuid").stringValue, StringComparison.Ordinal))?.Name ?? "<unknown>";

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(name, EditorStyles.boldLabel, GUILayout.Width(350f));
                EditorGUILayout.LabelField("Shortcut Input Usage: ", GUILayout.Width(125f));
                var active = useShortcutProperty.boolValue;
                active = EditorGUILayout.Toggle(active, GUILayout.Width(20f));
                useShortcutProperty.boolValue = active;
                EditorGUI.BeginDisabledGroup(!active);
                EditorGUILayout.PropertyField(inputProperty, GUIContent.none, GUILayout.ExpandWidth(true));
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();
            }

            _settings.ApplyModifiedProperties();
        }
    }
}