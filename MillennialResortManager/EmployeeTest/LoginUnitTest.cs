using System;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class LoginUnitTest
    {
        private Employee _validEmployee;
        private Employee _invalidEmployee;
        private UserManager _userManager;
        [TestInitialize]
        public void TestSetup()
        {
            _validEmployee = new Employee();

            _invalidEmployee = new Employee();

            _userManager = new UserManager();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/15/2019
        /// Unit tests to validate Logging an employee in
        /// </summary>
        [TestMethod]
        public void TestAuthenticateEmployeeValid()
        {
            //Arange
            string username = "joanne@company.com";
            string password = "newuser";
            Employee temp;
            //Act
            temp = _userManager.AuthenticateEmployee(username,password);
            //Assert
            Assert.AreEqual(temp.FirstName, "Joanne");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeInvalidUsername()
        {
            //Arange
            string username = "joanne@company.comm";
            string password = "newuser";
            Employee temp;
            //Act
            temp = _userManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeInvalidPassword()
        {
            //Arange
            string username = "joanne@company.com";
            string password = "newuser4123";
            Employee temp;
            //Act
            temp = _userManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }
    }
}
