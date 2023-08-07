using System.Collections.Generic;
using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridTest : MonoBehaviour
    {
        [SerializeField]
        private UnitBase _unit;
        private List<GridPosition> _validPositions;
        private void Start()
        {
            
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                GridSystemVisual.Instance.HideAllVisual();
                _validPositions = _unit.GetUnitMovement().GetValidGridPositions();
                GridSystemVisual.Instance.ShowAllVisual(_validPositions);
            }
        }

        public List<GridPosition> GetValidPositions()
        {
            return _validPositions;
        }
    }
}
