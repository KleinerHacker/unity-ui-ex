using System;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Types;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#if URP
#elif HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window.Misc
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.Ui.Window.MiscMenu + "/Blurred Background")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UiWindow))]
    public sealed class UiBlurredBackground : UIBehaviour
    {
        #region Inspector Data

        [SerializeField]
        private Volume volume;

        [SerializeField]
        private DepthOfFieldMode mode =
#if URP
            DepthOfFieldMode.Gaussian;
#elif HDRP
            DepthOfFieldMode.Manual;
#endif

        [Header("Animation")]
        [SerializeField]
        private AnimationCurve blurredCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [SerializeField]
        private float blurredSpeed = 1f;

        #endregion

        private UiWindow _window;
        private DepthOfField _depthOfField;

        private float _dofOriginalValue;

        #region Builtin Methods

        protected override void Awake()
        {
            _window = GetComponent<UiWindow>();
            if (!volume.profile.TryGet(out _depthOfField))
                throw new InvalidOperationException("Depth of field not found in volume!");

            switch (mode)
            {
                case DepthOfFieldMode.Off:
                    break;
#if URP
                case DepthOfFieldMode.Gaussian:
                    _dofOriginalValue = _depthOfField.gaussianStart.value;
                    break;
                case DepthOfFieldMode.Bokeh:
                    _dofOriginalValue = _depthOfField.focusDistance.value;
                    break;
#elif HDRP
                case DepthOfFieldMode.Manual:
                    _dofOriginalValue = _depthOfField.focusDistance.value;
                    break;
                case DepthOfFieldMode.UsePhysicalCamera:
                    break;
#endif
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnEnable()
        {
            _window.Showing += WindowOnShown;
            _window.Hiding += WindowOnHidden;
        }

        protected override void OnDisable()
        {
            _window.Shown += WindowOnShown;
            _window.Hidden += WindowOnHidden;
        }

        #endregion

        private void WindowOnShown(object sender, EventArgs e)
        {
            switch (mode)
            {
                case DepthOfFieldMode.Off:
                    break;
#if URP
                case DepthOfFieldMode.Gaussian:
                    AnimationBuilder.Create(this, AnimationType.Unscaled)
                        .Animate(blurredCurve, blurredSpeed, v => _depthOfField.gaussianStart.value = Mathf.Lerp(_dofOriginalValue, 0f, v))
                        .Start();
                    break;
                case DepthOfFieldMode.Bokeh:
                    AnimationBuilder.Create(this, AnimationType.Unscaled)
                        .Animate(blurredCurve, blurredSpeed, v => _depthOfField.focusDistance.value = Mathf.Lerp(_dofOriginalValue, 0f, v))
                        .Start();
                    break;
#elif HDRP
                case DepthOfFieldMode.Manual:
                    AnimationBuilder.Create(this, AnimationType.Unscaled)
                        .Animate(blurredCurve, blurredSpeed, v => _depthOfField.focusDistance.value = Mathf.Lerp(_dofOriginalValue, 0f, v))
                        .Start();
                    break;
                case DepthOfFieldMode.UsePhysicalCamera:
                    break;
#endif
                default:
                    throw new NotImplementedException();
            }
        }

        private void WindowOnHidden(object sender, EventArgs e)
        {
            switch (mode)
            {
                case DepthOfFieldMode.Off:
                    break;
#if URP
                case DepthOfFieldMode.Gaussian:
                    AnimationBuilder.Create(this, AnimationType.Unscaled)
                        .Animate(blurredCurve, blurredSpeed, v => _depthOfField.gaussianStart.value = Mathf.Lerp(0, _dofOriginalValue, v))
                        .Start();
                    break;
                case DepthOfFieldMode.Bokeh:
                    AnimationBuilder.Create(this, AnimationType.Unscaled)
                        .Animate(blurredCurve, blurredSpeed, v => _depthOfField.focusDistance.value = Mathf.Lerp(0, _dofOriginalValue, v))
                        .Start();
                    break;
#elif HDRP
                case DepthOfFieldMode.Manual:
                    AnimationBuilder.Create(this, AnimationType.Unscaled)
                        .Animate(blurredCurve, blurredSpeed, v => _depthOfField.focusDistance.value = Mathf.Lerp(0, _dofOriginalValue, v))
                        .Start();
                    break;
                case DepthOfFieldMode.UsePhysicalCamera:
                    break;
#endif
                default:
                    throw new NotImplementedException();
            }
        }
    }
}