using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Slider Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public sealed class UISliderInput : UIInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private GamepadButton negativeGamepadButton = GamepadButton.LeftShoulder;

        [SerializeField]
        private GamepadButton positiveGamepadButton = GamepadButton.RightShoulder;

        [Space]
        [SerializeField]
        private Key negativeKey = Key.LeftArrow;

        [SerializeField]
        private Key positiveKey = Key.RightArrow;

        [Space]
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

        private Slider _slider;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _slider = GetComponent<Slider>();
        }

        protected override void LateUpdate()
        {
            if ((GamepadAvailable && Gamepad.current[negativeGamepadButton].isPressed) ||
                (KeyboardAvailable && Keyboard.current[negativeKey].isPressed))
            {
                if (_slider.interactable)
                {
                    _slider.value -= CalculatedChange;
                }
            }
            else if ((GamepadAvailable && Gamepad.current[positiveGamepadButton].isPressed) ||
                     (KeyboardAvailable && Keyboard.current[positiveKey].isPressed))
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
            UpdateIconOnValidate(positiveKey, positiveGamepadButton, positiveIcon, positiveIconObject);
            UpdateIconOnValidate(negativeKey, negativeGamepadButton, negativeIcon, negativeIconObject);
        }
#endif

        #endregion

        private float CalculatedChange => _slider.wholeNumbers ? 1f : sensibility * Time.deltaTime;

        protected override void UpdateVisual()
        {
            UpdateIcon(positiveKey, positiveGamepadButton, positiveIcon, positiveIconObject);
            UpdateIcon(negativeKey, negativeGamepadButton, negativeIcon, negativeIconObject);
        }
    }
}