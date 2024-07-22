using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityExtension.Runtime.Projects.unity_extension.Scripts.Runtime.Utils.Extensions;
using UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Utils.Extensions;
#if PCSOFT_SHORTCUT && PCSOFT_ENV
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input;
#endif

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Window
{
    public abstract partial class UIStage : UIBehaviour, IViewable
    {
        #region Inspector Data

        [FormerlySerializedAs("fadingCurve")]
        [Header("Animation")]
        [SerializeField]
        protected AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [FormerlySerializedAs("fadingSpeed")]
        [SerializeField]
        protected float animationSpeed = 1f;

        [SerializeField]
        protected UIStageAnimationType animationType = UIStageAnimationType.Fading;

        [FormerlySerializedAs("revertAnimationCurveOnHide")]
        [SerializeField]
        protected bool revertAnimationOnHide;

        [Header("Behavior")]
        [SerializeField]
        protected ViewableState initialState = ViewableState.Hidden;

        #endregion

        #region Properties

        public ViewableState State => _canvasGroup.IsShown() ? ViewableState.Shown : ViewableState.Hidden;

        #endregion

        #region Events

        public event EventHandler Showing;
        public event EventHandler Shown;
        public event EventHandler Hiding;
        public event EventHandler Hidden;

        #endregion

        private CanvasGroup _canvasGroup;
#if PCSOFT_SHORTCUT && PCSOFT_ENV
        private UIInput[] _inputs;
#endif

        private Rect _originalRect;

        #region Builtin Methods

        protected override void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
#if PCSOFT_SHORTCUT && PCSOFT_ENV
            _inputs = GetComponentsInChildren<UIInput>();
#endif

            _originalRect = ((RectTransform)transform).CalculateAbsoluteRect(GetComponentInParent<Canvas>());

            switch (initialState)
            {
                case ViewableState.Hidden:
                    DisableInputs();
                    break;
                case ViewableState.Shown:
                    EnableInputs();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (EditorApplication.isPlaying)
                return;

            var canvasGroup = GetComponent<CanvasGroup>();

            switch (initialState)
            {
                case ViewableState.Hidden:
                    canvasGroup.Hide();
                    break;
                case ViewableState.Shown:
                    canvasGroup.Show();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
#endif

        #endregion

        public void Show()
        {
            Show(null);
        }

        public void Show(Action onFinished)
        {
            if (State == ViewableState.Shown)
            {
                Debug.LogWarning("Dialog already shown", this);
                return;
            }

            Debug.Log("Show dialog", this);

            StopAllCoroutines();
            OnShowing();
            Showing?.Invoke(this, EventArgs.Empty);

            switch (animationType)
            {
                case UIStageAnimationType.Fading:
                    ShowFading(onFinished);
                    break;
                case UIStageAnimationType.SlidingLeft:
                    ShowSlidingLeft(onFinished);
                    break;
                case UIStageAnimationType.SlidingRight:
                    ShowSlidingRight(onFinished);
                    break;
                case UIStageAnimationType.SlidingTop:
                    ShowSlidingTop(onFinished);
                    break;
                case UIStageAnimationType.SlidingBottom:
                    ShowSlidingBottom(onFinished);
                    break;
                case UIStageAnimationType.Scaling:
                    ShowScaling(onFinished);
                    break;
                default:
                    throw new NotImplementedException(animationType.ToString());
            }
        }

        public void Hide()
        {
            Hide(null);
        }

        public void Hide(Action onFinished)
        {
            if (State == ViewableState.Hidden)
            {
                Debug.LogWarning("Dialog already hidden", this);
                return;
            }

            Debug.Log("Hide dialog", this);

            StopAllCoroutines();
            OnHiding();
            Hiding?.Invoke(this, EventArgs.Empty);

            switch (animationType)
            {
                case UIStageAnimationType.Fading:
                    HideFading(onFinished);
                    break;
                case UIStageAnimationType.SlidingLeft:
                    HideSlidingLeft(onFinished);
                    break;
                case UIStageAnimationType.SlidingRight:
                    HideSlidingRight(onFinished);
                    break;
                case UIStageAnimationType.SlidingTop:
                    HideSlidingTop(onFinished);
                    break;
                case UIStageAnimationType.SlidingBottom:
                    HideSlidingBottom(onFinished);
                    break;
                case UIStageAnimationType.Scaling:
                    HideScaling(onFinished);
                    break;
                default:
                    throw new NotImplementedException(animationType.ToString());
            }
        }

        private void EnableInputs()
        {
#if PCSOFT_SHORTCUT && PCSOFT_ENV
            foreach (var input in _inputs)
            {
                input.enabled = true;
            }
#endif
        }

        private void DisableInputs()
        {
#if PCSOFT_SHORTCUT && PCSOFT_ENV
            foreach (var input in _inputs)
            {
                input.enabled = false;
            }
#endif
        }

        protected virtual void OnShowing()
        {
        }

        protected virtual void OnShown()
        {
        }

        protected virtual void OnHiding()
        {
        }

        protected virtual void OnHidden()
        {
        }
    }

    public enum UIStageAnimationType
    {
        Fading,
        SlidingLeft,
        SlidingRight,
        SlidingTop,
        SlidingBottom,
        Scaling,
    }
}