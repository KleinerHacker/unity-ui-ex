using UnityEditor;
using UnityEngine;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed partial class UIProvider
    {
        private SerializedProperty _notificationInfoColorProperty;
        private SerializedProperty _notificationWarningColorProperty;
        private SerializedProperty _notificationErrorColorProperty;

        private bool _notificationFold = true;

        private void LoadNotificationProperties()
        {
            var notification = _settings.FindProperty("notification");
            _notificationInfoColorProperty = notification.FindPropertyRelative("infoColor");
            _notificationWarningColorProperty = notification.FindPropertyRelative("warningColor");
            _notificationErrorColorProperty = notification.FindPropertyRelative("errorColor");
        }

        private void LayoutNotification()
        {
            _notificationFold = EditorGUILayout.BeginFoldoutHeaderGroup(_notificationFold, "Notification");
            if (_notificationFold)
            {
                EditorGUI.indentLevel = 1;

                EditorGUILayout.LabelField("Level Colors", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Information", GUILayout.Width(100f));
                EditorGUILayout.PropertyField(_notificationInfoColorProperty, GUIContent.none, GUILayout.Width(100f));
                EditorGUILayout.LabelField("Warning", GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_notificationWarningColorProperty, GUIContent.none, GUILayout.Width(100f));
                EditorGUILayout.LabelField("Error", GUILayout.Width(50f));
                EditorGUILayout.PropertyField(_notificationErrorColorProperty, GUIContent.none, GUILayout.Width(100f));
                EditorGUILayout.EndHorizontal();
                
                EditorGUI.indentLevel = 0;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}