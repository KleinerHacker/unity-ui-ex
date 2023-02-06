#if PCSOFT_SHORTCUT && PCSOFT_ENV
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Overlay
{
    [Overlay(typeof(SceneView), "Shortcut Input")]
    public class ShortcutInputOverlay : ToolbarOverlay
    {
        public ShortcutInputOverlay() : base("UnityUI/ShortcutInput")
        {
        }
    }

    [EditorToolbarElement("UnityUI/ShortcutInput", typeof(SceneView))]
    public class ShortcutInputButton : EditorToolbarButton
    {
        public ShortcutInputButton()
        {
            tooltip = "Change view of shortcut input";
            icon = (Texture2D)EditorGUIUtility.IconContent("d_Text Icon").image;
            clicked += OnClicked;
        }

        private void OnClicked()
        {
            var ctxMenu = new GenericMenu();
            ctxMenu.AddItem(new GUIContent("None"), UIShortcutInputSystem.CurrentInputScheme == null, () =>
            {
                UIShortcutInputSystem.CurrentInputScheme = null;
                foreach (var input in Object.FindObjectsOfType<UIInput>())
                {
                    input.Refresh();
                }
            });
            ctxMenu.AddSeparator(null);
            foreach (var scheme in UIShortcutInputSettings.Singleton.Schemes)
            {
                ctxMenu.AddItem(new GUIContent(scheme.Name), UIShortcutInputSystem.CurrentInputScheme == scheme, () =>
                {
                    UIShortcutInputSystem.CurrentInputScheme = scheme;
                    foreach (var input in Object.FindObjectsOfType<UIInput>())
                    {
                        input.Refresh();
                    }
                });                
            }
            ctxMenu.ShowAsContext();
        }
    }
}
#endif