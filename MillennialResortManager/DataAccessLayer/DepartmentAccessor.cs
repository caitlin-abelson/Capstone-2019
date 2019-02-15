using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Caitlin Abelson
    /// Created: 1/29/19
    /// 
    /// The DepartmentAccessor class implements the IDepartmentAccessor interface which holds all of the methods needed
    /// the CRUD functions of a Department when accessing the database.
    /// </summary>
    public class DepartmentAccessor : IDepartmentAccessor
    {

        public DepartmentAccessor()
        {

        }

        /// <summary>
        /// Caitlin Abelson
        /// Created: 1/29/19
        /// 
        /// The RetrieveAllDepartments method reads in the data objects from the stored procedure sp_retrieve_department
        /// so that they can be used in the combo drop down box when creating a new employee.
        /// </summary>
        public List<Department> RetrieveAllDepartments()
        {
            List<Department> departments = new List<Department>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_department";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Department department = new Department();
                        department.DepartmentID = reader.GetString(0);
                        departments.Add(department);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return departments;
        }


        public int CreateDepartment(Department newDepartment)
        {
            throw new NotImplementedException();
        }

        public void DeactiveDepartment()
        {
            throw new NotImplementedException();
        }

        public void PurgeDepartment()
        {
            throw new NotImplementedException();
        }

        public Department RetrieveDepartment()
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(Department newDepartment, Department oldDepartment)
        {
            throw new NotImplementedException();
        }
    }
}
