using System;
using LuminaStudio.Core.Input;
using LuminaStudio.Unit;
using LuminaStudio.Unit.Actions;
using UnityEngine;

namespace LuminaStudio.UI.Indicators
{
    public class LineDrawer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private bool _isActive;
        private Unit.Unit _unit;
        private MovementAction _action;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
        }
        private void Start()
        {
            // listen to events so no need to constant update check
            UnitActionSystem.Instance.OnSelectedActionChanged += OnMovementAction;
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnUnitSelected;
        }

        private void Update()
        {
            // return if not active [optimisation++]
            if (!_isActive)
                return;

            var unitPosition = _unit.GetWorldPosition();
            var mousePosition = InputManager.GetMousePosition();

            var direction = (mousePosition - unitPosition).normalized;
            // currently movement range is attached to action, will be placed in unit later.
            var distance = Mathf.Clamp(Vector3.Distance(unitPosition, mousePosition), 0, _action.GetMovementRange());
            var endPosition = unitPosition + direction * distance;

            _lineRenderer.SetPosition(0, unitPosition);
            _lineRenderer.SetPosition(1, endPosition);

        }

        #region Listeners
        
        // update selected unit
        private void OnUnitSelected(object sender, EventArgs evt)
        {
            _unit = UnitActionSystem.Instance.GetSelectedUnit();
        }

        // check if selected action == MovementAction
        private void OnMovementAction(object sender, EventArgs evt)
        {
            var selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            if (selectedAction is MovementAction action)
            {
                _action = action;
                _isActive = true;
                _lineRenderer.enabled = true;
            }
            else
            {
                _isActive = false;
                _lineRenderer.enabled = false;
            }

        }

        #endregion

    }
}
