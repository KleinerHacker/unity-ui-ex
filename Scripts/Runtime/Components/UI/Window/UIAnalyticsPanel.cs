using UnityAnalyticsEx.Runtime.analytics_ex.Scripts.Runtime;
using UnityAnalyticsEx.Runtime.analytics_ex.Scripts.Runtime.Utils;
using UnityAnimation.Runtime.animation.Scripts.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Window
{
    [AddComponentMenu(UnityUIExConstants.Menus.Components.UI.WindowMenu + "/Analytics Window")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(UIWindow))]
    public sealed class UIAnalyticsPanel : UIBehaviour
    {
        #region Inspector Data

        [Header("Behavior")]
        [SerializeField]
        private bool autoOpenIfRequired = true;

        [SerializeField]
        private float autoOpenDelay = 1f;

        [Header("References")]
        [SerializeField]
        private Toggle ageToggle;

        [SerializeField]
        private Toggle allowToggle;

        [SerializeField]
        private Button infoButton;

        #endregion

        #region Builtin Methods

        protected override void OnEnable()
        {
            ageToggle.onValueChanged.AddListener(AgeChanged);
            infoButton.onClick.AddListener(InfoClick);
            
            ageToggle.isOn = false;
            allowToggle.isOn = false;

            if (autoOpenIfRequired && AnalyticsEx.IsAnalyticsActivatedEx == null)
            {
                AnimationBuilder.Create(this)
                    .Wait(autoOpenDelay, () => GetComponent<UIWindow>().Show())
                    .Start();
            }
        }

        protected override void OnDisable()
        {
            ageToggle.onValueChanged.AddListener(AgeChanged);
            infoButton.onClick.AddListener(InfoClick);
        }

        #endregion

        public void HandleSubmit()
        {
            if (allowToggle.isOn)
            {
                AnalyticsEx.ActivateAnalytics();
            }
            else
            {
                AnalyticsEx.DeactivateAnalytics();
            }
        }

        private void InfoClick()
        {
            PrivacyUtils.OpenPrivacyInfo();
        }

        private void AgeChanged(bool active)
        {
            allowToggle.interactable = active;
            if (!active)
            {
                allowToggle.isOn = false;
            }
        }
    }
}