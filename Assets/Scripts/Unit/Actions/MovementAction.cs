using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using LuminaStudio.Core.Scene.Combat;
using LuminaStudio.Grid;
using UnityEditor.Build;
using UnityEditor.Rendering;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    [CreateAssetMenu(fileName = "MovementAction", menuName = "Lumina/Scriptable/Action/MovementAction")]
    public class MovementAction : BaseAction
    {
        #region Events

        public event EventHandler OnstartMoving;
        public event EventHandler OnstopMoving;

        #endregion

        #region attributes
        private float _stoppingDistance = 0.1f;
        [SerializeField]
        private float _movementSpeed = 5f;

        // REPLACE WITH UNIT DATA LATER.
        [SerializeField] 
        private float _movementRange = 10f;
        private float _distanceToDestination;
        private Vector3 _destination;
        #endregion

        public class MoveArgs : EventArgs
        {
            public Vector3 TargetPosition;
        }

        public override EventArgs GenerateArgs()
        {
            var mousePos = InputManager.GetMousePosition();
            return new MoveArgs() { TargetPosition = mousePos };
        }

        public override void OnUpdate()
        {
            _distanceToDestination = Vector3.Distance(rootUnit.transform.position, _destination);
            if (_distanceToDestination >= _stoppingDistance)
            {
                var direction = (_destination - rootUnit.transform.position).normalized;
                rootUnit.transform.forward = Vector3.Lerp(rootUnit.transform.forward, direction, Time.deltaTime * 10f);
                rootUnit.transform.position += direction * _movementSpeed * Time.deltaTime;
            }
            else
            {
                OnstopMoving?.Invoke(this, EventArgs.Empty);
                unitManagerAction.ActionComplete();
            }
        }

        public override bool IsValidPositionOrTarget()
        {
            var mousePosition = InputManager.GetMousePosition();
            var distance = Vector3.Distance(rootUnit.transform.position, mousePosition);
            return distance < _movementRange;
        }

        #region Grid

        //public override List<GridPosition> GetValidGridPositions()
        //{
        //    List<GridPosition> validGridPositions = new List<GridPosition>();

        //    GridPosition unitGridPosition = rootUnit.GetGridPosition();

        //    for (int x = -_movementRange; x <= _movementRange; x++)
        //    {
        //        for (int z = -_movementRange; z <= _movementRange; z++)
        //        {
        //            GridPosition pos = new GridPosition(x, z);
        //            GridPosition testPos = unitGridPosition + pos;

        //            if (!ValidateGridPosition(unitGridPosition, testPos)) continue;
        //            validGridPositions.Add(testPos);
        //        }
        //    }
        //    return validGridPositions;
        //}

        //private bool ValidateGridPosition(GridPosition unitPosition, GridPosition targetPosition)
        //{
        //    if (!GridLevel.Instance.IsValidGridPosition(targetPosition))
        //    {
        //        return false;
        //    }

        //    if (unitPosition == targetPosition)
        //    {
        //        return false;
        //    }

        //    if (GridLevel.Instance.HasUnitOnGridPosition(targetPosition))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        #endregion


        public override string GetActionName()
        {
            return "Move";
        }

        public override int GetActionResourceCost()
        {
            return 1;
        }

        public override void TakeAction(EventArgs parameters, Action onActionComplete)
        {
            var movementParameters = (MoveArgs)parameters;
            unitManagerAction.Actionstart(onActionComplete);
            _destination = movementParameters.TargetPosition;
            OnstartMoving?.Invoke(this, EventArgs.Empty);
        }

        public float GetMovementRange()
        {
            return this._movementRange;
        }

        public override void OnActionSelected(object sender, EventArgs evt)
        {
            if (UnitActionSystem.Instance.GetSelectedAction() == this)
            {
                
            }
        }
        public override void OnUnitSelected(object sender, EventArgs evt)
        {

        }
    }
}
