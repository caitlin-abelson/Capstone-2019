using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MockEventAccessor : IEventAccessor
    {
        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// A fake Event Accessor for testing methods used in the real Event Accessor class
        /// </summary>


        private List<Event> _events;

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Creates a list of mock events
        /// </summary>
        public MockEventAccessor()
        {
            _events = new List<Event>();
            _events.Add(new Event() { EventID = 111000, EventTitle = "TestEvent1", Price = 100.50M,  EmployeeID = 100001, EventTypeID = "Beach Party", Description = "Testing", EventStartDate = DateTime.Now.AddDays(1).Date, EventEndDate = DateTime.Now.AddDays(2).Date, NumGuests = 100, SeatsRemaining = 50, KidsAllowed = false, Location = "TestLobby", Sponsored = false, Approved = false, PublicEvent = false });
            _events.Add(new Event() { EventID = 111000, EventTitle = "TestEvent1", Price = 100.50M, EmployeeID = 100001, EventTypeID = "Beach Party", Description = "Testing", EventStartDate = DateTime.Now.AddDays(3).Date, EventEndDate = DateTime.Now.AddDays(4).Date, NumGuests = 100, SeatsRemaining = 50, KidsAllowed = false, Location = "TestLobby", Sponsored = false, Approved = false, PublicEvent = false });
            _events.Add(new Event() { EventID = 111000, EventTitle = "TestEvent1", Price = 100.50M, EmployeeID = 100001, EventTypeID = "Beach Party", Description = "Testing", EventStartDate = DateTime.Now.AddDays(5).Date, EventEndDate = DateTime.Now.AddDays(6).Date, NumGuests = 100, SeatsRemaining = 50, KidsAllowed = false, Location = "TestLobby", Sponsored = false, Approved = false, PublicEvent = false });
            _events.Add(new Event() { EventID = 111000, EventTitle = "TestEvent1", Price = 100.50M, EmployeeID = 100001, EventTypeID = "Beach Party", Description = "Testing", EventStartDate = DateTime.Now.AddDays(7).Date, EventEndDate = DateTime.Now.AddDays(8).Date, NumGuests = 100, SeatsRemaining = 50, KidsAllowed = false, Location = "TestLobby", Sponsored = false, Approved = false, PublicEvent = false });
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Deletes an Event by an ID
        /// </summary>
        /// <param name="EventID"></param>
        public void deleteEventByID(int EventID)
        {
            bool eventExists = true;
            foreach (var _event in _events)
            {
                if(_event.EventID == EventID)
                {
                    if(_event.Approved == false)
                    {
                        _events.Remove(_events.Find(x => x.EventID == EventID));
                    }
                    break;
                }
                else
                {
                    eventExists = false;
                }
            }
            if (!eventExists)
            {
                throw new ArgumentException("No event found!");
            }
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Adds a new event to the event list
        /// </summary>
        /// <param name="newEvent"></param>
        public void insertEvent(Event newEvent)
        {
            _events.Add(newEvent);
        }

        public List<Event> selectAllCancelledEvents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Retrieves all events
        /// </summary>
        /// <returns></returns>
        public List<Event> selectAllEvents()
        {
            return _events;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Selects a specific event by an Event ID
        /// </summary>
        /// <param name="eventReqID"></param>
        /// <returns></returns>
        public Event selectEventById(int eventReqID)
        {
            Event _event = new Event();
            _event = _events.Find(x => x.EventID == eventReqID);
            if(_event == null)
            {
                throw new ArgumentException("Event associated with the ID could not be found!");
            }

            return _event;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Updates an event from old to new
        /// </summary>
        /// <param name="oldEvent"></param>
        /// <param name="newEvent"></param>
        public void updateEvent(Event oldEvent, Event newEvent)
        {
            foreach (var _event in _events)
            {
                if(_event.EventID == oldEvent.EventID)
                {
                    _event.EventTitle = newEvent.EventTitle;
                    _event.Price = newEvent.Price;
                    _event.EmployeeID = newEvent.EmployeeID;
                    _event.EmployeeName = newEvent.EmployeeName;
                    _event.EventTypeID = newEvent.EventTypeID;
                    _event.Description = newEvent.Description;
                    _event.EventStartDate = newEvent.EventStartDate;
                    _event.EventEndDate = newEvent.EventEndDate;
                    _event.KidsAllowed = newEvent.KidsAllowed;
                    _event.NumGuests = newEvent.NumGuests;
                    _event.SeatsRemaining = newEvent.SeatsRemaining;
                    _event.Location = newEvent.Location;
                    _event.Sponsored = newEvent.Sponsored;
                    _event.Approved = newEvent.Approved;
                }
            }
        }

        public void updateEventToCancelled(Event cancelEvent)
        {
            throw new NotImplementedException();
        }

        public void updateEventToUncancelled(Event uncancelEvent)
        {
            throw new NotImplementedException();
        }
    }
}
