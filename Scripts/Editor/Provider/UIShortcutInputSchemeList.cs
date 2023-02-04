using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEngine;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIShortcutInputSchemeList : TableReorderableList
    {
        public UIShortcutInputSchemeList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            Columns.Add(new FixedColumn {HeaderText = "Name", AbsoluteWidth = 150f, MaxHeight = 20f, ElementCallback = NameElementCallback});
            Columns.Add(new FlexibleColumn {HeaderText = "Shortcuts", ElementCallback = ShortcutsElementCallback});
            
            elementHeightCallback += ElementHeightCallback;
        }

        private void NameElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("name"), GUIContent.none);
        }

        private void ShortcutsElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            new UIShortcutInputSchemeActionList(serializedProperty.serializedObject, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("items"))
                .DoList(rect);
        }

        private float ElementHeightCallback(int i)
        {
            return serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("items").arraySize * 25f + 25f;
        }
    }
}