using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityExtension.Runtime.extension.Scripts.Runtime.Components;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.Ui.Window.MiscMenu + "/Cursor Visibility")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UiWindow))]
    public sealed class UiCursorVisibility : UIBehaviour
    {
        private UiWindow _window;

        #region Builtin Methods

        protected override void Awake()
        {
            _window = GetComponent<UiWindow>();
        }

        protected override void OnEnable()
        {
            _window.Showing += WindowOnShowing;
            _window.Hiding += WindowOnHiding;
        }

        protected override void OnDisable()
        {
            _window.Showing -= WindowOnShowing;
            _window.Hiding -= WindowOnHiding;
        }

        #endregion

        private void WindowOnShowing(object sender, EventArgs e)
        {
            CursorSystem.Singleton.IsCursorVisible = true;
        }

        private void WindowOnHiding(object sender, EventArgs e)
        {
            CursorSystem.Singleton.IsCursorVisible = false;
        }
    }
}