using LuminaStudio.Custom_Editor.Data.Visual;
using LuminaStudio.Scriptable.Data.Visual;
using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor.Data
{
    public class EditorDataMenu : EditorWindow
    {
        
        private void OnGUI()
        {
            EditorLayout.CenterLabel("Editor Data Tools", EditorParameters.HEADER_STYLE_BOLD_FONT14);

            if (GUILayout.Button("Visual"))
            {
                EditorDataVisual.OnShow();
                Close();
            }

            if (GUILayout.Button("Back"))
            {
                EditorMenu.OnShow();
                Close();
            }
        }
        public static void OnShow()
        {
            EditorDataMenu window = GetWindow<EditorDataMenu>();
            window.position = EditorMenu.Instance.position;
            window.Show();
        }
    }
}