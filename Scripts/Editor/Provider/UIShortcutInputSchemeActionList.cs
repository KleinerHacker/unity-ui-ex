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
            Columns.Add(new FixedColumn {HeaderText = "Target Input Device", AbsoluteWidth = 100f, ElementCallback = TargetDeviceElementCallback});
            Columns.Add(new FixedColumn {HeaderText = "Input Type", AbsoluteWidth = 100f, ElementCallback = InputTypeElementCallback});
            Columns.Add(new FixedColumn {HeaderText = "Input", AbsoluteWidth = 100f, ElementCallback = InputElementCallback});
            Columns.Add(new FlexibleColumn {HeaderText = "Icon", MaxHeight = 20f, ElementCallback = IconElementCallback});
        }

        private void NameElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            GUI.Label(rect, serializedProperty.GetArrayElementAtIndex(i).GetRelativeString("assignedAction"));
        }

        private void TargetDeviceElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("inputDevice"), GUIContent.none);
        }

        private void InputTypeElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("inputType"), GUIContent.none);
        }

        private void InputElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);
            switch (prop.GetRelativeEnum<UIShortcutInput>("inputDevice"))
            {
                case UIShortcutInput.Keyboard:
                    HandleKeyboard();
                    break;
                case UIShortcutInput.Gamepad:
                    HandleGamepad();
                    break;
                default:
                    throw new NotImplementedException(prop.GetRelativeEnum<UIShortcutInput>("inputDevice").ToString());
            }

            #region Internal Methods

            void HandleKeyboard()
            {
                switch (prop.GetRelativeEnum<UIShortcutInputType>("inputType"))
                {
                    case UIShortcutInputType.Button:
                        EditorGUI.PropertyField(rect, prop.FindPropertyRelative("inputKeyButton"), GUIContent.none);
                        break;
                    case UIShortcutInputType.Axis:
                        EditorGUI.PropertyField(rect, prop.FindPropertyRelative("inputKeyAxis"), GUIContent.none);
                        break;
                    default:
                        throw new NotImplementedException(prop.GetRelativeEnum<UIShortcutInputType>("inputType").ToString());
                }
            }

            void HandleGamepad()
            {
                switch (prop.GetRelativeEnum<UIShortcutInputType>("inputType"))
                {
                    case UIShortcutInputType.Button:
                        EditorGUI.PropertyField(rect, prop.FindPropertyRelative("inputGamepadButton"), GUIContent.none);
                        break;
                    case UIShortcutInputType.Axis:
                        EditorGUI.PropertyField(rect, prop.FindPropertyRelative("inputGamepadAxis"), GUIContent.none);
                        break;
                    default:
                        throw new NotImplementedException(prop.GetRelativeEnum<UIShortcutInputType>("inputType").ToString());
                }
            }

            #endregion
        }

        private void IconElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            EditorGUI.PropertyField(rect, serializedProperty.GetArrayElementAtIndex(i).FindPropertyRelative("icon"), GUIContent.none);
        }
    }
}