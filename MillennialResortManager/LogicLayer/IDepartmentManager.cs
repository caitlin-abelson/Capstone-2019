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
    /// The IDepartmentManager is the interface for Department and hold all the CRUD methods for the logic layer.
    /// </summary>
    interface IDepartmentManager
    {
        bool AddDepartment(Department newDepartment);
        void EditDepartment(Department newDepartment, Department oldDepartment);
        Department GetDepartment();
        List<Department> GetAllDepartments();
        void DeleteDepartment();
    }
}
