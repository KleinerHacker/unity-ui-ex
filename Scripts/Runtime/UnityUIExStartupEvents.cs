using System.Linq;
using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime;
using UnityEngine;
using UnityExtension.Runtime.extension.Scripts.Runtime;
using UnityExtension.Runtime.extension.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime
{
    public static class UnityUIExStartupEvents
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Initialize()
        {
            Debug.Log("Initialize UI input");
            AssetResourcesLoader.LoadFromResources<UIShortcutInputSettings>("");
            AssetResourcesLoader.LoadFromResources<UIAudioSettings>("");
        }
    }
}