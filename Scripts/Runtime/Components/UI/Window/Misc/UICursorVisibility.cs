#if PCSOFT_CURSOR
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityExtension.Runtime.extension.Scripts.Runtime;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Window.MiscMenu + "/Cursor Visibility")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIStage))]
    public sealed class UICursorVisibility : UIBehaviour
    {
        private UIStage _stage;

        #region Builtin Methods

        protected override void Awake()
        {
            _stage = GetComponent<UIWindow>();
        }

        protected override void OnEnable()
        {
            _stage.Showing += StageOnShowing;
            _stage.Hiding += StageOnHiding;
        }

        protected override void OnDisable()
        {
            _stage.Showing -= StageOnShowing;
            _stage.Hiding -= StageOnHiding;
        }

        #endregion

        private void StageOnShowing(object sender, EventArgs e)
        {
            CursorSystem.IsVisible = true;
        }

        private void StageOnHiding(object sender, EventArgs e)
        {
            CursorSystem.IsVisible = false;
        }
    }
}
#endif