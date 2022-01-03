using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Audio
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.AudioMenu + "/Button Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public sealed class UIButtonJingle : UIJingle<Button>
    {
        #region Inspector Data

        [Header("Resources")]
        [SerializeField]
        private AudioClip jingle;

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