using System;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class LoginUnitTest
    {
        private IEmployeeManager _employeeManager;
        private EmployeeAccessorMock _employeeMock;
        [TestInitialize]
        public void TestSetup()
        {
            _employeeMock = new EmployeeAccessorMock();

            _employeeManager = new EmployeeManager(_employeeMock);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/15/2019
        /// 
        /// Unit tests to validate Logging an employee in
        /// Author: Matt LaMarche
        /// Updated : 3/7/2019
        /// Updated Login Unit tests to use EmployeeAccessor instead of UserAccessor
        /// </summary>
        [TestMethod]
        public void TestAuthenticateEmployeeValid()
        {
            //Arange
            string username = "harry.jingles@company.com";
            string password = "password";
            Employee temp;
            //Act
            temp = _employeeManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.AreEqual(temp.FirstName, "Harry");
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeInvalidUsername()
        {
            //Arange
            string username = "harry.jingles@company.comm";
            string password = "password";
            Employee temp;
            //Act
            temp = _employeeManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeInvalidPassword()
        {
            //Arange
            string username = "harry.jingles@company.com";
            string password = "badpassword";
            Employee temp;
            //Act
            temp = _employeeManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeNullUsername()
        {
            //Arange
            string username = null;
            string password = "password";
            Employee temp;
            //Act
            temp = _employeeManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeNullPassword()
        {
            //Arange
            string username = "harry.jingles@company.com";
            string password = null;
            Employee temp;
            //Act
            temp = _employeeManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeShortUsername()
        {
            //Arange
            string username = "";
            string password = "password";
            Employee temp;
            //Act
            temp = _employeeManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAuthenticateEmployeeShortPassword()
        {
            //Arange
            string username = "harry.jingles@company.com";
            string password = "";
            Employee temp;
            //Act
            temp = _employeeManager.AuthenticateEmployee(username, password);
            //Assert
            Assert.IsNull(temp);
        }
    }
}
