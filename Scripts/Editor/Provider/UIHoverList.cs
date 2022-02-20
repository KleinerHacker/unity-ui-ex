using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIHoverList : ReorderableList
    {
        private const float LeftMargin = 15f;
        private const float BottomMargin = 2f;
        private const float ColumnSpace = 5f;

        private const float TypeWidth = 100f;
        private const float MinWidth = 75f;
        private const float MaxWidth = 75f;
        private const float MinDistanceWidth = 150f;
        private const float MaxDistanceWidth = 150f;
        private const float InterpolationWidth = 100f;
        private const float CommonWidth = MinDistanceWidth + MaxDistanceWidth + InterpolationWidth + MinWidth + MaxWidth + TypeWidth;

        public UIHoverList(SerializedObject serializedObject, SerializedProperty elements) : base(serializedObject, elements)
        {
            drawHeaderCallback += DrawHeaderCallback;
            drawElementCallback += DrawElementCallback;
            elementHeight = 40f;
        }

        private void DrawHeaderCallback(Rect rect)
        {
            var commonWidth = rect.width - (CommonWidth + LeftMargin);
            var pos = new Rect(rect.x + LeftMargin, rect.y, commonWidth, rect.height);
            EditorGUI.LabelField(pos, new GUIContent("Key"));
            
            pos = new Rect(rect.x + LeftMargin + commonWidth, rect.y, TypeWidth, rect.height);
            EditorGUI.LabelField(pos, new GUIContent("Type"));

            pos = new Rect(rect.x + LeftMargin + commonWidth + TypeWidth, rect.y, MinWidth, rect.height);
            EditorGUI.LabelField(pos, new GUIContent("Min Value"));

            pos = new Rect(rect.x + LeftMargin + commonWidth + TypeWidth + MinWidth, rect.y, MinDistanceWidth, rect.height);
            EditorGUI.LabelField(pos, new GUIContent("Min Value Distance"));

            pos = new Rect(rect.x + LeftMargin + commonWidth + TypeWidth + MinWidth + MinDistanceWidth, rect.y, MaxWidth, rect.height);
            EditorGUI.LabelField(pos, new GUIContent("Max Value"));
            
            pos = new Rect(rect.x + LeftMargin + commonWidth + TypeWidth + MinWidth + MinDistanceWidth + MaxWidth, rect.y, MaxDistanceWidth, rect.height);
            EditorGUI.LabelField(pos, new GUIContent("Max Value Distance"));

            pos = new Rect(rect.x + LeftMargin + commonWidth + TypeWidth + MinWidth + MinDistanceWidth + MaxWidth + MaxDistanceWidth, rect.y, InterpolationWidth, rect.height);
            EditorGUI.LabelField(pos, new GUIContent("Interpolation"));
        }

        private void DrawElementCallback(Rect rect, int i, bool isactive, bool isfocused)
        {
            var hoverProperty = serializedProperty.GetArrayElementAtIndex(i);
            var hoverKeyProperty = hoverProperty.FindPropertyRelative("key");
            var hoverMinScaleProperty = hoverProperty.FindPropertyRelative("minScale");
            var hoverMaxScaleProperty = hoverProperty.FindPropertyRelative("maxScale");
            var hoverMinScaleDistanceProperty = hoverProperty.FindPropertyRelative("minScaleDistance");
            var hoverMaxScaleDistanceProperty = hoverProperty.FindPropertyRelative("maxScaleDistance");
            var hoverScaleInterpolationCurveProperty = hoverProperty.FindPropertyRelative("scaleInterpolationCurve");
            var hoverMinAlphaProperty = hoverProperty.FindPropertyRelative("minAlpha");
            var hoverMaxAlphaProperty = hoverProperty.FindPropertyRelative("maxAlpha");
            var hoverMinAlphaDistanceProperty = hoverProperty.FindPropertyRelative("minAlphaDistance");
            var hoverMaxAlphaDistanceProperty = hoverProperty.FindPropertyRelative("maxAlphaDistance");
            var hoverAlphaInterpolationCurveProperty = hoverProperty.FindPropertyRelative("alphaInterpolationCurve");

            var commonWidth = rect.width - CommonWidth;
            var halfHeight = rect.height / 2f - BottomMargin;
            var pos = new Rect(rect.x, rect.y, commonWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverKeyProperty, GUIContent.none);
            
            pos = new Rect(rect.x + commonWidth, rect.y, TypeWidth - ColumnSpace, halfHeight);
            EditorGUI.LabelField(pos, "Scale", EditorStyles.boldLabel);

            pos = new Rect(rect.x + commonWidth + TypeWidth, rect.y, MinWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMinScaleProperty, GUIContent.none);

            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth, rect.y, MinDistanceWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMinScaleDistanceProperty, GUIContent.none);

            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth + MinDistanceWidth, rect.y, MaxWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMaxScaleProperty, GUIContent.none);

            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth + MaxWidth + MinDistanceWidth, rect.y, MaxDistanceWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMaxScaleDistanceProperty, GUIContent.none);
            
            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth + MaxWidth + MinDistanceWidth + MaxDistanceWidth, rect.y, InterpolationWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverScaleInterpolationCurveProperty, GUIContent.none);
            
            /********************************************************************************/

            var rectY = rect.y + halfHeight + BottomMargin;
            //pos = new Rect(rect.x, rectY, commonWidth - ColumnSpace, halfHeight);
            //EditorGUI.PropertyField(pos, hoverKeyProperty, GUIContent.none);
            
            pos = new Rect(rect.x + commonWidth, rectY, TypeWidth - ColumnSpace, halfHeight);
            EditorGUI.LabelField(pos, "Alpha", EditorStyles.boldLabel);

            pos = new Rect(rect.x + commonWidth + TypeWidth, rectY, MinWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMinAlphaProperty, GUIContent.none);

            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth, rectY, MinDistanceWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMinAlphaDistanceProperty, GUIContent.none);

            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth + MinDistanceWidth, rectY, MaxWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMaxAlphaProperty, GUIContent.none);

            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth + MaxWidth + MinDistanceWidth, rectY, MaxDistanceWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverMaxAlphaDistanceProperty, GUIContent.none);
            
            pos = new Rect(rect.x + commonWidth + TypeWidth + MinWidth + MaxWidth + MinDistanceWidth + MaxDistanceWidth, rectY, InterpolationWidth - ColumnSpace, halfHeight);
            EditorGUI.PropertyField(pos, hoverAlphaInterpolationCurveProperty, GUIContent.none);
        }
    }
}