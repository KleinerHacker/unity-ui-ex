using UnityEngine;

namespace UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Utils.Extensions
{
    internal static class CanvasGroupExtensions
    {
        public static void Hide(this CanvasGroup cg)
        {
            cg.alpha = 0f;
            cg.blocksRaycasts = false;
        }

        public static void Show(this CanvasGroup cg)
        {
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
        }

        public static bool IsShown(this CanvasGroup cg)
        {
            return cg.blocksRaycasts;
        }
        
        public static bool IsHidden(this CanvasGroup cg)
        {
            return !cg.blocksRaycasts;
        }
    }
}