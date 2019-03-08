using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using DataAccessLayer;
using LogicLayer;

namespace UnitTest.LogicLayer
{
    /// <summary>
    /// Author: Craig Barkley
    /// Created : 2019/02/28
    /// Unit tests that test the methods of EventTypeManager and contraints of EventType
    /// </summary>
    /// 
    /// <summary>
    /// Summary description for EventTypeManagerTests
    /// </summary>
    [TestClass]
    public class EventTypeManagerTests
    {
        private IEventTypeManager eventManager;
        private List<EventType> eventTypes;
        private EventTypeAccessorMock accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new EventTypeAccessorMock();
            eventManager = new EventTypeManager(accessor);
            eventTypes = new List<EventType>();
            eventTypes = eventManager.RetrieveAllEventTypes("all");
        }

        private string createLongString(int length)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append("*");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Author: Craig Barkley
        /// Created : 2019/02/28
        /// Unit tests for RetrieveAllEventTypes method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllEventTypes()
        {
            // arrange
            List<EventType> testpets = null;
            // act
            testpets = eventManager.RetrieveAllEventTypes("all");
            // assert
            CollectionAssert.Equals(testpets, eventTypes);
        }

        /// <summary>
        /// Author: Craig Barkley
        /// Created : 2019/02/28
        /// Unit tests for CreateEventType method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreateEventTypeValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            EventType testEventType = new EventType()
            {
                EventTypeID = "GoodID",
                Description = "Good  Long Description",
            };

            // act
            actualResult = eventManager.AddEventType(testEventType);

            // assert - check if EventType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateEventTypeValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            EventType testEventType = new EventType()
            {
                EventTypeID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = eventManager.AddEventType(testEventType);

            // assert - check if EventType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateEventTypeEventTypeIDNull()
        {
            // arrange
            EventType testEventType = new EventType()
            {
                EventTypeID = null,
                Description = "Good Description",
            };

            string badEventTypeID = testEventType.EventTypeID;

            // act
            bool result = eventManager.AddEventType(testEventType);

            // assert - check that EventTypeID did not change
            Assert.AreEqual(badEventTypeID, testEventType.EventTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventTypeEventTypeIDTooLong()
        {
            // arrange
            EventType testEventType = new EventType()
            {
                EventTypeID = createLongString(51),
                Description = "Good Description",
            };

            string badEventTypeID = testEventType.EventTypeID;

            // act
            bool result = eventManager.AddEventType(testEventType);

            // assert - check that EventTypeID did not change
            Assert.AreEqual(badEventTypeID, testEventType.EventTypeID);
        }

        [TestMethod]
        public void TestCreateEventTypeDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            EventType testEventType = new EventType()
            {
                EventTypeID = "GoodID",
                Description = null,
            };

            // act
            actualResult = eventManager.AddEventType(testEventType);

            // assert - check if EventType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEventTypeDescriptionTooLong()
        {
            // arrange
            EventType testEventType = new EventType()
            {
                EventTypeID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testEventType.Description;

            // act
            bool result = eventManager.AddEventType(testEventType);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testEventType.Description);
        }
    }
}
