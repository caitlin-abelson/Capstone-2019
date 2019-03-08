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

namespace UnitTests
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
            results = _appMgr.CreateAppointment(appointment);
            Assert.IsTrue(results);
        }

        [TestMethod]
        public void CreateNewAppointmentTypeTooLong()
        {
            var appointmnet = BuildNewAppointment();
            appointmnet.AppointmentType = BuildStringOfGivenLenght(26);
            try
            {
                _appMgr.CreateAppointment(appointmnet);
            }
            catch (ApplicationException)
            {
                Assert.IsTrue(true);
            }
            catch(Exception)
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
                _appMgr.CreateAppointment(appointmnet);
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
                _appMgr.CreateAppointment(appointment);
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
                _appMgr.CreateAppointment(appointment);
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
        public void CreateNewAppointmentNoEndtDate()
        {
            Appointment appointment = new Appointment()
            {
                AppointmentType = "Spa",
                GuestID = 100000,
                StartDate = new DateTime(2020, 12, 25, 10, 50, 50),
                EndDate = new DateTime(2020, 12, 24, 10, 50, 50),
                Description = "Stuff"
            };
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
                _appMgr.CreateAppointment(appointment);
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
                _appMgr.CreateAppointment(appointmnet);
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
            for (int i = 1; i == length; i++)
            {
                newString = newString + "*";
            }
            return newString;
        }
    }
}
