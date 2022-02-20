using System;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    [Serializable]
    public sealed class UINotification
    {
        #region Inspector Data

        [SerializeField]
        private Color infoColor = Color.white;
        
        [SerializeField]
        private Color warningColor = Color.yellow;
        
        [SerializeField]
        private Color errorColor = Color.red;

        #endregion

        #region Properties

        public Color InfoColor => infoColor;

        public Color WarningColor => warningColor;

        public Color ErrorColor => errorColor;

        #endregion
    }
}