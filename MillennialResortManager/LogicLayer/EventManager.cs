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
    /// @Author Phillip Hansen
    /// @Created 1/23/2019
    /// 
    /// This class is for the Event objects in the logic layer, to be a building block between
    /// the Presentation Layer and the Data Access layer
    /// </summary>
    public class EventManager : IEventManager
    {
        private IEventAccessor _eventAccessor;

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Constructor for calling non-static methods
        /// </summary>
        public EventManager()
        {
            _eventAccessor = new EventAccessor();
        }

        public EventManager(MockEventAccessor _mockEventAccessor)
        {
            _eventAccessor = _mockEventAccessor;
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Method for creating an event calling to the accessor for events
        /// </summary>
        /// <param name="newEvent"></param> creates a new Event object called newEvent
        public void CreateEvent(Event newEvent)
        {

            try
            {
                if (!IsValid(newEvent))
                {
                    throw new ArgumentException("Input for the new event was invalid!");
                }
                _eventAccessor.insertEvent(newEvent);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Method for retrieving all events as a list
        /// </summary>
        /// <returns></returns>
        public List<Event> RetrieveAllEvents()
        {
            List<Event> events;

            try
            {
                events = _eventAccessor.selectAllEvents();
            }
            catch (Exception)
            {
                throw;
            }

            return events;
        }

        public Event RetrieveEventByID(int eventId)
        {
            Event _event;
            
            try
            {
                _event = _eventAccessor.selectEventById(eventId);
            }
            catch (Exception)
            {

                throw;
            }

            return _event;
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Updates the event
        /// </summary>
        /// <param name="oldEvent"></param> the old event
        /// <param name="newEvent"></param> the new event after updating
        public void UpdateEvent(Event oldEvent, Event newEvent)
        {

            try
            {
                if (!IsValid(newEvent))
                {
                    throw new ArgumentException("Input for the new event was invalid!");
                }
                _eventAccessor.updateEvent(oldEvent, newEvent);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Deletes the event by taking the object as a whole, and passes only the ID
        /// </summary>
        /// <param name="purgeEvent"></param> the event to be purged
        public void DeleteEvent(Event purgeEvent)
        {

            try
            {
                _eventAccessor.deleteEventByID(purgeEvent.EventID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool IsValid(Event _event)
        {
            if(ValidateStrings(_event) && ValidateDates(_event))
            {
                return true;
            }

            return false;
        }

        public bool ValidateStrings(Event _event)
        {
            if(_event.EventTitle == null || _event.EventTitle == "")
            {
                return false;
            }
            else if(_event.EventTitle.Length > 50)
            {
                return false;
            }
            else if(_event.EventTypeID == null || _event.EventTypeID == "")
            {
                return false;
            }
            else if(_event.EventTypeID.Length > 15)
            {
                return false;
            }
            else if (_event.Description.Length > 1000)
            {
                return false;
            }
            else if (_event.Location == null || _event.Location == "")
            {
                return false;
            }
            else if (_event.Location.Length > 50)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateDates(Event _event)
        {
            if(_event.EventStartDate.Date == null || _event.EventStartDate.Date <= DateTime.Now)
            {
                return false;
            }
            else if(_event.EventEndDate.Date == null || _event.EventEndDate.Date < _event.EventStartDate.Date)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
