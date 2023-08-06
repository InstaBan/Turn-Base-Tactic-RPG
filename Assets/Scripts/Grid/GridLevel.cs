using System.Collections.Generic;
using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridLevel : MonoBehaviour
    {
        public static GridLevel Instance { get; private set; }

        [SerializeField] 
        private Transform _gridDebugObjecTransform;
        private GridBase _gridBase;
        private GridObject _grid;
        private void Awake()
        {
            Instance = this;
            _gridBase = new GridBase(10, 10, 2f);
            _gridBase.DebugObjects(_gridDebugObjecTransform);
        }

        public void AddUnitAtGridPosition(GridPosition gridPosition, UnitBase unit)
        {
            _grid = _gridBase.GetGridObject(gridPosition);
            _grid.AddUnitOnGrid(unit);

        }
        public void RemoveUnitAtGridPosition(GridPosition gridPosition, UnitBase unit)
        {
            _grid = _gridBase.GetGridObject(gridPosition);
            _grid.RemoveUnitFromGird(unit);
        }
        public void UpdateUnitAtGridPosition(UnitBase unit, GridPosition oldPos, GridPosition newPos)
        {
            RemoveUnitAtGridPosition(oldPos, unit);
            AddUnitAtGridPosition(newPos, unit);
        }
        public List<UnitBase> GetUnitsAtGridPosition(GridPosition gridPosition)
        {
            _grid = _gridBase.GetGridObject(gridPosition);
            var units = _grid.GetUnitsOnGrid();
            return units ?? default;
        }

        public GridPosition GetGridPosition(Vector3 worldPos)
        {
            return _gridBase.GetGridPosition(worldPos);
        }
        public Vector3 GetWorldPosition(GridPosition worldPos)
        {
            return _gridBase.GetWorldPosition(worldPos);
        }


        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            return _gridBase.IsValidGridPosition(gridPosition);
        }

        public bool HasUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject grid = _gridBase.GetGridObject(gridPosition);
            return grid.HasAnyUnit();
        }
    }
}
