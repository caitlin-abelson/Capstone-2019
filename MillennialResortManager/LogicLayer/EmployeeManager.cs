using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using System.Security.Cryptography;

namespace LogicLayer
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 1/30/19
    /// 
    /// The EmployeeManager class implements the IEmployeeManager interface and all of it's CRUD methods.
    /// </summary>
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _employeeAccessor;

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The constructor for the EmployeeManager class
        /// </summary>
        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Constructor for the mock accessor
        /// </summary>
        /// <param name="employeeAccessorMock"></param>
        public EmployeeManager(EmployeeAccessorMock employeeAccessorMock)
        {
            _employeeAccessor = employeeAccessorMock;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Method to see if everything is valid.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool isValid(Employee employee)
        {
            if (validateFirstName(employee.FirstName) && validateLastName(employee.LastName) &&
                validatePhoneNumber(employee.PhoneNumber) && validateEmail(employee.Email) &&
                validateDepartmentID(employee.DepartmentID))
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Validation for first name
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public bool validateFirstName(string firstName)
        {

            if (firstName.Length < 1 || firstName.Length > 50)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Validation for last name
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public bool validateLastName(string lastName)
        {
            if (lastName.Length < 1 || lastName.Length > 100)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Validation for phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public bool validatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 11)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Validation for email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool validateEmail(string email)
        {
            if (email.Length < 7 || email.Length > 250)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Validation for DepartmentID
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        public bool validateDepartmentID(string departmentID)
        {
            if (departmentID.Length < 1 || departmentID.Length > 50)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The InsertEmployee method adds and employee to the database.
        /// </summary>
        public void InsertEmployee(Employee newEmployee)
        {
            try
            {
                if (!isValid(newEmployee))
                {
                    throw new ArgumentException("Invalid data for the employee.");
                }
                _employeeAccessor.InsertEmployee(newEmployee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/3/19
        /// 
        /// The DeleteEmployee method return an active or inactive employee from the EmployeeAccessor
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="isActive"></param>
        public void DeleteEmployee(int employeeID, bool isActive)
        {
            // If the employee is active, they must be deactived first.
            if (isActive == true)
            {
                try
                {
                    _employeeAccessor.DeactiveEmployee(employeeID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            // If the employee is already inactive, they will be taken out of the system.
            else
            {
                try
                {
                    _employeeAccessor.DeleteEmployeeRole(employeeID);
                    _employeeAccessor.DeleteEmployee(employeeID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/2/19
        /// 
        /// The UpdateEmployee method returns a new employee and an existing employee from the EmployeeAccessor
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <param name="oldEmployee"></param>
        public void UpdateEmployee(Employee newEmployee, Employee oldEmployee)
        {
            try
            {
                if (!isValid(newEmployee))
                {
                    throw new ArgumentException("Data is invalid for this employee.");
                }
                _employeeAccessor.UpdateEmployee(newEmployee, oldEmployee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/7/19
        /// 
        /// This method gets all of the employees in the table
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _employeeAccessor.SelectAllEmployees();
            }
            catch (Exception)
            {
                throw;
            }
            return employees;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/2/19
        /// 
        /// The SelectEmployee method returns an employee from the EmployeeAccessor
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee SelectEmployee(int employeeID)
        {
            Employee employee = new Employee();

            try
            {
                employee = _employeeAccessor.SelectEmployee(employeeID);
            }
            catch (Exception)
            {
                throw;
            }

            return employee;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Retrieves a list of all of the active employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectAllActiveEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _employeeAccessor.SelectActiveEmployees();
            }
            catch (Exception)
            {
                throw;
            }
            return employees;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/14/19
        /// 
        /// Retrieves a list of all of the inactive employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectAllInActiveEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _employeeAccessor.SelectInactiveEmployees();
            }
            catch (Exception)
            {
                throw;
            }
            return employees;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created Date: 3/7/19
        /// Taken straight from Jims code and modified to return an Employee instead of a User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Employee Object</returns>
        public Employee AuthenticateEmployee(string username, string password)
        {
            if (username == null || password == null)
            {
                throw new ApplicationException("Username or password was null. ");
            }
            Employee employee = null;

            // hast the password
            password = hashSHA256(password);

            // this is unsafe code...
            try
            {
                if (1 == _employeeAccessor.VerifyUsernameAndPassword(username, password))     // if the user is verified I want to create a user object
                {
                    // the user is validated, so instantiate a user
                    employee = _employeeAccessor.RetrieveEmployeeByEmail(username);

                    if (password == hashSHA256("newuser"))
                    {
                        //user.Roles.Clear();
                        //user.Roles.Add("New User");
                    }
                }
                else
                {
                    throw new ApplicationException("User not found. ");
                }

            }
            catch (Exception ex)       // this is were we would communicate with the log
            {
                throw new ApplicationException(ex.Message, ex);  // ex as the inner exception, we we are preserving the inner exception
            }

            return employee;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created Date: 3/7/19
        /// Taken straight from Jims code. Converts a string to its SHA256 equivalent
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string hashSHA256(string source)        // source is the password passed in 
        {
            string result = "";

            // we need a byte array, hash algorthms do not work on strings or characters
            byte[] data;

            // use a .NET hash provider
            using (SHA256 sha256hash = SHA256.Create())      //using is a complier directive, do not confuse with using statements above which is a C# keyword 
            {
                // hash the input
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            // now, we just need to build the result string with a String Builder
            var s = new StringBuilder();

            // loop through the bytes creating hex digits
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));       //x2 - formating string will take byte char and give the hexidecimal string 
            }

            // conver String Builder to a string
            result = s.ToString();

            return result;
        }

    }
}
