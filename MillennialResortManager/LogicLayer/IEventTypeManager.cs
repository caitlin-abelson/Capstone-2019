using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IEventTypeManager
    {
        bool AddEventType(EventType newEventType);
        bool DeleteEventType(string eventType);
        List<string> RetrieveAllEventTypes();
        List<EventType> RetrieveAllEventTypes(string status);
    }
}