using System;
using System.Collections.Generic;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit Unit;
        protected Animator Animator;
        protected bool IsActive;
        protected Action OnActionComplete;

        public partial class ActionArgs { }
        public abstract ActionArgs GenerateArgs();

        protected virtual void Awake()
        {
            Unit = GetComponent<Unit>();
            Animator = Unit.GetAnimator();
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
        #region Grid

        //public virtual bool IsValidGridPosition(GridPosition gridPosition)
        //{
        //    List<GridPosition> validPositionsList = GetValidGridPositions();
        //    return validPositionsList.Contains(gridPosition);
        //}

        //public abstract List<GridPosition> GetValidGridPositions();

        #endregion
    }
}
