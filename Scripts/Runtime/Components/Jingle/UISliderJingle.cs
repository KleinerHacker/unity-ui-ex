using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    [AddComponentMenu(UnityUIExConstants.Root + "/UI Slider Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(Slider))]
    public sealed class UISliderJingle : UIJingle
    {
        #region Inspector Data

        [Header("Component State")] [SerializeField]
        private UIJingleClip valueClip;

        #endregion

        private Slider slider;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            slider = GetComponent<Slider>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float x) => PlayOneShot(valueClip);

        #endregion
    }
}