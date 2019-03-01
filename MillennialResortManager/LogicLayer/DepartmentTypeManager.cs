using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Austin Berquam
    /// Created: 2019/02/06
    /// 
    /// Used to manage the Department table
    /// and the stored procedures as well
    /// </summary
    public class DepartmentTypeManager : IDepartmentTypeManager
    {
        IDepartmentAccessor departmentAccessor;

        public DepartmentTypeManager()
        {
            departmentAccessor = new DepartmentAccessor();
        }
        public DepartmentTypeManager(MockDepartmentAccessor mock)
        {
            departmentAccessor = new MockDepartmentAccessor();
        }
        /// <summary>
        /// Method that collects the departments from the accessor
        /// </summary>
        /// <returns> List of Departments </returns>
        public List<Department> RetrieveAllDepartments(string status)
        {
            List<Department> types = null;
            if (status != "")
            {
                try
                {
                    types = departmentAccessor.SelectDepartmentTypes(status);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return types;
        }

        /// <summary>
        /// Method that creates a department through the accessor
        /// </summary>
        /// <param name="department">Object department to create</param>
        /// <returns> bool on if the department was created </returns>
        public bool CreateDepartment(Department department)
        {
            ValidationExtensionMethods.ValidateID(department.DepartmentID);
            ValidationExtensionMethods.ValidateDescription(department.Description);
            bool result = false;

            try
            {
                result = (1 == departmentAccessor.InsertDepartment(department));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Method that deletes a department through the accessor
        /// </summary>
        /// <param name="departmentID">ID department to delete</param>
        /// <returns> bool on if the department was deleted </returns>
        public bool DeleteDepartment(string departmentID)
        {
            bool result = false;

            try
            {
                result = (1 == departmentAccessor.DeleteDepartmentType(departmentID));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Method that retrieves all Department types and stores it in a list
        /// </summary>
        /// <returns> Stores Departments in a List returns>
        public List<string> RetrieveAllDepartmentTypes()
        {
            List<string> types = null;
            try
            {
                types = departmentAccessor.SelectAllTypes();
            }
            catch (Exception)
            {
                throw;
            }
            return types;
        }
    }
}
