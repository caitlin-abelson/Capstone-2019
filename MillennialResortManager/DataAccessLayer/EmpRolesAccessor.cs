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
	/// EmpRolesAccessor class is used to access the Roles table
	/// and the stored procedures as well
	/// </summary>
    public class EmpRolesAccessor : IEmpRolesAccessor
    {
        /// <summary>
        /// Method that retrieves the Roles table and stores it as a list
        /// </summary>
        /// <returns>List of Roles </returns>	
        public List<EmpRoles> SelectEmpRoles(string status)
        {
            List<EmpRoles> roles = new List<EmpRoles>();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_roles";

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
                        roles.Add(new EmpRoles()
                        {
                            RoleID = reader.GetString(0),
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

            return roles;
        }

        /// <summary>
        /// Method that retrieves the Role IDs to store into a combo box
        /// </summary>
        /// <returns>List of Role Types </returns>	
        public List<string> SelectAllRoleID()
        {
            var roles = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_roleID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        roles.Add(r.GetString(0));
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
            return roles;
        }

        /// <summary>
        /// Method that creates a new Employee and stores it in the table
        /// </summary>
        /// <param name="empRoles">Object holding the data to add to the table</param>
        /// <returns> Row Count </returns>	
        public int InsertEmpRole(EmpRoles empRoles)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_roles";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", empRoles.RoleID);
            cmd.Parameters.AddWithValue("@Description", empRoles.Description);

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
        /// Method that deletes a Employee Role and removes it from the table
        /// </summary>
        /// <param name="roleID">The ID of the Employee Roles being deleted</param>
        /// <returns> Row Count </returns>
        public int DeleteEmpRole(string roleID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_roles";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", roleID);

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
    }
}
