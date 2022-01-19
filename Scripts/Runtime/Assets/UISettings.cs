#if !UNITY_EDITOR
using UnityAssetLoader.Runtime.asset_loader.Scripts.Runtime.Loader;
#endif
using System;
using UnityEditor;
using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets
{
    public sealed class UISettings : ScriptableObject
    {
        #region Static Area

#if UNITY_EDITOR
        private const string Path = "Assets/Resources/ui.asset";
#endif

        public static UISettings Singleton
        {
            get
            {
#if UNITY_EDITOR
                var settings = AssetDatabase.LoadAssetAtPath<UISettings>(Path);
                if (settings == null)
                {
                    Debug.Log("Unable to find UI settings, create new");

                    settings = new UISettings();
                    if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                    {
                        AssetDatabase.CreateFolder("Assets", "Resources");
                    }
                    
                    AssetDatabase.CreateAsset(settings, Path);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }

                return settings;
#else
                return AssetResourcesLoader.Instance.GetAsset<UISettings>();
#endif
            }
        }

#if UNITY_EDITOR
        public static SerializedObject SerializedSingleton => new SerializedObject(Singleton);
#endif

        #endregion

        #region Inspector Data

        [SerializeField]
        private UIHoverItem hoverDefault;

        [SerializeField]
        private UINamedHoverItem[] hoverItems;

        #endregion

        #region Properties

        public UIHoverItem HoverDefault => hoverDefault;

        public UINamedHoverItem[] HoverItems => hoverItems;

        #endregion
    }

    [Serializable]
    public class UIHoverItem
    {
        #region Inspector Data

        [SerializeField]
        //[Range(0f, 1f)]
        private float minScale = 0.5f;

        [SerializeField]
        //[Range(0f, 1f)]
        private float maxScale = 1f;

        [SerializeField]
        private float minScaleDistance = 10f;

        [SerializeField]
        private float maxScaleDistance = 5f;

        [SerializeField]
        private AnimationCurve scaleInterpolationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

        [SerializeField]
        //[Range(0f, 1f)]
        private float minAlpha = 0f;

        [SerializeField]
        //[Range(0f, 1f)]
        private float maxAlpha = 1f;

        [SerializeField]
        private float minAlphaDistance = 15f;

        [SerializeField]
        private float maxAlphaDistance = 7f;
        
        [SerializeField]
        private AnimationCurve alphaInterpolationCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

        #endregion

        #region Properties

        public float MinScale => minScale;

        public float MaxScale => maxScale;

        public float MinScaleDistance => minScaleDistance;

        public float MaxScaleDistance => maxScaleDistance;

        public AnimationCurve ScaleInterpolationCurve => scaleInterpolationCurve;

        public float MinAlpha => minAlpha;

        public float MaxAlpha => maxAlpha;

        public float MinAlphaDistance => minAlphaDistance;

        public float MaxAlphaDistance => maxAlphaDistance;

        public AnimationCurve AlphaInterpolationCurve => alphaInterpolationCurve;

        #endregion
    }

    [Serializable]
    public sealed class UINamedHoverItem : UIHoverItem
    {
        #region Inspector Data

        [SerializeField]
        private string key;

        #endregion

        #region Properties

        public string Key => key;

        #endregion
    }
}