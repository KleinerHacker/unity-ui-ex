using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Audio
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.AudioMenu + "/Slider Jingle")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    public sealed class UISliderJingle : UIJingle<Slider>
    {
        #region Inspector Data

        [Header("Resources")]
        [SerializeField]
        private AudioClip jingleUp;
        
        [SerializeField]
        private AudioClip jingleDown;

        #endregion

        private float _currentValue;

        #region Builtin Methods

        protected override void OnEnable()
        {
            _currentValue = _component.value;
            _component.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnDisable()
        {
            _component.onValueChanged.RemoveListener(OnValueChanged);
        }

        #endregion

        private void OnValueChanged(float v)
        {
            PlayJingle(_currentValue < v ? jingleDown : jingleUp);
            _currentValue = v;
        }
    }
}