using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 1/30/19
    /// 
    /// The IEmployeeManager is the interface for Employees and hold all the CRUD methods for the logic layer.
    /// 
    /// Author: Matt LaMarche
    /// Updated Date: 3/7/19
    /// 
    /// Added in AuthenticateEmployee for login features
    /// </summary>
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
        List<Employee> RetrieveAllEmployeeInfo(); //eduardo colon 2019-03-20
        Employee RetrieveEmployeeInfo(int employeeID);//eduardo colon 2019-03-20
    }
}
