using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IEventAccessor
    {
        /// <summary>
        /// Author: Phillip Hansen
        /// Created: 2/8/2019
        /// 
        /// IEventAccessor is an interface for interacting with Event Accessor
        /// </summary>
        /// <param name="newEvent"></param>

        void insertEvent(Event newEvent);
        void updateEvent(Event oldEvent, Event newEvent);
        void deleteEventByID(int EventID);
        List<Event> selectAllEvents();
        Event selectEventById(int eventReqID);
    }
}
