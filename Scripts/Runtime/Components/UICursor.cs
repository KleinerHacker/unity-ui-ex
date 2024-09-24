using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime;
using UnityCommons.Runtime.Projects.unity_commons.Scripts.Runtime.Assets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components
{
    [AddComponentMenu(UnityUIExConstants.Root + "/UI Cursor")]
    [DisallowMultipleComponent]
    public sealed class UICursor : UIBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Inspector Data

        [SerializeField] private string cursorKey;

        #endregion

        #region Properties

        public string CursorKey => cursorKey;

        #endregion

        public void OnPointerEnter(PointerEventData eventData)
        {
            CursorSystem.ChangeCursor(cursorKey);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CursorSystem.ResetCursor();
        }
    }
}