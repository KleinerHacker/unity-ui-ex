using System;
using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Window")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindow : UIStage
    {
        #region Inspector Data

        [SerializeField]
        private DialogEscapeAction escapeAction = DialogEscapeAction.None;

        #endregion

        #region Builtin Methods

        protected override void OnEnable()
        {
            base.OnEnable();
#if ENABLE_INPUT_SYSTEM
            EventSystem.current.GetComponent<InputSystemUIInputModule>().cancel.action.performed += CancelPerformed;
#endif
        }

        protected override void OnDisable()
        {
            base.OnDisable();
#if ENABLE_INPUT_SYSTEM           
            EventSystem.current.GetComponent<InputSystemUIInputModule>().cancel.action.performed -= CancelPerformed;
#endif
        }

        #endregion

        private void HandleEscape()
        {
            switch (escapeAction)
            {
                case DialogEscapeAction.None:
                    break;
                case DialogEscapeAction.Toggle:
                    Debug.Log("Toggle dialog (escape)", this);
                    if (State == ViewableState.Shown)
                        Hide();
                    else
                        Show();
                    break;
                case DialogEscapeAction.HideOnly:
                    if (State == ViewableState.Shown)
                    {
                        Debug.Log("Hide dialog (escape)", this);
                        Hide();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("escapeAction", escapeAction, null);
            }
        }

#if ENABLE_INPUT_SYSTEM
        private void CancelPerformed(InputAction.CallbackContext obj) => HandleEscape();
#endif
    }

    public enum DialogEscapeAction
    {
        Toggle,
        HideOnly,
        None
    }
}