using System;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    [Serializable]
    public sealed class UIHover
    {
        #region Inspector Data

        [SerializeField]
        private UIHoverItem hoverDefault;

        [SerializeField]
        private UINamedHoverItem[] hoverItems = Array.Empty<UINamedHoverItem>();

        #endregion

        #region Properties

        public UIHoverItem HoverDefault => hoverDefault;

        public UINamedHoverItem[] HoverItems => hoverItems;

        #endregion
    }
    
    [Serializable]
    public class UIHoverItem
    {
        #region Inspector Data

        [SerializeField]
        //[Range(0f, 1f)]
        private float minScale = 0.5f;

        [SerializeField]
        //[Range(0f, 1f)]
        private float maxScale = 1f;

        [SerializeField]
        private float minScaleDistance = 10f;

        [SerializeField]
        private float maxScaleDistance = 5f;

        [SerializeField]
        private AnimationCurve scaleInterpolationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

        [SerializeField]
        //[Range(0f, 1f)]
        private float minAlpha = 0f;

        [SerializeField]
        //[Range(0f, 1f)]
        private float maxAlpha = 1f;

        [SerializeField]
        private float minAlphaDistance = 15f;

        [SerializeField]
        private float maxAlphaDistance = 7f;
        
        [SerializeField]
        private AnimationCurve alphaInterpolationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

        #endregion

        #region Properties

        public float MinScale => minScale;

        public float MaxScale => maxScale;

        public float MinScaleDistance => minScaleDistance;

        public float MaxScaleDistance => maxScaleDistance;

        public AnimationCurve ScaleInterpolationCurve => scaleInterpolationCurve;

        public float MinAlpha => minAlpha;

        public float MaxAlpha => maxAlpha;

        public float MinAlphaDistance => minAlphaDistance;

        public float MaxAlphaDistance => maxAlphaDistance;

        public AnimationCurve AlphaInterpolationCurve => alphaInterpolationCurve;

        #endregion
    }

    [Serializable]
    public sealed class UINamedHoverItem : UIHoverItem
    {
        #region Inspector Data

        [SerializeField]
        private string key;

        #endregion

        #region Properties

        public string Key => key;

        #endregion
    }
}