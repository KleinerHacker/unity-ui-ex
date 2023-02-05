using System;
using UnityEngine;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Extras
{
    [AttributeUsage(AttributeTargets.Field)]
    public class UIShortcutSchemeActionAttribute : PropertyAttribute
    {
        public UIShortcutInputType InputType { get; }

        public UIShortcutSchemeActionAttribute(UIShortcutInputType inputType)
        {
            InputType = inputType;
        }
    }
}