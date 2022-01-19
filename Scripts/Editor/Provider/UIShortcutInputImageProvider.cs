using System;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Utils.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Types;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public abstract class UIShortcutInputImageProvider<TButton, TAxis> : SettingsProvider where TButton : Enum where TAxis : Enum
    {
        private SerializedObject _settings;
        private SerializedProperty[] _itemsButtonProperties;
        private SerializedProperty[] _itemsAxisProperties;

        private readonly string[] _enumNamesButton;
        private readonly string[] _enumNamesAxis;

        private bool _foldButton, _foldAxis;
        
        protected UIShortcutInputImageProvider(string path, string keyword) : 
            base("Project/UI/Shortcut Input/" + path, SettingsScope.Project, new []{"UI", "Input", "Image", keyword})
        {
            _enumNamesButton = Enum.GetNames(typeof(TButton));
            _enumNamesAxis = Enum.GetNames(typeof(TAxis));
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = UIShortcutInputSettings.SerializedSingleton;
            if (_settings == null)
                return;

            _itemsButtonProperties = _settings.FindProperties(ItemsButtonPropertyName);
            _itemsAxisProperties = _settings.FindProperties(ItemsAxisPropertyName);
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            _foldButton = EditorGUILayout.BeginFoldoutHeaderGroup(_foldButton, "Button");
            if (_foldButton)
            {
                foreach (var property in _itemsButtonProperties)
                {
                    EditorGUILayout.BeginHorizontal();
                    var name = _enumNamesButton[property.FindPropertyRelative("identifier").enumValueIndex];
                    EditorGUILayout.LabelField(name, GUILayout.Width(150f));
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("icon"), GUIContent.none);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.Space(25f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            _foldAxis = EditorGUILayout.BeginFoldoutHeaderGroup(_foldAxis, "Axis");
            if (_foldAxis)
            {
                foreach (var property in _itemsAxisProperties)
                {
                    EditorGUILayout.BeginHorizontal();
                    var name = _enumNamesAxis[property.FindPropertyRelative("identifier").enumValueIndex];
                    EditorGUILayout.LabelField(name, GUILayout.Width(150f));
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("icon"), GUIContent.none);
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.Space(25f);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            _settings.ApplyModifiedProperties();
        }

        protected abstract string ItemsButtonPropertyName { get; }
        protected abstract string ItemsAxisPropertyName { get; }
    }
    
    public sealed class UIShortcutInputKeyboardImageProvider : UIShortcutInputImageProvider<Key, KeyAxis>
    {
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new UIShortcutInputKeyboardImageProvider();
        }

        #endregion
        
        public UIShortcutInputKeyboardImageProvider() : base("Keyboard Images", "Keyboard")
        {
        }

        protected override string ItemsButtonPropertyName => "keyboardButtonShortcutImageItems";
        protected override string ItemsAxisPropertyName => "keyboardAxisShortcutImageItems";
    }
    
    public sealed class UIShortcutInputGamepadImageProvider : UIShortcutInputImageProvider<GamepadButton, GamepadAxis>
    {
        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new UIShortcutInputGamepadImageProvider();
        }

        #endregion
        
        public UIShortcutInputGamepadImageProvider() : base("Gamepad Images", "Keyboard")
        {
        }

        protected override string ItemsButtonPropertyName => "gamepadButtonShortcutImageItems";
        protected override string ItemsAxisPropertyName => "gamepadAxisShortcutImageItems";
    }
}