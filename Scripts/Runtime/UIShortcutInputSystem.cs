#if PCSOFT_SHORTCUT
using System.Linq;
using UnityEngine;
using UnityExtension.Runtime.extension.Scripts.Runtime;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime
{
    public static class UIShortcutInputSystem
    {
        public static UIShortcutScheme CurrentInputScheme
        {
            get;
#if UNITY_EDITOR
            set;
#else
            private set;
#endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void Initialize()
        {
            Debug.Log("[SHORTCUT] Initialize Shortcut System");
            var environment = RuntimeEnvironment.CurrentEnvironment;
            if (environment != null)
            {
                var assignment = UIShortcutInputSettings.Singleton.Assignments
                    .FirstOrDefault(x => x.EnvironmentGroupName == environment.DetectedEnvironmentGroup);
                if (assignment != null)
                {
                    var scheme = UIShortcutInputSettings.Singleton.Schemes
                        .FirstOrDefault(x => x.Name == assignment.InputSchemeName);
                    CurrentInputScheme = scheme;

                    if (scheme != null)
                    {
#if PCSOFT_SHORTCUT_LOGGING
                        Debug.Log("[SHORTCUT] Use input scheme " + scheme.Name);
#endif
                    }
                    else
                    {
#if PCSOFT_SHORTCUT_LOGGING
                        Debug.Log("[SHORTCUT] No fit input scheme found");
#endif
                    }
                }
                else
                {
#if PCSOFT_SHORTCUT_LOGGING
                    Debug.Log("[SHORTCUT] No fit environment assignment found");
#endif
                }
            }
            else
            {
#if PCSOFT_SHORTCUT_LOGGING
                Debug.Log("[SHORTCUT] No fit environment found");
#endif
            }
        }
    }
}
#endif