using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using DataAccessLayer;
using LogicLayer;

namespace UnitTest.LogicLayer
{
    /// <summary>
    /// Author: Craig Barkley
    /// Created : 2019/02/28
    /// Unit tests that test the methods of PetTypeManager and contraints of PetType
    /// </summary>
    /// 
    /// <summary>
    /// Summary description for PetTypeManagerTests
    /// </summary>
    [TestClass]
    public class PetTypeManagerTests
    {
        private IPetTypeManager petManager;
        private List<PetType> petTypes;
        private PetTypeAccessorMock accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new PetTypeAccessorMock();
            petManager = new PetTypeManager(accessor);
            petTypes = new List<PetType>();
            petTypes = petManager.RetrieveAllPetTypes("all");
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
        /// Unit tests for RetrieveAllPetTypes method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllPetTypes()
        {
            // arrange
            List<PetType> testpets = null;
            // act
            testpets = petManager.RetrieveAllPetTypes("all");
            // assert
            CollectionAssert.Equals(testpets, petTypes);
        }

        /// <summary>
        /// Author: Craig Barkley
        /// Created : 2019/02/28
        /// Unit tests for CreatePetType method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreatePetTypeValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            PetType testPetType = new PetType()
            {
                PetTypeID = "GoodID",
                Description = "Good  Long Description",
            };

            // act
            actualResult = petManager.AddPetType(testPetType);

            // assert - check if PetType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreatePetTypeValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            PetType testPetType = new PetType()
            {
                PetTypeID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = petManager.AddPetType(testPetType);

            // assert - check if PetType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreatePetTypePetTypeIDNull()
        {
            // arrange
            PetType testPetType = new PetType()
            {
                PetTypeID = null,
                Description = "Good Description",
            };

            string badPetTypeID = testPetType.PetTypeID;

            // act
            bool result = petManager.AddPetType(testPetType);

            // assert - check that PetTypeID did not change
            Assert.AreEqual(badPetTypeID, testPetType.PetTypeID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreatePetTypePetTypeIDTooLong()
        {
            // arrange
            PetType testPetType = new PetType()
            {
                PetTypeID = createLongString(51),
                Description = "Good Description",
            };

            string badPetTypeID = testPetType.PetTypeID;

            // act
            bool result = petManager.AddPetType(testPetType);

            // assert - check that PetTypeID did not change
            Assert.AreEqual(badPetTypeID, testPetType.PetTypeID);
        }

        [TestMethod]
        public void TestCreatePetTypeDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            PetType testPetType = new PetType()
            {
                PetTypeID = "GoodID",
                Description = null,
            };

            // act
            actualResult = petManager.AddPetType(testPetType);

            // assert - check if PetType was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreatePetTypeDescriptionTooLong()
        {
            // arrange
            PetType testPetType = new PetType()
            {
                PetTypeID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testPetType.Description;

            // act
            bool result = petManager.AddPetType(testPetType);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testPetType.Description);
        }
    }
}
