using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Components.UI.Component
{
    [CustomPropertyDrawer(typeof(InputPresenterItem))]
    public sealed class InputPresenterItemDrawer : ExtendedDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return lineHeight * 2f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = new Rect(position.x, position.y, position.width, lineHeight);
            
            var presetGuidProperty = property.FindPropertyRelative("presetGuid");
            var presetIndex = UIInputSettings.Singleton.InputPresets.IndexOf(x => x.Guid == presetGuidProperty.stringValue);
            var options = UIInputSettings.Singleton.InputPresets.Select(x => x.Name).ToArray();
            presetIndex = EditorGUI.Popup(rect, presetIndex, options);
            presetGuidProperty.stringValue = presetIndex < 0 ? "" : UIInputSettings.Singleton.InputPresets[presetIndex].Guid;

            rect = CalculateNext(rect);
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("presenter"));
        }
    }
}