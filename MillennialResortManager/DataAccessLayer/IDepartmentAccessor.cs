using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 1/30/19
    /// 
    /// The IDepartmentAccessor interface that has all CRUD methods for Department
    /// </summary>
    interface IDepartmentAccessor
    {
        List<Department> RetrieveAllDepartments();
        int CreateDepartment(Department newDepartment);
        void UpdateDepartment(Department newDepartment, Department oldDepartment);
        Department RetrieveDepartment();
        void PurgeDepartment();
        void DeactiveDepartment();
    }
}
