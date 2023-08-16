using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public class ShootAction : BaseAction
    {
        private List<Unit> _possibleTargetUnitList;
        [SerializeField]
        private float _range = 20f;

        public class ShootArgs : ActionArgs
        {
            public Unit TargetUnit;
        }

        protected override void Awake()
        {
            base.Awake();
            _possibleTargetUnitList = new List<Unit>();
        }

        public override ActionArgs GenerateArgs()
        {
            var targetUnit = UnitActionSystem.Instance.GetSelectedTargetUnit();
            return new ShootArgs() { TargetUnit = targetUnit };
        }

        public override string GetActionName()
        {
            return "shoot";
        }

        public override void TakeAction(ActionArgs args, Action onActionComplete)
        {
            this.OnActionComplete = onActionComplete;
            IsActive = true;
            
            Debug.Log("ye");
            
            IsActive = false;
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
