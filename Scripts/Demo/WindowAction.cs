#if DEMO
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window;

namespace UnityUIEx.Demo.ui_ex.Scripts.Demo
{
    public sealed class WindowAction : UIBehaviour
    {
        [FormerlySerializedAs("sideMenu")]
        [SerializeField]
        private UIWindow leftSideMenu;
        
        [SerializeField]
        private UIWindow rightSideMenu;
        
        [SerializeField]
        private UIWindow topSideMenu;
        
        [SerializeField]
        private UIWindow bottomSideMenu;

        [Space]
        [SerializeField]
        private UIWindow fadeWindow;

        public void ToggleLeftSideMenu()
        {
            leftSideMenu.ToggleVisibility();
        }
        
        public void ToggleRightSideMenu()
        {
            rightSideMenu.ToggleVisibility();
        }
        
        public void ToggleTopSideMenu()
        {
            topSideMenu.ToggleVisibility();
        }
        
        public void ToggleBottomSideMenu()
        {
            bottomSideMenu.ToggleVisibility();
        }

        public void ToggleFadeWindow()
        {
            fadeWindow.ToggleVisibility();
        }
    }
}
#endif