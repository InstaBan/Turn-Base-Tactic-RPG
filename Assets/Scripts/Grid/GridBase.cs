using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridBase
    {
        private int width;
        private int length;
        private float _gridSize;
        private GridObject [,] _gridObjects;

        public GridBase(int width, int length, float gridSize)
        {
            this.width = width;
            this.length = length;
            this._gridSize = gridSize;

            _gridObjects = new GridObject[width, length];
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    GridPosition gridPosition = new(x,z);
                    _gridObjects[x, z] = new GridObject(this, gridPosition);
                }
            }
            
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return new Vector3(gridPosition.x, 0, gridPosition.z) * _gridSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _gridSize),
                Mathf.RoundToInt(worldPosition.z / _gridSize));
        }

        public void DebugObjects(Transform debugTransform)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform debugtTransform = GameObject.Instantiate(debugTransform, GetWorldPosition(gridPosition), Quaternion.identity);
                    GridDebugObject gridDebugObject = debugtTransform.GetComponent<GridDebugObject>();
                    gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }

        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return _gridObjects[gridPosition.x, gridPosition.z];
        }
    }
}
