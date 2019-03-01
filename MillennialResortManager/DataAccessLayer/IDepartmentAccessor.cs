/// Austin Berquam
/// Created: 2019/02/12
/// 
/// Interface that implements Create and Delete functions for Department Types
/// accessor classes.
/// </summary>
using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IDepartmentAccessor
    {
        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new Department type
        /// </summary>
        List<Department> SelectDepartmentTypes(string status);
        int InsertDepartment(Department department);


        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a department type
        /// </summary>
        int DeleteDepartmentType(string departmentID);
        List<string> SelectAllTypes();

    }
}