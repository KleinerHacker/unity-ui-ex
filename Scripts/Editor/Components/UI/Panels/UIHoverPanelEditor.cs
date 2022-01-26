using System;
using System.Linq;
using UnityCommonEx.Runtime.common_ex.Scripts.Runtime.Utils.Extensions;
using UnityEditor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor;
using UnityEditorEx.Editor.editor_ex.Scripts.Editor.Commons;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Assets;
using UnityUIEx.Runtime.ui_ex.Scripts.Runtime.Components.UI.Panels;

namespace UnityUIEx.Editor.ui_ex.Scripts.Editor.Components.UI.Panels
{
    [CustomEditor(typeof(UIHoverPanel))]
    public sealed class UIHoverPanelEditor : AutoEditor
    {
        [SerializedPropertyReference("hoverKey")]
        private SerializedProperty _keyProperty;

        [SerializedPropertyReference("target")]
        [SerializedPropertyDefaultRepresentation]
        private SerializedProperty _targetProperty;
        
        [SerializedPropertyReference("camera")]
        [SerializedPropertyDefaultRepresentation]
        private SerializedProperty _cameraProperty;

        private string[] _keyOptions;

        public UIHoverPanelEditor() : base(CustomGUIPosition.Top)
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _keyOptions = new[] { "<default>" }.Concat(UISettings.Singleton.Hover.HoverItems.Select(x => x.Key).ToArray()).ToArray();
        }

        protected override void DoInspectorGUI()
        {
            var index = UISettings.Singleton.Hover.HoverItems.IndexOf(x => string.Equals(x.Key, _keyProperty.stringValue, StringComparison.Ordinal)) + 1;
            var newIndex = EditorGUILayout.Popup("Hover Key", index, _keyOptions);
            if (index != newIndex)
            {
                _keyProperty.stringValue = newIndex <= 0 ? null : UISettings.Singleton.Hover.HoverItems[newIndex - 1].Key;
            }
        }
    }
}