using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime
{
    public static class UnityUIExStartupEvents
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#endif
        public static void Initialize()
        {
            Debug.Log("Initialize UI extensions");
        }
    }
}