using System;
using UnityEngine;

namespace LuminaStudio.Core.Events
{
    // WARNING: DO NOT ATTEMPT TO MAKE IT STATIC
    public class EventBase
    {
        public event Action MyEvent;

        public void RegisterEvent(Action eventHandler)
        {
            MyEvent += eventHandler;
        }

        public void UnregisterEvent(Action eventHandler)
        {
            MyEvent -= eventHandler;
        }

        public void TriggerEvent()
        {
            MyEvent?.Invoke();
        }
    }
}
