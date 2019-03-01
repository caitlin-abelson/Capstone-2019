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
    public class RoleAccessor : IRoleAccessor
    {

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/30
        /// 
        /// method to retrieve all roles
        /// </summary>

        public List<Role> RetrieveAllRoles()
        {
            //return new List<Role>();  // will fail test
            List<Role> roles = new List<Role>();
            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_all_roles";

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

                        roles.Add(new Role()
                        {
                            RoleID = reader.GetString(0),
                            Description = reader.GetString(1),
                            //  Active = reader.GetBoolean(2)
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
        /// Eduardo Colon
        /// Created: 2019/01/30
        /// 
        /// method to insert role
        /// </summary>

        public int InsertRole(Role newRole)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", newRole.RoleID);
            cmd.Parameters.AddWithValue("@Description", newRole.Description);


            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method to select role by role id
        /// </summary>

        public Role RetrieveRoleByRoleId(string roleID)
        {
            Role role = new Role();
            var cmdText = @"sp_retrieve_role_by_id";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", roleID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    role.RoleID = reader.GetString(0);

                    role.Description = reader.GetString(1);

                    //  role.Active = reader.GetBoolean(2);
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
            return role;
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/30
        /// 
        /// method to update role 
        /// </summary>

        public void UpdateRole(Role oldRole, Role newRole)
        {
            //  int result = 0;

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = "sp_update_role_by_id";

            // command object
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@RoleID", oldRole.RoleID);

            cmd.Parameters.AddWithValue("@OldDescription", oldRole.Description);

            cmd.Parameters.AddWithValue("@NewDescription", newRole.Description);
            // execute command
            try
            {
                // open the connection
                conn.Open();


                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }



        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method  to deactivate employee role by id
        /// </summary>

        public void DeactivateRole(string roleID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_employee_role_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", roleID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method  to delete  a role 
        /// </summary>

        public void DeleteRole(string roleID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", roleID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method  to delete  a role from the employeeRole 
        /// </summary>

        public void DeleteEmployeeRole(string roleID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_roleID_role";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoleID", roleID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method  to retrieve all active roles 
        /// </summary>
        public List<Role> RetrieveActiveRoles()
        {
            List<Role> roles = new List<Role>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_role_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Role role = new Role();
                        role.RoleID = reader.GetString(0);
                        role.Description = reader.GetString(1);
                        //   role.Active = reader.GetBoolean(2);
                        roles.Add(role);
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
        /// Eduardo Colon
        /// Created: 2019/02/24
        /// 
        /// method  to retrieve all inactive roles 
        /// </summary>
        public List<Role> RetrieveInactiveRoles()
        {
            List<Role> roles = new List<Role>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_role_inactive";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Role role = new Role();
                        role.RoleID = reader.GetString(0);
                        role.Description = reader.GetString(1);

                        roles.Add(role);
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

    }
}
