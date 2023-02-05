using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEngine;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIShortcutInputActionList : TableReorderableList
    {
        public UIShortcutInputActionList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            Columns.Add(new FlexibleColumn {HeaderText = "Name", MaxHeight = 20f, ElementCallback = NameElementCallback});
            Columns.Add(new FixedColumn {HeaderText = "Input Type", AbsoluteWidth = 150f, ElementCallback = InputTypeElementCallback});
        }

        private void NameElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("name"), GUIContent.none);
        }

        private void InputTypeElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("inputType"), GUIContent.none);
        }
    }
}