using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    [AddComponentMenu(UnityUIExConstants.Root + "/UI Toggle Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(Toggle))]
    public sealed class UIToggleJingle : UIJingle
    {
        #region Inspector Data

        [Header("Component State")] [SerializeField]
        private UIJingleClip onClip;

        [SerializeField] private UIJingleClip offClip;

        #endregion

        private Toggle toggle;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            toggle = GetComponent<Toggle>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool x) => PlayOneShot(x ? onClip : offClip);

        #endregion
    }
}