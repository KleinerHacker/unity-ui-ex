using System;
using UnityAnimation.Runtime.Projects.unity_animation.Scripts.Runtime.Types;
using UnityAnimation.Runtime.Projects.unity_animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Utils.Extensions;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Dialog")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UIDialog : UIWindow
    {
        private static readonly int Radius = Shader.PropertyToID("_Radius");

        #region Inspector Data

        [Header("Raycast Blocker")]
        [SerializeField]
        private DialogRaycastBlockerType blockerType = DialogRaycastBlockerType.None;

        [Space]
        [SerializeField]
        private Color blockerColor = Color.clear;

        [SerializeField]
        private Sprite blockerSprite;

        [SerializeField]
        private Material blockerMaterial;

        [Space]
        [SerializeField]
        [Range(0f, 0.01f)]
        private float blurRadius = 0.001f;

        [FormerlySerializedAs("overrideBlockerAnimation")]
        [Header("Raycast Blocker Animation")]
        [SerializeField]
        private bool divergentBlockerAnimation;

        [SerializeField]
        private AnimationCurve blockerAnimationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

#if ENABLE_INPUT_SYSTEM
        [Header("Input Action Preventer")]
        [SerializeField]
        private InputActionAsset inputActionToPrevent;
#endif

        #endregion

        private CanvasGroup _raycastBlocker;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();

            if (blockerType != DialogRaycastBlockerType.None)
            {
                var go = new GameObject("Dialog Blocker");
                go.AddComponent<RectTransform>();
                //Set blocker directly beside the dialog
                go.transform.SetParent(transform.parent);
                //Set the blocker directly above (as a background) of the dialog
                go.transform.SetSiblingIndex(transform.GetSiblingIndex());
                ((RectTransform)go.transform).pivot = new Vector2(0.5f, 0.5f);
                ((RectTransform)go.transform).anchoredPosition = Vector2.zero;
                ((RectTransform)go.transform).sizeDelta = new Vector2(Screen.width, Screen.height);
                var image = go.AddComponent<Image>();
                switch (blockerType)
                {
                    case DialogRaycastBlockerType.Image:
                        image.color = blockerColor;
                        image.sprite = blockerSprite;
                        image.material = blockerMaterial;
                        break;
                    case DialogRaycastBlockerType.Blurred:
                        image.color = blockerColor;
                        image.material = new Material(Resources.Load<Material>("Blur"));
                        image.material.SetFloat(Radius, blurRadius);
                        break;
                    case DialogRaycastBlockerType.None:
                        throw new InvalidOperationException("Cannot happen: " + blockerType);
                    default:
                        throw new ArgumentOutOfRangeException("blockerType", blockerType, null);
                }

                _raycastBlocker = go.AddComponent<CanvasGroup>();
                _raycastBlocker.Hide();
            }
        }

        #endregion

        protected override void OnShowing()
        {
            if (blockerType != DialogRaycastBlockerType.None)
            {
                _raycastBlocker.alpha = 0f;
                _raycastBlocker.blocksRaycasts = true;
                AnimationBuilder.Create(this, AnimationType.Unscaled)
                    .Animate(divergentBlockerAnimation ? blockerAnimationCurve : animationCurve, animationSpeed,
                        v => _raycastBlocker.alpha = Mathf.Lerp(0f, 1f, v))
                    .Start();
            }

#if ENABLE_INPUT_SYSTEM
            inputActionToPrevent?.Disable();
#endif
        }

        protected override void OnHiding()
        {
            if (blockerType != DialogRaycastBlockerType.None)
            {
                _raycastBlocker.alpha = 1f;
                _raycastBlocker.blocksRaycasts = true;
                AnimationBuilder.Create(this, AnimationType.Scaled)
                    .Animate(divergentBlockerAnimation ? blockerAnimationCurve : animationCurve, animationSpeed,
                        v => _raycastBlocker.alpha = Mathf.Lerp(1f, 0f, v))
                    .WithFinisher(() =>
                    {
                        _raycastBlocker.blocksRaycasts = false;
#if ENABLE_INPUT_SYSTEM
                        inputActionToPrevent?.Enable();
#endif
                    })
                    .Start();
            }
            else
            {
#if ENABLE_INPUT_SYSTEM
                inputActionToPrevent?.Enable();
#endif
            }
        }
    }

    public enum DialogRaycastBlockerType : byte
    {
        None = 0x00,
        Image = 0x10,
        Blurred = 0x20,
    }
}