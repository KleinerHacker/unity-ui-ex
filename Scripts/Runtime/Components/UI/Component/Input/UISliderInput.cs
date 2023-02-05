using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Slider Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public sealed class UISliderInput : UIInput
    {
        #region Inspector Data
        
        [SerializeField]
        private string negativeAction;

        [SerializeField]
        private string positiveAction;
        
        [SerializeField]
        private GameObject negativeIconObject;

        [SerializeField]
        private Image negativeIcon;

        [SerializeField]
        private GameObject positiveIconObject;

        [SerializeField]
        private Image positiveIcon;

        [Space]
        [SerializeField]
        private float sensibility = 0.01f;

        #endregion

        #region Properties

        protected override string[] AssignedShortcutActions => new[] { positiveAction, negativeAction };

        #endregion

        private Slider _slider;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _slider = GetComponent<Slider>();
        }

        protected override void LateUpdate()
        {
            if (WasShortcutPressedThisFrame(negativeAction))
            {
                if (_slider.interactable)
                {
                    _slider.value -= CalculatedChange;
                }
            }
            else if (WasShortcutPressedThisFrame(positiveAction))
            {
                if (_slider.interactable)
                {
                    _slider.value += _slider.wholeNumbers ? 1f : sensibility * Time.deltaTime;
                }
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateIconOnValidate(positiveIcon, positiveIconObject, positiveAction);
            UpdateIconOnValidate(negativeIcon, negativeIconObject, negativeAction);
        }
#endif

        #endregion

        private float CalculatedChange => _slider.wholeNumbers ? 1f : sensibility * Time.deltaTime;

        protected override void UpdateVisual()
        {
            UpdateIcon(positiveIcon, positiveIconObject, positiveAction);
            UpdateIcon(negativeIcon, negativeIconObject, negativeAction);
        }
    }
}