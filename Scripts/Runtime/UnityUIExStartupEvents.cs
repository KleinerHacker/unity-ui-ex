using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime.Loader;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime
{
    public static class UnityUIExStartupEvents
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Initialize()
        {
            Debug.Log("Initialize UI input");
            AssetResourcesLoader.Instance.LoadAssets<UIShortcutInputSettings>("");
            AssetResourcesLoader.Instance.LoadAssets<UIAudioSettings>("");
        }
    }
}