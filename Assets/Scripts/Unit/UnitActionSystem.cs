using System;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }
        public event EventHandler OnSelectedUnitChanged;
        [SerializeField]
        private static Unit _selectedUnit;
        private LayerMask _layerMask;
        private bool _isBusy;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Error: Duplicate UnitActionSystem Found in: " + transform + " / " + Instance);
                Destroy(gameObject);
            }
            Instance = this;
            _layerMask = LayerMask.GetMask("Unit");
        }
        private void Update()
        {
            if (_isBusy) return;

            if (InputManager.IsMouseLeftClicked())
            {
                if (!SelectUnit() && _selectedUnit != null)
                {
                    GridPosition mouseGridPosition = 
                        GridLevel.Instance.GetGridPosition(InputManager.GetMousePosition());
                    if (_selectedUnit.GetUnitMovement().IsValidGridPosition(mouseGridPosition))
                    {
                        EnterBusy();
                        _selectedUnit.GetUnitMovement().SetDestination(mouseGridPosition, QuitBusy);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                EnterBusy();
                if (_selectedUnit != null) _selectedUnit.GeTestAction().Spin(QuitBusy);
            }
        }

        private void EnterBusy()
        {
            _isBusy = true;
        }

        private void QuitBusy()
        {
            _isBusy = false;
        }
        private bool SelectUnit()
        {
            var ray = InputManager.GetRayCast();
            if (Physics.Raycast(ray, out RaycastHit _hit, float.MaxValue, _layerMask))
            {
                if (_hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            _selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }
        public static Unit GetSelectedUnit()
        {
            return _selectedUnit;
        }
    }
}
