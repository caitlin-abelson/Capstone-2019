
using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Austin Berquam" created="2019/02/12">
	/// Interface that implements Create and Delete functions for Department Types
	/// manager classes.
	/// </summary>
	public interface IDepartmentTypeManager
	{
		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Creates a new Department type
		/// </summary>
		bool CreateDepartment(Department department);
        List<Department> RetrieveAllDepartments(string status);

		/// <summary author="Austin Berquam" created="2019/02/06">
		/// Deletes a department type
		/// </summary>
		List<string> RetrieveAllDepartmentTypes();
        bool DeleteDepartment(string departmentID);
    }
}