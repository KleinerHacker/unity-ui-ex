using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;
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
        private SerializedProperty _actionsProperty;
        private SerializedProperty _schemesProperty;
        private SerializedProperty _assignmentsProperty;

        private UIShortcutInputActionList _actionList;
        private UIShortcutInputSchemeList _schemeList;
        private UIShortcutInputAssignmentList _assignmentList;

        public UIShortcutInputProvider() : base("Project/UI/Shortcut Input", SettingsScope.Project, new[] { "UI", "Input", "Input System", "Short", "Key" })
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = UIShortcutInputSettings.SerializedSingleton;
            if (_settings == null)
                return;

            _actionsProperty = _settings.FindProperty("actions");
            _schemesProperty = _settings.FindProperty("schemes");
            _assignmentsProperty = _settings.FindProperty("assignments");

            _actionList = new UIShortcutInputActionList(_settings, _actionsProperty);
            _schemeList = new UIShortcutInputSchemeList(_settings, _schemesProperty);
            _assignmentList = new UIShortcutInputAssignmentList(_settings, _assignmentsProperty);
        }

        public override void OnTitleBarGUI()
        {
            GUILayout.BeginVertical();
            {
                EditorGUI.BeginDisabledGroup(
#if PCSOFT_ENV
                    false
#else
                    true
#endif
                );
                {
                    ExtendedEditorGUILayout.SymbolField("Activate System", "PCSOFT_SHORTCUT");
                    EditorGUI.BeginDisabledGroup(
#if PCSOFT_SHORTCUT
                    false
#else
                        true
#endif
                    );
                    {
                        ExtendedEditorGUILayout.SymbolField("Verbose Logging", "PCSOFT_SHORTCUT_LOGGING");
                    }
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.EndDisabledGroup();
            }
            GUILayout.EndVertical();
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            GUILayout.Space(15f);

#if PCSOFT_SHORTCUT && PCSOFT_ENV
            GUILayout.Label("Shortcut Actions", EditorStyles.boldLabel);
            _actionList.DoLayoutList();

            GUILayout.Space(10f);
            GUILayout.Label("Shortcut Schemes", EditorStyles.boldLabel);
            _schemeList.DoLayoutList();

            GUILayout.Space(10f);
            GUILayout.Label("Environment Assignment", EditorStyles.boldLabel);
            _assignmentList.DoLayoutList();
#elif !PCSOFT_ENV
            EditorGUILayout.HelpBox("Environment System deactivated but required", MessageType.Warning);
#else
            EditorGUILayout.HelpBox("Shortcut Input System deactivated", MessageType.Info);
#endif

            _settings.ApplyModifiedProperties();
        }
    }
}