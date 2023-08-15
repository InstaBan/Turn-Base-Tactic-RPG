using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;
using LuminaStudio.Unit.Actions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LuminaStudio.Unit
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }

        public event EventHandler OnSelectedUnitChanged;
        public event EventHandler OnSelectedActionChanged;
        public event EventHandler<bool> OnBusyChanged;
        public event EventHandler OnActionStarted;
        [SerializeField]
        private Unit _selectedUnit;

        private List<Unit> _targetUnitList;
        private LayerMask _layerMask;
        private bool _isBusy;
        private BaseAction _selectedAction;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Error: Duplicate UnitActionSystem Found in: " + transform + " / " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _layerMask = LayerMask.GetMask("Unit");
        }
        private void Update()
        {
            if (_isBusy) return;
            if (SelectUnit()) return;

            // Check if mouse on UI element
            if (EventSystem.current.IsPointerOverGameObject()) return;

            ExecuteSelectedAction();
        }

        private void EnterBusy()
        {
            _isBusy = true;
            OnBusyChanged?.Invoke(this, _isBusy);
        }

        private void QuitBusy()
        {
            _isBusy = false;
            OnBusyChanged?.Invoke(this, _isBusy);
        }
        private bool SelectUnit()
        {
            if (!InputManager.IsMouseLeftClicked())
                return false;

            var ray = InputManager.GetRayCast();
            if (Physics.Raycast(ray, out RaycastHit _hit, float.MaxValue, _layerMask) 
                && _hit.transform.TryGetComponent<Unit>(out Unit unit) 
                && unit != _selectedUnit && unit.isPlayerFaction())
            {
                SetSelectedUnit(unit);
                return true;
            }

            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            _selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ExecuteSelectedAction()
        {
            if (InputManager.IsMouseLeftClicked())
            {
                //var mouseGridPosition =
                //    GridLevel.Instance.GetGridPosition(InputManager.GetMousePosition());
                if (_selectedAction == null) return;
                //if (!_selectedAction.IsValidGridPosition(mouseGridPosition)) return;
                if (!_selectedAction.IsValidPositionOrTarget()) return;
                if (!_selectedUnit.TryExecuteAction(_selectedAction)) return;

                EnterBusy();
                var args = _selectedAction.GenerateArgs();
                _selectedAction.TakeAction(args, QuitBusy);
                OnActionStarted.Invoke(this, EventArgs.Empty);
            }
        }

        public BaseAction GetSelectedAction()
        {
            return _selectedAction;
        }

        public void SetSelectedAction(BaseAction baseAction)
        {
            _selectedAction = baseAction;
            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }
        public bool IsSelectedAction(BaseAction action)
        {
            return action == _selectedAction;
        }

        public Unit GetSelectedUnit()
        {
            return _selectedUnit;
        }
    }
}
