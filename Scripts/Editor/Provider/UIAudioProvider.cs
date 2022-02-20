using System.Linq;
using Unity.Collections;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;
using UnitySfx.Runtime.sfx_system.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Provider
{
    public sealed class UIAudioProvider : SettingsProvider
    {
        private const string SfxSystemSymbol = "SFX_SYSTEM";

        #region Static Area

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new UIAudioProvider();
        }

        #endregion

        private SerializedObject _settings;
        private SerializedProperty _sfxSystemNameProperty;
        private SerializedProperty _audioSystemProperty;

        public UIAudioProvider() : base("Project/UI/Audio", SettingsScope.Project, new[] { "tooling", "UI", "audio", "SFX" })
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = UIAudioSettings.SerializedSingleton;
            if (_settings == null)
                return;

            _sfxSystemNameProperty = _settings.FindProperty("sfxSystemName");
            _audioSystemProperty = _settings.FindProperty("audioSystem");
        }

        public override void OnGUI(string searchContext)
        {
            _settings.Update();

            var values = new[] { "<Default>" }.Concat(SfxSystemSettings.Singleton.Items.Select(x => x.Identifier)).ToArray();
            var index = string.IsNullOrEmpty(_sfxSystemNameProperty.stringValue) ? 0 : values.IndexOf(x => x == _sfxSystemNameProperty.stringValue);
            index = EditorGUILayout.Popup("SFX System Name", index, values);
            if (index == 0)
            {
                _sfxSystemNameProperty.stringValue = null;
            }
            else if (index > 0)
            {
                _sfxSystemNameProperty.stringValue = values[index];
            }

            PlayerSettings.GetScriptingDefineSymbols(NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup), out var symbols);
            var newValue = (UIAudioSystem)EditorGUILayout.EnumPopup(new GUIContent("Audio System Usage"), (UIAudioSystem)_audioSystemProperty.intValue);
            if ((int)newValue != _audioSystemProperty.intValue)
            {
                if (newValue == UIAudioSystem.AudioClips && symbols.Contains(SfxSystemSymbol))
                {
                    PlayerSettings.SetScriptingDefineSymbols(
                        NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                        symbols.Remove(SfxSystemSymbol).ToArray()
                    );
                }
                else if (newValue == UIAudioSystem.SfxClips && !symbols.Contains(SfxSystemSymbol))
                {
                    PlayerSettings.SetScriptingDefineSymbols(
                        NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup),
                        symbols.Append(SfxSystemSymbol).ToArray()
                    );
                }

                _audioSystemProperty.intValue = (int)newValue;
            }

            _settings.ApplyModifiedProperties();
        }
    }
}