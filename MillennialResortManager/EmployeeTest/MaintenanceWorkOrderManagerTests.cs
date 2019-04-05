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
    /// Created : 3/7/2019
    /// Here are the Test Methods for MaintenanceWorkOrderManagerMSSQL
    /// </summary>
    [TestClass]
    public class MaintenanceWorkOrderManagerTests
    {
        private IMaintenanceWorkOrderManager _maintenanceWorkOrderManager;
        private List<MaintenanceWorkOrder> _maintenanceWorkOrders;
        private MaintenanceWorkOrderAccessorMock _mwo;

        [TestInitialize]
        public void testSetupMSSQL()
        {
            _mwo = new MaintenanceWorkOrderAccessorMock();
            _maintenanceWorkOrderManager = new MaintenanceWorkOrderManagerMSSQL(_mwo);
            _maintenanceWorkOrders = new List<MaintenanceWorkOrder>();
            _maintenanceWorkOrders = _maintenanceWorkOrderManager.RetrieveAllMaintenanceWorkOrders();
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

        private void setMaintenanceWorkOrders(MaintenanceWorkOrder oldMaintenanceWorkOrder, MaintenanceWorkOrder newMaintenanceWorkOrder)
        {
            newMaintenanceWorkOrder.MaintenanceWorkOrderID = oldMaintenanceWorkOrder.MaintenanceWorkOrderID;
            newMaintenanceWorkOrder.MaintenanceTypeID = oldMaintenanceWorkOrder.MaintenanceTypeID;
            newMaintenanceWorkOrder.MaintenanceStatusID = oldMaintenanceWorkOrder.MaintenanceStatusID;
            newMaintenanceWorkOrder.DateRequested = oldMaintenanceWorkOrder.DateRequested;
            newMaintenanceWorkOrder.DateCompleted = oldMaintenanceWorkOrder.DateCompleted;
            newMaintenanceWorkOrder.RequestingEmployeeID = oldMaintenanceWorkOrder.RequestingEmployeeID;
            newMaintenanceWorkOrder.WorkingEmployeeID = oldMaintenanceWorkOrder.WorkingEmployeeID;
            newMaintenanceWorkOrder.Description = oldMaintenanceWorkOrder.Description;
            newMaintenanceWorkOrder.Comments = oldMaintenanceWorkOrder.Comments;
            newMaintenanceWorkOrder.ResortPropertyID = oldMaintenanceWorkOrder.ResortPropertyID;
            newMaintenanceWorkOrder.Complete = oldMaintenanceWorkOrder.Complete;
        }

        [TestMethod]
        public void TestRetrieveAllMaintenanceWorkOrders()
        {
            //Arange
            List<MaintenanceWorkOrder> maintenanceWorkOrders = null;
            //Act
            maintenanceWorkOrders = _maintenanceWorkOrderManager.RetrieveAllMaintenanceWorkOrders();
            //Assert
            CollectionAssert.Equals(_maintenanceWorkOrders, maintenanceWorkOrders);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/7/2019
        /// Here starts the Create Unit Tests
        /// </summary>



        [TestMethod]
        public void TestCreateMaintenanceWorkOrderValidInput()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder() { MaintenanceTypeID = "Plumbing", DateRequested = DateTime.Now, DateCompleted = DateTime.Now.AddDays(1), RequestingEmployeeID = 100000, WorkingEmployeeID = 100001, Description = "Created In Unit Test: TestCreateMaintenanceWorkOrderValidInput()", Comments = "Test Comment", MaintenanceStatusID = "Open", ResortPropertyID = 1, Complete = true };

            //Act
            _maintenanceWorkOrderManager.AddMaintenanceWorkOrder(newMaintenanceWorkOrder);
           
            //Assert
            //Updates the list of MaintenanceWorkOrder
            _maintenanceWorkOrders = _maintenanceWorkOrderManager.RetrieveAllMaintenanceWorkOrders();

            //Checks to see if the new MaintenanceWorkOrder is in the updated list of MaintenanceWorkOrders
            Assert.IsNotNull(_maintenanceWorkOrders.Find(x => x.MaintenanceTypeID == newMaintenanceWorkOrder.MaintenanceTypeID &&
                                                              x.MaintenanceStatusID == newMaintenanceWorkOrder.MaintenanceStatusID &&
                                                              x.DateRequested.Day == newMaintenanceWorkOrder.DateRequested.Day &&
                                                              x.RequestingEmployeeID == newMaintenanceWorkOrder.RequestingEmployeeID &&
                                                              x.WorkingEmployeeID == newMaintenanceWorkOrder.WorkingEmployeeID &&
                                                              x.Description == newMaintenanceWorkOrder.Description &&
                                                              x.Comments == newMaintenanceWorkOrder.Comments &&
                                                              x.ResortPropertyID == newMaintenanceWorkOrder.ResortPropertyID));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateMaintenanceWorkOrderInValidInputRequestingEmployeeID()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder() { MaintenanceTypeID = "Plumbing", DateRequested = DateTime.Now, DateCompleted = DateTime.Now.AddDays(1), RequestingEmployeeID = -1, WorkingEmployeeID = 100001, Description = "Created In Unit Test: TestCreateMaintenanceWorkOrderInValidInputRequestingEmployeeID()", Comments = "Test Comment", MaintenanceStatusID = "Open", ResortPropertyID = 1, Complete = true };
            //Act
            //Since RequestingEmployeeID is invlaid, this should throw an Exception
            _maintenanceWorkOrderManager.AddMaintenanceWorkOrder(newMaintenanceWorkOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateMaintenanceWorkOrderInValidInputWorkingEmployeeID()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder() { MaintenanceTypeID = "Plumbing", DateRequested = DateTime.Now, DateCompleted = DateTime.Now.AddDays(1), RequestingEmployeeID = 100000, WorkingEmployeeID = -1, Description = "Created In Unit Test: TestCreateMaintenanceWorkOrderInValidInputWorkingEmployeeID()", Comments = "Test Comment", MaintenanceStatusID = "Open", ResortPropertyID = 1, Complete = true };
            //Act
            //Since RequestingEmployeeID is invalid, this should throw an Exception
            _maintenanceWorkOrderManager.AddMaintenanceWorkOrder(newMaintenanceWorkOrder);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateMaintenanceWorkOrderInValidInputDescription()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder() { MaintenanceTypeID = "Plumbing", DateRequested = DateTime.Now, DateCompleted = DateTime.Now.AddDays(1), RequestingEmployeeID = 100000, WorkingEmployeeID = 100000, Description = "Created In Unit Test: TestCreateMaintenanceWorkOrderInValidInputDescription()" + createLongString(1001), Comments = "Test Comment", MaintenanceStatusID = "Open", ResortPropertyID = 1, Complete = true };
            //Act
            //Since Description is invalid, this should throw an Exception
            _maintenanceWorkOrderManager.AddMaintenanceWorkOrder(newMaintenanceWorkOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateMaintenanceWorkOrderInValidInputComments()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder() { MaintenanceTypeID = "Plumbing", DateRequested = DateTime.Now, DateCompleted = DateTime.Now.AddDays(1), RequestingEmployeeID = 100000, WorkingEmployeeID = 100000, Description = "Created In Unit Test: TestCreateMaintenanceWorkOrderInValidInputComments()", Comments = createLongString(1001), MaintenanceStatusID = "Open", ResortPropertyID = 1, Complete = true };
            //Act
            //Since Description is invalid, this should throw an Exception
            _maintenanceWorkOrderManager.AddMaintenanceWorkOrder(newMaintenanceWorkOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateMaintenanceWorkOrderInValidInputMaintenanceResortID()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder() { MaintenanceTypeID = "Plumbing", DateRequested = DateTime.Now, DateCompleted = DateTime.Now.AddDays(1), RequestingEmployeeID = 100000, WorkingEmployeeID = 100000, Description = "Created In Unit Test: TestCreateMaintenanceWorkOrderInValidInputMaintenanceResortID()", Comments = "Test Comment", MaintenanceStatusID = "Open", ResortPropertyID = -1, Complete = true };
            //Act
            //Since Status is invalid, this should throw an Exception
            _maintenanceWorkOrderManager.AddMaintenanceWorkOrder(newMaintenanceWorkOrder);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/7/2019
        /// Here starts the Retrieve Unit Tests
        /// </summary>

        [TestMethod]
        public void TestRetrieveMaintenanceWorkOrderValidInput()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
            
            //Act
            newMaintenanceWorkOrder = _maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(_maintenanceWorkOrders[0].MaintenanceWorkOrderID);
            
            //Assert
            Assert.AreEqual(newMaintenanceWorkOrder.MaintenanceWorkOrderID, _maintenanceWorkOrders[0].MaintenanceWorkOrderID);
            Assert.AreEqual(newMaintenanceWorkOrder.MaintenanceTypeID, _maintenanceWorkOrders[0].MaintenanceTypeID);
            Assert.AreEqual(newMaintenanceWorkOrder.MaintenanceStatusID, _maintenanceWorkOrders[0].MaintenanceStatusID);
            Assert.AreEqual(newMaintenanceWorkOrder.DateRequested.Day, _maintenanceWorkOrders[0].DateRequested.Day);
            Assert.AreEqual(newMaintenanceWorkOrder.RequestingEmployeeID, _maintenanceWorkOrders[0].RequestingEmployeeID);
            Assert.AreEqual(newMaintenanceWorkOrder.WorkingEmployeeID, _maintenanceWorkOrders[0].WorkingEmployeeID);
            Assert.AreEqual(newMaintenanceWorkOrder.Description, _maintenanceWorkOrders[0].Description);
            Assert.AreEqual(newMaintenanceWorkOrder.Comments, _maintenanceWorkOrders[0].Comments);
            Assert.AreEqual(newMaintenanceWorkOrder.ResortPropertyID, _maintenanceWorkOrders[0].ResortPropertyID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveMaintenanceWorkOrderInValidInput()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
            int invalidMaintenanceWorkOrderID = -1;

            //Act
            newMaintenanceWorkOrder = _maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(invalidMaintenanceWorkOrderID);
            
            //Assert
            //Assert.AreEqual(newMaintenanceWorkOrder.MaintenanceWorkOrderID, 0);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/7/2019
        /// Here starts the Update Unit Tests
        /// </summary>

        [TestMethod]
        public void TestUpdateMaintenanceWorkOrderValidDescription()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
            setMaintenanceWorkOrders(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
            string newDescription = "This test is updating the description in TestUpdateMaintenanceWorkOrderValidDescription()";
            newMaintenanceWorkOrder.Description = newDescription;
            //Act
            _maintenanceWorkOrderManager.EditMaintenanceWorkOrder(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
            //Assert
            _maintenanceWorkOrders = _maintenanceWorkOrderManager.RetrieveAllMaintenanceWorkOrders();
            Assert.AreEqual(_maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(_maintenanceWorkOrders[0].MaintenanceWorkOrderID).Description, newDescription);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMaintenanceWorkOrderInValidDescription()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
            setMaintenanceWorkOrders(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
            string newDescription = "This test is updating the description in TestUpdateMaintenanceWorkOrderValidDescription()"+ createLongString(1001);
            newMaintenanceWorkOrder.Description = newDescription;
            //Act
            _maintenanceWorkOrderManager.EditMaintenanceWorkOrder(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMaintenanceWorkOrderInValidRequestingEmployeeID()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
            setMaintenanceWorkOrders(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
            newMaintenanceWorkOrder.RequestingEmployeeID = -1;
            //Act
            _maintenanceWorkOrderManager.EditMaintenanceWorkOrder(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMaintenanceWorkOrderInValidWorkingEmployeeID()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
            setMaintenanceWorkOrders(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
            newMaintenanceWorkOrder.WorkingEmployeeID = -1;
            //Act
            _maintenanceWorkOrderManager.EditMaintenanceWorkOrder(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateMaintenanceWorkOrderInValidResortPropertyID()
        {
            //Arrange
            MaintenanceWorkOrder newMaintenanceWorkOrder = new MaintenanceWorkOrder();
            setMaintenanceWorkOrders(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
            newMaintenanceWorkOrder.ResortPropertyID = -1;
            //Act
            _maintenanceWorkOrderManager.EditMaintenanceWorkOrder(_maintenanceWorkOrders[0], newMaintenanceWorkOrder);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/7/2019
        /// Here starts the Deactivate Unit Tests
        /// </summary>

        [TestMethod]
        public void TestDeactivateMaintenanceWorkOrderValid()
        {
            //Arrange
            int validMaintenanceWorkOrderID = _maintenanceWorkOrders[0].MaintenanceWorkOrderID;
            bool activeStatus = _maintenanceWorkOrders[0].Complete;
            //Act
            _maintenanceWorkOrderManager.DeleteMaintenanceWorkOrder(validMaintenanceWorkOrderID, activeStatus);
            //Assert
            Assert.IsFalse(_maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(validMaintenanceWorkOrderID).Complete);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateMaintenanceWorkOrderInValidMaintenanceWorkOrderID()
        {
            //Arrange
            int invalidMaintenanceWorkOrderID = -1;
            bool activeStatus = true;
            //Act
            _maintenanceWorkOrderManager.DeleteMaintenanceWorkOrder(invalidMaintenanceWorkOrderID, activeStatus);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteMaintenanceWorkOrderValid()
        {
            //Arrange
            int validMaintenanceWorkOrderID = _maintenanceWorkOrders[0].MaintenanceWorkOrderID;
            bool activeStatus = _maintenanceWorkOrders[0].Complete;
            //Act
            _maintenanceWorkOrderManager.DeleteMaintenanceWorkOrder(validMaintenanceWorkOrderID, false);
            //Assert
            _maintenanceWorkOrderManager.RetrieveMaintenanceWorkOrder(validMaintenanceWorkOrderID);
        }
    }
}
