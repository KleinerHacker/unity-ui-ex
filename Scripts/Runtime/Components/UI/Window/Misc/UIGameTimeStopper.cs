using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityExtension.Runtime.extension.Scripts.Runtime.Utils;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Window.MiscMenu + "/Game Time Stopper")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIStage))]
    public sealed class UIGameTimeStopper : UIBehaviour
    {
        #region Inspector Data

        [Header("Animation")]
        [SerializeField]
        private AnimationCurve stopAnimation = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [SerializeField]
        private float stopSpeed = 1f;

        #endregion

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
            GameTimeController.Pause(this, stopAnimation, stopSpeed);
        }

        private void StageOnHiding(object sender, EventArgs e)
        {
            GameTimeController.Resume(this, stopAnimation, stopSpeed);
        }
    }
}