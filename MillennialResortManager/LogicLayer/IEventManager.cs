using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// @Author: Phillip Hansen
    /// @Created 2/8/2019
    /// 
    /// The interface for 'Event Manager'
    /// </summary>
    public interface IEventManager
    {
        void CreateEvent(Event newEvent);
        void UpdateEvent(Event oldEvent, Event newEvent);
        void DeleteEvent(Event purgeEvent);
        List<Event> RetrieveAllEvents();
        Event RetrieveEventByID(int eventId);
    }
}
