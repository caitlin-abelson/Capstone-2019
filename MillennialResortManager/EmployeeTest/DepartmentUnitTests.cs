/// <summary>
/// Author: Austin Berquam
/// Created : 2019/02/23
/// Unit tests that test the methods of DepartmentManager and contraints of Department
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
    public class DepartmentUnitTests
    {

        private IDepartmentTypeManager departmentManager;
        private List<Department> departments;
        private MockDepartmentAccessor accessor;

        [TestInitialize]
        public void TestSetup()
        {
            accessor = new MockDepartmentAccessor();
            departmentManager = new DepartmentTypeManager(accessor);
            departments = new List<Department>();
            departments = departmentManager.RetrieveAllDepartments("all");
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
        /// Unit tests for RetrieveAlldepartments method
        /// </summary>
        /// 

        [TestMethod]
        public void TestRetrieveAllDepartments()
        {
            // arrange
            List<Department> testDepartments = null;

            // act
            testDepartments = departmentManager.RetrieveAllDepartments("all");

            // assert
            CollectionAssert.Equals(testDepartments, departments);
        }

        /// <summary>
        /// Author: Austin Berquam
        /// Created : 2019/02/23
        /// Unit tests for Createdepartment method
        /// </summary>
        /// 

        [TestMethod]
        public void TestCreatedepartmentValidInput()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Department testdepartment = new Department()
            {
                DepartmentID = "GoodID",
                Description = "Good Description",
            };

            // act
            actualResult = departmentManager.CreateDepartment(testdepartment);

            // assert - check if department was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateDepartmentValidInputMaxLengths()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Department testDepartment = new Department()
            {
                DepartmentID = createLongString(50),
                Description = createLongString(1000),
            };

            // act
            actualResult = departmentManager.CreateDepartment(testDepartment);

            // assert - check if department was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCreateDepartmentdepartmentIDNull()
        {
            // arrange
            Department testDepartment = new Department()
            {
                DepartmentID = null,
                Description = "Good Description",
            };

            string badDepartmentID = testDepartment.DepartmentID;

            // act
            bool result = departmentManager.CreateDepartment(testDepartment);

            // assert - check that departmentID did not change
            Assert.AreEqual(badDepartmentID, testDepartment.DepartmentID);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateDepartmentDepartmentIDTooLong()
        {
            // arrange
            Department testDepartment = new Department()
            {
                DepartmentID = createLongString(51),
                Description = "Good Description",
            };

            string badDepartmentID = testDepartment.DepartmentID;

            // act
            bool result = departmentManager.CreateDepartment(testDepartment);

            // assert - check that departmentID did not change
            Assert.AreEqual(badDepartmentID, testDepartment.DepartmentID);
        }

        [TestMethod]
        public void TestCreateDepartmentDescriptionNull()
        {
            bool expectedResult = true;
            bool actualResult;

            // arrange
            Department testDepartment = new Department()
            {
                DepartmentID = "GoodID",
                Description = null,
            };

            // act
            actualResult = departmentManager.CreateDepartment(testDepartment);

            // assert - check if department was added
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateDepartmentDescriptionTooLong()
        {
            // arrange
            Department testDepartment = new Department()
            {
                DepartmentID = "GoodID",
                Description = createLongString(1001),
            };

            string badDescription = testDepartment.Description;

            // act
            bool result = departmentManager.CreateDepartment(testDepartment);

            // assert - check that description did not change
            Assert.AreEqual(badDescription, testDepartment.Description);
        }

    }
}
