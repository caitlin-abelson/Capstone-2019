using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class EmployeeAccessorMock : IEmployeeAccessor
    {
        private List<Employee> _employee;
        private List<int> _allEmployees;

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Constructor for mock accessor
        /// </summary>
        public EmployeeAccessorMock()
        {
            _employee = new List<Employee>();
            _employee.Add(new Employee() { EmployeeID = 100000, FirstName = "Harry", LastName = "Jingles", PhoneNumber = "13195554657", Email = "harry.jingles@company.com", DepartmentID = "Events", Active = true });
            _employee.Add(new Employee() { EmployeeID = 100001, FirstName = "Jack", LastName = "Parsh", PhoneNumber = "13195554652", Email = "jack.parsh@company.com", DepartmentID = "Kitchen", Active = true });
            _employee.Add(new Employee() { EmployeeID = 100002, FirstName = "Jane", LastName = "Doob", PhoneNumber = "13195554658", Email = "jane.doob@company.com", DepartmentID = "Catering", Active = true });
            _employee.Add(new Employee() { EmployeeID = 100003, FirstName = "Barb", LastName = "Marsh", PhoneNumber = "13195554637", Email = "barb.marsh@company.com", DepartmentID = "Grooming", Active = true });
            _employee.Add(new Employee() { EmployeeID = 100004, FirstName = "Toby", LastName = "Fish", PhoneNumber = "13195554677", Email = "toby.fish@company.com", DepartmentID = "Talent", Active = true });

            _allEmployees = new List<int>();
            foreach (var worker in _employee)
            {
                _allEmployees.Add(worker.EmployeeID);
            }
            
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Inserting an employee for mock accessor
        /// </summary>
        /// <param name="newEmployee"></param>
        public void InsertEmployee(Employee newEmployee)
        {
            _employee.Add(newEmployee);
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Deactivate employee for mock accessor
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeactiveEmployee(int employeeID)
        {
            bool foundEmployee = false;
            foreach(var worker in _employee)
            {
                if(worker.EmployeeID == employeeID)
                {
                    worker.Active = false;
                    foundEmployee = true;
                    break;
                }
            }
            if (!foundEmployee)
            {
                throw new ArgumentException("No employee found with that ID in the system.");
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Purge employee for mock accessor
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(int employeeID)
        {
            try
            {
                SelectEmployee(employeeID);
            }
            catch (Exception)
            {
                throw;
            }
            _employee.Remove(_employee.Find(x => x.EmployeeID == employeeID));
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Purge employee role for mock accessor
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployeeRole(int employeeID)
        {
            try
            {
                SelectEmployee(employeeID);
            }
            catch (Exception)
            {
                throw;
            }
            _employee.Remove(_employee.Find(x => x.EmployeeID == employeeID));
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Retrieve all employees for mock accessor
        /// </summary>
        /// <returns></returns>
        public List<Employee> SelectAllEmployees()
        {
            return _employee;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Retrieve employee for mock accessor
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee SelectEmployee(int employeeID)
        {
            Employee worker = new Employee();
            worker = _employee.Find(x => x.EmployeeID == employeeID);
            if (worker == null)
            {
                throw new ArgumentException("EmployeeID did not match.");
            }

            return worker;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/13/19
        /// 
        /// Update employee for the mock accessor
        /// </summary>
        /// <param name="newEmployee"></param>
        /// <param name="oldEmployee"></param>
        public void UpdateEmployee(Employee newEmployee, Employee oldEmployee)
        {
            foreach (var worker in _employee)
            {
                if (worker.EmployeeID == oldEmployee.EmployeeID)
                {
                    worker.FirstName = newEmployee.FirstName;
                    worker.LastName = newEmployee.LastName;
                    worker.PhoneNumber = newEmployee.PhoneNumber;
                    worker.Email = newEmployee.Email;
                    worker.DepartmentID = newEmployee.DepartmentID;
                    worker.Active = newEmployee.Active;
                }
            }
        }

        public List<Employee> SelectActiveEmployees()
        {
            return _employee;
        }

        public List<Employee> SelectInactiveEmployees()
        {
            return _employee;
        }
    }
}
