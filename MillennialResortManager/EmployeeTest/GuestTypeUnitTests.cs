/// <summary>
/// Author: Austin Berquam
/// Created : 2019/02/23
/// Unit tests that test the methods of GuestTypeManager and contraints of GuestType
/// </summary>
/// 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;
using LogicLayer;
using System.Text;

namespace LogicLayer.Tests
{
    [TestClass]
    public class GuestTypeUnitTests
    {
        private IGuestTypeManager guestManager;
        private List<GuestType> guests;
        private MockGuestTypeAccessor accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new MockGuestTypeAccessor();
            guestManager = new GuestTypeManager(accessor);
            guests = new List<GuestType>();
            guests = guestManager.RetrieveAllGuestTypes("all");
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
        /// Author: Austin Berquam
        /// Created : 2019/02/23
        /// Unit tests for RetrieveAllguests method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllGuestTypes()
        {
            // arrange
            List<GuestType> testguests = null;

            // act
            testguests = guestManager.RetrieveAllGuestTypes("all");

            // assert
            CollectionAssert.Equals(testguests, guests);
        }

        /// <summary>
        /// Author: Austin Berquam
        /// Created : 2019/02/23
        /// Unit tests for CreateGuestType method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreateGuestTypeValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            GuestType testGuestType = new GuestType()
            {
                GuestTypeID = "GoodID",
                Description = "Good Description",
            };

            // act
            actualResult = guestManager.CreateGuestType(testGuestType);

            // assert - check if GuestType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateGuestTypeValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            GuestType testGuestType = new GuestType()
            {
                GuestTypeID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = guestManager.CreateGuestType(testGuestType);

            // assert - check if GuestType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateGuestTypeGuestTypeIDNull()
        {
            // arrange
            GuestType testGuestType = new GuestType()
            {
                GuestTypeID = null,
                Description = "Good Description",
            };

            string badGuestTypeID = testGuestType.GuestTypeID;

            // act
            bool result = guestManager.CreateGuestType(testGuestType);

            // assert - check that GuestTypeID did not change
            Assert.AreEqual(badGuestTypeID, testGuestType.GuestTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestTypeGuestTypeIDTooLong()
        {
            // arrange
            GuestType testGuestType = new GuestType()
            {
                GuestTypeID = createLongString(51),
                Description = "Good Description",
            };

            string badGuestTypeID = testGuestType.GuestTypeID;

            // act
            bool result = guestManager.CreateGuestType(testGuestType);

            // assert - check that GuestTypeID did not change
            Assert.AreEqual(badGuestTypeID, testGuestType.GuestTypeID);
        }

        [TestMethod]
        public void TestCreateGuestTypeDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            GuestType testGuestType = new GuestType()
            {
                GuestTypeID = "GoodID",
                Description = null,
            };

            // act
            actualResult = guestManager.CreateGuestType(testGuestType);

            // assert - check if GuestType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestTypeDescriptionTooLong()
        {
            // arrange
            GuestType testGuestType = new GuestType()
            {
                GuestTypeID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testGuestType.Description;

            // act
            bool result = guestManager.CreateGuestType(testGuestType);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testGuestType.Description);
        }
    }
}
