using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor
{
    public class EditorParameters
    {
        internal static readonly GUIStyle HEADER_STYLE_BOLD_FONT14 = new(EditorStyles.boldLabel)
        {
            fontSize = 14
        };

        public enum Page
        {
            MainMenu,
            EditorDataMenu,
            EditorDataColor,
        }
    }
}
