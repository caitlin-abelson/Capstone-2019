using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IEventTypeAccessor
    {
        int CreateEventType(EventType newEventType);
        int DeleteEventType(string eventTypeID);
        List<EventType> RetrievetAllEventTypes(string status);
        List<string> SelectAllEventTypeID();
    }
}