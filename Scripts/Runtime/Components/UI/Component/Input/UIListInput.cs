using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Input
{
    public abstract class UIListInput : UIInput
    {
        #region Inspector Data

        [SerializeField]
        private string negativeAction;

        [SerializeField]
        private string positiveAction;
        
        [Tooltip("Behavior to define what is happen if first or last toggle was reached")]
        [SerializeField]
        private UIListInputBehavior behavior = UIListInputBehavior.Circle;
        
        [Space]
        [SerializeField]
        private GameObject negativeIconObject;
        
        [SerializeField]
        private Image negativeIcon;
        
        [SerializeField]
        private GameObject positiveIconObject;
        
        [SerializeField]
        private Image positiveIcon;

        #endregion

        #region Properties

        protected abstract int CurrentIndex { get; }
        protected abstract int ListLength { get; }
        protected abstract bool Interactable { get; }

        protected override string[] AssignedShortcutActions => new[] { negativeAction, positiveAction };

        #endregion
        
        private int _index;

        #region Builtin Methods

        protected override void Start()
        {
            base.Start();
            _index = CurrentIndex;
        }

        protected override void LateUpdate()
        {
            if (WasShortcutPressedThisFrame(negativeAction))
            {
                if (Interactable)
                {
                    GotoPrev();
                }
            }
            else if (WasShortcutPressedThisFrame(positiveAction))
            {
                if (Interactable)
                {
                    GotoNext();
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
        
        private void GotoNext()
        {
            _index++;
            if (_index >= ListLength)
            {
                _index = behavior switch
                {
                    UIListInputBehavior.Stays => ListLength - 1,
                    UIListInputBehavior.Circle => 0,
                    _ => throw new NotImplementedException(behavior.ToString())
                };
                
                if (behavior == UIListInputBehavior.Stays)
                    return;
            }

            if (!ActivateItem(_index))
            {
                GotoNext();
            }
        }

        private void GotoPrev()
        {
            _index--;
            if (_index < 0)
            {
                _index = behavior switch
                {
                    UIListInputBehavior.Stays => 0,
                    UIListInputBehavior.Circle => ListLength - 1,
                    _ => throw new NotImplementedException(behavior.ToString())
                };
                
                if (behavior == UIListInputBehavior.Stays)
                    return;
            }

            if (!ActivateItem(_index))
            {
                GotoPrev();
            }
        }

        protected abstract bool ActivateItem(int index);

        protected override void UpdateVisual()
        {
            UpdateIcon(positiveIcon, positiveIconObject, positiveAction);
            UpdateIcon(negativeIcon, negativeIconObject, negativeAction);
        }
    }
    
    public enum UIListInputBehavior
    {
        Stays,
        Circle,
    }
}