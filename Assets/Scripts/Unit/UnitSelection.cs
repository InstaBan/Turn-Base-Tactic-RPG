using System;
using LuminaStudio.Core.Input;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitSelection : MonoBehaviour
    {
        public static UnitSelection Instance { get; private set; }
        public event EventHandler OnSelectedUnitChanged;
        [SerializeField]
        private static UnitBase _selectedUnit;

        private LayerMask _layerMask;

        private void Awake()
        {
            Instance = this;
            _layerMask = LayerMask.GetMask("Unit");
        }
        private void Update()
        {
            if (!InputManager.IsMouseClicked()) return;
            if (!SelectUnit() && _selectedUnit != null)
            {
                _selectedUnit.SetDestination();
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
