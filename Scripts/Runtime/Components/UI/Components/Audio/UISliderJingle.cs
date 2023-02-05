using UnityAudio.Runtime.audio_system.Scripts.Runtime.Assets.Sfx;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Audio
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.AudioMenu + "/Slider Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public sealed class UISliderJingle : UIJingle<Slider>
    {
        #region Inspector Data

        [Header("Resources")]
#if SFX_SYSTEM
        [SerializeField]
        private SfxClip jingleUp;

        [SerializeField]
        private SfxClip jingleDown;
#else
        [SerializeField]
        private AudioClip jingleUp;

        [SerializeField]
        private AudioClip jingleDown;
#endif

        #endregion

        private float _currentValue;

        #region Builtin Methods

        protected override void OnEnable()
        {
            _currentValue = _component.value;
            _component.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            _component.onValueChanged.RemoveListener(OnValueChanged);
        }

        #endregion

        private void OnValueChanged(float v)
        {
            PlayJingle(_currentValue < v ? jingleDown : jingleUp);
            _currentValue = v;
        }
    }
}