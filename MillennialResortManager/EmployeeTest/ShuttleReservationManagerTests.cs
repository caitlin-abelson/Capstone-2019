using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataObjects;
using DataAccessLayer;
namespace LogicLayer.Tests
{
  


    ///  /// <summary>
    /// Eduardo Colon
    /// Created: 2019/03/20
    /// 
    /// class for ShuttleReservationManagerTests
    /// </summary>

    [TestClass]
    public class ShuttleReservationManagerTests
    {
        private List<ShuttleReservation> _shuttleReservations;
        private IShuttleReservationManager _shuttleReservationManager;
        private ShuttleReservationAccessorMock _shuttleReservationMock;

        [TestInitialize]
        public void testSetup()
        {
            _shuttleReservationMock = new ShuttleReservationAccessorMock();
            _shuttleReservationManager = new ShuttleReservationManager(_shuttleReservationMock);
            _shuttleReservations = new List<ShuttleReservation>();
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
        }


        private string createStringLength(int length)
        {
            string testingString = "";
            for (int i = 0; i < length; i++)
            {
                testingString += "X";
            }
            return testingString;
        }

        private void setShuttleReservation(ShuttleReservation oldShuttleReservation, ShuttleReservation newShuttleReservation)
        {
            newShuttleReservation.ShuttleReservationID = oldShuttleReservation.ShuttleReservationID;
            newShuttleReservation.GuestID = oldShuttleReservation.GuestID;
            newShuttleReservation.EmployeeID = oldShuttleReservation.EmployeeID;
            newShuttleReservation.PickupLocation = oldShuttleReservation.PickupLocation;
            newShuttleReservation.DropoffDestination = oldShuttleReservation.DropoffDestination;
            newShuttleReservation.PickupDateTime = oldShuttleReservation.PickupDateTime;
            newShuttleReservation.DropoffDateTime = oldShuttleReservation.DropoffDateTime;
            newShuttleReservation.Active = oldShuttleReservation.Active;

        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// Testing  to retrieve all shuttleReservations

        [TestMethod]
        public void TestRetrieveAllShuttleReservation()
        {
            //Arrange
            List<ShuttleReservation> shuttleReservations = null;
            //Act
            shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            //Assert
            CollectionAssert.Equals(_shuttleReservations, shuttleReservations);
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing valid  retrieving all shuttleReservations with Not Null
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllShuttleReservationWithValidInput()
        {
            //Arrange
            List<ShuttleReservation> shuttleReservations = new List<ShuttleReservation>();

            //Act
            shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();

            //Assert
            Assert.IsNotNull(shuttleReservations);
        }




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// 
        /// Testing retrieving all shuttleReservations with at least one object
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllShuttleReservation_RetrieveAtLeastOneObject()
        {

            var shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();

            bool hasAtLeastOneElement = shuttleReservations.Count > 0;

            Assert.IsTrue(hasAtLeastOneElement);
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// 
        /// Testing retrieving all shuttleReservations with  zero  object
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllShuttleReservationsWithZeroObject()
        {
            var setupLists = _shuttleReservationManager.RetrieveAllShuttleReservations();

            bool hasAtLeastZeroElement = setupLists.Count < 0;

            Assert.IsFalse(hasAtLeastZeroElement);
        }




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// Testing  all valid inputs to create shuttleReservation
        /// </summary>
        [TestMethod]
        public void TestCreateShuttleReservationWithAllValidInputs()
        {

            //arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation() { ShuttleReservationID = 100000, GuestID = 100000, EmployeeID = 100000, PickupLocation = "1840 Las Vegas Blvd", DropoffDestination = "840 Broadway Street", PickupDateTime = new DateTime(2008, 11, 11), DropoffDateTime = new DateTime(2008, 11, 12) };
            //Act
            _shuttleReservationManager.CreateShuttleReservation(newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();

            Assert.IsNotNull(_shuttleReservations.Find(s => s.ShuttleReservationID == newShuttleReservation.ShuttleReservationID &&
            s.GuestID == newShuttleReservation.GuestID && s.EmployeeID == newShuttleReservation.EmployeeID &&
            s.PickupLocation == newShuttleReservation.PickupLocation && s.DropoffDestination == newShuttleReservation.DropoffDestination &&
            s.PickupDateTime == newShuttleReservation.PickupDateTime && s.DropoffDateTime == newShuttleReservation.DropoffDateTime

            ));
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// Testing invalid input for pickuplocation too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShuttleReservationInValidInputPickupLocationTooLong()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation() { ShuttleReservationID = 100000, GuestID = 100000, EmployeeID = 100000, PickupLocation = createStringLength(200), DropoffDestination = "840 Broadway Street", PickupDateTime = new DateTime(2008, 11, 11), DropoffDateTime = new DateTime(2008, 11, 12) };
            //Act
            _shuttleReservationManager.CreateShuttleReservation(newShuttleReservation);
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/10
        /// Testing invalid input for pickuplocation too small
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShutleReservationInValidInputPickupLocationTooSmall()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation() { ShuttleReservationID = 100000, GuestID = 100000, EmployeeID = 100000, PickupLocation = createStringLength(-0), DropoffDestination = "840 Broadway Street", PickupDateTime = new DateTime(2008, 11, 11), DropoffDateTime = new DateTime(2008, 11, 12) };
            //Act
            _shuttleReservationManager.CreateShuttleReservation(newShuttleReservation);
        }






        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing invalid input empty for pickuplocation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreatePickupLocationInValidInputEmpty()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation() { ShuttleReservationID = 100000, GuestID = 100000, EmployeeID = 100000, PickupLocation = "", DropoffDestination = "840 Broadway Street", PickupDateTime = new DateTime(2008, 11, 11), DropoffDateTime = new DateTime(2008, 11, 12) };
            //Act
            _shuttleReservationManager.CreateShuttleReservation(newShuttleReservation);
        }





        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing invalid input  for dropoff location in ShuttleReservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShuttleReservationIDEmptyDropofflocation()
        {

            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation() { ShuttleReservationID = 100000, GuestID = 100000, EmployeeID = 100000, PickupLocation = "1840 Las Vegas Blvd", DropoffDestination = "", PickupDateTime = new DateTime(2008, 11, 11), DropoffDateTime = new DateTime(2008, 11, 12) };
            //Act
            _shuttleReservationManager.CreateShuttleReservation(newShuttleReservation);
        }




        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing valid input  for pickuptime in ShuttleReservation
        /// </summary>
        [TestMethod]
    
        public void TestCreateShuttleReservationIDValidPickupTimw()
        {

            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation() { ShuttleReservationID = 100000, GuestID = -1, EmployeeID = 100000, PickupLocation = "1840 Las Vegas Blvd", DropoffDestination = "1940 Broadway street", PickupDateTime= new DateTime(2008, 11, 12), DropoffDateTime = new DateTime(2008, 11, 12) };
            //Act
            _shuttleReservationManager.CreateShuttleReservation(newShuttleReservation);
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing Invalid input too long to update pickuplocation in the  shuttle reservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShuttleReservationInPickupLocationWithInputTooLong()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation();
            setShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            string newPickupLocation = createStringLength(1001);
            newShuttleReservation.PickupLocation = newPickupLocation;
            //Act
            _shuttleReservationManager.UpdateShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            Assert.AreEqual(_shuttleReservationManager.RetrieveShutteReservationByShuttleReservationID(_shuttleReservations[0].ShuttleReservationID).PickupLocation, newShuttleReservation.PickupLocation);


        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing Invalid input too small to update pickuplocation in the  shuttle reservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShuttleReservationInPickupLocationWithInputTooSmall()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation();
            setShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            string newPickupLocation = createStringLength(0);
            newShuttleReservation.PickupLocation = newPickupLocation;
            //Act
            _shuttleReservationManager.UpdateShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            Assert.AreEqual(_shuttleReservationManager.RetrieveShutteReservationByShuttleReservationID(_shuttleReservations[0].ShuttleReservationID).PickupLocation, newShuttleReservation.PickupLocation);


        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing only valid input for PickupDate to update in the  shuttle reservation and other parameter no provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShuttleReservationInPickupDateWithValidInput()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation();
            setShuttleReservation(_shuttleReservations[0], newShuttleReservation);
           DateTime newPickupDate = new DateTime(2008, 11, 11);
            newShuttleReservation.PickupDateTime = newPickupDate;
            //Act
            _shuttleReservationManager.UpdateShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            Assert.AreEqual(_shuttleReservationManager.RetrieveShutteReservationByShuttleReservationID(_shuttleReservations[0].ShuttleReservationID).PickupDateTime, newShuttleReservation.PickupDateTime);


        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing Invalid input  to update PickupDate in the  shuttle reservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestUpdateShuttleReservationInDropoffDateWithValidInput()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation();
            setShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            DateTime newPickupDate = new DateTime(0, 0, 0);
            newShuttleReservation.PickupDateTime = newPickupDate;
            //Act
            _shuttleReservationManager.UpdateShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            Assert.AreEqual(_shuttleReservationManager.RetrieveShutteReservationByShuttleReservationID(_shuttleReservations[0].ShuttleReservationID).PickupDateTime, newShuttleReservation.PickupDateTime);


        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing Invalid input too small to update dropoff destionation in the  shuttle reservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShuttleReservationInDropoffDestinationWithInputTooSmall()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation();
            setShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            string newDropoffDestination = createStringLength(0);
            newShuttleReservation.PickupLocation = newDropoffDestination;
            //Act
            _shuttleReservationManager.UpdateShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            Assert.AreEqual(_shuttleReservationManager.RetrieveShutteReservationByShuttleReservationID(_shuttleReservations[0].ShuttleReservationID).DropoffDestination, newShuttleReservation.DropoffDestination);


        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing Invalid input too big to update dropoff destionation in the  shuttle reservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShuttleReservationInDropoffDestinationWithInputTooBig()
        {
            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation();
            setShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            string newDropoffDestination = createStringLength(200);
            newShuttleReservation.PickupLocation = newDropoffDestination;
            //Act
            _shuttleReservationManager.UpdateShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            Assert.AreEqual(_shuttleReservationManager.RetrieveShutteReservationByShuttleReservationID(_shuttleReservations[0].ShuttleReservationID).DropoffDestination, newShuttleReservation.DropoffDestination);


        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// Testing invalid input too long for dropoff destination when updating the  shuttle reservation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShuttleReservationInDropoffDestinationWithInputTooLong()
        {



            //Arrange
            ShuttleReservation newShuttleReservation = new ShuttleReservation();
            setShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            string newDropoffDestination = createStringLength(1001);
            newShuttleReservation.DropoffDestination = newDropoffDestination;
            //Act
            _shuttleReservationManager.UpdateShuttleReservation(_shuttleReservations[0], newShuttleReservation);
            //Assert
            _shuttleReservations = _shuttleReservationManager.RetrieveAllShuttleReservations();
            Assert.AreEqual(_shuttleReservationManager.RetrieveShutteReservationByShuttleReservationID(_shuttleReservations[0].ShuttleReservationID).DropoffDestination, newShuttleReservation.DropoffDestination);
        }





        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// Testing valid input when deactivating a shuttle reservation
        /// </summary>
        [TestMethod]

        public void TestDeactivateShuttleReservationWithValidSetupListID()
        {


            //Arrange
            int shuttleReservationID = _shuttleReservations[0].ShuttleReservationID;
            bool activeShuttleReservation = _shuttleReservations[0].Active;

            ////Act
            _shuttleReservationManager.DeactivateShuttleReservation(shuttleReservationID, activeShuttleReservation);

        }

        



    }
    
}
