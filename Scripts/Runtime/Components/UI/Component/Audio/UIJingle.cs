using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

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

        #endregion

        protected T _component;
        private AudioSource _audioSource; //TODO: Usage SFX System

        #region Builtin Methods

        protected override void Awake()
        {
            _component = GetComponent<T>();
            
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        #endregion

        protected void PlayJingle(AudioClip jingle)
        {
            if (jingle == null)
                return;
            
            var delay = Random.Range(minDelay, maxDelay);
            AnimationBuilder.Create(this)
                .Wait(delay, () => _audioSource.PlayOneShot(jingle))
                .Start();
        }
    }
}