using System;
using System.Linq;
using UnityEditor;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Types;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityExtension.Runtime.extension.Scripts.Runtime;
using UnityExtension.Runtime.extension.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Types;
#if !UNITY_EDITOR
using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime.Loader;
#endif

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UIShortcutInputSettings : ScriptableObject
    {
        #region Static Area

#if UNITY_EDITOR
        private const string Path = "Assets/Resources/ui-shortcut-input.asset";
#endif

        public static UIShortcutInputSettings Singleton
        {
            get
            {
#if UNITY_EDITOR
                var settings = AssetDatabase.LoadAssetAtPath<UIShortcutInputSettings>(Path);
                if (settings == null)
                {
                    Debug.Log("Unable to find game settings, create new");

                    settings = new UIShortcutInputSettings();
                    AssetDatabase.CreateAsset(settings, Path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }

                return settings;
#else
                return AssetResourcesLoader.Instance.GetAsset<UIInputSettings>();
#endif
            }
        }

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => new SerializedObject(Singleton);
#endif

        #endregion

        #region Inspector Data
        
        [SerializeField]
        private UIShortcutInput shortcutInput = UIShortcutInput.Keyboard;

        [SerializeField]
        private bool useShortcut;

        [SerializeField]
        private ShortcutConstraintItem[] constraintItems = Array.Empty<ShortcutConstraintItem>();

        [SerializeField]
        private KeyboardButtonShortcutImageItem[] keyboardButtonShortcutImageItems = Array.Empty<KeyboardButtonShortcutImageItem>();
        
        [SerializeField]
        private KeyboardAxisShortcutImageItem[] keyboardAxisShortcutImageItems = Array.Empty<KeyboardAxisShortcutImageItem>();
        
        [SerializeField]
        private GamepadButtonShortcutImageItem[] gamepadButtonShortcutImageItems = Array.Empty<GamepadButtonShortcutImageItem>();
        
        [SerializeField]
        private GamepadAxisShortcutImageItem[] gamepadAxisShortcutImageItems = Array.Empty<GamepadAxisShortcutImageItem>();

        #endregion

        #region Properties

        public UIShortcutInput ShortcutInput => shortcutInput;

        public bool UseShortcut => useShortcut;

        public ShortcutConstraintItem[] ConstraintItems => constraintItems;

        public KeyboardButtonShortcutImageItem[] KeyboardButtonShortcutImageItems => keyboardButtonShortcutImageItems;

        public GamepadButtonShortcutImageItem[] GamepadButtonShortcutImageItems => gamepadButtonShortcutImageItems;

        public KeyboardAxisShortcutImageItem[] KeyboardAxisShortcutImageItems => keyboardAxisShortcutImageItems;

        public GamepadAxisShortcutImageItem[] GamepadAxisShortcutImageItems => gamepadAxisShortcutImageItems;

        #endregion

        public UIShortcutInputSettings()
        {
            keyboardButtonShortcutImageItems = ArrayUtils.CreateIdentifierArray<KeyboardButtonShortcutImageItem, Key>();
            keyboardAxisShortcutImageItems = ArrayUtils.CreateIdentifierArray<KeyboardAxisShortcutImageItem, KeyAxis>();
            gamepadButtonShortcutImageItems = ArrayUtils.CreateIdentifierArray<GamepadButtonShortcutImageItem, GamepadButton>();
            gamepadAxisShortcutImageItems = ArrayUtils.CreateIdentifierArray<GamepadAxisShortcutImageItem, GamepadAxis>();
        }

        #region Builtin Methods

        private void Awake()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            var removed = constraintItems
                .Where(x => EnvironmentDetectionSettings.Singleton.Items.All(y => y.Guid != x.EnvironmentGuid))
                .Select(x => x.EnvironmentGuid)
                .ToArray();
            var added = EnvironmentDetectionSettings.Singleton.Items
                .Where(x => constraintItems.All(y => y.EnvironmentGuid != x.Guid))
                .Select(x => x.Guid)
                .ToArray();

            constraintItems = constraintItems.Where(x => !removed.Contains(x.EnvironmentGuid)).ToArray();
            constraintItems = constraintItems.Concat(added.Select(x => new ShortcutConstraintItem {EnvironmentGuid = x}).ToArray()).ToArray();
        }

        #endregion

        public ShortcutConstraintItem CurrentItem => constraintItems
            .Where(x => x.UseShortcut)
            .FirstOrDefault(x => x.EnvironmentGuid == RuntimeEnvironment.GetDetectedEnvironment()?.ToString());

        public UIShortcutInput? CurrentInput => CurrentItem?.ShortcutInput ?? (useShortcut ? shortcutInput : null);
    }

    [Serializable]
    public abstract class ShortcutImageItem<T> : IIdentifiedObject<T> where T : Enum
    {
        #region Inspector Data

        [SerializeField]
        private T identifier;

        [SerializeField]
        private Sprite icon;

        #endregion

        #region Properties

        public T Identifier => identifier;

        public Sprite Icon => icon;

        #endregion

        protected ShortcutImageItem(T identifier)
        {
            this.identifier = identifier;
        }
    }

    [Serializable]
    public sealed class KeyboardButtonShortcutImageItem : ShortcutImageItem<Key>
    {
        public KeyboardButtonShortcutImageItem(Key identifier) : base(identifier)
        {
        }
    }
    
    [Serializable]
    public sealed class KeyboardAxisShortcutImageItem : ShortcutImageItem<KeyAxis>
    {
        public KeyboardAxisShortcutImageItem(KeyAxis identifier) : base(identifier)
        {
        }
    }

    [Serializable]
    public sealed class GamepadButtonShortcutImageItem : ShortcutImageItem<GamepadButton>
    {
        public GamepadButtonShortcutImageItem(GamepadButton identifier) : base(identifier)
        {
        }
    }
    
    [Serializable]
    public sealed class GamepadAxisShortcutImageItem : ShortcutImageItem<GamepadAxis>
    {
        public GamepadAxisShortcutImageItem(GamepadAxis identifier) : base(identifier)
        {
        }
    }

    [Serializable]
    public sealed class ShortcutConstraintItem
    {
        #region Inspector Data

        [SerializeField]
        private string environmentGuid;

        [SerializeField]
        private UIShortcutInput shortcutInput = UIShortcutInput.Keyboard;

        [SerializeField]
        private bool useShortcut;

        #endregion

        #region Properties

        public string EnvironmentGuid
        {
            get => environmentGuid;
#if UNITY_EDITOR
            set => environmentGuid = value;
#endif
        }

        public UIShortcutInput ShortcutInput => shortcutInput;

        public bool UseShortcut => useShortcut;

        #endregion
    }

    public enum UIShortcutInput
    {
        Keyboard,
        Gamepad
    }
}