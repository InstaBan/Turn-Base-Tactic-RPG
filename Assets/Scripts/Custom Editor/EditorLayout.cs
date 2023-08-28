using UnityEditor;
using UnityEngine;

namespace LuminaStudio.Custom_Editor
{
    public class EditorLayout
    {
        /// <summary>
        /// essentially GUILayout.Label but at center
        /// </summary>
        /// <param name="label"></param>
        internal static void CenterLabel(string label, GUIStyle style)
        {
            GUILayout.BeginHorizontal(); // Start a horizontal layout group
            GUILayout.FlexibleSpace(); // Push content to the left
            GUILayout.Label(label, style);
            GUILayout.FlexibleSpace(); // Push content to the right
            GUILayout.EndHorizontal(); // End the horizontal layout group
        }
    }
}
