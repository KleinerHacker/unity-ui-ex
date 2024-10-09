using System;
using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Assets;
using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource))]
    public abstract class UIJingle : UIBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Inspector Data

        [Header("Mouse & Pointer Handling")]
        [SerializeField] protected UIJingleClip enterClip;
        [SerializeField] protected UIJingleClip exitClip;

        #endregion
        
        protected AudioGroupSource audioGroupSource;
        protected AudioSource audioSource;

        #region Builtin Methods

        protected override void Awake()
        {
            audioGroupSource = GetComponent<AudioGroupSource>();
            audioSource = GetComponent<AudioSource>();
        }

        #endregion

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayOneShot(enterClip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PlayOneShot(exitClip);
        }

        protected void PlayOneShot(UIJingleClip clip)
        {
            switch (clip.Type)
            {
                case UIJingleClipType.None:
                    break;
                case UIJingleClipType.Single:
                    audioSource.PlayOneShot(clip.SingleClip);
                    break;
                case UIJingleClipType.Group:
                    audioGroupSource.PlayOneShot(clip.GroupClip);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    [Serializable]
    public sealed class UIJingleClip
    {
        #region Inspector Data

        [SerializeField] private UIJingleClipType type;
        [SerializeField] private AudioClip singleClip;
        [SerializeField] private AudioClipGroup groupClip;

        #endregion

        #region Properties

        public UIJingleClipType Type => type;

        public AudioClip SingleClip => singleClip;

        public AudioClipGroup GroupClip => groupClip;

        #endregion
    }

    public enum UIJingleClipType : byte
    {
        None = 0x00,
        Single = 0x10,
        Group = 0x11,
    }
}