#if !UNITY_EDITOR
using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime.Loader;
#endif
using UnityEditor;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UISettings : ScriptableObject
    {
        #region Static Area

#if UNITY_EDITOR
        private const string Path = "Assets/Resources/ui.asset";
#endif

        public static UISettings Singleton
        {
            get
            {
#if UNITY_EDITOR
                var settings = AssetDatabase.LoadAssetAtPath<UISettings>(Path);
                if (settings == null)
                {
                    Debug.Log("Unable to find UI settings, create new");

                    settings = new UISettings();
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
                return AssetResourcesLoader.Instance.GetAsset<UISettings>();
#endif
            }
        }

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => new SerializedObject(Singleton);
#endif

        #endregion

        #region Inspector Data

        [SerializeField]
        private UIHover hover = new UIHover();

        [SerializeField]
        private UINotification notification = new UINotification();

        #endregion

        #region Properties

        public UIHover Hover => hover;

        public UINotification Notification => notification;

        #endregion
    }
}