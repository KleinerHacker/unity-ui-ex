using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    [AddComponentMenu(UnityUIExConstants.Root + "/UI Scroll Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(Scrollbar))]
    public sealed class UIScrollJingle : UIJingle
    {
        #region Inspector Data

        [Header("Component State")] [SerializeField]
        private UIJingleClip scrollClip;

        #endregion

        private Scrollbar scrollbar;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            scrollbar = GetComponent<Scrollbar>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            scrollbar.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            scrollbar.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float x) => PlayOneShot(scrollClip);

        #endregion
    }
}