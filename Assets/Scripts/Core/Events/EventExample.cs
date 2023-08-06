using UnityEngine;

namespace LuminaStudio.Core.Events
{
    public  class EventExample
    {
        private EventBase<EventTestCase> eventManager = new();

        private void OnEvent1()
        {
            Debug.Log("Event 1 was triggered!");
        }
        public void RegisterEvent()
        {
            eventManager.RegisterEvent(EventTestCase.test1, OnEvent1);
        }

        public void TriggerEvent()
        {
            eventManager.TriggerEvent(EventTestCase.test1);
            eventManager.UnregisterEvent(EventTestCase.test1, OnEvent1);
        }
    }

    public enum EventTestCase
    {
        test1,
        test2,
    }
}
