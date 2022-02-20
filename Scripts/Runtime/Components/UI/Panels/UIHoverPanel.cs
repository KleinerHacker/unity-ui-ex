using System;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Panels
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.PanelMenu + "/Hover Panel")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class UIHoverPanel : UIBehaviour
    {
        #region Inspector Data

        [SerializeField]
        private string hoverKey;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private Camera camera;

        #endregion

        #region Properties

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        public Camera Camera
        {
            get => camera;
            set => camera = value;
        }

        #endregion

        private CanvasGroup _canvasGroup;

        private UIHoverItem _item;

        #region Builtin Methods

        protected override void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _item = string.IsNullOrEmpty(hoverKey) ? UISettings.Singleton.Hover.HoverDefault : UISettings.Singleton.Hover.HoverItems.FirstOrThrow(x => string.Equals(x.Key, hoverKey, StringComparison.Ordinal),
                () => new InvalidOperationException("Key " + hoverKey + " not found for hover UI"));
        }

        protected override void Start()
        {
            camera ??= Camera.main;
        }

        private void LateUpdate()
        {
            if (Camera == null)
                return;
            
            var position = target.position;
            var screenPoint = Camera.WorldToScreenPoint(position);
            var distance = Mathf.Abs(Vector3.Distance(Camera.transform.position, position));
            var relativeScaleDistance = MathfEx.Remap01(distance, _item.MaxScaleDistance, _item.MinScaleDistance);
            var relativeAlphaDistance = MathfEx.Remap01(distance, _item.MaxAlphaDistance, _item.MinAlphaDistance);

            transform.position = screenPoint;
            transform.localScale = Vector3.one * MathfEx.Remap(_item.ScaleInterpolationCurve.Evaluate(relativeScaleDistance), 0f, 1f, _item.MinScale, _item.MaxScale);
            _canvasGroup.alpha = MathfEx.Remap(_item.AlphaInterpolationCurve.Evaluate(relativeAlphaDistance), 0f, 1f, _item.MinAlpha, _item.MaxAlpha);
        }

        #endregion
    }
}