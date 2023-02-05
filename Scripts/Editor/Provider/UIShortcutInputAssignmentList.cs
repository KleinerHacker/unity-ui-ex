using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils.Extensions;
using UnityEngine;
using UnityExtension.Runtime.extension.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIShortcutInputAssignmentList : TableReorderableList
    {
        public UIShortcutInputAssignmentList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            Columns.Add(new FlexibleColumn { HeaderText = "Environment Target Group", PercentageWidth = 0.5f, ElementCallback = EnvironmentTargetGroupElementCallback });
            Columns.Add(new FlexibleColumn { HeaderText = "Input Scheme", PercentageWidth = 0.5f, ElementCallback = InputSchemeElementCallback });
        }

        private void EnvironmentTargetGroupElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);

            var all = EnvironmentDetectionSettings.Singleton.Groups.Select(x => x.Name).ToArray();
            var selected = all.IndexOf(x => prop.GetRelativeString("environmentGroupName") == x);
            var newSelected = EditorGUI.Popup(rect, selected, all);
            if (selected != newSelected)
            {
                prop.SetRelativeString("environmentGroupName", newSelected < 0 ? null : all[newSelected]);
            }
        }

        private void InputSchemeElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var prop = serializedProperty.GetArrayElementAtIndex(i);

            var all = UIShortcutInputSettings.Singleton.Schemes.Select(x => x.Name).ToArray();
            var selected = all.IndexOf(x => prop.GetRelativeString("inputSchemeName") == x);
            var newSelected = EditorGUI.Popup(rect, selected, all);
            if (selected != newSelected)
            {
                prop.SetRelativeString("inputSchemeName", newSelected < 0 ? null : all[newSelected]);
            }
        }
    }
}