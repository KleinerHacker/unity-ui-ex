#if PCSOFT_SHORTCUT && PCSOFT_ENV
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Components.Input
{
    [AddComponentMenu(UnityUIExConstants.Menu.Component.UI.Component.InputMenu + "/Toggle Group Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ToggleGroup))]
    public sealed class UIToggleGroupInput : UIListInput
    {
        #region Inspector Data

        [Space]
        [SerializeField]
        private Toggle[] toggles;

        #endregion

        #region Properties

        protected override int CurrentIndex => toggles.IndexOf(x => x.isOn);
        protected override int ListLength => toggles.Length;
        protected override bool Interactable => true;

        #endregion

        protected override bool ActivateItem(int index)
        {
            if (!toggles[index].interactable)
                return false;
            
            toggles[index].isOn = true;
            return true;
        }
    }
}
#endif