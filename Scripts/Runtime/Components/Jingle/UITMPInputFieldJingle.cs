using System;
using TMPro;
using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Components;
using UnityEngine;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.Jingle
{
    [AddComponentMenu(UnityUIExConstants.Root + "/UI TMP Input Field Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioGroupSource), typeof(AudioSource), typeof(TMP_InputField))]
    public sealed class UITMPInputFieldJingle : UIInputFieldJingleBase<TMP_InputField>
    {
        #region Inspector Data

        [Space]
        [SerializeField] private UIJingleClip focusClip;
        [SerializeField] private UIJingleClip unfocusClip;
        [Space]
        [SerializeField] private UIJingleClip beginSelectionClip;
        [SerializeField] private UIJingleClip selectCharacterClip;
        [SerializeField] private UIJingleClip unselectCharacterClip;
        [SerializeField] private UIJingleClip endSelectionClip;

        #endregion

        private int selectionState = -1;
        
        #region Builtin Methods

        protected override void OnEnable()
        {
            base.OnEnable();
            inputField.onValueChanged.AddListener(OnValueChanged);
            inputField.onSubmit.AddListener(OnSubmit);
            inputField.onEndEdit.AddListener(OnEndEdit);
            inputField.onSelect.AddListener(OnSelect);
            inputField.onDeselect.AddListener(OnDeselect);
            inputField.onTextSelection.AddListener(OnTextSelection);
            inputField.onEndTextSelection.AddListener(OnEndTextSelection);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            inputField.onValueChanged.RemoveListener(OnValueChanged);
            inputField.onSubmit.RemoveListener(OnSubmit);
            inputField.onEndEdit.RemoveListener(OnEndEdit);
            inputField.onSelect.RemoveListener(OnSelect);
            inputField.onDeselect.RemoveListener(OnDeselect);
            inputField.onTextSelection.RemoveListener(OnTextSelection);
            inputField.onEndTextSelection.RemoveListener(OnEndTextSelection);
        }

        #endregion

        private void OnEndTextSelection(string _, int arg1, int arg2)
        {
            PlayOneShot(endSelectionClip);
            selectionState = -1;
        }

        private void OnTextSelection(string _, int start, int end)
        {
            var newLength = Math.Abs(end - start);
            
            if (selectionState < 0)
            {
                PlayOneShot(beginSelectionClip);
                selectionState = newLength;
                
                return;
            }
            
            if (selectionState == newLength)
                return;
            if (selectionState < newLength)
            {
                PlayOneShot(selectCharacterClip);
            }
            else
            {
                PlayOneShot(unselectCharacterClip);
            }

            selectionState = newLength;
        }

        private void OnDeselect(string _) => PlayOneShot(unfocusClip);

        private void OnSelect(string _) => PlayOneShot(focusClip);

        private void OnEndEdit(string _) => PlayOneShot(endEditClip); 

        private void OnSubmit(string _) => PlayOneShot(submitClip);

        private void OnValueChanged(string _) => PlayOneShot(typingClip);
    }
}