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
            public Unit thisUnit;
        }

        #endregion
        private List<Unit> _possibleTargetUnitList;
        private Unit _targetUnit;
        private Quaternion _originalLookAt;
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
                LookAt(Vector3.Lerp(transform.position, InputManager.GetMousePosition(), Time.deltaTime * 10f));
            }
            if (!IsActive)
                return;
            ActionComplete();
        }

        private void LookAt(Vector3 position)
        {
            var forwardDirection = position - transform.position;
            var newRotation = Quaternion.LookRotation(forwardDirection, Vector3.up);
            transform.rotation = newRotation;
        }

        private void ResetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        private void Shoot()
        {
            _isAiming = false;
            LookAt(_targetUnit.GetWorldPosition());
            _originalLookAt = transform.rotation;

            //WARNING: TESTING PURPOSE
            _targetUnit.OnDamage(40);
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
                thisUnit = rootUnit
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
            if (UnitActionSystem.Instance.GetSelectedAction() == this)
            {
                _originalLookAt = transform.rotation;
            }
            if (UnitActionSystem.Instance.GetSelectedAction() != this)
            {
                _isAiming = false;
                ResetRotation(_originalLookAt);
            }
            else
            {
                _isAiming = true;
            }
        }
        public override void OnUnitSelected(object sender, EventArgs evt)
        {
            if (UnitActionSystem.Instance.GetSelectedUnit() == rootUnit) 
                return;
            _isAiming = false;
            if (UnitActionSystem.Instance.GetSelectedAction() == this)
            {
                ResetRotation(_originalLookAt);
            }
        }
    }
}
