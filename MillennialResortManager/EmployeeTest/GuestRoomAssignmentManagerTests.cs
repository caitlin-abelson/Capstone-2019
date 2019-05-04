using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using DataAccessLayer;

namespace LogicLayer.Tests
{

    /// <summary>
    /// Author: Jared Greenfield
    /// Created On: 2019-04-25
    /// Tests for Guest Room Assignment Manager
    /// </summary>
    [TestClass]
    public class GuestRoomAssignmentManagerTests
    {
        private IGuestRoomAssignmentManager _assignmentManager;
        public GuestRoomAssignmentManagerTests()
        {
            _assignmentManager = new GuestRoomAssignmentManager(new GuestRoomAssignmentMock());
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SelectGuestRoomAssignmentVMSByReservationIDValid()
        {
            //Arrange 

            //Act 
            List<GuestRoomAssignmentVM> assignments = _assignmentManager.SelectGuestRoomAssignmentVMSByReservationID(100000);
            //Assert
            Assert.IsNotNull(assignments);
        }

        [TestMethod]
        public void UpdateGuestRoomAssignmentToCheckedOutValid()
        {
            //Arrange 
            int GuestID = 100000;
            int RoomReservationID = 100000;
            //Act
            var result = _assignmentManager.UpdateGuestRoomAssignmentToCheckedOut(GuestID, RoomReservationID);
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateGuestRoomAssignmentToCheckedOutInvalid()
        {
            //Arrange 
            int GuestID = 10;
            int RoomReservationID = 100000;
            //Act
            var result = _assignmentManager.UpdateGuestRoomAssignmentToCheckedOut(GuestID, RoomReservationID);
            //Assert
            Assert.IsFalse(result);
        }
    }
}
