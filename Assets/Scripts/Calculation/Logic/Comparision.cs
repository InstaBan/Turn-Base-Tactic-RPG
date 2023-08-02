using UnityEngine;

namespace LuminaStudio.Calculation.Logic
{
    public class Comparision
    {
        public static bool IsMatch<T>(T obj_a, T obj_b)
        {
            return Equals(obj_a, obj_b);
        }
    }
}
