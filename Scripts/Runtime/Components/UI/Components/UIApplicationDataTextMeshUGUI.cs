using TMPro;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.ComponentMenu + "/Application Label Text Mesh Pro UGUI")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class UIApplicationDataTextMeshUGUI : UIApplicationData
    {
        protected override void UpdateText(string text) => GetComponent<TextMeshProUGUI>().text = text;
    }
}