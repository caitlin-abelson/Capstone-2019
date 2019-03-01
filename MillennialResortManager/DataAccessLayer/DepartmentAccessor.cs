using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
	/// Austin Berquam
	/// Created: 2019/01/26
	/// 
	/// DepartmentAccessor class is used to access the Department table
	/// and the stored procedures as well
	/// </summary>
    public class DepartmentAccessor : IDepartmentAccessor
    {
        /// <summary>
        /// Method that retrieves the department types table and stores it as a list
        /// </summary>
        /// <returns>List of Department Types </returns>	
        public List<Department> SelectDepartmentTypes(string status)
        {
            List<Department> departmentTypes = new List<Department>();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_departments";

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
                        departmentTypes.Add(new Department()
                        {
                            DepartmentID = reader.GetString(0),
                            Description = reader.GetString(1)

                        });
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

            return departmentTypes;
        }

        /// <summary>
        /// Method that creates a new department type and stores it in the table
        /// </summary>
        /// <param name="department">Object holding the data to add to the table</param>
        /// <returns> Row Count </returns>	
        public int InsertDepartment(Department department)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_department";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
            cmd.Parameters.AddWithValue("@Description", department.Description);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;

        }

        /// <summary>
        /// Method that deletes a department type and removes it from the table
        /// </summary>
        /// <param name="departmentID">The ID of the department type being deleted</param>
        /// <returns> Row Count </returns>
        public int DeleteDepartmentType(string departmentID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_department";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Method that retrieves the department type IDs to store into a combo box
        /// </summary>
        /// <returns>List of Department Types </returns>	
        public List<string> SelectAllTypes()
        {
            var types = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_departmentTypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        types.Add(r.GetString(0));
                    }
                }
                r.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return types;
        }
    }
}
