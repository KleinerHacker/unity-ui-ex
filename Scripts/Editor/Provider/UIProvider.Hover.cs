using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed partial class UIProvider
    {
        private SerializedProperty _hoverMinScaleProperty;
        private SerializedProperty _hoverMaxScaleProperty;
        private SerializedProperty _hoverMinScaleDistanceProperty;
        private SerializedProperty _hoverMaxScaleDistanceProperty;
        private SerializedProperty _hoverScaleInterpolationCurveProperty;
        private SerializedProperty _hoverMinAlphaProperty;
        private SerializedProperty _hoverMaxAlphaProperty;
        private SerializedProperty _hoverMinAlphaDistanceProperty;
        private SerializedProperty _hoverMaxAlphaDistanceProperty;
        private SerializedProperty _hoverAlphaInterpolationCurveProperty;
        private SerializedProperty _hoverItemsProperty;

        private UIHoverList _hoverList;

        private bool _hoverFold = true;

        private void LoadHoverProperties()
        {
            var hover = _settings.FindProperty("hover");
            var hoverProperty = hover.FindPropertyRelative("hoverDefault");
            _hoverMinScaleProperty = hoverProperty.FindPropertyRelative("minScale");
            _hoverMaxScaleProperty = hoverProperty.FindPropertyRelative("maxScale");
            _hoverMinScaleDistanceProperty = hoverProperty.FindPropertyRelative("minScaleDistance");
            _hoverMaxScaleDistanceProperty = hoverProperty.FindPropertyRelative("maxScaleDistance");
            _hoverScaleInterpolationCurveProperty = hoverProperty.FindPropertyRelative("scaleInterpolationCurve");
            _hoverMinAlphaProperty = hoverProperty.FindPropertyRelative("minAlpha");
            _hoverMaxAlphaProperty = hoverProperty.FindPropertyRelative("maxAlpha");
            _hoverMinAlphaDistanceProperty = hoverProperty.FindPropertyRelative("minAlphaDistance");
            _hoverMaxAlphaDistanceProperty = hoverProperty.FindPropertyRelative("maxAlphaDistance");
            _hoverAlphaInterpolationCurveProperty = hoverProperty.FindPropertyRelative("alphaInterpolationCurve");
            _hoverItemsProperty = hover.FindPropertyRelative("hoverItems");

            _hoverList = new UIHoverList(_settings, _hoverItemsProperty);
        }

        private void LayoutHover()
        {
            _hoverFold = EditorGUILayout.BeginFoldoutHeaderGroup(_hoverFold, "Hover");
            if (_hoverFold)
            {
                EditorGUI.indentLevel = 1;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel, GUILayout.Width(75f));
                EditorGUILayout.LabelField("Min Value", RightLabelStyle, GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMinScaleProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Min Value Distance", RightLabelStyle, GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMinScaleDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value", RightLabelStyle, GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMaxScaleProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value Distance", RightLabelStyle, GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMaxScaleDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Interpolation", RightLabelStyle, GUILayout.Width(100f));
                EditorGUILayout.PropertyField(_hoverScaleInterpolationCurveProperty, GUIContent.none, GUILayout.Width(150f));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Alpha", EditorStyles.boldLabel, GUILayout.Width(75f));
                EditorGUILayout.LabelField("Min Value", RightLabelStyle, GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMinAlphaProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Min Value Distance", RightLabelStyle, GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMinAlphaDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value", RightLabelStyle, GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMaxAlphaProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value Distance", RightLabelStyle, GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMaxAlphaDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Interpolation", RightLabelStyle, GUILayout.Width(100f));
                EditorGUILayout.PropertyField(_hoverAlphaInterpolationCurveProperty, GUIContent.none, GUILayout.Width(150f));
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel = 0;

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Variant Hovers", EditorStyles.boldLabel);
                if (UISettings.Singleton.Hover.HoverItems.Any(x => string.IsNullOrEmpty(x.Key)))
                {
                    EditorGUILayout.HelpBox("Any keys are empty!", MessageType.Warning);
                }

                if (UISettings.Singleton.Hover.HoverItems.HasDoublets(x => x.Key))
                {
                    EditorGUILayout.HelpBox("Any keys are double!", MessageType.Warning);
                }

                _hoverList.DoLayoutList();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}