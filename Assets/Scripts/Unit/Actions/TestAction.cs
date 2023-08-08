using System;
using System.Collections.Generic;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public class TestAction : BaseAction
    {
        private float _totalSpinAmount;

        public override ActionArgs GenerateArgs()
        {
            return default;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            if (!IsActive) return;

            float spinAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAmount, 0);
            _totalSpinAmount += spinAmount;
            if (_totalSpinAmount >= 360f)
            {
                IsActive = false;
                OnActionComplete();
            }
        }
        public override List<GridPosition> GetValidGridPositions()
        {
            List<GridPosition> validGridPositions = new List<GridPosition>();
            GridPosition unitGridPosition = Unit.GetGridPosition();

            // only need current selected unit's position
            return new List<GridPosition>()
            {
                unitGridPosition
            };
        }

        public override string GetActionName()
        {
            return "Test";
        }

        public override int GetActionResourceCost()
        {
            return 1;
        }

        public override void TakeAction(ActionArgs args, Action onActionComplete)
        {
            this.OnActionComplete = onActionComplete;
            _totalSpinAmount = 0f;
            IsActive = true;
        }
    }
}
