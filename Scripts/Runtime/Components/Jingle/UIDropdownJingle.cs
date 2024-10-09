using TMPro;
using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    public abstract class UIDropdownJingleBase<T> : UIJingle where T : UIBehaviour
    {
        #region Inspector Data

        [Header("Component State")] [SerializeField]
        protected UIJingleClip valueClip;

        #endregion

        protected T dropdown;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            dropdown = GetComponent<T>();
        }

        #endregion
    }

    

    [AddComponentMenu(UnityUIExConstants.Root + "/UI Dropdown Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(Dropdown))]
    public sealed class UIDropdownJingle : UIDropdownJingleBase<Dropdown>
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