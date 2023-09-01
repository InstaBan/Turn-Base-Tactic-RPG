using LuminaStudio.Scriptable.Data.Visual;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor.Data.Visual
{
    public class EditorDataVisual : EditorWindow
    {
        private void OnGUI()
        {
            EditorLayout.CenterLabel("Visual", EditorParameters.HEADER_STYLE_BOLD_FONT20);

            if (GUILayout.Button("Color Data"))
            {
                EditorDataVisualColor.OnShow();
            }

            if (GUILayout.Button("Back"))
            {
                EditorDataMenu.OnShow();
            }
        }

        private void OnEnable()
        {

        }
        public static void OnShow()
        {
            EditorDataVisual window = GetWindow<EditorDataVisual>();
            window.position = EditorMenu.Instance.position;
            window.Show();
        }
    }
}