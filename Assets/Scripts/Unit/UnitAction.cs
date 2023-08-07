using System;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitAction : MonoBehaviour
    {
        public static UnitAction Instance { get; private set; }
        public event EventHandler OnSelectedUnitChanged;
        [SerializeField]
        private static UnitBase _selectedUnit;

        private LayerMask _layerMask;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Error: Duplicate UnitAction Found in: " + transform + " / " + Instance);
                Destroy(gameObject);
            }
            Instance = this;
            _layerMask = LayerMask.GetMask("Unit");
        }
        private void Update()
        {
            if (InputManager.IsMouseLeftClicked())
            {
                if (!SelectUnit() && _selectedUnit != null)
                {
                    GridPosition mouseGridPosition = 
                        GridLevel.Instance.GetGridPosition(InputManager.GetMousePosition());
                    if (_selectedUnit.GetUnitMovement().IsValidGridPosition(mouseGridPosition))
                    {
                        _selectedUnit.GetUnitMovement().SetDestination(mouseGridPosition);
                    }
                }
            }
        }

        private bool SelectUnit()
        {
            var ray = InputManager.GetRayCast();
            if (Physics.Raycast(ray, out RaycastHit _hit, float.MaxValue, _layerMask))
            {
                if (_hit.transform.TryGetComponent<UnitBase>(out UnitBase unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            return false;
        }

        private void SetSelectedUnit(UnitBase unit)
        {
            _selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }
        public static UnitBase GetSelectedUnit()
        {
            return _selectedUnit;
        }
    }
}
