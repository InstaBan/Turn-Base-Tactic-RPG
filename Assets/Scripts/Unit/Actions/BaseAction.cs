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
            Animator = GetComponent<Animator>();
        }

        public abstract string GetActionName();

        public abstract void TakeAction(ActionArgs args, Action onActionComplete);


        public virtual bool IsValidGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validPositionsList = GetValidGridPositions();
            return validPositionsList.Contains(gridPosition);
        }

        public abstract List<GridPosition> GetValidGridPositions();

        public abstract int GetActionResourceCost();
    }
}
