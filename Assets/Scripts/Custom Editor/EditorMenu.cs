using System;
using LuminaStudio.Custom_Editor.Data;
using LuminaStudio.Custom_Editor.Data.Visual;
using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor
{
    public class EditorMenu : EditorWindow
    {
        public static EditorMenu Instance;
        public static Rect WindowPosition;
        public static EditorParameters.Page CurrentPage;

        [MenuItem("LUMINA/Menu")]
        public static void ShowWindow()
        {
            Instance = GetWindow<EditorMenu>("Menu");
            CurrentPage = EditorParameters.Page.EditorMenu;
            WindowPosition = Instance.position;
        }

        private void OnEnable()
        {
            
        }

        private void OnGUI()
        {
            EditorLayout.CenterLabel("Editor Tools", EditorParameters.HEADER_STYLE_BOLD_FONT14);

            if (GUILayout.Button("Data Management"))
            {
                EditorDataMenu.OnShow();
                this.Close();
            }

            if (GUILayout.Button("Test Two"))
            {
                // Handle "Test Two" button click
                Debug.Log("Test Two Button Clicked");
            }
        }

        public static void OnShow()
        {
            Instance = GetWindow<EditorMenu>("Menu");
            Instance.Show();
        }
    }
}
