using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

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

		/// <summary author="Caitlin Abelson" created="2019/01/30">
		/// The GetAllDepartments gets a list of all the departments to be used in a dropdown box.
		/// </summary>
		public List<Department> GetAllDepartments()
        {
            List<Department> departments;

            try
            {
                departments = _departmentAccessor.SelectDepartmentTypes("all");
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return departments;
        }

        public Department GetDepartment()
        {
            throw new NotImplementedException();
        }
    }
}
