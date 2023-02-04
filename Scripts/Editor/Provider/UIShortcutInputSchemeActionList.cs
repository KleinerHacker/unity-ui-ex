using System;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils.Extensions;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIShortcutInputSchemeActionList : TableReorderableList
    {
        public UIShortcutInputSchemeActionList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements, false, true, false, false)
        {
            Columns.Add(new FixedColumn {HeaderText = "Action Name", AbsoluteWidth = 150f, ElementCallback = NameElementCallback});
            Columns.Add(new FixedColumn {HeaderText = "Target Input Device", AbsoluteWidth = 150f, ElementCallback = TargetDeviceElementCallback});
            Columns.Add(new FixedColumn {HeaderText = "Input", AbsoluteWidth = 150f, ElementCallback = InputElementCallback});
            Columns.Add(new FlexibleColumn {HeaderText = "Icon", MaxHeight = 20f, ElementCallback = IconElementCallback});
        }

        private void NameElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            GUI.Label(rect, serializedProperty.GetArrayElementAtIndex(i).GetRelativeString("assignedAction"));
        }

        private void TargetDeviceElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("inputType"), GUIContent.none);
        }

        private void InputElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            if (prop.GetRelativeInt("inputType") == (int)UIShortcutInput.Keyboard)
            {
                EditorGUI.PropertyField(rect, prop.FindPropertyRelative("inputKey"), GUIContent.none);
            } 
            else if (prop.GetRelativeInt("inputType") == (int)UIShortcutInput.Gamepad)
            {
                EditorGUI.PropertyField(rect, prop.FindPropertyRelative("inputGamepad"), GUIContent.none);
            }
            else
                throw new NotImplementedException(prop.GetRelativeInt("inputType") + "");
        }

        private void IconElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("icon"), GUIContent.none);
        }
    }
}