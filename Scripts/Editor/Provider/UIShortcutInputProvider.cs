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

        public override void OnGUI(string searchContext)
        {
            _settings.Update();
            
            GUILayout.Space(15f);
            
            GUILayout.Label("Shortcut Actions", EditorStyles.boldLabel);
            _actionList.DoLayoutList();
            
            GUILayout.Space(10f);
            GUILayout.Label("Shortcut Schemes", EditorStyles.boldLabel);
            _schemeList.DoLayoutList();
            
            GUILayout.Space(10f);
            GUILayout.Label("Environment Assignment", EditorStyles.boldLabel);
            _assignmentList.DoLayoutList();

            _settings.ApplyModifiedProperties();
        }
    }
}