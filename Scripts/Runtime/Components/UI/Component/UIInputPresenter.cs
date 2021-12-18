using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Component
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.ComponentMenu + "/Input Presenter")]
    [DisallowMultipleComponent]
    public sealed class UIInputPresenter : UIBehaviour
    {
        #region Inspector Data

        [SerializeField]
        private InputPresenterItem[] items;
        
        [SerializeField]
        private bool allowMultiplePresets;

        #endregion

        #region Properties

        public UIInputPreset CurrentInputPreset { get; private set; }

        #endregion

        #region Builtin Methods

        protected override void Awake()
        {
            var presenterItems = items.Where(x => x.Presenter != null).ToArray();
            
            foreach (var item in presenterItems)
            {
                item.Presenter.SetActive(false);
            }

            if (allowMultiplePresets)
            {
                foreach (var item in presenterItems.Where(x => UIInputSettings.Singleton.FindPreset(x.PresetGuid)?.IsUsable() ?? false))
                {
                    item.Presenter.SetActive(true);
                    CurrentInputPreset ??= UIInputSettings.Singleton.FindPreset(item.PresetGuid);
                }
            }
            else
            {
                var presenterItem = presenterItems.FirstOrDefault(x => UIInputSettings.Singleton.FindPreset(x.PresetGuid)?.IsUsable() ?? false);
                if (presenterItem != null)
                {
                    presenterItem.Presenter.SetActive(true);
                    CurrentInputPreset = UIInputSettings.Singleton.FindPreset(presenterItem.PresetGuid);
                }
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            foreach (var item in items)
            {
                if (item.Presenter != null)
                {
                    item.Presenter.SetActive(true);
                }
            }
        }
#endif

        #endregion
    }

    [Serializable]
    public sealed class InputPresenterItem 
    {
        #region Inspector Data

        [SerializeField]
        private string presetGuid;

        [SerializeField]
        private GameObject presenter;

        #endregion

        #region Properties

        public string PresetGuid => presetGuid;

        public GameObject Presenter => presenter;

        #endregion
    }
}