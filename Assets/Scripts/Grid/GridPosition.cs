using System;

namespace LuminaStudio.Grid
{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int x;
        public int z;
        public GridPosition(int x, int z)
        {
            this.x = x; 
            this.z = z;
        }
        public bool Equals(GridPosition other)
        {
            return x == other.x && z == other.z;
        }

        public override bool Equals(object obj)
        {
            return obj is GridPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, z);
        }
        public static bool operator ==(GridPosition posA, GridPosition posB)
        {
            return posA.x == posB.x && posA.z == posB.z;
        }

        public static bool operator !=(GridPosition posA, GridPosition posB)
        {
            return !(posA == posB);
        }

        public override string ToString()
        {
            return x + " / " + z;
        }
    }
}
