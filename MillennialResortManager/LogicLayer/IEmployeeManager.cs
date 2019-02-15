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
    }
}
