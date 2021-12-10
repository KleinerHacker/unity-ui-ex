using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityExtension.Runtime.extension.Scripts.Runtime.Utils;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.Ui.Window.MiscMenu + "/Game Time Stopper")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UiWindow))]
    public sealed class UiGameTimeStopper : UIBehaviour
    {
        #region Inspector Data

        [Header("Animation")]
        [SerializeField]
        private AnimationCurve stopAnimation = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [SerializeField]
        private float stopSpeed = 1f;

        #endregion

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
            GameTimeController.Pause(this, stopAnimation, stopSpeed);
        }

        private void WindowOnHiding(object sender, EventArgs e)
        {
            GameTimeController.Resume(this, stopAnimation, stopSpeed);
        }
    }
}