using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    public abstract class UIInputFieldJingleBase<T> : UIJingle where T : UIBehaviour
    {
        #region Inspector Data

        [Header("Component State")] [SerializeField]
        protected UIJingleClip typingClip;

        [SerializeField] protected UIJingleClip submitClip;
        [SerializeField] protected UIJingleClip endEditClip;

        #endregion

        protected T inputField;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            inputField = GetComponent<T>();
        }

        #endregion
    }
    
    [AddComponentMenu(UnityUIExConstants.Root + "/UI Input Field Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(InputField))]   
    public sealed class UIInputFieldJingle : UIInputFieldJingleBase<InputField>
    {
        #region Builtin Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            inputField.onValueChanged.AddListener(OnValueChanged);
            inputField.onSubmit.AddListener(OnSubmit);
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            inputField.onValueChanged.RemoveListener(OnValueChanged);
            inputField.onSubmit.RemoveListener(OnSubmit);
            inputField.onEndEdit.RemoveListener(OnEndEdit);
        }

        #endregion

        private void OnEndEdit(string _) => PlayOneShot(endEditClip); 

        private void OnSubmit(string _) => PlayOneShot(submitClip);

        private void OnValueChanged(string _) => PlayOneShot(typingClip);
    }
}