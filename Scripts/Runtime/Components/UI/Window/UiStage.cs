using System;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Types;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    public abstract class UiStage : UIBehaviour, IViewable
    {
        #region Inspector Data

        [Header("Animation")]
        [SerializeField]
        protected AnimationCurve fadingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [SerializeField]
        protected float fadingSpeed = 1f;
        
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

        #region Builtin Methods

        protected override void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
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

            _canvasGroup.Hide();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(fadingCurve, fadingSpeed, v => _canvasGroup.alpha = v)
                .WithFinisher(() =>
                {
                    _canvasGroup.Show();
                    OnShown();
                    Shown?.Invoke(this, EventArgs.Empty);
                    onFinished?.Invoke();
                })
                .Start();
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

            _canvasGroup.Hide();
            _canvasGroup.alpha = 1f;
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(fadingCurve, fadingSpeed, v => _canvasGroup.alpha = 1f - v)
                .WithFinisher(() =>
                {
                    OnHidden();
                    Hidden?.Invoke(this, EventArgs.Empty);
                    onFinished?.Invoke();
                })
                .Start();
        }
        
        protected virtual void OnShowing() {}
        protected virtual void OnShown() {}
        
        protected virtual void OnHiding() {}
        protected virtual void OnHidden() {}
    }
}