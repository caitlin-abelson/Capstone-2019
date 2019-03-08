/// <summary>
/// Author: Austin Berquam
/// Created : 2019/03/04
/// Unit tests that test the methods of MaintenanceTypeManager and contraints of MaintenanceType
/// </summary>
/// 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;
using LogicLayer;
using System.Text;
namespace MaintenanceTypeTests
{
    [TestClass]
    public class MaintenanceTypeUnitTests
    {
        private IMaintenanceTypeManager maintenanceManager;
        private List<MaintenanceTypes> types;
        private MockMaintenanceTypeAccessor accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new MockMaintenanceTypeAccessor();
            maintenanceManager = new MaintenanceTypeManager(accessor);
            types = new List<MaintenanceTypes>();
            types = maintenanceManager.RetrieveMaintenanceTypes("all");
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
        /// Unit tests for RetrieveAlltypes method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllMaintenanceTypes()
        {
            // arrange
            List<MaintenanceTypes> testtypes = null;

            // act
            testtypes = maintenanceManager.RetrieveMaintenanceTypes("all");

            // assert
            CollectionAssert.Equals(testtypes, types);
        }

        /// <summary>
        /// Author: Austin Berquam
        /// Created : 2019/02/23
        /// Unit tests for CreateMaintenanceType method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreateMaintenanceTypeValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            MaintenanceTypes testMaintenanceType = new MaintenanceTypes()
            {
                MaintenanceTypeID = "GoodID",
                Description = "Good Description",
            };

            // act
            actualResult = maintenanceManager.CreateMaintenanceType(testMaintenanceType);

            // assert - check if MaintenanceType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateMaintenanceTypeValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            MaintenanceTypes testMaintenanceType = new MaintenanceTypes()
            {
                MaintenanceTypeID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = maintenanceManager.CreateMaintenanceType(testMaintenanceType);

            // assert - check if MaintenanceType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateMaintenanceTypeMaintenanceTypeIDNull()
        {
            // arrange
            MaintenanceTypes testMaintenanceType = new MaintenanceTypes()
            {
                MaintenanceTypeID = null,
                Description = "Good Description",
            };

            string badMaintenanceTypeID = testMaintenanceType.MaintenanceTypeID;

            // act
            bool result = maintenanceManager.CreateMaintenanceType(testMaintenanceType);

            // assert - check that MaintenanceTypeID did not change
            Assert.AreEqual(badMaintenanceTypeID, testMaintenanceType.MaintenanceTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateMaintenanceTypeMaintenanceTypeIDTooLong()
        {
            // arrange
            MaintenanceTypes testMaintenanceType = new MaintenanceTypes()
            {
                MaintenanceTypeID = createLongString(51),
                Description = "Good Description",
            };

            string badMaintenanceTypeID = testMaintenanceType.MaintenanceTypeID;

            // act
            bool result = maintenanceManager.CreateMaintenanceType(testMaintenanceType);

            // assert - check that MaintenanceTypeID did not change
            Assert.AreEqual(badMaintenanceTypeID, testMaintenanceType.MaintenanceTypeID);
        }

        [TestMethod]
        public void TestCreateMaintenanceTypeDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            MaintenanceTypes testMaintenanceType = new MaintenanceTypes()
            {
                MaintenanceTypeID = "GoodID",
                Description = null,
            };

            // act
            actualResult = maintenanceManager.CreateMaintenanceType(testMaintenanceType);

            // assert - check if MaintenanceType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateMaintenanceTypeDescriptionTooLong()
        {
            // arrange
            MaintenanceTypes testMaintenanceType = new MaintenanceTypes()
            {
                MaintenanceTypeID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testMaintenanceType.Description;

            // act
            bool result = maintenanceManager.CreateMaintenanceType(testMaintenanceType);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testMaintenanceType.Description);
        }
    }
}
