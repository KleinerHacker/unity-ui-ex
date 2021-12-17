using System;
using UnityEditorEx.Runtime.editor_ex.Scripts.Runtime.Extra;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    [Serializable]
    public sealed class UIInputPreset
    {
        #region Inspector Data

        [SerializeField]
        private string name;

        [ReadOnly]
        [SerializeField]
        private string guid;

        [Space]
        [SerializeField]
        private UIInputDevice[] requiredInputDevices;

        [SerializeField]
        private UIInputPresetConstraint[] constraints;

        #endregion

        #region Properties

        public string Name => name;

        public string Guid => guid;

        public UIInputDevice[] RequiredInputDevices => requiredInputDevices;

        public UIInputPresetConstraint[] Constraints => constraints;

        #endregion

#if UNITY_EDITOR
        internal void UpdateGuid()
        {
            guid = System.Guid.NewGuid().ToString();
        }
#endif
    }

    [Serializable]
    public sealed class UIInputPresetConstraint
    {
        #region Inspector Data

        [SerializeField]
        private RuntimePlatform supportedPlatform;

        [SerializeField]
        private bool requiresTV;

        #endregion

        #region Properties

        public RuntimePlatform SupportedPlatform => supportedPlatform;

        public bool RequiresTV => requiresTV;

        #endregion
    }

    public enum UIInputDevice
    {
        Keyboard,
        Mouse,
        Touch,
        Gamepad,
    }
}