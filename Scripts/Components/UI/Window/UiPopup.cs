using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.Ui.WindowMenu + "/Popup")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class UiPopup : UiStage
    {
    }
}