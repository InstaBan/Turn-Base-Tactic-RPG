using System;
using UnityEngine;

namespace LuminaStudio.Unit.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit Unit;
        protected Animator Animator;
        protected bool IsActive;
        protected Action OnActionComplete;

        protected virtual void Awake()
        {
            Unit = GetComponent<Unit>();
            Animator = GetComponent<Animator>();
        }
    }
}
