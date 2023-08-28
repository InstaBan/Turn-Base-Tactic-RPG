using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuminaStudio.Scriptable.Data.Visual
{

    [CreateAssetMenu(fileName = "ColorData", menuName = "Lumina/Scriptable/Data/Visual/Color Data")]
    public class ColorDataScriptable : ScriptableObject
    {
        [Header("Color Collection")]
        public List<ColorEntry> ColorList = new();
    }

    [Serializable]
    public class ColorEntry
    {
        public string colorName;
        public Color colorValue = Color.white;
    }
}
