using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Components
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.ComponentMenu + "/Application Label Text")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public sealed class UIApplicationDataText : UIApplicationData
    {
        protected override void UpdateText(string text) => GetComponent<Text>().text = text;
    }
}