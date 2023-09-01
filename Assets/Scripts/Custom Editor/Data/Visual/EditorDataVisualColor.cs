using System.Collections.Generic;
using LuminaStudio.Scriptable.Data.Visual;
using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor.Data.Visual
{
    public class EditorDataVisualColor : EditorWindow
    {
        private static ColorDataScriptable _colorData;
        private static List<ColorEntry> _colorList;
        private void OnGUI()
        {
            EditorLayout.CenterLabel("Color", EditorParameters.HEADER_STYLE_BOLD_FONT20);

            if (_colorList != null)
            {
                foreach (var color in _colorList)
                {
                    color.colorValue = EditorGUILayout.ColorField("New Color", color.colorValue);
                }
            }

            if (GUILayout.Button("Add"))
            {
                var color = new ColorEntry();
                color.colorName = "New Color";
                color.colorValue = Color.white;
                _colorList.Add(color);
            }

            if (GUILayout.Button("Back"))
            {
                EditorDataVisual.OnShow();
            }
        }

        private void OnEnable()
        {
            _colorData = (ColorDataScriptable)AssetDatabase.LoadAssetAtPath
            ("Assets/ScriptableDatabase/Data/Visual/ColorData.asset",
                typeof(ColorDataScriptable));
            _colorList = _colorData.ColorList;

        }
        public static void OnShow()
        {
            EditorDataVisualColor window = GetWindow<EditorDataVisualColor>();
            window.position = EditorMenu.Instance.position;
            window.Show();
        }
    }
}
