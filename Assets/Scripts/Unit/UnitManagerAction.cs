using System;
using System.Collections.Generic;
using LuminaStudio.Core.Scene.Combat;
using LuminaStudio.Unit.Actions;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitManagerAction : MonoBehaviour
    {
        [SerializeField]
        internal List<BaseAction> _actions = new List<BaseAction>();
        private BaseAction _action;
        public bool IsActive;
        protected Action OnActionComplete;
        // ADD SOME SORT OF FUNCTION TO GRAB THE CORRESPONDING ACTIONS!

        private void Awake()
        {

        }

        private void Start()
        {
            foreach (var action in _actions)
            {
                action.SetRootUnit(this.gameObject.GetComponent<Unit>());
            }

            UnitActionSystem.Instance.OnSelectedActionChanged += OnActionSelected;
        }

        private void Update()
        {
            if (!IsActive) return;
            _action.OnUpdate();
        }

        private void OnActionSelected(object sender, EventArgs args)
        {
            foreach (var action in _actions)
            {
                if (action == UnitActionSystem.Instance.GetSelectedAction())
                {
                    _action = action;
                }
            }
        }

        public void Actionstart(Action onActionComplete)
        {
            IsActive = true;
            this.OnActionComplete = onActionComplete;
        }

        public void ActionComplete()
        {
            IsActive = false;
            OnActionComplete();
        }
    }
}
