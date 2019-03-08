using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class EventTypeAccessorMock : IEventTypeAccessor
    {
        /// <summary>
        /// Craig Barkley
        /// Created: 2019/02/28
        /// 
        /// This is a mock Data Accessor which implements IEventTypeAccessor.  This is for testing purposes only.
        /// </summary>
        /// 

        private List<EventType> eventType;
        /// <summary>
        /// Author: Craig Barkley
        /// Created: 2019/02/28
        /// This constructor that sets up dummy data
        /// </summary>
        public EventTypeAccessorMock()
        {
            eventType = new List<EventType>
            {
                new EventType {EventTypeID = "EventType1", Description = "EventType is a eventType"},
                new EventType {EventTypeID = "EventType2", Description = "EventType is a eventType"},
                new EventType {EventTypeID = "EventType3", Description = "EventType is a eventType"},
                new EventType {EventTypeID = "EventType4", Description = "EventType is a eventType"}
            };
        }

        public int CreateEventType(EventType newEventType)
        {
            int listLength = eventType.Count;
            eventType.Add(newEventType);
            if (listLength == eventType.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteEventType(string eventTypeID)
        {
            int rowsDeleted = 0;
            foreach (var type in eventType)
            {
                if (type.EventTypeID == eventTypeID)
                {
                    int listLength = eventType.Count;
                    eventType.Remove(type);
                    if (listLength == eventType.Count - 1)
                    {
                        rowsDeleted = 1;
                    }
                }
            }

            return rowsDeleted;
        }

        public List<string> SelectAllEventTypeID()
        {
            throw new NotImplementedException();
        }

        public List<EventType> RetrievetAllEventTypes(string status)
        {
            return eventType;
        }
    }
}


