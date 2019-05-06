using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Caitlin Abelson" created="2019/01/30">
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
