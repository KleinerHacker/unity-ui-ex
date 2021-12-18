using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.Window.MiscMenu + "/Visibility Sfx")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIStage))]
    public sealed class UIVisibilitySfx : UIBehaviour
    {
        #region Inspector Data

        [SerializeField]
        private AudioClip showClip;

        [SerializeField]
        private AudioClip hideClip;

        [Space]
        [SerializeField]
        private AudioMixerGroup mixerGroup;

        #endregion

        private UIStage _stage;
        private AudioSource _audioSource;

        #region Builtin Methods

        protected override void Awake()
        {
            _stage = GetComponent<UIWindow>();
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = mixerGroup;
        }

        protected override void OnEnable()
        {
            _stage.Showing += StageOnShowing;
            _stage.Hiding += StageOnHiding;
        }

        protected override void OnDisable()
        {
            _stage.Showing -= StageOnShowing;
            _stage.Hiding -= StageOnHiding;
        }

        #endregion

        private void StageOnShowing(object sender, EventArgs e)
        {
            if (showClip == null)
                return;
            
            _audioSource.PlayOneShot(showClip);
        }

        private void StageOnHiding(object sender, EventArgs e)
        {
            if (hideClip == null)
                return;
            
            _audioSource.PlayOneShot(hideClip);
        }
    }
}