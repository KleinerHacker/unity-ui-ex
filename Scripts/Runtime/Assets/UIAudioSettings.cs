#if !UNITY_EDITOR
using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime.Loader;
#endif
using UnityEditor;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UIAudioSettings : ScriptableObject
    {
        #region Static Area

#if UNITY_EDITOR
        private const string Path = "Assets/Resources/ui-audio.asset";
#endif

        public static UIAudioSettings Singleton
        {
            get
            {
#if UNITY_EDITOR
                var settings = AssetDatabase.LoadAssetAtPath<UIAudioSettings>(Path);
                if (settings == null)
                {
                    Debug.Log("Unable to find UI audio settings, create new");

                    settings = new UIAudioSettings();
                    if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                    {
                        AssetDatabase.CreateFolder("Assets", "Resources");
                    }
                    
                    AssetDatabase.CreateAsset(settings, Path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }

                return settings;
#else
                return AssetResourcesLoader.Instance.GetAsset<UIAudioSettings>();
#endif
            }
        }

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => new SerializedObject(Singleton);
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