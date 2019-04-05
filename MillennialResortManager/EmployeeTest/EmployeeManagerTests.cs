using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataObjects;
using DataAccessLayer;


namespace UnitTests
{
    [TestClass]
    public class EmployeeManagerTests
    {
        private IEmployeeManager _employeeManager;
        private List<Employee> _employee;
        private EmployeeAccessorMock _employeeMock;

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Setting up the TestInitializer
        /// </summary>
        [TestInitialize]
        public void TestSetupLayers()
        {
            _employeeMock = new EmployeeAccessorMock();
            _employeeManager = new EmployeeManager(_employeeMock);
            _employee = new List<Employee>();
            _employee = _employeeManager.SelectAllEmployees();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// A simple string builder helper method
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        private string createString(int strlength)
        {
            string longString = "";
            for (int i = 0; i < strlength; i++)
            {
                longString += "o";
            }
            return longString;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Setting up the update employee
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <param name="oldEmployee"></param>
        private void setUpdateEmployee(Employee newEmployee, Employee oldEmployee)
        {
            newEmployee.EmployeeID = oldEmployee.EmployeeID;
            newEmployee.FirstName = oldEmployee.FirstName;
            newEmployee.LastName = oldEmployee.LastName;
            newEmployee.PhoneNumber = oldEmployee.PhoneNumber;
            newEmployee.Email = oldEmployee.Email;
            newEmployee.DepartmentID = oldEmployee.DepartmentID;
            newEmployee.Active = newEmployee.Active;
        }




        //CREATE TEST METHODS


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing valid input for creating an employee
        /// </summary>
        [TestMethod]
        public void TestCreateEmployeeValidInput()
        {
            //Arrange
            Employee newEmployee = new Employee() { EmployeeID = 100005, FirstName = "John", LastName = "Duden", PhoneNumber = "1319555467", Email = "john.duden@company.com", DepartmentID = "Talent", Active = true };
            //Act
            _employeeManager.InsertEmployee(newEmployee);
            //Assert
            _employee = _employeeManager.SelectAllEmployees();

            Assert.IsNotNull(_employee.Find(x => x.EmployeeID == newEmployee.EmployeeID &&
                                                x.FirstName == newEmployee.FirstName &&
                                                x.LastName == newEmployee.LastName &&
                                                x.PhoneNumber == newEmployee.PhoneNumber &&
                                                x.Email == newEmployee.Email &&
                                                x.DepartmentID == newEmployee.DepartmentID &&
                                                x.Active == newEmployee.Active));
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for first name when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputFirstNameLongString()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = createString(51),
                LastName = "Duden",
                PhoneNumber = "13195554677",
                Email = "john.duden@company.com",
                DepartmentID = "Talent",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for first name when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputFirstNameEmptyString()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "",
                LastName = "Duden",
                PhoneNumber = "13195554677",
                Email = "john.duden@company.com",
                DepartmentID = "Talent",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for last name when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputLastNameLongString()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "John",
                LastName = createString(101),
                PhoneNumber = "13195554677",
                Email = "john.duden@company.com",
                DepartmentID = "Talent",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for last name when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputLastNameEmptyString()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "John",
                LastName = "",
                PhoneNumber = "13195554677",
                Email = "john.duden@company.com",
                DepartmentID = "Talent",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the phone number when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputPhoneNumber()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "John",
                LastName = "Duden",
                PhoneNumber = "131955546447",
                Email = "john.duden@company.com",
                DepartmentID = "Talent",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the email when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputEmailLongString()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "John",
                LastName = "Duden",
                PhoneNumber = "13195554677",
                Email = createString(251),
                DepartmentID = "Talent",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the email when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputEmailEmptyString()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "John",
                LastName = "Duden",
                PhoneNumber = "13195554677",
                Email = "",
                DepartmentID = "Talent",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the department when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputDepartmentLongLength()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "John",
                LastName = "Duden",
                PhoneNumber = "13195554677",
                Email = "john.duden@company.com",
                DepartmentID = createString(51),
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the department when creating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateEmployeeInValidInputDepartmentEmptyLength()
        {
            //Arrange
            Employee employee = new Employee()
            {
                EmployeeID = 100005,
                FirstName = "John",
                LastName = "Duden",
                PhoneNumber = "13195554677",
                Email = "john.duden@company.com",
                DepartmentID = "",
                Active = true
            };
            //Act
            _employeeManager.InsertEmployee(employee);
        }

        //SELECT TEST METHODS

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing valid input when selecting an employee
        /// </summary>
        [TestMethod]
        public void TestSelectEmployeeValidInput()
        {
            //Arrange
            Employee employee = new Employee();

            //Act
            employee = _employeeManager.SelectEmployee(_employee[0].EmployeeID);

            //Assert
            Assert.AreEqual(employee.EmployeeID, _employee[0].EmployeeID);
            Assert.AreEqual(employee.FirstName, _employee[0].FirstName);
            Assert.AreEqual(employee.LastName, _employee[0].LastName);
            Assert.AreEqual(employee.PhoneNumber, _employee[0].PhoneNumber);
            Assert.AreEqual(employee.Email, _employee[0].Email);
            Assert.AreEqual(employee.DepartmentID, _employee[0].DepartmentID);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input when selecting an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestSelectEmployeeInValidInput()
        {
            //Arrange
            Employee employee = new Employee();
            int invalidEmployeeID = -1;

            //Act
            employee = _employeeManager.SelectEmployee(invalidEmployeeID);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing valid input when selecting all employees
        /// </summary>
        public void TestSelectAllEmployeesValidInput()
        {
            //Arrange
            List<Employee> employees = new List<Employee>();

            //Act
            employees = _employeeManager.SelectAllEmployees();

            //Assert
            Assert.IsNotNull(employees);
        }


        // UPDATE TEST METHODS

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing valid input when updating an employee
        /// </summary>
        [TestMethod]
        public void TestUpdateEmployeeValidInput()
        {
            //Arrange
            Employee newEmployee = new Employee();
            setUpdateEmployee(newEmployee, _employee[0]);

            //Act
            _employeeManager.UpdateEmployee(newEmployee, _employee[0]);

            //Assert
            _employee = _employeeManager.SelectAllEmployees();
            Assert.AreEqual(_employeeManager.SelectEmployee(_employee[0].EmployeeID),
                _employeeManager.SelectEmployee(newEmployee.EmployeeID));

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the first name when updating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEmployeeInvalidInputFirstName()
        {
            //Arrange
            Employee newEmployee = new Employee();
            setUpdateEmployee(newEmployee, _employee[0]);
            newEmployee.FirstName = createString(51);

            //Act
            _employeeManager.UpdateEmployee(newEmployee, _employee[0]);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the last name when updating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEmployeeInvalidInputLastName()
        {
            //Arrange
            Employee newEmployee = new Employee();
            setUpdateEmployee(newEmployee, _employee[0]);
            newEmployee.LastName = createString(101);

            //Act
            _employeeManager.UpdateEmployee(newEmployee, _employee[0]);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the phone number when updating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEmployeeInvalidInputPhoneNumber()
        {
            //Arrange
            Employee newEmployee = new Employee();
            setUpdateEmployee(newEmployee, _employee[0]);
            newEmployee.PhoneNumber = createString(12);

            //Act
            _employeeManager.UpdateEmployee(newEmployee, _employee[0]);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the email when updating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEmployeeInvalidInputEmail()
        {
            //Arrange
            Employee newEmployee = new Employee();
            setUpdateEmployee(newEmployee, _employee[0]);
            newEmployee.Email = createString(251);

            //Act
            _employeeManager.UpdateEmployee(newEmployee, _employee[0]);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid input for the department when updating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateEmployeeInvalidInputDepartment()
        {
            //Arrange
            Employee newEmployee = new Employee();
            setUpdateEmployee(newEmployee, _employee[0]);
            newEmployee.DepartmentID = createString(51);

            //Act
            _employeeManager.UpdateEmployee(newEmployee, _employee[0]);
        }


        // DEACTIVATION TEST METHODS

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing valid input for deactivating an employee
        /// </summary>
        [TestMethod]
        public void TestDeactivateEmployeeValid()
        {
            //Arrange
            int employeeID = _employee[0].EmployeeID;
            bool activeEmployee = _employee[0].Active;

            //Act
            _employeeManager.DeleteEmployee(employeeID, activeEmployee);

            //Assert
            Assert.IsFalse(_employeeManager.SelectEmployee(employeeID).Active);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing invalid inpot for deactivating an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateEmployeeInvalidEmployeeID()
        {
            //Arrange
            int employeeID = -1;
            bool activeEmployee = true;

            //Act
            _employeeManager.DeleteEmployee(employeeID, activeEmployee);
        }

        // DELETION TEST METHOD

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created date: 2/14/19
        /// 
        /// Testing valid input for deleting an employee
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteEmployeeValid()
        {
            //Arrange
            int employeeID = _employee[0].EmployeeID;
            bool activeEmployee = _employee[0].Active;

            //Act
            _employeeManager.DeleteEmployee(employeeID, false);

            //Assert
            _employeeManager.SelectEmployee(employeeID);
        }
    }
}
