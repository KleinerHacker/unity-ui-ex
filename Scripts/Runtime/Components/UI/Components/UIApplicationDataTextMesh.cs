using TMPro;
using UnityEngine;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Components
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.ComponentMenu + "/Application Label Text Mesh Pro")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshPro))]
    public sealed class UIApplicationDataTextMesh : UIApplicationData
    {
        protected override void UpdateText(string text) => GetComponent<TextMeshPro>().text = text;
    }
}