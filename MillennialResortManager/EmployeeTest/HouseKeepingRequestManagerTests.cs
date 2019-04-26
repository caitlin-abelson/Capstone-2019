using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessLayer;
using DataObjects;

namespace UnitTests
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/29/2019
    /// Here are the Test Methods for HouseKeepingRequestManagerMSSQL
    /// </summary>
    [TestClass]
    public class HouseKeepingRequestManagerTests
    {
        private IHouseKeepingRequestManager _houseKeepingRequestManager;
        private List<HouseKeepingRequest> _houseKeepingRequests;
        private HouseKeepingRequestAccessorMock _hr;

        [TestInitialize]
        public void testSetupMSSQL()
        {
            _hr = new HouseKeepingRequestAccessorMock();
            _houseKeepingRequestManager = new HouseKeepingRequestManagerMSSQL(_hr);
            _houseKeepingRequests = new List<HouseKeepingRequest>();
            _houseKeepingRequests = _houseKeepingRequestManager.RetrieveAllHouseKeepingRequests();
        }

        private string createLongString(int length)
        {
            string longString = "";
            for (int i = 0; i < length; i++)
            {
                longString += "I";
            }
            return longString;
        }

        private void setHouseKeepingRequests(HouseKeepingRequest oldHouseKeepingRequest, HouseKeepingRequest newHouseKeepingRequest)
        {
            newHouseKeepingRequest.HouseKeepingRequestID = oldHouseKeepingRequest.HouseKeepingRequestID;
            newHouseKeepingRequest.BuildingNumber = oldHouseKeepingRequest.BuildingNumber;
            newHouseKeepingRequest.RoomNumber = oldHouseKeepingRequest.RoomNumber;
            newHouseKeepingRequest.WorkingEmployeeID = oldHouseKeepingRequest.WorkingEmployeeID;
            newHouseKeepingRequest.Description = oldHouseKeepingRequest.Description;
            newHouseKeepingRequest.Active = oldHouseKeepingRequest.Active;
        }

        [TestMethod]
        public void TestRetrieveAllHouseKeepingRequests()
        {
            //Arange
            List<HouseKeepingRequest> houseKeepingRequests = null;
            //Act
            houseKeepingRequests = _houseKeepingRequestManager.RetrieveAllHouseKeepingRequests();
            //Assert
            CollectionAssert.Equals(_houseKeepingRequests, houseKeepingRequests);
        }

        // <summary>
        // Author: Dalton Cleveland
        // Created : 3/29/2019
        // Here starts the Create Unit Tests
        // </summary>
        

        [TestMethod]
        public void TestCreateHouseKeepingRequestValidInput()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest() {  BuildingNumber = 1, RoomNumber = 1, Description = "Created In Unit Test: TestCreateHouseKeepingRequestValidInput()",  WorkingEmployeeID = 100000, Active = true };

            //Act
            _houseKeepingRequestManager.AddHouseKeepingRequest(newHouseKeepingRequest);

            //Assert
            //Updates the list of HouseKeepingRequests
            _houseKeepingRequests = _houseKeepingRequestManager.RetrieveAllHouseKeepingRequests();

            //Checks to see if the new HouseKeepingRequest is in the updated list of HouseKeepingRequests
            Assert.IsNotNull(_houseKeepingRequests.Find(x => x.BuildingNumber == newHouseKeepingRequest.BuildingNumber &&
                                                              x.RoomNumber == newHouseKeepingRequest.RoomNumber &&
                                                              x.Description == newHouseKeepingRequest.Description &&
                                                              x.WorkingEmployeeID == newHouseKeepingRequest.WorkingEmployeeID &&
                                                              x.Active == newHouseKeepingRequest.Active));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateHouseKeepingRequestInValidInputBuildingNumber()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest() { BuildingNumber = 10000, RoomNumber = 1, Description = "Created In Unit Test: TestCreateHouseKeepingRequestInValidInputBuildingNumber()", WorkingEmployeeID = 100000, Active = true };
            //Act
            //Since BuildingNumber is invalid, this should throw an Exception
            _houseKeepingRequestManager.AddHouseKeepingRequest(newHouseKeepingRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateHouseKeepingRequestInValidInputRoomNumber()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest() { BuildingNumber = 1, RoomNumber = 10000, Description = "Created In Unit Test: TestCreateHouseKeepingRequestInValidInputRoomNumber()", WorkingEmployeeID = 100000, Active = true };
            //Act
            //Since RoomNumber is invalid, this should throw an Exception
            _houseKeepingRequestManager.AddHouseKeepingRequest(newHouseKeepingRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateHouseKeepingRequestInValidInputDescription()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest() { BuildingNumber = 1, RoomNumber = 10000, Description = "Created In Unit Test: TestCreateHouseKeepingRequestInValidInputDescription()" + createLongString(1001), WorkingEmployeeID = 100000, Active = true };
            //Act
            //Since Description is invalid, this should throw an Exception
            _houseKeepingRequestManager.AddHouseKeepingRequest(newHouseKeepingRequest);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/29/2019
        /// Here starts the Retrieve Unit Tests
        /// </summary>

        [TestMethod]
        public void TestRetrieveHouseKeepingRequestValidInput()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest();

            //Act
            newHouseKeepingRequest = _houseKeepingRequestManager.RetrieveHouseKeepingRequest(_houseKeepingRequests[0].HouseKeepingRequestID);

            //Assert
            Assert.AreEqual(newHouseKeepingRequest.HouseKeepingRequestID, _houseKeepingRequests[0].HouseKeepingRequestID);
            Assert.AreEqual(newHouseKeepingRequest.BuildingNumber, _houseKeepingRequests[0].BuildingNumber);
            Assert.AreEqual(newHouseKeepingRequest.RoomNumber, _houseKeepingRequests[0].RoomNumber);
            Assert.AreEqual(newHouseKeepingRequest.Description, _houseKeepingRequests[0].Description);
            Assert.AreEqual(newHouseKeepingRequest.WorkingEmployeeID, _houseKeepingRequests[0].WorkingEmployeeID);
            Assert.AreEqual(newHouseKeepingRequest.Active, _houseKeepingRequests[0].Active);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveHouseKeepingRequestInValidInput()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest();
            int invalidHouseKeepingRequestID = -1;

            //Act
            newHouseKeepingRequest = _houseKeepingRequestManager.RetrieveHouseKeepingRequest(invalidHouseKeepingRequestID);

            //Assert
            Assert.AreEqual(newHouseKeepingRequest.HouseKeepingRequestID, 0);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/29/2019
        /// Here starts the Update Unit Tests
        /// </summary>

        [TestMethod]
        public void TestUpdateHouseKeepingRequestValidDescription()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest();
            setHouseKeepingRequests(_houseKeepingRequests[0], newHouseKeepingRequest);
            string newDescription = "This test is updating the description in TestUpdateHouseKeepingRequestValidDescription()";
            newHouseKeepingRequest.Description = newDescription;
            //Act
            _houseKeepingRequestManager.EditHouseKeepingRequest(_houseKeepingRequests[0], newHouseKeepingRequest);
            //Assert
            _houseKeepingRequests = _houseKeepingRequestManager.RetrieveAllHouseKeepingRequests();
            Assert.AreEqual(_houseKeepingRequestManager.RetrieveHouseKeepingRequest(_houseKeepingRequests[0].HouseKeepingRequestID).Description, newDescription);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateHouseKeepingRequestInValidDescription()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest();
            setHouseKeepingRequests(_houseKeepingRequests[0], newHouseKeepingRequest);
            string newDescription = "This test is updating the description in TestUpdateHouseKeepingRequestValidDescription()" + createLongString(1001);
            newHouseKeepingRequest.Description = newDescription;
            //Act
            _houseKeepingRequestManager.EditHouseKeepingRequest(_houseKeepingRequests[0], newHouseKeepingRequest);
          }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateHouseKeepingRequestInValidBuildingNumber()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest();
            setHouseKeepingRequests(_houseKeepingRequests[0], newHouseKeepingRequest);
            newHouseKeepingRequest.BuildingNumber = -1;
            //Act
            _houseKeepingRequestManager.EditHouseKeepingRequest(_houseKeepingRequests[0], newHouseKeepingRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateHouseKeepingRequestInValidRoomNumber()
        {
            //Arrange
            HouseKeepingRequest newHouseKeepingRequest = new HouseKeepingRequest();
            setHouseKeepingRequests(_houseKeepingRequests[0], newHouseKeepingRequest);
            newHouseKeepingRequest.RoomNumber = -1;
            //Act
            _houseKeepingRequestManager.EditHouseKeepingRequest(_houseKeepingRequests[0], newHouseKeepingRequest);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/29/2019
        /// Here starts the Deactivate Unit Tests
        /// </summary>

        [TestMethod]
        public void TestDeactivateHouseKeepingRequestValid()
        {
            //Arrange
            int validHouseKeepingRequestID = _houseKeepingRequests[0].HouseKeepingRequestID;
            bool activeStatus = _houseKeepingRequests[0].Active;
            //Act
            _houseKeepingRequestManager.DeleteHouseKeepingRequest(validHouseKeepingRequestID, activeStatus);
            //Assert
            Assert.IsFalse(_houseKeepingRequestManager.RetrieveHouseKeepingRequest(validHouseKeepingRequestID).Active);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateHouseKeepingRequestInValidHouseKeepingRequestID()
        {
            //Arrange
            int invalidHouseKeepingRequestID = -1;
            bool activeStatus = true;
            //Act
            _houseKeepingRequestManager.DeleteHouseKeepingRequest(invalidHouseKeepingRequestID, activeStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteHouseKeepingRequestValid()
        {
            //Arrange
            int validHouseKeepingRequestID = _houseKeepingRequests[0].HouseKeepingRequestID;
            bool activeStatus = _houseKeepingRequests[0].Active;
            //Act
            _houseKeepingRequestManager.DeleteHouseKeepingRequest(validHouseKeepingRequestID, false);
            //Assert
            _houseKeepingRequestManager.RetrieveHouseKeepingRequest(validHouseKeepingRequestID);
        }
    }
}
