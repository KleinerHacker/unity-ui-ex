using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.WindowMenu + "/Popup")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPopup : UIStage
    {
    }
}