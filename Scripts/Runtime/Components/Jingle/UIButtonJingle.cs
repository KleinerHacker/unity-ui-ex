using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    [AddComponentMenu(UnityUIExConstants.Root + "/UI Button Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(Button))]
    public sealed class UIButtonJingle : UIJingle
    {
        #region Inspector Data

        [SerializeField] private UIJingleClip pressClip;

        #endregion
        
        private Button button;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            button = GetComponent<Button>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            button.onClick.AddListener(OnPress);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            button.onClick.RemoveListener(OnPress);
        }

        private void OnPress()
        {
            PlayOneShot(pressClip);
        }

        #endregion
    }
}