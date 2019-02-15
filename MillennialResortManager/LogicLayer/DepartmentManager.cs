using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{

    public class DepartmentManager : IDepartmentManager
    {
        private DepartmentAccessor _departmentAccessor;

        public DepartmentManager()
        {
            _departmentAccessor = new DepartmentAccessor();
        }
        public bool AddDepartment(Department newDepartment)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment()
        {
            throw new NotImplementedException();
        }

        public void EditDepartment(Department newDepartment, Department oldDepartment)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/30/19
        /// 
        /// The GetAllDepartments gets a list of all the departments to be used in a dropdown box.
        /// </summary>
        public List<Department> GetAllDepartments()
        {
            List<Department> departments;

            try
            {
                departments = _departmentAccessor.RetrieveAllDepartments();
            }
            catch (Exception)
            {
                throw;
            }
            return departments;
        }

        public Department GetDepartment()
        {
            throw new NotImplementedException();
        }
    }
}
