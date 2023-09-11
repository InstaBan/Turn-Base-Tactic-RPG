using System;
using System.Collections.Generic;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit rootUnit;
        protected bool IsActive;
        protected Action OnActionComplete;

        public partial class ActionArgs { }
        public abstract ActionArgs GenerateArgs();

        protected virtual void Awake()
        {
            rootUnit = GetComponent<Unit>();
        }

        public abstract string GetActionName();

        public abstract void TakeAction(ActionArgs args, Action onActionComplete);

        public abstract bool IsValidPositionOrTarget();

        // REPLACE INT WITH TYPE/COST STRUCT OR CLASS LATER
        public abstract int GetActionResourceCost();

        protected void Actionstart(Action onActionComplete)
        {
            IsActive = true;
            this.OnActionComplete = onActionComplete;
        }

        protected void ActionComplete()
        {
            IsActive = false;
            OnActionComplete();
        }
        public abstract void OnActionSelected(object sender, EventArgs evt);

        public abstract void OnUnitSelected(object sender, EventArgs evt);
    }
}
