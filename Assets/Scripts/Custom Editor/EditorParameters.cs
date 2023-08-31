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

        internal static readonly GUIStyle HEADER_STYLE_BOLD_FONT20 = new(EditorStyles.boldLabel)
        {
            fontSize = 20
        };

        public enum Page
        {
            EditorMenu,
            EditorDataMenu,
            EditorDataVisual,
            EditorDataColor,
        }
    }
}
