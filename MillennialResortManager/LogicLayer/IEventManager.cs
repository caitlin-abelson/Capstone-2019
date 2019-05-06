using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Phillip Hansen" created="2019/02/08">
	/// The interface for 'Event Manager'
	/// </summary>
	public interface IEventManager
    {
        int CreateEvent(Event newEvent);
        void UpdateEvent(Event oldEvent, Event newEvent);
        void DeleteEvent(Event purgeEvent);
        List<Event> RetrieveAllEvents();
        Event RetrieveEventByID(int eventId);
    }
}
