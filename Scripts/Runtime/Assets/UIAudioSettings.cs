using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Assets;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UIAudioSettings : ProviderAsset<UIAudioSettings>
    {
        #region Static Area

        public static UIAudioSettings Singleton => GetSingleton("UI Audio", "ui-audio.asset");

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => GetSerializedSingleton("UI Audio", "ui-audio.asset");
#endif

        #endregion

        #region Inspector Data

        [SerializeField]
        private string sfxSystemName;

        [SerializeField]
        private UIAudioSystem audioSystem = UIAudioSystem.AudioClips;

        #endregion

        #region Properties

        public string SfxSystemName => sfxSystemName;

        public UIAudioSystem AudioSystem => audioSystem;

        #endregion
    }

    public enum UIAudioSystem
    {
        AudioClips,
        SfxClips,
    }
}