using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public class TestAction : BaseAction
    {
        private float _totalSpinAmount;
        private List<Unit> _targetUnitsList;
        //private int _targetAmount = 2;

        public override ActionArgs GenerateArgs()
        {
            return default;
        }

        protected override void Awake()
        {
            base.Awake();
            _targetUnitsList = new List<Unit>();
        }

        private void Update()
        {
            if (!IsActive) return;

            float spinAmount = 360f * Time.deltaTime;
            foreach (var target in _targetUnitsList)
            {
                target.transform.eulerAngles += new Vector3(0, spinAmount, 0);
            }
            _totalSpinAmount += spinAmount;
            if (_totalSpinAmount >= 360f)
            {
                ActionComplete();
            }
        }
        public override bool IsValidPositionOrTarget()
        {
            _targetUnitsList.Clear();
            _targetUnitsList.Add(UnitActionSystem.Instance.GetSelectedUnit());
            return _targetUnitsList.Count != 0;
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
            Actionstart(onActionComplete);
            _totalSpinAmount = 0f;
        }
    }
}
