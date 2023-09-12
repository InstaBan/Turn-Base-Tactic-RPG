using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using LuminaStudio.Core.Scene.Combat;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    [CreateAssetMenu(fileName = "TestAction", menuName = "Lumina/Scriptable/Action/TestAction")]
    public class TestAction : BaseAction
    {
        private float _totalSpinAmount;
        private List<Unit> _targetUnitsList;
        //private int _targetAmount = 2;

        public override EventArgs GenerateArgs()
        {
            return default;
        }

        protected override void Awake()
        {
            base.Awake();
            _targetUnitsList = new List<Unit>();
        }

        public override void OnUpdate()
        {
            float spinAmount = 360f * Time.deltaTime;
            foreach (var target in _targetUnitsList)
            {
                target.transform.eulerAngles += new Vector3(0, spinAmount, 0);
            }
            _totalSpinAmount += spinAmount;
            if (_totalSpinAmount >= 360f)
            {
                unitManagerAction.ActionComplete();
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

        public override void TakeAction(EventArgs args, Action onActionComplete)
        {
            unitManagerAction.Actionstart(onActionComplete);
            _totalSpinAmount = 0f;
        }

        public override void OnActionSelected(object sender, EventArgs evt)
        {
            
        }
        public override void OnUnitSelected(object sender, EventArgs evt)
        {

        }
    }
}
