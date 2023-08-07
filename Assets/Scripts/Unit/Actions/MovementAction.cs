using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public class MovementAction : BaseAction
    {
        #region attributes
        private float _stoppingDistance = 0.1f;
        [SerializeField]
        private float _movementSpeed = 5f;

        [SerializeField] private int _movementRadius = 4;
        private float _distanceToDestination;
        private Vector3 _destination;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            _destination = transform.position;
        }

        private void Start()
        {

        }
        private void Update()
        {
            if (!IsActive) return;

            _distanceToDestination = Vector3.Distance(transform.position, _destination);
            if (_distanceToDestination >= _stoppingDistance)
            {
                var direction = (_destination - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 10f);
                transform.position += direction * _movementSpeed * Time.deltaTime;
                Animator.SetBool("isMoving", true);
            }
            else
            {
                Animator.SetBool("isMoving", false);
                IsActive = false;
                OnActionComplete();
            }
        }

        internal void SetDestination()
        {
            _destination = InputManager.GetMousePosition();
        }
        internal void SetDestination(Vector3 destination)
        {
            _destination = destination;
        }

        internal void SetDestination(GridPosition gridPosition, Action onActionComplete)
        {
            this.OnActionComplete = onActionComplete;
            _destination = GridLevel.Instance.GetWorldPosition(gridPosition);
            IsActive = true;
        }

        public List<GridPosition> GetValidGridPositions()
        {
            List<GridPosition> validGridPositions = new List<GridPosition>();

            GridPosition unitGridPosition = Unit.GetGridPosition();

            for (int x = -_movementRadius; x <= _movementRadius; x++)
            {
                for (int z = -_movementRadius; z <= _movementRadius; z++)
                {
                    GridPosition pos = new GridPosition(x, z);
                    GridPosition testPos = unitGridPosition + pos;

                    if (!ValidateGridPosition(unitGridPosition, testPos)) continue;
                    validGridPositions.Add(testPos);
                }
            }
            return validGridPositions;
        }

        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validPositionsList = GetValidGridPositions();
            return validPositionsList.Contains(gridPosition);
        }

        private bool ValidateGridPosition(GridPosition unitPosition, GridPosition targetPosition)
        {
            if (!GridLevel.Instance.IsValidGridPosition(targetPosition))
            {
                return false;
            }

            if (unitPosition == targetPosition)
            {
                return false;
            }

            if (GridLevel.Instance.HasUnitOnGridPosition(targetPosition))
            {
                return false;
            }
            return true;
        }
    }
}
