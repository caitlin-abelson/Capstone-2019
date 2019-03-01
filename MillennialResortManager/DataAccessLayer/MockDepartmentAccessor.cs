/// <summary>
/// Austin Berquam
/// Created: 2019/02/23
/// 
/// This is a mock Data Accessor which implements IDepartmentAccessor.  This is for testing purposes only.
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class MockDepartmentAccessor : IDepartmentAccessor
    {
        private List<Department> department;

        /// <summary>
        /// Author: Austin Berquam
        /// Created: 2019/02/23
        /// This constructor that sets up dummy data
        /// </summary>
        public MockDepartmentAccessor()
        {
            department = new List<Department>
            {
                new Department {DepartmentID = "Department1", Description = "department is a department"},
                new Department {DepartmentID = "Department2", Description = "department is a department"},
                new Department {DepartmentID = "Department3", Description = "department is a department"},
                new Department {DepartmentID = "Department4", Description = "department is a department"}
            };
        }


        public int InsertDepartment(Department newDepartment)
        {
            int listLength = department.Count;
            department.Add(newDepartment);
            if (listLength == department.Count - 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int DeleteDepartmentType(string departmentID)
        {
            int rowsDeleted = 0;
            foreach (var type in department)
            {
                if (type.DepartmentID == departmentID)
                {
                    int listLength = department.Count;
                    department.Remove(type);
                    if (listLength == department.Count - 1)
                    {
                        rowsDeleted = 1 ;
                    }
                }
            }

            return rowsDeleted;
        }

        public List<Department> SelectDepartmentTypes(string status)
        {
            return department;
        }

        public List<string> SelectAllTypes()
        {
            throw new NotImplementedException();
        }
    }
}
