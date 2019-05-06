/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Tests the Methods of AppointmentManager
/// </summary>
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using DataObjects;
using LogicLayer;

namespace LogicLayer.Tests
{
    /// <summary>
    /// Tests the Methods of AppointmentManager
    /// </summary>
    [TestClass]
    public class AppointmentManagerTest
    {

        private List<Appointment> _testAppointments;
        private List<AppointmentType> _testAppointmentTypes;
        private List<AppointmentGuestViewModel> _testGuestViewModels;
        private int nextAppID;
        IAppointmentAccessor _appAccs;
        IAppointmentManager _appMgr;
        public AppointmentManagerTest()
        {
            _appAccs = new AppointmentAccessorMock();
            _appMgr = new AppointmentManager(_appAccs);
            _testAppointments = new List<Appointment>();
            _testAppointmentTypes = new List<AppointmentType>();
            _testGuestViewModels = new List<AppointmentGuestViewModel>();
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
        public void CreateNewAppointmentAllValid()
        {
            Appointment appointment = BuildNewAppointment();
            bool results = false;
            results = _appMgr.CreateAppointmentByGuest(appointment);
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void CreateNewAppointmentTypeTooLong()
        {
            var appointmnet = BuildNewAppointment();
            appointmnet.AppointmentType = BuildStringOfGivenLenght(26);
            try
            {
                _appMgr.CreateAppointmentByGuest(appointmnet);
                Assert.IsTrue(false);
            }
            catch (ApplicationException ex)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CreateNewAppointmentTypeEmpty()
        {
            var appointmnet = BuildNewAppointment();
            appointmnet.AppointmentType = "";
            try
            {
                _appMgr.CreateAppointmentByGuest(appointmnet);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CreateNewAppointmentNoStartDate()
        {
            Appointment appointment = new Appointment()
            {
                AppointmentType = "Spa",
                GuestID = 100000,
                EndDate = new DateTime(2020, 12, 25, 10, 50, 50),
                Description = "Stuff"
            };
            try
            {
                _appMgr.CreateAppointmentByGuest(appointment);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CreateNewAppointmentStartPastDate()
        {
            Appointment appointment = new Appointment()
            {
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(),
                EndDate = new DateTime(2020, 12, 25, 10, 50, 50),
                Description = "Stuff"
            };
            try
            {
                _appMgr.CreateAppointmentByGuest(appointment);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CreateNewAppointmentNoEndDate()
        {
            Appointment appointment = new Appointment()
            {
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 25, 10, 50, 50),
                EndDate = new DateTime(),
                Description = "Stuff"
            };
            try
            {
                _appMgr.CreateAppointmentByGuest(appointment);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CreateNewAppointmentEndDateEarlyThenStartDate()
        {
            Appointment appointment = new Appointment()
            {
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 25, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 24, 10, 50, 50),
                Description = "Stuff"
            };
            try
            {
                _appMgr.CreateAppointmentByGuest(appointment);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void CreateNewAppointmentDescriptionTooLong()
        {
            var appointmnet = BuildNewAppointment();
            appointmnet.Description = BuildStringOfGivenLenght(10002);
            try
            {
                _appMgr.CreateAppointmentByGuest(appointmnet);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [TestMethod]
        public void UpdateAppointmentAllValid()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.Description = "Test Type 1";
            appt1.AppointmentType = "Test Type 1";
            bool results = _appMgr.UpdateAppointment(appt1);
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void UpdateAppointmentDescriptionTooLong()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.Description = BuildStringOfGivenLenght(10002);
            try
            {
                _appMgr.UpdateAppointment(appt1);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }


        }

        [TestMethod]
        public void UpdateAppointmentEndDateEarlyThenStartDate()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.EndDate = new DateTime(2020, 12, 25, 10, 00, 50);
            try
            {
                _appMgr.UpdateAppointment(appt1);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void UpdateAppointmentNoEndDate()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.EndDate = new DateTime();
            try
            {
                _appMgr.UpdateAppointment(appt1);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateAppointmentNoStartDate()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.StartDate = new DateTime();
            try
            {
                _appMgr.UpdateAppointment(appt1);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateAppointmentStartPastDate()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.EndDate = new DateTime(2018, 12, 25, 10, 00, 50);
            try
            {
                _appMgr.UpdateAppointment(appt1);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateAppointmentAppointmentTypeEmpty()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.AppointmentType = "";
            try
            {
                _appMgr.UpdateAppointment(appt1);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateAppointmentAppointmentTypeTooLong()
        {
            Appointment appt1 = BuildNewAppointment();
            appt1.AppointmentID = 100000;
            appt1.AppointmentType = BuildStringOfGivenLenght(26);
            try
            {
                _appMgr.UpdateAppointment(appt1);
                Assert.IsTrue(false);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
        }
        /*
        [TestMethod]
        public void RetrieveAppointmentsByGuestID()
        {
            int guestID = 100000;

            List<Appointment> apptList1 = _appMgr.RetrieveAppointmentsByGuestID(guestID);
            BuildAppointmentList();

            for (int i = 0; i < apptList1.Count; i++)
            {
                Assert.AreEqual(apptList1[i].AppointmentType, _testAppointments[i].AppointmentType);
                Assert.AreEqual(apptList1[i].StartDate, _testAppointments[i].StartDate);
                Assert.AreEqual(apptList1[i].EndDate, _testAppointments[i].EndDate);
                Assert.AreEqual(apptList1[i].GuestID, _testAppointments[i].GuestID);
                Assert.AreEqual(apptList1[i].Description, _testAppointments[i].Description);
            }
        }
        */
        [TestMethod]
        public void RetrieveAppointmentByID()
        {
            Appointment apt1 = new Appointment()
            {
                AppointmentID = 100000,
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 25, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 25, 10, 50, 50),
                Description = "Spa"
            };

            var apt2 = _appMgr.RetrieveAppointmentByID(apt1.AppointmentID);

            Assert.AreEqual(apt1.AppointmentType, apt2.AppointmentType);
            Assert.AreEqual(apt1.StartDate, apt2.StartDate);
            Assert.AreEqual(apt1.EndDate, apt2.EndDate);
            Assert.AreEqual(apt1.GuestID, apt2.GuestID);
            Assert.AreEqual(apt1.Description, apt2.Description);
        }

        [TestMethod]
        public void DeleteAppointment()
        {
            int appointmentIDToDelete = 100001;
            try
            {
                bool results = _appMgr.DeleteAppointmentByID(appointmentIDToDelete);
                Assert.IsTrue(results);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// Testing  to retrieve all appointments

        [TestMethod]
        public void TestRetrieveAllAppointments()
        {
            //Arrange
            List<Appointment> appointments = null;
            //Act
            appointments = _appMgr.RetrieveAppointments();
            //Assert
            CollectionAssert.Equals(_testAppointments, appointments);

        }

        private Appointment BuildNewAppointment()
        {
            Appointment appointment = new Appointment()
            {
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 25, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 25, 10, 50, 50),
                Description = "Stuff"
            };
            return appointment;
        }
        private void BuildAppointmentTypeList()
        {
            AppointmentType at1 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 1",
                Description = "Test Type 1"
            };
            _testAppointmentTypes.Add(at1);

            AppointmentType at2 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 2",
                Description = "Test Type 2"
            };
            _testAppointmentTypes.Add(at2);

            AppointmentType at3 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 3",
                Description = "Test Type 3"
            };
            _testAppointmentTypes.Add(at3);

            AppointmentType at4 = new AppointmentType()
            {
                AppointmentTypeID = "Test Type 4",
                Description = "Test Type 4"
            };
            _testAppointmentTypes.Add(at4);
        }

        private void BuildGuestList()
        {
            AppointmentGuestViewModel apgm1 = new AppointmentGuestViewModel()
            {
                GuestID = 100000,
                FirstName = "John",
                LastName = "Doe",
                Email = "John@Company.com"
            };
            _testGuestViewModels.Add(apgm1);

            AppointmentGuestViewModel apgm2 = new AppointmentGuestViewModel()
            {
                GuestID = 100001,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "Jane@Company.com"
            };
            _testGuestViewModels.Add(apgm2);
        }
        private void BuildAppointmentList()
        {

            Appointment apt1 = new Appointment()
            {
                AppointmentID = nextAppID,
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 25, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 25, 10, 50, 50),
                Description = "Spa"
            };
            nextAppID++;
            _testAppointments.Add(apt1);

            Appointment apt2 = new Appointment()
            {
                AppointmentID = nextAppID,
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 26, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 26, 10, 50, 50),
                Description = "Spa"
            };
            nextAppID++;
            _testAppointments.Add(apt2);

            Appointment apt3 = new Appointment()
            {
                AppointmentID = nextAppID,
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 27, 10, 30, 50),
                EndDate = new DateTime(2020, 12, 27, 10, 50, 50),
                Description = "Spa"
            };
            nextAppID++;
            _testAppointments.Add(apt3);

        }

        private string BuildStringOfGivenLenght(int length)
        {
            string newString = "";
            for (int i = 1; i <= length; i++)
            {
                newString = newString + "*";
            }
            return newString;
        }
    }
}
