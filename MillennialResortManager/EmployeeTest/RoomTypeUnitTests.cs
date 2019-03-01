/// <summary>
/// Author: Austin Berquam
/// Created : 2019/02/23
/// Unit tests that test the methods of RoomTypeManager and contraints of RoomType
/// </summary>
/// 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;
using LogicLayer;
using System.Text;

namespace RoomTypeLogicTests
{
    [TestClass]
    public class RoomTypeUnitTests
    {
        private IRoomType roomTypeManager;
        private List<RoomType> roomTypes;
        private MockRoomTypeAccessor accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new MockRoomTypeAccessor();
            roomTypeManager = new RoomTypeManager(accessor);
            roomTypes = new List<RoomType>();
            roomTypes = roomTypeManager.RetrieveAllRoomTypes("all");
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
        /// Unit tests for RetrieveAllroomTypes method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllRoomTypes()
        {
            // arrange
            List<RoomType> testroomTypes = null;

            // act
            testroomTypes = roomTypeManager.RetrieveAllRoomTypes("all");

            // assert
            CollectionAssert.Equals(testroomTypes, roomTypes);
        }

        /// <summary>
        /// Author: Austin Berquam
        /// Created : 2019/02/23
        /// Unit tests for CreateRoomType method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreateRoomTypeValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            RoomType testRoomType = new RoomType()
            {
                RoomTypeID = "GoodID",
                Description = "Good Description",
            };

            // act
            actualResult = roomTypeManager.CreateRoomType(testRoomType);

            // assert - check if RoomType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateRoomTypeValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            RoomType testRoomType = new RoomType()
            {
                RoomTypeID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = roomTypeManager.CreateRoomType(testRoomType);

            // assert - check if RoomType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateRoomTypeRoomTypeIDNull()
        {
            // arrange
            RoomType testRoomType = new RoomType()
            {
                RoomTypeID = null,
                Description = "Good Description",
            };

            string badRoomTypeID = testRoomType.RoomTypeID;

            // act
            bool result = roomTypeManager.CreateRoomType(testRoomType);

            // assert - check that RoomTypeID did not change
            Assert.AreEqual(badRoomTypeID, testRoomType.RoomTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRoomTypeRoomTypeIDTooLong()
        {
            // arrange
            RoomType testRoomType = new RoomType()
            {
                RoomTypeID = createLongString(51),
                Description = "Good Description",
            };

            string badRoomTypeID = testRoomType.RoomTypeID;

            // act
            bool result = roomTypeManager.CreateRoomType(testRoomType);

            // assert - check that RoomTypeID did not change
            Assert.AreEqual(badRoomTypeID, testRoomType.RoomTypeID);
        }

        [TestMethod]
        public void TestCreateRoomTypeDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            RoomType testRoomType = new RoomType()
            {
                RoomTypeID = "GoodID",
                Description = null,
            };

            // act
            actualResult = roomTypeManager.CreateRoomType(testRoomType);

            // assert - check if RoomType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateRoomTypeDescriptionTooLong()
        {
            // arrange
            RoomType testRoomType = new RoomType()
            {
                RoomTypeID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testRoomType.Description;

            // act
            bool result = roomTypeManager.CreateRoomType(testRoomType);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testRoomType.Description);
        }
    }
}
