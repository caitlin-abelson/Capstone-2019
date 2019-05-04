/// <summary>
/// Author: Dani Russo
/// Created : 2019/02/20
/// Unit tests that test the methods of BuildingManager and contraints of Building
/// </summary>
/// 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;
using LogicLayer;
using System.Text;

namespace LogicLayer.Tests
{
    [TestClass]
    public class BuildingLogicTests
    {
        private IBuildingManager buildingManager;
        private List<Building> buildings;
        private BuildingAccessorMock accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new BuildingAccessorMock();
            buildingManager = new BuildingManager(accessor);
            buildings = new List<Building>();
            buildings = buildingManager.RetrieveAllBuildings();
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
        /// Author: Dani Russo
        /// Created : 2019/02/20
        /// Unit tests for RetrieveAllBuildings method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllBuildings()
        {
            // arrange
            List<Building> testBuildings = null;

            // act
            testBuildings = buildingManager.RetrieveAllBuildings();

            // assert
            CollectionAssert.Equals(testBuildings, buildings);
        }

        /// <summary>
        /// Author: Dani Russo
        /// Created : 2019/02/20
        /// Unit tests for CreateBuilding method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreateBuildingValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.CreateBuilding(testBuilding);

            // assert - check if building was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateBuildingValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = createLongString(50),
                Name = createLongString(150),
                Address = createLongString(150),
                Description = createLongString(1000),
                StatusID = createLongString(25),
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.CreateBuilding(testBuilding);

            // assert - check if building was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingBuildingIDNull()
        {
            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = null,
                Name = "Good Name",
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badBuildingID = testBuilding.BuildingID;

            // act
            bool result = buildingManager.CreateBuilding(testBuilding);

            // assert - check that buildingID did not change
            Assert.AreEqual(badBuildingID, testBuilding.BuildingID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateBuildingBuildingIDTooLong()
        {
            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = createLongString(51),
                Name = "Good Name",
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badBuildingID = testBuilding.BuildingID;

            // act
            bool result = buildingManager.CreateBuilding(testBuilding);

            // assert - check that buildingID did not change
            Assert.AreEqual(badBuildingID, testBuilding.BuildingID);
        }



        [TestMethod]
        public void TestCreateBuildingNameNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = null,
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.CreateBuilding(testBuilding);

            // assert - check if building was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateBuildingNameTooLong()
        {
            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = createLongString(151),
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badName = testBuilding.Name;

            // act
            bool result = buildingManager.CreateBuilding(testBuilding);

            // assert - check that name did not change
            Assert.AreEqual(badName, testBuilding.Name);
        }

        [TestMethod]
        public void TestCreateBuildingAddressNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = null,
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.CreateBuilding(testBuilding);

            // assert - check if building was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateBuildingAddressTooLong()
        {
            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = createLongString(151),
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badAddress = testBuilding.Address;

            // act
            bool result = buildingManager.CreateBuilding(testBuilding);

            // assert - check that address did not change
            Assert.AreEqual(badAddress, testBuilding.Address);
        }

        [TestMethod]
        public void TestCreateBuildingDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = "123 Good Address",
                Description = null,
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.CreateBuilding(testBuilding);

            // assert - check if building was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateBuildingDescriptionTooLong()
        {
            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = "123 Good Address",
                Description = createLongString(1001),
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badDescription = testBuilding.Description;

            // act
            bool result = buildingManager.CreateBuilding(testBuilding);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testBuilding.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateBuildingBuildingStatusIDNull()
        {
            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = null,
                ResortPropertyID = 123456,
            };

            string badStatusID = testBuilding.StatusID;

            // act
            bool result = buildingManager.CreateBuilding(testBuilding);

            // assert - check that statusID did not change
            Assert.AreEqual(badStatusID, testBuilding.StatusID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateBuildingStatusIDTooLong()
        {
            // arrange
            Building testBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = createLongString(26),
                ResortPropertyID = 123456,
            };

            string badStatusID = testBuilding.StatusID;

            // act
            bool result = buildingManager.CreateBuilding(testBuilding);

            // assert - check that statusID did not change
            Assert.AreEqual(badStatusID, testBuilding.StatusID);
        }

        /// <summary>
        /// Author: Dani Russo
        /// Created : 2019/02/20
        /// Unit tests for UpdateBuilding method
        /// </summary>
        /// 

        [TestMethod]
        public void TestUpdateBuildingValidInput()
        {
            // arrange
            bool expectedResult = true;
            bool actualResult;
            Building oldBuilding = buildings[0];    // Update first building in list
            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = "New Good Name",
                Address = "123 New Good Address",
                Description = "New Good Description",
                StatusID = "Undergoing Maintanance",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check if building was updated
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBuildingBadBuildingID()
        {
            // arrange
            // fake building
            Building oldBuilding = new Building()
            {
                BuildingID = "FakeBuildingID",
                Name = "New Good Name",
                Address = "123 New Good Address",
                Description = "New Good Description",
                StatusID = "Undergoing Maintanance",
                ResortPropertyID = 123456,
            };

            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = "New Good Name",
                Address = "123 New Good Address",
                Description = "New Good Description",
                StatusID = "Undergoing Maintanance",
                ResortPropertyID = 123456,
            };

            // act
            bool result = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - building does not exist, catch exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBuildingBuildingIDChanged()
        {
            Building oldBuilding = buildings[0];
            Building newBuilding = new Building()
            {
                BuildingID = "Non Matching ID",
                Name = "New Good Name",
                Address = "123 New Good Address",
                Description = "New Good Description",
                StatusID = "Undergoing Maintanance",
                ResortPropertyID = 123456,
            };

            string badID = newBuilding.BuildingID;
            // act
            bool result = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check that building id did not change
            Assert.AreEqual(badID, newBuilding.BuildingID);
        }

        [TestMethod]
        public void TestUpdateBuildingAddressNull()
        {
            // arrange
            bool expectedResult = true;
            bool actualResult;
            Building oldBuilding = buildings[0];    // Update first building in list
            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = "New Good Name",
                Address = null,
                Description = "New Good Description",
                StatusID = "Undergoing Maintanance",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check if building was updated
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBuildingAddressTooLong()
        {
            // arrange
            Building oldBuilding = buildings[0];
            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = "Good Name",
                Address = createLongString(151),
                Description = "Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badAddress = newBuilding.Address;

            // act
            bool result = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check that address did not change
            Assert.AreEqual(badAddress, newBuilding.Address);

        }

        [TestMethod]
        public void TestUpdateBuildingNameNull()
        {
            // arrange
            bool expectedResult = true;
            bool actualResult;
            Building oldBuilding = buildings[0];    // Update first building in list
            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = null,
                Address = "123 New Good Address",
                Description = "New Good Description",
                StatusID = "Undergoing Maintanance",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check if building was updated
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBuildingNameTooLong()
        {
            // arrange
            Building oldBuilding = buildings[0];
            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = createLongString(151),
                Address = "123 Good Address",
                Description = "New Good Description",
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badName = newBuilding.Name;

            // act
            bool result = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check that address did not change
            Assert.AreEqual(badName, newBuilding.Name);
        }

        [TestMethod]
        public void TestUpdateBuildingDescriptionNull()
        {
            // arrange
            bool expectedResult = true;
            bool actualResult;
            Building oldBuilding = buildings[0];    // Update first building in list
            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = "New Good Name",
                Address = "123 New Good Address",
                Description = null,
                StatusID = "Undergoing Maintanance",
                ResortPropertyID = 123456,
            };

            // act
            actualResult = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check if building was updated
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBuildingDescriptionTooLong()
        {
            // arrange
            Building oldBuilding = buildings[0];
            Building newBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = "123 Good Address",
                Description = createLongString(1001),
                StatusID = "Available",
                ResortPropertyID = 123456,
            };

            string badDescription = newBuilding.Description;

            // act
            bool result = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check that address did not change
            Assert.AreEqual(badDescription, newBuilding.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestUpdateBuildingBuildingStatusIDNull()
        {
            // arrange
            Building oldBuilding = buildings[0];
            Building newBuilding = new Building()
            {
                BuildingID = oldBuilding.BuildingID,
                Name = "Good Name",
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = null,
                ResortPropertyID = 123456,
            };

            string badStatus = newBuilding.StatusID;

            // act
            bool result = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check that address did not change
            Assert.AreEqual(badStatus, newBuilding.StatusID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateBuildingBuildingStatusIDTooLong()
        {
            // arrange
            Building oldBuilding = buildings[0];
            Building newBuilding = new Building()
            {
                BuildingID = "GoodID",
                Name = "Good Name",
                Address = "123 Good Address",
                Description = "Good Description",
                StatusID = createLongString(26),
                ResortPropertyID = 123456,
            };

            string badStatus = newBuilding.StatusID;

            // act
            bool result = buildingManager.UpdateBuilding(oldBuilding, newBuilding);

            // assert - check that address did not change
            Assert.AreEqual(badStatus, newBuilding.StatusID);
        }



    }
}
