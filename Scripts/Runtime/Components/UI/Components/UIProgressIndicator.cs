using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.ComponentMenu + "/Progress Indicator")]
    [DisallowMultipleComponent]
    public class UIProgressIndicator : UIBehaviour
    {
        #region Inspector Data

        [Header("Animation")]
        [SerializeField]
        [Range(1f, 10f)]
        private float minSpeed = 1f;
        
        [SerializeField]
        [Range(1f, 10f)]
        private float maxSpeed = 1f;

        [SerializeField]
        private ProgressIndicatorDirection direction = ProgressIndicatorDirection.Random;
        
        [Header("References")]
        [SerializeField]
        private RectTransform animationTransform;

        #endregion
        
        private float _speed;

        #region Builtin Methods

        protected override void Awake()
        {
            bool dir;
            switch (direction)
            {
                case ProgressIndicatorDirection.Clockwise:
                    dir = true;
                    break;
                case ProgressIndicatorDirection.AntiClockwise:
                    dir = false;
                    break;
                case ProgressIndicatorDirection.Random:
                    dir = Random.Range(0, 2) == 0;
                    break;
                default:
                    throw new NotImplementedException();
            }
            
            _speed = Random.Range(minSpeed, maxSpeed) * (dir ? 1 : -1);
        }
        
        protected virtual void LateUpdate()
        {
            animationTransform.rotation *= Quaternion.Euler(0f, 0f, _speed * Time.deltaTime);
        }

        #endregion
    }

    public enum ProgressIndicatorDirection
    {
        Clockwise,
        AntiClockwise,
        Random
    }
}