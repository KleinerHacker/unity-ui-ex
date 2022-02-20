using System;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    public interface IViewable
    {
        ViewableState State { get; }
        
        void Show(Action onFinished);
        void Show();
        void Hide(Action onFinished);
        void Hide();
    }
    
    public enum ViewableState
    {
        Shown,
        Hidden,
    }
}