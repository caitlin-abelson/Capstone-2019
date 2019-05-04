/// <summary>
/// Author: Austin Berquam
/// Created : 2019/02/23
/// Unit tests that test the methods of EmpRolesManager and contraints of EmpRoles
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
    public class EmployeeRolesUnitTests
    {
        private IEmpRolesManager rolesManager;
        private List<EmpRoles> roless;
        private MockEmpRolesAccessor accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new MockEmpRolesAccessor();
            rolesManager = new EmpRolesManager(accessor);
            roless = new List<EmpRoles>();
            roless = rolesManager.RetrieveAllRoles("all");
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
        /// Unit tests for RetrieveAllroless method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllEmpRoless()
        {
            // arrange
            List<EmpRoles> testroless = null;

            // act
            testroless = rolesManager.RetrieveAllRoles("all");

            // assert
            CollectionAssert.Equals(testroless, roless);
        }

        /// <summary>
        /// Author: Austin Berquam
        /// Created : 2019/02/23
        /// Unit tests for Createroles method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreaterolesValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            EmpRoles testroles = new EmpRoles()
            {
                RoleID = "GoodID",
                Description = "Good Description",
            };

            // act
            actualResult = rolesManager.CreateRole(testroles);

            // assert - check if roles was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreaterolesValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            EmpRoles testroles = new EmpRoles()
            {
                RoleID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = rolesManager.CreateRole(testroles);

            // assert - check if roles was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreaterolesrolesIDNull()
        {
            // arrange
            EmpRoles testroles = new EmpRoles()
            {
                RoleID = null,
                Description = "Good Description",
            };

            string badrolesID = testroles.RoleID;

            // act
            bool result = rolesManager.CreateRole(testroles);

            // assert - check that rolesID did not change
            Assert.AreEqual(badrolesID, testroles.RoleID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreaterolesrolesIDTooLong()
        {
            // arrange
            EmpRoles testroles = new EmpRoles()
            {
                RoleID = createLongString(51),
                Description = "Good Description",
            };

            string badrolesID = testroles.RoleID;

            // act
            bool result = rolesManager.CreateRole(testroles);

            // assert - check that rolesID did not change
            Assert.AreEqual(badrolesID, testroles.RoleID);
        }

        [TestMethod]
        public void TestCreaterolesDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            EmpRoles testroles = new EmpRoles()
            {
                RoleID = "GoodID",
                Description = null,
            };

            // act
            actualResult = rolesManager.CreateRole(testroles);

            // assert - check if roles was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreaterolesDescriptionTooLong()
        {
            // arrange
            EmpRoles testroles = new EmpRoles()
            {
                RoleID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testroles.Description;

            // act
            bool result = rolesManager.CreateRole(testroles);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testroles.Description);
        }
    }
}
