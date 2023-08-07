using System.Collections.Generic;
using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }

        [SerializeField] 
        private Transform gridVisualPrefab;
        private GridVisual[,] _gridVisualArray;
        private int _width;
        private int _length;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Error: Duplicate GridSystemVisual Found in: " + transform + " / " + Instance);
                Destroy(gameObject);
            }
            Instance = this;
        }
        private void Start()
        {
            _width = GridLevel.Instance.GetWidth();
            _length = GridLevel.Instance.GetLength();
            _gridVisualArray = new GridVisual[_width, _length];
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _length; z++)
                {
                    var pos = new GridPosition(x, z);
                    var gridVisualTransform = Instantiate(gridVisualPrefab, 
                                        GridLevel.Instance.GetWorldPosition(pos), 
                                        Quaternion.identity);
                    _gridVisualArray[x, z] = gridVisualTransform.GetComponent<GridVisual>();
                }
            }

            HideAllVisual();
        }

        private void Update()
        {
            // WARNING: Do not call it in Update, it is here for test build only.
            UpdateVisual();
        }

        public void HideAllVisual()
        {
            foreach (var visual in _gridVisualArray)
            {
                visual.HideVisual();
            }
        }
        // Only shows the validated grids
        public void ShowAllVisual(List<GridPosition> positions)
        {
            positions?.ForEach(position => _gridVisualArray[position.x, position.z]?.ShowVisual());
        }

        private void UpdateVisual()
        {
            HideAllVisual();
            // WARNING: Please call it with event later
            Unit.Unit unit = UnitActionSystem.GetSelectedUnit();
            if (unit == null) return;
            ShowAllVisual(unit.GetUnitMovement().GetValidGridPositions());
        }
    }
}
