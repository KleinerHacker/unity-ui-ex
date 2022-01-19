using System.Collections.Generic;
using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIProvider : SettingsProvider
    {
        private static readonly GUIStyle RightLabelStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleRight
        };
        
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new UIProvider();
        }

        #endregion
        
        private SerializedObject _settings;
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
        
        public UIProvider() : base("Project/UI", SettingsScope.Project, new []{"UI", "Hover", "Tooling"})
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = UISettings.SerializedSingleton;
            if (_settings == null)
                return;

            var hoverProperty = _settings.FindProperty("hoverDefault");
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
            _hoverItemsProperty = _settings.FindProperty("hoverItems");

            _hoverList = new UIHoverList(_settings, _hoverItemsProperty);
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            _hoverFold = EditorGUILayout.BeginFoldoutHeaderGroup(_hoverFold, "Hover");
            if (_hoverFold)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel, GUILayout.Width(75f));
                EditorGUILayout.LabelField("Min Value", RightLabelStyle,GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMinScaleProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Min Value Distance", RightLabelStyle,GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMinScaleDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value", RightLabelStyle,GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMaxScaleProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value Distance", RightLabelStyle,GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMaxScaleDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Interpolation", RightLabelStyle, GUILayout.Width(100f));
                EditorGUILayout.PropertyField(_hoverScaleInterpolationCurveProperty, GUIContent.none, GUILayout.Width(150f));
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Alpha", EditorStyles.boldLabel, GUILayout.Width(75f));
                EditorGUILayout.LabelField("Min Value", RightLabelStyle,GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMinAlphaProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Min Value Distance", RightLabelStyle,GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMinAlphaDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value", RightLabelStyle,GUILayout.Width(75f));
                EditorGUILayout.PropertyField(_hoverMaxAlphaProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Max Value Distance", RightLabelStyle,GUILayout.Width(150f));
                EditorGUILayout.PropertyField(_hoverMaxAlphaDistanceProperty, GUIContent.none, GUILayout.Width(50f));
                EditorGUILayout.LabelField("Interpolation", RightLabelStyle, GUILayout.Width(100f));
                EditorGUILayout.PropertyField(_hoverAlphaInterpolationCurveProperty, GUIContent.none, GUILayout.Width(150f));
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Variant Hovers", EditorStyles.boldLabel);
            if (UISettings.Singleton.HoverItems.Any(x => string.IsNullOrEmpty(x.Key)))
            {
                EditorGUILayout.HelpBox("Any keys are empty!", MessageType.Warning);
            }

            if (UISettings.Singleton.HoverItems.HasDoublets(x => x.Key))
            {
                EditorGUILayout.HelpBox("Any keys are double!", MessageType.Warning);
            }
            _hoverList.DoLayoutList();

            _settings.ApplyModifiedProperties();
        }
    }
}