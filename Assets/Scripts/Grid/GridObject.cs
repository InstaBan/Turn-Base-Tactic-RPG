using System.Collections.Generic;
using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridObject
    {
        private GridBase _gridBase;
        private GridPosition _gridPosition;
        private List<Unit.Unit> _unitList;

        public GridObject(GridBase gridBase, GridPosition gridPosition)
        {
            this._gridBase = gridBase;
            this._gridPosition = gridPosition;
            _unitList = new();
        }

        public GridPosition GetGridPosition()
        {
            return _gridPosition;
        }

        public void AddUnitOnGrid(Unit.Unit unit)
        {
            _unitList.Add(unit);
        }

        public void RemoveUnitFromGird(Unit.Unit unit)
        {
            _unitList.Remove(unit);
        }

        public List<Unit.Unit> GetUnitsOnGrid()
        {
            return _unitList;
        }

        public bool HasAnyUnit()
        {
            return _unitList.Count > 0;
        }

        public override string ToString()
        {
            string output = _gridPosition.ToString();
            if (_unitList == null) return output;
            for (int i = 0; i < _unitList.Count; i++)
            {
                output += ("\n" + _unitList[i].name);
            }
            return output;
        }
    }
}
