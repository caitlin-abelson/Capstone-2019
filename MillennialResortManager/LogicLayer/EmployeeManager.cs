using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using System.Security.Cryptography;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Caitlin Abelson" created="2019/01/30">
	/// Author: 
	/// Created Date: 1/30/19
	/// 
	/// The EmployeeManager class implements the IEmployeeManager interface and all of it's CRUD methods.
	/// </summary>
	public class EmployeeManager : IEmployeeManager
	{
		private IEmployeeAccessor _employeeAccessor;

		/// <summary author="Caitlin Abelson" created="2019/01/30">
		/// The constructor for the EmployeeManager class
		/// </summary>
		public EmployeeManager()
		{
			_employeeAccessor = new EmployeeAccessor();
		}

		/// <summary author="Caitlin Abelson" created="2019/02/14">
		/// Constructor for the mock accessor
		/// </summary>
		/// <param name="employeeAccessorMock"></param>
		public EmployeeManager(EmployeeAccessorMock employeeAccessorMock)
		{
			_employeeAccessor = employeeAccessorMock;
		}

		/// <summary author="Caitlin Abelson" created="2019/02/14">
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

		/// <summary author="Caitlin Abelson" created="2019/02/14">
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

		/// <summary author="Caitlin Abelson" created="2019/02/14">
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

		/// <summary author="Caitlin Abelson" created="2019/02/14">
		/// Validation for phone number
		/// </summary>
		/// <param name="phoneNumber"></param>
		/// <returns></returns>
		public bool validatePhoneNumber(string phoneNumber)
		{
			if (phoneNumber.Length >= 11)
			{
				return false;
			}
			return true;
		}

		/// <summary author="Caitlin Abelson" created="2019/02/14">
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

		/// <summary author="Caitlin Abelson" created="2019/02/14">
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

		/// <summary author="Caitlin Abelson" created="2019/01/30">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Caitlin Abelson" created="2019/02/03">
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
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
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
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
		}

		/// <summary author="Caitlin Abelson" created="2019/02/02">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Caitlin Abelson" created="2019/02/07">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return employees;
		}

		/// <summary author="Caitlin Abelson" created="2019/02/02">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return employee;
		}

		/// <summary author="Caitlin Abelson" created="2019/02/14">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return employees;
		}

		/// <summary author="Caitlin Abelson" created="2019/02/14">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return employees;
		}

		/// <summary author="Matt LaMarche" created="2019/03/07">
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
					throw new ApplicationException("User not found.");
				}

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw new ApplicationException(ex.Message, ex);  // ex as the inner exception, we are preserving the inner exception
			}

			return employee;
		}

		/// <summary author="Matt LaMarche" created="2019/03/07">
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

			// now, we just need to build the result string with a StringBuilder
			var s = new StringBuilder();

			// loop through the bytes creating hex digits
			for (int i = 0; i < data.Length; i++)
			{
				s.Append(data[i].ToString("x2"));       //x2 - formating string will take byte char and give the hexidecimal string 
			}

			// conver StringBuilder to a string
			result = s.ToString();

			return result;
		}

		/// <summary author="Alisa Roehr" created="2019/04/05">
		/// get an employee id when first created.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public Employee RetrieveEmployeeIDByEmail(string email)
		{
			Employee employee = new Employee();

			try
			{
				employee = _employeeAccessor.RetrieveEmployeeByEmail(email);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return employee;
		}

		/// <summary author="Alisa Roehr" created="2019/04/01">
		/// add a role to an employee.
		/// </summary>
		/// <param name="employeeID"></param>
		/// <param name="role"></param>
		public void AddEmployeeRole(int employeeID, Role role)
		{
			try
			{
				_employeeAccessor.InsertEmployeeRole(employeeID, role);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Alisa Roehr" created="2019/04/01">
		/// remove a role from an employee
		/// </summary>
		/// <param name="employeeID"></param>
		/// <param name="role"></param>
		public void RemoveEmployeeRole(int employeeID, Role role)
		{
			try
			{
				_employeeAccessor.DeleteEmployeeRole(employeeID, role);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Alisa Roehr" created="2019/04/01">
		/// get all the roles from the employee.
		/// </summary>
		/// <param name="EmployeeID"></param>
		/// <returns></returns>
		public List<Role> SelectEmployeeRoles(int EmployeeID)
		{
			List<Role> roles;
			try
			{
				roles = _employeeAccessor.RetrieveEmployeeRoles(EmployeeID);
			}

			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return roles;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to retrieve all employeeinfo 
		/// </summary>
		public List<Employee> RetrieveAllEmployeeInfo()
		{
			var employees = new List<Employee>();

			try
			{
				employees = _employeeAccessor.RetrieveAllEmployeeInfo();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return employees;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// Method to retrieve all employeeinfo by employeeid
		/// </summary>
		public Employee RetrieveEmployeeInfo(int employeeID)
		{
			Employee employee = new Employee();

			try
			{
				employee = _employeeAccessor.RetrieveEmployeeInfo(employeeID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return employee;
		}
	}
}