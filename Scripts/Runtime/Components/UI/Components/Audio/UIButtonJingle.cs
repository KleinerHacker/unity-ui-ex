using UnityAudio.Runtime.audio_system.Scripts.Runtime.Assets.Sfx;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Audio
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.AudioMenu + "/Button Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButtonJingle : UIJingle<Button>
    {
        #region Inspector Data

        [Header("Resources")]
#if SFX_SYSTEM
        [SerializeField]
        private SfxClip jingle;
#else
        [SerializeField]
        private AudioClip jingle;
#endif

        #endregion

        #region Builin Methods

        protected override void OnEnable()
        {
            _component.onClick.AddListener(OnClick);
        }

        protected override void OnDisable()
        {
            _component.onClick.RemoveListener(OnClick);
        }

        #endregion

        private void OnClick()
        {
            PlayJingle(jingle);
        }
    }
}