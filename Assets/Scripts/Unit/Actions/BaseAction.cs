using System;
using System.Collections.Generic;
using LuminaStudio.Grid;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    [CreateAssetMenu(fileName = "BaseAction", menuName = "Lumina/Scriptable/Action/BaseAction")]
    public abstract class BaseAction : ScriptableObject
    {
        protected Unit rootUnit;
        protected UnitManagerAction unitManagerAction;

        protected virtual void Awake()
        {
            
        }
        public virtual void OnUpdate()
        {

        }
        public virtual void SetRootUnit(Unit unit)
        {
            rootUnit = unit;
            unitManagerAction = unit.GetManagerAction();
        }
        public abstract EventArgs GenerateArgs();

        public abstract string GetActionName();

        public abstract void TakeAction(EventArgs args, Action onActionComplete);

        public abstract bool IsValidPositionOrTarget();

        // REPLACE INT WITH TYPE/COST STRUCT OR CLASS LATER
        public abstract int GetActionResourceCost();

        
        public abstract void OnActionSelected(object sender, EventArgs evt);

        public abstract void OnUnitSelected(object sender, EventArgs evt);
    }
}
