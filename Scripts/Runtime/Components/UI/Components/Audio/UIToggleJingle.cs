using UnityAudio.Runtime.audio_system.Scripts.Runtime.Assets.Sfx;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Audio
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.AudioMenu + "/Toggle Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public sealed class UIToggleJingle : UIJingle<Toggle>
    {
        #region Inspector Data

        [Header("Resources")]
#if SFX_SYSTEM
        [SerializeField]
        private SfxClip jingleOn;

        [SerializeField]
        private SfxClip jingleOff;
#else
        [SerializeField]
        private AudioClip jingleOn;

        [SerializeField]
        private AudioClip jingleOff;
#endif

        #endregion

        #region Builtin Methods

        protected override void OnEnable()
        {
            _component.onValueChanged.AddListener(OnSwitch);
        }

        protected override void OnDisable()
        {
            _component.onValueChanged.RemoveListener(OnSwitch);
        }

        #endregion

        private void OnSwitch(bool active)
        {
            if (active)
            {
                PlayJingle(jingleOn);
            }
            else
            {
                PlayJingle(jingleOff);
            }
        }
    }
}