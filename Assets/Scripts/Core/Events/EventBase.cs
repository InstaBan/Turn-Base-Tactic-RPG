using System;
using System.Collections.Generic;

namespace LuminaStudio.Core.Events
{
    public class EventBase<TEnum> where TEnum : Enum
    {
        private Dictionary<TEnum, Action> eventHandlers = new Dictionary<TEnum, Action>();

        public void RegisterEvent(TEnum eventType, Action eventHandler)
        {
            eventHandlers.TryAdd(eventType, null);
            eventHandlers[eventType] += eventHandler;
        }

        public void UnregisterEvent(TEnum eventType, Action eventHandler)
        {
            eventHandlers[eventType] -= eventHandler;
        }

        public void TriggerEvent(TEnum eventType)
        {
            if (eventHandlers.TryGetValue(eventType, out Action handler))
            {
                handler?.Invoke();
            }
        }
    }
}