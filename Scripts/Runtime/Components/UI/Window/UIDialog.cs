using UnityAnimation.Runtime.animation.Scripts.Runtime.Types;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Dialog")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UIDialog : UIWindow
    {
        #region Inspector Data

        [Header("Raycast Blocker")]
        [SerializeField]
        private bool useBlocker;

        [SerializeField]
        private Sprite raycastBlockerSprite;

        [SerializeField]
        private Color raycastBlockerColor = Color.clear;

        [Header("Raycast Blocker Animation")]
        [SerializeField]
        private bool overrideBlockerAnimation;

        [SerializeField]
        private AnimationCurve blockerAnimationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        #endregion

        private CanvasGroup _raycastBlocker;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();

            if (useBlocker)
            {
                var go = new GameObject("Dialog Blocker");
                go.AddComponent<RectTransform>();
                //Set blocker direct beside the dialog
                go.transform.SetParent(transform.parent);
                //Set the blocker direct above (as background) of dialog
                go.transform.SetSiblingIndex(transform.GetSiblingIndex());
                ((RectTransform)go.transform).pivot = new Vector2(0.5f, 0.5f);
                ((RectTransform)go.transform).anchoredPosition = Vector2.zero;
                ((RectTransform)go.transform).sizeDelta = new Vector2(Screen.width, Screen.height);
                var image = go.AddComponent<Image>();
                image.sprite = raycastBlockerSprite;
                image.color = raycastBlockerColor;
                _raycastBlocker = go.AddComponent<CanvasGroup>();
                _raycastBlocker.Hide();
            }
        }

        #endregion

        protected override void OnShowing()
        {
            if (useBlocker)
            {
                _raycastBlocker.alpha = 0f;
                _raycastBlocker.blocksRaycasts = true;
                AnimationBuilder.Create(this, AnimationType.Unscaled)
                    .Animate(overrideBlockerAnimation ? blockerAnimationCurve : animationCurve, animationSpeed,
                        v => _raycastBlocker.alpha = Mathf.Lerp(0f, 1f, v))
                    .Start();
            }
        }

        protected override void OnHiding()
        {
            if (useBlocker)
            {
                _raycastBlocker.alpha = 1f;
                _raycastBlocker.blocksRaycasts = true;
                AnimationBuilder.Create(this, AnimationType.Scaled)
                    .Animate(overrideBlockerAnimation ? blockerAnimationCurve : animationCurve, animationSpeed,
                        v => _raycastBlocker.alpha = Mathf.Lerp(1f, 0f, v))
                    .WithFinisher(() => _raycastBlocker.blocksRaycasts = false)
                    .Start();
            }
        }
    }
}