/// Austin Berquam
/// Created: 2019/02/12
/// 
/// Interface that implements Create and Delete functions for Department Types
/// manager classes.
/// </summary>
using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IDepartmentTypeManager
    {

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new Department type
        /// </summary>
        bool CreateDepartment(Department department);
        List<Department> RetrieveAllDepartments(string status);

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a department type
        /// </summary>
        List<string> RetrieveAllDepartmentTypes();
        bool DeleteDepartment(string departmentID);
    }
}