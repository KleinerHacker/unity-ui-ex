using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Extras;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Extras
{
    [CustomPropertyDrawer(typeof(UIShortcutSchemeActionAttribute))]
    public sealed class UIShortcutSchemeActionAttributeDrawer : ExtendedDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var actionAttribute = (UIShortcutSchemeActionAttribute)attribute;
            var all = UIShortcutInputSettings.Singleton.Actions
                .Where(x => x.InputType == actionAttribute.InputType)
                .Select(x => x.Name)
                .ToArray();
            
            var selected = all.IndexOf(x => property.stringValue == x);
            var newSelected = EditorGUI.Popup(position, property.displayName + " (" + actionAttribute.InputType + ")", selected, all);
            if (selected != newSelected)
            {
                property.stringValue = newSelected < 0 ? null : all[newSelected];
            }
        }
    }
}