using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridBase
    {
        private int _width;
        private int _length;
        private float _gridSize;
        private GridObject [,] _gridObjects;

        public GridBase(int width, int length, float gridSize)
        {
            this._width = width;
            this._length = length;
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
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _length; z++)
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

        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            return gridPosition is { x: >= 0, z: >= 0 } 
                   && gridPosition.x < _width 
                   && gridPosition.z < _length;
        }

        public int GetWidth()
        {
            return _width;
        }

        public int GetLength()
        {
            return _length;
        }
    }
}
