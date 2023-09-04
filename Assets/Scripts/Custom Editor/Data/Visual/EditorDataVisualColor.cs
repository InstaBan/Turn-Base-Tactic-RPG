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
        private List<bool> renameChecks;
        string newName;
        private void OnGUI()
        {
            EditorLayout.CenterLabel("Color", EditorParameters.HEADER_STYLE_BOLD_FONT20);

            if (_colorList != null)
            {
                DrawColorFields(_colorList);
            }

            if (GUILayout.Button("Add"))
            {
                var color = new ColorEntry();
                color.colorName = "New Color";
                color.colorValue = Color.white;
                _colorList.Add(color);
                renameChecks.Add(false);
            }

            if (GUILayout.Button("Back"))
            {
                EditorDataVisual.OnShow();
                Close();
            }
        }

        private void OnEnable()
        {
            _colorData = (ColorDataScriptable)AssetDatabase.LoadAssetAtPath
            ("Assets/ScriptableDatabase/Data/Visual/ColorData.asset",
                typeof(ColorDataScriptable));
            _colorList = _colorData.ColorList;
            renameChecks = new();
            for (int i = 0; i < _colorList.Count; i++)
            {
                renameChecks.Add(false);
            }
        }
        public static void OnShow()
        {
            EditorDataVisualColor window = GetWindow<EditorDataVisualColor>();
            window.position = EditorMenu.Instance.position;
            window.Show();
        }

        private void DrawColorFields(List<ColorEntry> colorList)
        {
            List<string> nameList = new();
            if (colorList.Count == 0) 
                return;
            for (var i = 0; i < colorList.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (renameChecks[i] == false)
                {
                    nameList.Add(colorList[i].colorName);
                    GUILayout.Label(colorList[i].colorName);
                }
                else
                {
                    newName = GUILayout.TextField(newName);
                }
                colorList[i].colorValue = EditorGUILayout.ColorField(colorList[i].colorValue);
                if (renameChecks[i] == true)
                {
                    if (GUILayout.Button("Apply"))
                    {
                        colorList[i].colorName = newName;
                        renameChecks[i] = !renameChecks[i];
                    }
                }
                else
                {
                    if (GUILayout.Button("Rename"))
                    {
                        newName = colorList[i].colorName;
                        renameChecks[i] = !renameChecks[i];
                    }
                }
                
                if (GUILayout.Button("Remove"))
                {
                    colorList.RemoveAt(i);
                    renameChecks.RemoveAt(i);
                }
                GUILayout.EndHorizontal();
            }
        }
    }
}
