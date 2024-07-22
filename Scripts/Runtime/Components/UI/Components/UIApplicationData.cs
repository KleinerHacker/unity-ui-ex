using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityUIEx.Runtime.Projects.unity_ui_ex.Scripts.Runtime.Components.UI.Components
{
    public abstract class UIApplicationData : UIBehaviour
    {
        #region Inspector Data

        [Header("Data")]
        [SerializeField]
        private ApplicationData data = ApplicationData.Version;

        #endregion

        #region Builtin Methods

        protected override void Awake()
        {
            UpdateText(GetApplicationData());
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateText(GetApplicationData());
        }
#endif

        #endregion

        /// <summary>
        /// Is called to update text. Cause Application Data is static it is only called one time in Awake.
        /// <param name="text">Text to update on UI element</param>
        /// </summary>
        protected abstract void UpdateText(string text);

        private string GetApplicationData()
        {
            return data switch
            {
                ApplicationData.Name => Application.productName,
                ApplicationData.Version => Application.version,
                ApplicationData.Company => Application.companyName,
                ApplicationData.Identifier => Application.identifier,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum ApplicationData
    {
        Name,
        Version,
        Company,
        Identifier,
    }
}