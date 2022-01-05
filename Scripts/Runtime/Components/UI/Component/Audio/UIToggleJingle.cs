using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Audio
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.AudioMenu + "/Toggle Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public sealed class UIToggleJingle : UIJingle<Toggle>
    {
        #region Inspector Data

        [Header("Resources")]
        [SerializeField]
        private AudioClip jingleOn;

        [SerializeField]
        private AudioClip jingleOff;

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