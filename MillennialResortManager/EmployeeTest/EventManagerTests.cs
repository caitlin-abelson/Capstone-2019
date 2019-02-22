using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessLayer;
using DataObjects;

namespace UnitTests
{

    /// <summary>
    /// @Author Phillip Hansen
    /// @Created 2/15/2019
    /// 
    /// Test Class for the Event data object
    /// </summary>
    [TestClass]
    public class EventManagerTests
    {
        private MockEventAccessor _eventAccessor;
        private IEventManager _eventManager;
        private List<Event> _events;
        

        [TestInitialize]
        public void testSetup()
        {
            _eventAccessor = new MockEventAccessor();
            _eventManager = new EventManager(_eventAccessor);
            _events = new List<Event>();
            _events = _eventManager.RetrieveAllEvents();
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// All tests for creating a new event data object
        /// </summary>
        [TestMethod]
        public void TestCreateEventCorrect()
        {
            //Arrange
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Testing Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            //Act
            _eventManager.CreateEvent(newEvent);


            //Assert
            Assert.IsNotNull(_events.Find(x => 
                        x.EventID == newEvent.EventID 
                    && x.EventTitle == newEvent.EventTitle 
                    && x.EmployeeID == newEvent.EmployeeID
                    && x.EventTypeID == newEvent.EventTypeID 
                    && x.Description == newEvent.Description
                    && x.EventStartDate == newEvent.EventStartDate 
                    && x.EventEndDate == newEvent.EventEndDate
                    && x.KidsAllowed == newEvent.KidsAllowed
                    && x.NumGuests == newEvent.NumGuests
                    && x.Location == newEvent.Location
                    && x.Sponsored == newEvent.Sponsored
                    && x.SponsorID == newEvent.SponsorID 
                    && x.Approved == newEvent.Approved
                 ));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectEventTitleNull()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = null,
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Testing Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectEventTitleTooLong()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = createString(51),
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Testing Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectEventTypeIDNull()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = null,
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Testing Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectEventTypeIDTooLong()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = createString(16),
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Testing Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectDescriptionTooLong()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = createString(1001),
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Testing Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectLocationNull()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = null,
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectLocationTooLong()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(1).Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = createString(51),
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectStartDateToday()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = "This is a test",
                EventStartDate = DateTime.Now.Date,
                EventEndDate = DateTime.Now.AddDays(2).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Test Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventIncorrectEndDateBeforeStartDate()
        {
            Event newEvent = new Event()
            {
                EventID = 121000,
                EventTitle = "CreateEventTest",
                EmployeeID = 100000,
                EventTypeID = "Concert Event",
                Description = "This is a test",
                EventStartDate = DateTime.Now.AddDays(4).Date,
                EventEndDate = DateTime.Now.AddDays(3).Date,
                KidsAllowed = false,
                NumGuests = 100,
                Location = "Test Lobby",
                Sponsored = false,
                SponsorID = 0,
                Approved = false
            };

            _eventManager.CreateEvent(newEvent);
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Tests for retrieving all events
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventListCorrect()
        {
            List<Event> events = null;

            events = _eventManager.RetrieveAllEvents();

            CollectionAssert.Equals(_events, events);
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Tests for updating an event object
        /// </summary>
        [TestMethod]
        public void TestUpdateEventCorrect()
        {
            Event newEvent = new Event();
            setEvent(_events[1], newEvent);
            string updateDesc = "This description has been successfully updated through testing.";
            newEvent.Description = updateDesc;

            _eventManager.UpdateEvent(_events[1], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[1].EventID).Description, newEvent.Description);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectEventTitleNull()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            string updateTitle = null;
            newEvent.EventTitle = updateTitle;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).EventTitle, newEvent.EventTitle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectEventTitleTooLong()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            string updateTitle = createString(51);
            newEvent.EventTitle = updateTitle;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).EventTitle, newEvent.EventTitle);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectEventTypeIDNull()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            string updateType = null;
            newEvent.EventTypeID = updateType;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).EventTypeID, newEvent.EventTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectEventTypeIDTooLong()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            string updateType = createString(16);
            newEvent.EventTypeID = updateType;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).EventTypeID, newEvent.EventTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectDescriptionTooLong()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            string updateDesc = createString(1001);
            newEvent.Description = updateDesc;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).Description, newEvent.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectLocationNull()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            string updateLoc = null;
            newEvent.Location = updateLoc;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).Location, newEvent.Location);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectLocationTooLong()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            string updateLoc = createString(51);
            newEvent.Location = updateLoc;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).Location, newEvent.Location);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectStartDateToday()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            DateTime updateStartDate = DateTime.Now.Date;
            newEvent.EventStartDate = updateStartDate;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).EventStartDate, newEvent.EventStartDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEventIncorrectEndDateBeforeStartDate()
        {
            Event newEvent = new Event();
            setEvent(_events[2], newEvent);
            DateTime updateEndDate = DateTime.Now.Date;
            newEvent.EventEndDate = updateEndDate;

            _eventManager.UpdateEvent(_events[2], newEvent);

            _events = _eventManager.RetrieveAllEvents();
            Assert.AreEqual(_eventManager.RetrieveEventByID(_events[2].EventID).EventEndDate, newEvent.EventEndDate);
        }


        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Test for deleting a room by the ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteRoomByIDCorrect()
        {
            Event deletedEvent = _events[2];
            deletedEvent.EventID = 110005;
            
            _eventManager.DeleteEvent(deletedEvent);
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// 
        /// Modified this helper method to set two events
        /// </summary>
        /// <param name="oldEvent"></param>
        /// <param name="newEvent"></param>
        private void setEvent(Event oldEvent, Event newEvent)
        {
            newEvent.EventTitle = oldEvent.EventTitle;
            newEvent.EmployeeID = oldEvent.EmployeeID;
            newEvent.EventTypeID = oldEvent.EventTypeID;
            newEvent.Description = oldEvent.Description;
            newEvent.EventStartDate = oldEvent.EventStartDate;
            newEvent.EventEndDate = oldEvent.EventEndDate;
            newEvent.KidsAllowed = oldEvent.KidsAllowed;
            newEvent.NumGuests = oldEvent.NumGuests;
            newEvent.Location = oldEvent.Location;
            newEvent.Sponsored = oldEvent.Sponsored;
            newEvent.SponsorID = oldEvent.SponsorID;
            newEvent.Approved = oldEvent.Approved;
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// 
        /// Used this helper method to help create long strings for input validation
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string createString(int length)
        {
            string testLength = "";
            for(int i = 0; i < length; i++)
            {
                testLength += "x";
            }

            return testLength;
        }
        
    }
}
