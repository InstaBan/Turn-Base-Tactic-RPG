using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridObject
    {
        private GridBase _gridBase;
        private GridPosition _gridPosition;

        public GridObject(GridBase gridBase, GridPosition gridPosition)
        {
            this._gridBase = gridBase;
            this._gridPosition = gridPosition;
        }

        public GridPosition GetGridPosition()
        {
            return _gridPosition;
        }

        public override string ToString()
        {
            return _gridPosition.ToString();
        }
    }
}
