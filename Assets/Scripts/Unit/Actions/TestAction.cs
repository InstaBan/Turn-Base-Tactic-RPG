using System;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public class TestAction : BaseAction
    {
        private float _totalSpinAmount;

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
        public void Spin(Action onActionComplete)
        {
            this.OnActionComplete = onActionComplete;
            _totalSpinAmount = 0f;
            IsActive = true;
        }
    }
}
