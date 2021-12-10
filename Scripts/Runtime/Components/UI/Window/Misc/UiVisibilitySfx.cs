using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.Ui.Window.MiscMenu + "/Visibility Sfx")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UiWindow))]
    public sealed class UiVisibilitySfx : UIBehaviour
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

        private UiWindow _window;
        private AudioSource _audioSource;

        #region Builtin Methods

        protected override void Awake()
        {
            _window = GetComponent<UiWindow>();
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.outputAudioMixerGroup = mixerGroup;
        }

        protected override void OnEnable()
        {
            _window.Showing += WindowOnShowing;
            _window.Hiding += WindowOnHiding;
        }

        protected override void OnDisable()
        {
            _window.Showing -= WindowOnShowing;
            _window.Hiding -= WindowOnHiding;
        }

        #endregion

        private void WindowOnShowing(object sender, EventArgs e)
        {
            if (showClip == null)
                return;
            
            _audioSource.PlayOneShot(showClip);
        }

        private void WindowOnHiding(object sender, EventArgs e)
        {
            if (hideClip == null)
                return;
            
            _audioSource.PlayOneShot(hideClip);
        }
    }
}