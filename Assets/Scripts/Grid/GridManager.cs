using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridManager
    {
        private int width;
        private int length;
        private float gridSize;

        public GridManager(int width, int length, float gridSize)
        {
            this.width = width;
            this.length = length;
            this.gridSize = gridSize;

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    Debug.DrawLine(GetWorldPosition(x,z), GetWorldPosition(x,z) + Vector3.right * .2f, Color.white, 1000);
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x,0,z) * gridSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / gridSize),
                Mathf.RoundToInt(worldPosition.z / gridSize));
        }
    }
}
