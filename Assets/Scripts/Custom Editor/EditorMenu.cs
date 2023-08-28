using System;
using LuminaStudio.Custom_Editor.Data;
using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor
{
    public class EditorMenu : EditorWindow
    {
        public static EditorMenu Instance;

        
        public EditorParameters.Page currentPage = EditorParameters.Page.MainMenu;

        [MenuItem("LUMINA/Menu")]
        public static void ShowWindow()
        {
            Instance = GetWindow<EditorMenu>("Menu");
        }

        private void OnEnable()
        {

        }

        private void OnGUI()
        {
            switch (currentPage)
            {
                case EditorParameters.Page.MainMenu:
                    Draw();
                    break;
                case EditorParameters.Page.EditorDataMenu:
                    EditorDataMenu.Draw();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ChangePage(EditorParameters.Page newPage)
        {
            currentPage = newPage;
            Repaint();
        }

        public void Draw()
        {
            EditorLayout.CenterLabel("Editor Tools", EditorParameters.HEADER_STYLE_BOLD_FONT14);

            if (GUILayout.Button("Data Management"))
            {
                EditorMenu.Instance.ChangePage(EditorParameters.Page.EditorDataMenu);
            }

            if (GUILayout.Button("Test Two"))
            {
                // Handle "Test Two" button click
                Debug.Log("Test Two Button Clicked");
            }
        }

        
    }
}
