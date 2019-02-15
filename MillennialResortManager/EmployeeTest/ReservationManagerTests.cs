using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessLayer;

using DataObjects;

namespace UnitTests
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 2/6/2019
    /// Here are the Test Methods for ReservationManagerMSSQL
    /// </summary>
    [TestClass]
    public class ReservationManagerTests
    {
        private IReservationManager _reservationManager;
        private List<Reservation> _reservations;
        private ReservationAccessorMock _res;

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/15/2019
        /// Initializes test parameters
        /// </summary>
        [TestInitialize]
        public void testSetupMSSQL()
        {
            _res = new ReservationAccessorMock();
            _reservationManager = new ReservationManagerMSSQL(_res);
            _reservations = new List<Reservation>();
            _reservations = _reservationManager.RetrieveAllReservations();
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

        private void setReservations(Reservation old, Reservation newReservation)
        {
            newReservation.ReservationID = old.ReservationID;
            newReservation.MemberID = old.MemberID;
            newReservation.NumberOfGuests = old.NumberOfGuests;
            newReservation.NumberOfPets = old.NumberOfPets;
            newReservation.ArrivalDate = old.ArrivalDate;
            newReservation.DepartureDate = old.DepartureDate;
            newReservation.Notes = old.Notes;
            newReservation.Active = old.Active;
        }


        [TestMethod]
        public void TestRetrieveAllreservations()
        {
            //Arange
            List<Reservation> reservations = null;
            //Act
            reservations = _reservationManager.RetrieveAllReservations();
            //Assert
            CollectionAssert.Equals(_reservations,reservations);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/6/2019
        /// Here starts the CreateReservation Unit Tests
        /// </summary>


        
        [TestMethod]
        public void TestCreateReservationValidInput()
        {
            //Arrange
            Reservation newReservation = new Reservation() { ReservationID = 100004, MemberID = 100000, NumberOfGuests = 1, NumberOfPets = 0, ArrivalDate = DateTime.Now, DepartureDate = DateTime.Now.AddDays(1), Notes = "this was created in Unit Test: TestCreateReservationValidInput()" };
            //Act
            _reservationManager.AddReservation(newReservation);
            //Assert
            //Updates the list of Reservations
            _reservations = _reservationManager.RetrieveAllReservations();
            //Checks to see if the new Reservation is in the updated list of reservations
            Assert.IsNotNull(_reservations.Find(x => x.MemberID == newReservation.MemberID &&
                                                x.NumberOfGuests == newReservation.NumberOfGuests &&
                                                x.NumberOfPets == newReservation.NumberOfPets &&
                                                x.ArrivalDate.Day == newReservation.ArrivalDate.Day &&
                                                x.DepartureDate.Day == newReservation.DepartureDate.Day &&
                                                x.Notes == newReservation.Notes));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateReservationInValidInputNumberOfGuests()
        {
            //Arrange
            Reservation newReservation = new Reservation() { MemberID = 100000, NumberOfGuests = 1000, NumberOfPets = 0, ArrivalDate = DateTime.Now, DepartureDate = DateTime.Now.AddDays(1), Notes = "this was created in Unit Test: TestCreateReservationInValidInputNumberOfGuests()" };
            //Act
            //Since NumberOfGuests is out of the current range of valid Guests This should throw an Exception
            _reservationManager.AddReservation(newReservation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateReservationInValidInputNumberOfPets()
        {
            //Arrange
            Reservation newReservation = new Reservation() { MemberID = 100000, NumberOfGuests = 1, NumberOfPets = 1000, ArrivalDate = DateTime.Now, DepartureDate = DateTime.Now.AddDays(1), Notes = "this was created in Unit Test: TestCreateReservationInValidInputNumberOfPets()" };
            //Act
            //Since NumberOfPets is out of the current range of valid Pets This should throw an Exception
            _reservationManager.AddReservation(newReservation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateReservationInValidInputArrivalDate()
        {
            //Arrange
            Reservation newReservation = new Reservation() { MemberID = 100000, NumberOfGuests = 1, NumberOfPets = 1, ArrivalDate = DateTime.Now.AddYears(-1000), DepartureDate = DateTime.Now.AddDays(1), Notes = "this was created in Unit Test: TestCreateReservationInValidInputArrivalDate" };
            //Act
            //Since ArrivalDate is out of the current range of valid ArrivalDates This should throw an Exception
            _reservationManager.AddReservation(newReservation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateReservationInValidInputDepartureDate()
        {
            //Arrange
            Reservation newReservation = new Reservation() { MemberID = 100000, NumberOfGuests = 1, NumberOfPets = 1, ArrivalDate = DateTime.Now, DepartureDate = DateTime.Now.AddDays(-5), Notes = "this was created in Unit Test: TestCreateReservationInValidInputDepartureDate" };
            //Act
            //Since DepartureDate is out of the current range of valid DepartureDates This should throw an Exception
            _reservationManager.AddReservation(newReservation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateReservationInValidInputNotes()
        {
            //Arrange
            Reservation newReservation = new Reservation() { MemberID = 100000, NumberOfGuests = 1, NumberOfPets = 1, ArrivalDate = DateTime.Now, DepartureDate = DateTime.Now.AddDays(1), Notes = "this was created in Unit Test: TestCreateReservationInValidInputNotes"+createLongString(1000) };
            //Act
            //Since Notes is longer than out currently acceptable range this should throw an Exception
            _reservationManager.AddReservation(newReservation);
        }

        //Need to add a test for validating Member
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateReservationInValidInputMember()
        {
            //Arrange
            Reservation newReservation = new Reservation() { MemberID = -1, NumberOfGuests = 1, NumberOfPets = 1, ArrivalDate = DateTime.Now, DepartureDate = DateTime.Now.AddDays(1), Notes = "this was created in Unit Test: TestCreateReservationInValidInputMember" };
            //Act
            //Since MemberID is not the ID of a Member at our resort this should throw an Exception
            _reservationManager.AddReservation(newReservation);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/6/2019
        /// Here starts the RetrieveReservation Unit Tests
        /// </summary>

        [TestMethod]
        public void TestRetrieveReservationValidInput()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            //Act
            newReservation = _reservationManager.RetrieveReservation(_reservations[0].ReservationID);
            //Assert
            Assert.AreEqual(newReservation.ReservationID, _reservations[0].ReservationID);
            Assert.AreEqual(newReservation.MemberID, _reservations[0].MemberID);
            Assert.AreEqual(newReservation.NumberOfGuests, _reservations[0].NumberOfGuests);
            Assert.AreEqual(newReservation.NumberOfPets, _reservations[0].NumberOfPets);
            Assert.AreEqual(newReservation.ArrivalDate.Day, _reservations[0].ArrivalDate.Day);
            Assert.AreEqual(newReservation.DepartureDate.Day, _reservations[0].DepartureDate.Day);
            Assert.AreEqual(newReservation.Notes, _reservations[0].Notes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveReservationInValidInput()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            int invalidReservationID = -1;
            //Act
            newReservation = _reservationManager.RetrieveReservation(invalidReservationID);
            //Assert
            //Assert.AreEqual(newReservation.ReservationID, 0);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/6/2019
        /// Here starts the UpdateReservation Unit Tests
        /// </summary>

        [TestMethod]

        public void TestUpdateReservationValidInput()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            setReservations(_reservations[0],newReservation);
            string newNotes = "This test is updating the notes in TestUpdateReservationValidInput()";
            newReservation.Notes = newNotes;
            //Act
            _reservationManager.EditReservation(_reservations[0],newReservation);
            //Assert
            _reservations = _reservationManager.RetrieveAllReservations();
            Assert.AreEqual(_reservationManager.RetrieveReservation(_reservations[0].ReservationID).Notes,newNotes);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateReservationInValidInputNotes()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            setReservations(_reservations[0], newReservation);
            string newNotes = "This test is updating the notes in TestUpdateReservationValidInput()"+createLongString(1000);
            newReservation.Notes = newNotes;
            //Act
            _reservationManager.EditReservation(_reservations[0], newReservation);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateReservationInValidInputArrivalDate()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            setReservations(_reservations[0], newReservation);
            newReservation.ArrivalDate = DateTime.Now.AddYears(-1000);
            //Act
            _reservationManager.EditReservation(_reservations[0], newReservation);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateReservationInValidInputDepartureDate()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            setReservations(_reservations[0], newReservation);
            newReservation.DepartureDate = newReservation.ArrivalDate.AddDays(-5);
            //Act
            _reservationManager.EditReservation(_reservations[0], newReservation);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateReservationInValidNumberOfGuests()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            setReservations(_reservations[0], newReservation);
            newReservation.NumberOfGuests = 500;
            //Act
            _reservationManager.EditReservation(_reservations[0], newReservation);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateReservationInValidNumberOfPets()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            setReservations(_reservations[0], newReservation);
            newReservation.NumberOfPets = 500;
            //Act
            _reservationManager.EditReservation(_reservations[0], newReservation);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateReservationInValidMemberID()
        {
            //Arrange
            Reservation newReservation = new Reservation();
            setReservations(_reservations[0], newReservation);
            newReservation.MemberID = -8;
            //Act
            _reservationManager.EditReservation(_reservations[0], newReservation);
        }


        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/6/2019
        /// Here starts the DeactivateReservation Unit Tests
        /// </summary>

        [TestMethod]
        public void TestDeactivateReservationValid()
        {
            //Arrange
            int validReservationID = _reservations[0].ReservationID;
            bool activeStatus = _reservations[0].Active;
            //Act
            _reservationManager.DeleteReservation(validReservationID,activeStatus);
            //Assert
            Assert.IsFalse(_reservationManager.RetrieveReservation(validReservationID).Active);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateReservationInValidReservationID()
        {
            //Arrange
            int invalidReservationID = -1;
            bool activeStatus = true;
            //Act
            _reservationManager.DeleteReservation(invalidReservationID, activeStatus);
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteReservationValid()
        {
            //Arrange
            int validReservationID = _reservations[0].ReservationID;
            bool activeStatus = _reservations[0].Active;
            //Act
            _reservationManager.DeleteReservation(validReservationID, false);
            //Assert
            _reservationManager.RetrieveReservation(validReservationID);
        }

        [TestMethod]
        public void TestRetrieveAllVMReservations()
        {
            //Arrange
            List<VMBrowseReservation> _vms = new List<VMBrowseReservation>();
            //Act
            _vms = _reservationManager.RetrieveAllVMReservations();
            //Assert
            Assert.IsNotNull(_vms);
        }
    }
}
