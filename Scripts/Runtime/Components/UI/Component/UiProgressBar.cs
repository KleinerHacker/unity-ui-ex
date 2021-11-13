using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.Ui.ComponentMenu + "/Progress Bar")]
    [DisallowMultipleComponent]
    public sealed class UiProgressBar : UIBehaviour
    {
        #region Inspector Data

        [Header("Values")]
        [SerializeField]
        [Range(0f, 1f)]
        private float value = 0.5f;

        [SerializeField]
        private ProgressFillType fillType = ProgressFillType.Image;

        [Header("References")]
        [SerializeField]
        private Image progressImage;

        [SerializeField]
        private Text progressPercentage;

        [SerializeField]
        private string progressPercentageFormat = "{0}%";

        [Space]
        [SerializeField]
        private Image fillPointImage;

        [SerializeField]
        private float relativeX;

        #endregion

        #region Properties

        public float Value
        {
            get
            {
                switch (fillType)
                {
                    case ProgressFillType.Image:
                        return progressImage.fillAmount;
                    case ProgressFillType.Slider:
                        return _slider.value;
                    default:
                        throw new NotImplementedException();
                }
            }
            set
            {
                switch (fillType)
                {
                    case ProgressFillType.Image:
                        progressImage.fillAmount = value;
                        break;
                    case ProgressFillType.Slider:
                        _slider.value = value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                
                if (progressPercentage != null)
                {
                    progressPercentage.text = string.Format(progressPercentageFormat, (value * 100f).ToString("F0"));
                }

                if (fillPointImage != null)
                {
                    var ownRectTrans = (RectTransform) transform;
                    var pointRectTrans = (RectTransform) fillPointImage.transform;
                    pointRectTrans.localPosition = new Vector3(relativeX + ownRectTrans.rect.width * value, pointRectTrans.localPosition.y, pointRectTrans.localPosition.z);
                }
            }
        }

        #endregion

        private Slider _slider;

        #region Builtin Methods

        protected override void Awake()
        {
            _slider = GetComponent<Slider>();
            Value = value;
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            switch (fillType)
            {
                case ProgressFillType.Image:
                    if (gameObject.GetComponent<Slider>() != null)
                    {
                        var cmp = gameObject.GetComponent<Slider>();
                        cmp.value = 1f;

                        DestroyImmediate(cmp);
                        _slider = null;
                    }

                    break;
                case ProgressFillType.Slider:
                    if (gameObject.GetComponent<Slider>() == null)
                    {
                        _slider = gameObject.AddComponent<Slider>();
                        _slider.interactable = false;
                        _slider.fillRect = progressImage.rectTransform;
                    }

                    break;
                default:
                    throw new NotImplementedException();
            }

            Awake();
        }
#endif

        #endregion
    }

    public enum ProgressFillType
    {
        Image,
        Slider
    }
}