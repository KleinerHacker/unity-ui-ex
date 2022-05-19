using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityAudio.Runtime.audio_system.Scripts.Runtime;
using UnityAudio.Runtime.audio_system.Scripts.Runtime.Assets;
using UnityAudio.Runtime.audio_system.Scripts.Runtime.Assets.Sfx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Audio
{
    public abstract class UIJingle<T> : UIBehaviour where T : UnityEngine.Component
    {
        #region Inspector Data

        [Header("Behavior")]
        [SerializeField]
        private float minDelay;
        
        [SerializeField]
        private float maxDelay;

        [Space]
        [SerializeField]
        [Tooltip("Leave field empty to use default SFX system")]
        private string sfxSystemName;

        #endregion

        protected T _component;
        private SfxSystemInstance _sfxSystem;

        #region Builtin Methods

        protected override void Awake()
        {
            _component = GetComponent<T>();

            var sfxName = sfxSystemName ?? UIAudioSettings.Singleton.SfxSystemName;
            _sfxSystem = string.IsNullOrEmpty(sfxName) ? SfxSystem.Default : SfxSystem.Get(sfxName);
        }

        #endregion

        protected void PlayJingle(SfxClip jingle)
        {
            if (jingle == null)
                return;
            
            var delay = Random.Range(minDelay, maxDelay);
            AnimationBuilder.Create(this)
                .Wait(delay, () => _sfxSystem.PlayOneShot(jingle))
                .Start();
        }
        
        protected void PlayJingle(AudioClip jingle)
        {
            if (jingle == null)
                return;
            
            var delay = Random.Range(minDelay, maxDelay);
            AnimationBuilder.Create(this)
                .Wait(delay, () => _sfxSystem.PlayOneShot(jingle))
                .Start();
        }
    }
}