using UnityEngine;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Input
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.InputMenu + "/Dropdown Input")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Dropdown))]
    public sealed class UIDropdownInput : UIListInput
    {
        #region Properties

        protected override int CurrentIndex => _dropdown.value;
        protected override int ListLength => _dropdown.options.Count;

        #endregion

        private Dropdown _dropdown;

        #region Builtin Methods

        protected override void Awake()
        {
            base.Awake();
            _dropdown = GetComponent<Dropdown>();
        }

        #endregion
        
        protected override void ActivateItem(int index) => _dropdown.value = index;
    }
}