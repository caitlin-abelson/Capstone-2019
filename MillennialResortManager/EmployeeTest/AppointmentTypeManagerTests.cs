using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using DataAccessLayer;
using LogicLayer;

namespace LogicLayer.Tests
{
    /// <summary>
    /// Author: Craig Barkley
    /// Created : 2019/02/28
    /// Unit tests that test the methods of AppointmentTypeManager and contraints of  AppointmentType
    /// </summary>
    /// 
    /// <summary>
    /// Summary description for AppointmentTypeManagerTests
    /// </summary>
    [TestClass]
    public class AppointmentTypeManagerTests
    {
        private IAppointmentTypeManager appointmenttManager;
        private List<AppointmentType> appointmentTypes;
        private AppointmentTypeAccessorMock accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new AppointmentTypeAccessorMock();
            appointmenttManager = new AppointmentTypeManager(accessor);
            appointmentTypes = new List<AppointmentType>();
            appointmentTypes = appointmenttManager.RetrieveAllAppointmentTypes("all");
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
        /// Unit tests for RetrieveAllAppointmentTypes method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllAppointmentTypes()
        {
            // arrange
            List<AppointmentType> testpets = null;
            // act
            testpets = appointmenttManager.RetrieveAllAppointmentTypes("all");
            // assert
            CollectionAssert.Equals(testpets, appointmentTypes);
        }

        /// <summary>
        /// Author: Craig Barkley
        /// Created : 2019/02/28
        /// Unit tests for CreateAppointmentType method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreateAppointmentTypeValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            AppointmentType testAppointmentType = new AppointmentType()
            {
                AppointmentTypeID = "GoodID",
                Description = "Good  Long Description",
            };

            // act
            actualResult = appointmenttManager.AddAppointmentType(testAppointmentType);

            // assert - check if AppointmentType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateAppointmentTypeValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            AppointmentType testAppointmentType = new AppointmentType()
            {
                AppointmentTypeID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = appointmenttManager.AddAppointmentType(testAppointmentType);

            // assert - check if AppointmentType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateAppointmentTypeAppointmentTypeIDNull()
        {
            // arrange
            AppointmentType testAppointmentType = new AppointmentType()
            {
                AppointmentTypeID = null,
                Description = "Good Description",
            };

            string badAppointmentTypeID = testAppointmentType.AppointmentTypeID;

            // act
            bool result = appointmenttManager.AddAppointmentType(testAppointmentType);

            // assert - check that AppointmentTypeID did not change
            Assert.AreEqual(badAppointmentTypeID, testAppointmentType.AppointmentTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateAppointmentTypeAppointmentTypeIDTooLong()
        {
            // arrange
            AppointmentType testAppointmentType = new AppointmentType()
            {
                AppointmentTypeID = createLongString(51),
                Description = "Good Description",
            };

            string badAppointmentTypeID = testAppointmentType.AppointmentTypeID;

            // act
            bool result = appointmenttManager.AddAppointmentType(testAppointmentType);

            // assert - check that AppointmentTypeID did not change
            Assert.AreEqual(badAppointmentTypeID, testAppointmentType.AppointmentTypeID);
        }

        [TestMethod]
        public void TestCreateAppointmentTypeDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            AppointmentType testAppointmentType = new AppointmentType()
            {
                AppointmentTypeID = "GoodID",
                Description = null,
            };

            // act
            actualResult = appointmenttManager.AddAppointmentType(testAppointmentType);

            // assert - check if AppointmentType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateAppointmentTypeDescriptionTooLong()
        {
            // arrange
            AppointmentType testAppointmentType = new AppointmentType()
            {
                AppointmentTypeID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testAppointmentType.Description;

            // act
            bool result = appointmenttManager.AddAppointmentType(testAppointmentType);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testAppointmentType.Description);
        }
    }
}
