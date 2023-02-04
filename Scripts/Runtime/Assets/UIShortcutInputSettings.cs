using System;
using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Assets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UIShortcutInputSettings : ProviderAsset<UIShortcutInputSettings>
    {
        #region Static Area

        public static UIShortcutInputSettings Singleton => GetSingleton("UI Shortcut Input", "ui-shortcut-input.asset");

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => GetSerializedSingleton("UI Shortcut Input", "ui-shortcut-input.asset");
#endif

        #endregion

        #region Inspector Data

        [SerializeField]
        private UIShortcutAction[] actions = Array.Empty<UIShortcutAction>();

        [SerializeField]
        private UIShortcutScheme[] schemes = Array.Empty<UIShortcutScheme>();

        [SerializeField]
        private UIShortcutEnvironmentAssignment[] assignments = Array.Empty<UIShortcutEnvironmentAssignment>();

        #endregion

        #region Properties

        public UIShortcutAction[] Actions => actions;

        public UIShortcutScheme[] Schemes => schemes;

        public UIShortcutEnvironmentAssignment[] Assignments => assignments;

        #endregion

        #region Builtin Methods

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var scheme in schemes)
            {
                var removeList = scheme.Items.Where(x => actions.All(y => y.Name != x.AssignedAction));
                var addList = actions.Where(x => scheme.Items.All(y => y.AssignedAction != x.Name));

                scheme.Items = scheme.Items.RemoveAll(removeList.ToArray()).ToArray();
                scheme.Items = scheme.Items.Concat(addList.Select(x => new UIShortcutSchemeItem { AssignedAction = x.Name })).ToArray();
            }
        }
#endif

        #endregion
    }

    [Serializable]
    public sealed class UIShortcutEnvironmentAssignment
    {
        #region Inspector Data

        [SerializeField]
        private string environmentGroupName;

        [SerializeField]
        private string inputSchemeName;

        #endregion

        #region Properties

        public string EnvironmentGroupName => environmentGroupName;

        public string InputSchemeName => inputSchemeName;

        #endregion
    }

    [Serializable]
    public sealed class UIShortcutScheme
    {
        #region Inspector Data

        [SerializeField]
        private string name;

        [SerializeField]
        private UIShortcutSchemeItem[] items = Array.Empty<UIShortcutSchemeItem>();

        #endregion

        #region Properties

        public string Name => name;

        public UIShortcutSchemeItem[] Items
        {
            get => items;
#if UNITY_EDITOR
            set => items = value;
#endif
        }

        #endregion
    }

    [Serializable]
    public sealed class UIShortcutSchemeItem
    {
        #region Inspector Data

        [SerializeField]
        private string assignedAction;

        [SerializeField]
        private UIShortcutInput inputType = UIShortcutInput.Keyboard;

        [SerializeField]
        private Key inputKey = Key.Enter;

        [SerializeField]
        private GamepadButton inputGamepad = GamepadButton.A;

        [SerializeField]
        private Sprite icon;

        #endregion

        #region Properties

        public string AssignedAction
        {
            get => assignedAction;
#if UNITY_EDITOR
            set => assignedAction = value;
#endif
        }

        public UIShortcutInput InputType => inputType;

        public Key InputKey => inputKey;

        public GamepadButton InputGamepad => inputGamepad;

        public Sprite Icon => icon;

        #endregion
    }

    [Serializable]
    public sealed class UIShortcutAction
    {
        #region Inspector Data

        [SerializeField]
        private string name;

        #endregion

        #region Properties

        public string Name => name;

        #endregion
    }

    public enum UIShortcutInput
    {
        Keyboard,
        Gamepad
    }
}