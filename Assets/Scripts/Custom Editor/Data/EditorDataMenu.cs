using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor.Data
{
    public class EditorDataMenu : EditorWindow
    {
        private void OnGUI()
        {
            EditorLayout.CenterLabel("Data Management", EditorParameters.HEADER_STYLE_BOLD_FONT14);
        }
        public static void Draw()
        {
            EditorLayout.CenterLabel("Editor Data Tools", EditorParameters.HEADER_STYLE_BOLD_FONT14);

            if (GUILayout.Button("Color Data"))
            {
                EditorMenu.Instance.ChangePage(EditorParameters.Page.MainMenu);
            }

            if (GUILayout.Button("Back"))
            {
                EditorMenu.Instance.ChangePage(EditorParameters.Page.MainMenu);
            }
        }
    }
}