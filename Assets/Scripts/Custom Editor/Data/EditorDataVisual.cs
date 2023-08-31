using LuminaStudio.Scriptable.Data.Visual;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor.Data
{
    public class EditorDataVisual : EditorWindow
    {
        private static ColorDataScriptable _colorData;
        private void OnGUI()
        {
            
        }

        private void OnEnable()
        {
            _colorData = (ColorDataScriptable)AssetDatabase.LoadAssetAtPath
                ("Assets/ScriptableDatabase/Data/Visual/ColorData.asset", 
                typeof(ColorDataScriptable));
            
        }
        public static void Draw()
        {
            EditorLayout.CenterLabel("Visual", EditorParameters.HEADER_STYLE_BOLD_FONT20);

            if (GUILayout.Button("Color Data"))
            {
                Debug.Log("not yet");
            }

            if (GUILayout.Button("Back"))
            {
                EditorMenu.Instance.ChangePage(EditorParameters.Page.EditorDataMenu);
            }
        }
    }
}