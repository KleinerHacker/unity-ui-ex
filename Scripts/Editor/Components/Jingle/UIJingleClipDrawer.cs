using System;
using System.Linq;
using UnityBase.Runtime.Projects.unity_base.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.Projects.unity_editor_ex.Scripts.Editor;
using UnityEngine;
using UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle;

namespace UnityUIEx.Editor.Projects.unity_ui_ex.Scripts.Editor.Components.Jingle
{
    [CustomPropertyDrawer(typeof(UIJingleClip))]
    public sealed class UIJingleClipDrawer : ExtendedDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.FindPropertyRelative("type").enumValueIndex == (int)UIJingleClipType.None ? 20f : 40f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position.HalfX().Height(20f), label, EditorStyles.boldLabel);

            var typeProperty = property.FindPropertyRelative("type");
            var type = Enum.GetValues(typeof(UIJingleClipType)).Cast<UIJingleClipType>().ElementAt(typeProperty.enumValueIndex);
            var newType = (UIJingleClipType) EditorGUI.EnumPopup(position.ShiftHalfX().HalfY(), type);
            if (type != newType)
            {
                Debug.LogWarning("From " + (int)type + " to " + (int)newType);
                typeProperty.enumValueIndex = Enum.GetValues(typeof(UIJingleClipType)).Cast<UIJingleClipType>().IndexOf(x => x == newType);
            }
            
            switch (newType)
            {
                case UIJingleClipType.None:
                    return;
                case UIJingleClipType.Single:
                    EditorGUI.PropertyField(position.ShiftHalfX().ShiftY(20f).Height(20f), property.FindPropertyRelative("singleClip"), GUIContent.none);
                    break;
                case UIJingleClipType.Group:
                    EditorGUI.PropertyField(position.ShiftHalfX().ShiftY(20f).Height(20f), property.FindPropertyRelative("groupClip"), GUIContent.none);
                    break;
                default:
                    throw new NotImplementedException("Unsupported UIJingleClipType: " + newType);
            }
        }
    }
}