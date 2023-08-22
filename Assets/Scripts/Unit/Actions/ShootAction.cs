using System;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public class ShootAction : BaseAction
    {
        #region Events

        public event EventHandler<OnShootEventArgs> OnstartShoot;

        public class ShootArgs : ActionArgs
        {
            public Unit TargetUnit;
        }

        public class OnShootEventArgs : EventArgs
        {
            public Unit targetUnit;
            public Unit rootUnit;
        }

        #endregion
        private List<Unit> _possibleTargetUnitList;
        private Unit _targetUnit;
        private Vector3 _originalPosition;
        [SerializeField]
        private float _range = 20f;

        private bool _isAiming;

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

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedActionChanged += OnActionSelected;
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnUnitSelected;
        }

        private void Update()
        {
            if (_isAiming)
            {
                transform.forward = Vector3.Lerp(transform.forward, InputManager.GetMousePosition(), Time.deltaTime * 10f);
            }
            if (!IsActive)
                return;
            Debug.Log("Valid targets count: " 
                      + _possibleTargetUnitList.Count 
                      +"\n Selected target: " 
                      + _targetUnit.name);
            ActionComplete();
        }

        private void LookAt(Vector3 position)
        {
            transform.forward = position;
        }

        private void Shoot()
        {
            LookAt(_targetUnit.GetWorldPosition());
            _isAiming = false;
            _targetUnit.OnDamage();
        }

        public override string GetActionName()
        {
            return "shoot";
        }

        public override void TakeAction(ActionArgs args, Action onActionComplete)
        {
            Actionstart(onActionComplete);
            Shoot();
            OnstartShoot?.Invoke(this, new OnShootEventArgs()
            {
                targetUnit = _targetUnit,
                rootUnit = rootUnit
            });
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

        public override void OnActionSelected(object sender, EventArgs evt)
        {
            _originalPosition = rootUnit.GetWorldPosition();
            _isAiming = UnitActionSystem.Instance.GetSelectedAction() == this;
        }
        public override void OnUnitSelected(object sender, EventArgs evt)
        {
            var unit = UnitActionSystem.Instance.GetSelectedUnit();
            if (UnitActionSystem.Instance.GetSelectedUnit() != rootUnit)
            {
                _isAiming = false;
                LookAt(_originalPosition);
            }
        }
    }
}
