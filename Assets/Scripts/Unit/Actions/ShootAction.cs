using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public class ShootAction : BaseAction
    {
        private List<Unit> _possibleTargetUnitList;
        private Unit _targetUnit;
        [SerializeField]
        private float _range = 20f;

        public class ShootArgs : ActionArgs
        {
            public Unit TargetUnit;
        }
        public override ActionArgs GenerateArgs()
        {
            _targetUnit = UnitActionSystem.Instance.GetSelectedTargetUnit();
            return new ShootArgs() { TargetUnit = _targetUnit };
        }

        protected override void Awake()
        {
            base.Awake();
            _possibleTargetUnitList = new List<Unit>();
        }

        private void Update()
        {
            if (!IsActive)
                return;
            Debug.Log("Valid targets count: " 
                      + _possibleTargetUnitList.Count 
                      +"\n Selected target: " 
                      + _targetUnit.name);
            ActionComplete();
        }

        public override string GetActionName()
        {
            return "shoot";
        }

        public override void TakeAction(ActionArgs args, Action onActionComplete)
        {
            Actionstart(onActionComplete);
        }

        public override bool IsValidPositionOrTarget()
        {
            _possibleTargetUnitList = UnitActionSystem.Instance.GetViableTargetList(_range, false);
            var target = UnitActionSystem.Instance.GetSelectedTargetUnit();
            return target != null && _possibleTargetUnitList.Contains(target);
        }

        public override int GetActionResourceCost()
        {
            return 1;
        }
    }
}
