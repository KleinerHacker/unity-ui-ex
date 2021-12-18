using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UIInputSettings : ScriptableObject
    {
        #region Static Area

        private const string Path = "Assets/ui-input.asset";

        public static UIInputSettings Singleton
        {
            get
            {
                var settings = AssetDatabase.LoadAssetAtPath<UIInputSettings>(Path);
                if (settings == null)
                {
                    Debug.Log("Unable to find game settings, create new");

                    settings = new UIInputSettings();
                    AssetDatabase.CreateAsset(settings, Path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }

                return settings;
            }
        }

        public static SerializedObject SerializedSingleton => new SerializedObject(Singleton);

        #endregion

        #region Inspector Data

        [SerializeField]
        private UIInputPreset[] inputPresets;

        #endregion

        #region Properties

        public UIInputPreset[] InputPresets => inputPresets;

        #endregion

        #region Builtin Methods

#if UNITY_EDITOR
        private void OnValidate()
        {
            var set = new HashSet<string>();
            foreach (var inputPreset in inputPresets)
            {
                if (string.IsNullOrWhiteSpace(inputPreset.Guid))
                {
                    inputPreset.UpdateGuid();
                }
                else if (set.Contains(inputPreset.Guid))
                {
                    set.Remove(inputPreset.Guid);
                    inputPreset.UpdateGuid();
                }
                
                set.Add(inputPreset.Guid);
            }
        }
#endif

        #endregion

        public UIInputPreset FindPreset(string guid) => inputPresets.FirstOrDefault(x => x.Guid == guid);

#if UNITY_EDITOR
        public void UpdateGuids()
        {
            foreach (var inputPreset in inputPresets)
            {
                inputPreset.UpdateGuid();
            }
        }
#endif
    }
}