using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Caitlin Abelson" created="2019/01/30">
	/// The IEmployeeManager is the interface for Employees and hold all the CRUD methods for the logic layer.
	/// </summary>
	/// <updates>
	/// <update author="Matt LaMarche" date="2019/03/07">
	/// Added in AuthenticateEmployee for login features
	/// </update>
	/// <update author="Eduardo Colon" date="2019/03/20">
	/// Added RetrieveAllEmployeeInfo and RetrieveEmployeeInfo.
	/// </update>
	/// </updates>
	public interface IEmployeeManager
    {
        void InsertEmployee(Employee newEmployee);
        void UpdateEmployee(Employee newEmployee, Employee oldEmployee);
        Employee SelectEmployee(int employeeID);
        List<Employee> SelectAllEmployees();
        List<Employee> SelectAllActiveEmployees();
        List<Employee> SelectAllInActiveEmployees();
        void DeleteEmployee(int employeeID, bool isActive);
        Employee AuthenticateEmployee(string username, string password);
        Employee RetrieveEmployeeIDByEmail(string email);
        List<Role> SelectEmployeeRoles(int EmployeeID);
        void AddEmployeeRole(int employeeID, Role role);
        void RemoveEmployeeRole(int employeeID, Role role);
        List<Employee> RetrieveAllEmployeeInfo();
        Employee RetrieveEmployeeInfo(int employeeID);
    }
}