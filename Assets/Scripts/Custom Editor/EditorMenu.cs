using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor
{
    public class EditorMenu : EditorWindow
    {
        [MenuItem("LUMINA/Menu")]
        public static void ShowWindow()
        {
            GetWindow<EditorMenu>("Menu");
        }

        private void OnGUI()
        {
            EditorLayout.CenterLabel("Editor Tools", EditorParameters.HEADER_STYLE_BOLD_FONT14);

            if (GUILayout.Button("Test One"))
            {
                // Handle "Test One" button click
                Debug.Log("Test One Button Clicked");
            }

            if (GUILayout.Button("Test Two"))
            {
                // Handle "Test Two" button click
                Debug.Log("Test Two Button Clicked");
            }
        }
    }
}
