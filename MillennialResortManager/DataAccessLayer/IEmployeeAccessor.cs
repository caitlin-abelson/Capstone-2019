using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IEmployeeAccessor
    {
        List<Employee> SelectAllEmployees();
        List<Employee> SelectActiveEmployees();
        List<Employee> SelectInactiveEmployees();
        void InsertEmployee(Employee newEmployee);
        void UpdateEmployee(Employee newEmployee, Employee oldEmployee);
        Employee SelectEmployee(int employeeID);
        void DeleteEmployee(int employeeID);
        void DeleteEmployeeRole(int employeeID);
        void DeactiveEmployee(int employeeID);
    }
}
