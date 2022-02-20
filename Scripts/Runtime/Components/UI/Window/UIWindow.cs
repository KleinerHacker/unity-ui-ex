using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityExtension.Runtime.extension.Scripts.Runtime.Utils;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
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

        #region Properties

        public DialogEscapeAction EscapeAction => escapeAction;

        #endregion

        #region Builtin Methods

        #endregion

        /// <summary>
        /// Call this to handle toggle action based on given <see cref="EscapeAction"/>
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ToggleVisibility()
        {
            switch (EscapeAction)
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
                    throw new NotImplementedException();
            }
        }
    }

    public enum DialogEscapeAction
    {
        Toggle,
        HideOnly,
        None
    }
}