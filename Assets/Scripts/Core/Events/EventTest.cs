using UnityEngine;

namespace LuminaStudio.Core.Events
{
    public  class EventTest
    {
        private EventBase eventManager = new();

        private void OnEvent1()
        {
            Debug.Log("Event 1 was triggered!");
        }
        public void RegisterEvent()
        {
            eventManager.RegisterEvent(OnEvent1);
        }

        public void TriggerEvent()
        {
            eventManager.TriggerEvent();
            eventManager.UnregisterEvent(OnEvent1);
        }
    }
}
