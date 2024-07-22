#if PCSOFT_CURSOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityExtension.Runtime.Projects.unity_extension.Scripts.Runtime;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Components.Cursor
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.CursorMenu + "/Cursor Change")]
    [DisallowMultipleComponent]
    public sealed class UICursorChange : UIBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Inspector Data

        [SerializeField]
        private string cursorKey;

        #endregion
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorSystem.ChangeUICursor(cursorKey);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorSystem.ResetUICursor();
        }
    }
}
#endif