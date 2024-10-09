using TMPro;
using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    [AddComponentMenu(UnityUIExConstants.Root + "/UI TMP Dropdown Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(TMP_Dropdown))]
    public sealed class UITMPDropdownJingle : UIDropdownJingleBase<TMP_Dropdown>
    {
        #region Builtin Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            dropdown.onValueChanged.RemoveListener(OnValueChanged);
        }

        #endregion

        private void OnValueChanged(int x) => PlayOneShot(valueClip);
    }
}