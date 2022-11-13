using System;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Types;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    public abstract partial class UIStage
    {
        private void ShowFading(Action onFinished)
        {
            _canvasGroup.Hide();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => _canvasGroup.alpha = v)
                .WithFinisher(() =>
                {
                    _canvasGroup.Show();
                    EnableInputs();
                    
                    OnShown();
                    Shown?.Invoke(this, EventArgs.Empty);
                    onFinished?.Invoke();
                })
                .Start();
        }

        private void ShowSlidingLeft(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = rect.x - _originalRect.width;
            var endPosition = _originalRect.x;

            var t = transform;
            t.position = new Vector3(startPosition, t.position.y);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(Mathf.Lerp(startPosition, endPosition, v), t.position.y))
                .WithFinisher(onFinished)
                .Start();
        }
        
        private void ShowSlidingRight(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = rect.width + _originalRect.width;
            var endPosition = _originalRect.x;

            var t = transform;
            t.position = new Vector3(startPosition, t.position.y);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(Mathf.Lerp(startPosition, endPosition, v), t.position.y))
                .WithFinisher(onFinished)
                .Start();
        }
        
        private void ShowSlidingTop(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = rect.height + _originalRect.height;
            var endPosition = _originalRect.y;

            var t = transform;
            t.position = new Vector3(t.position.x, startPosition);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(t.position.x, Mathf.Lerp(startPosition, endPosition, v)))
                .WithFinisher(onFinished)
                .Start();
        }
        
        private void ShowSlidingBottom(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = rect.y - _originalRect.height;
            var endPosition = _originalRect.y;

            var t = transform;
            t.position = new Vector3(t.position.x, startPosition);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(t.position.x, Mathf.Lerp(startPosition, endPosition, v)))
                .WithFinisher(onFinished)
                .Start();
        }

        private void HideFading(Action onFinished)
        {
            _canvasGroup.Hide();
            _canvasGroup.alpha = 1f;
            DisableInputs();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => _canvasGroup.alpha = 1f - v)
                .WithFinisher(() =>
                {
                    OnHidden();
                    Hidden?.Invoke(this, EventArgs.Empty);
                    onFinished?.Invoke();
                })
                .Start();
        }
        
        private void HideSlidingLeft(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = _originalRect.x;
            var endPosition = rect.x - _originalRect.width;

            var t = transform;
            t.position = new Vector3(startPosition, t.position.y);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(Mathf.Lerp(startPosition, endPosition, v), t.position.y))
                .WithFinisher(() =>
                {
                    _canvasGroup.Hide();
                    onFinished?.Invoke();
                })
                .Start();
        }
        
        private void HideSlidingRight(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = _originalRect.x;
            var endPosition = rect.width + _originalRect.width;

            var t = transform;
            t.position = new Vector3(startPosition, t.position.y);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(Mathf.Lerp(startPosition, endPosition, v), t.position.y))
                .WithFinisher(() =>
                {
                    _canvasGroup.Hide();
                    onFinished?.Invoke();
                })
                .Start();
        }
        
        private void HideSlidingTop(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = _originalRect.y;
            var endPosition = rect.height + _originalRect.height;

            var t = transform;
            t.position = new Vector3(t.position.x, startPosition);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(t.position.x, Mathf.Lerp(startPosition, endPosition, v)))
                .WithFinisher(() =>
                {
                    _canvasGroup.Hide();
                    onFinished?.Invoke();
                })
                .Start();
        }
        
        private void HideSlidingBottom(Action onFinished)
        {
            var canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                throw new InvalidOperationException("Cannot find parent canvas");

            var rect = canvas.pixelRect;

            var startPosition = _originalRect.y;
            var endPosition = rect.y - _originalRect.height;

            var t = transform;
            t.position = new Vector3(t.position.x, startPosition);
            _canvasGroup.Show();
            AnimationBuilder.Create(this, AnimationType.Unscaled)
                .Animate(animationCurve, animationSpeed, v => t.position = new Vector3(t.position.x, Mathf.Lerp(startPosition, endPosition, v)))
                .WithFinisher(() =>
                {
                    _canvasGroup.Hide();
                    onFinished?.Invoke();
                })
                .Start();
        }
    }
}