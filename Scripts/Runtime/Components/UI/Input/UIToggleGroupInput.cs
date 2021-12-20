using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.InputMenu + "/Toggle Group Input")]
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

        #endregion

        protected override void ActivateItem(int index) => toggles[index].isOn = true;
    }
}