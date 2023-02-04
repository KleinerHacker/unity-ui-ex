using UnityEditor;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Actions
{
    public static class InputActions
    {
        private const string MenuNothing = "View/Scene/Input/Show Nothing";
        private const string MenuGamepad = "View/Scene/Input/Show Gamepad";
        private const string MenuKeyboard = "View/Scene/Input/Show Keyboard";

        [InitializeOnLoadMethod]
        public static void InitInput()
        {
            UpdateCheckmarks();
        }
        
        [MenuItem(MenuNothing)]
        public static void ShowNothing()
        {
            // UIShortcutInputSettings.DisplayedShortcut = null;
            UpdateCheckmarks();
        }
        
        [MenuItem(MenuKeyboard)]
        public static void ShowKeyboard()
        {
            // UIShortcutInputSettings.DisplayedShortcut = UIShortcutInput.Keyboard;
            UpdateCheckmarks();
        }
        
        [MenuItem(MenuGamepad)]
        public static void ShowGamepad()
        {
            // UIShortcutInputSettings.DisplayedShortcut = UIShortcutInput.Gamepad;
            UpdateCheckmarks();
        }

        private static void UpdateCheckmarks()
        {
            // Menu.SetChecked(MenuNothing, UIShortcutInputSettings.DisplayedShortcut == null);
            // Menu.SetChecked(MenuKeyboard, UIShortcutInputSettings.DisplayedShortcut == UIShortcutInput.Keyboard);
            // Menu.SetChecked(MenuGamepad, UIShortcutInputSettings.DisplayedShortcut == UIShortcutInput.Gamepad);
        }
    }
}