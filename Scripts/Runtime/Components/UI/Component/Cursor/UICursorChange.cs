using UnityEngine;
using UnityEngine.EventSystems;
using UnityExtension.Runtime.extension.Scripts.Runtime;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component.Cursor
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