
using System;
using System.Collections.Generic;
using System.Data.SqlClient;        
using System.Data;                  
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    // Classes in lower layers are usually static, where upper are usually concrete
    public static class UserAccessor
    {
        // In C# if something is public capitolize first letter
        public static int VerifyUsernameAndPassword(string userName, string password)
        {
            int result = 0;     // this will be the number of users found

            // we begin with a connection
            var conn = DBConnection.GetDbConnection();

            // next, we need command text
            string cmdText = @"sp_authenticate_user";

            // then we create a command object from command text and a connection
            var cmd = new SqlCommand(cmdText, conn);

            // now we need to set the command type
            cmd.CommandType = CommandType.StoredProcedure;      //CommandType defined in System.Data

            // next, set up the stored procedure's parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // pass values to the parameters
            cmd.Parameters["@Email"].Value = userName;
            cmd.Parameters["@PasswordHash"].Value = password;

            // now we need to use these connected types in an open connection
            // and this means unsafe code, so a try-catch
            try
            {
                // open the connections
                conn.Open();

                // execute the command
                result = (int)cmd.ExecuteScalar();   // need to cast in order to use it
            }
            catch (Exception up)
            {
                throw up;       // you NEVER hadle exceptions in the DataAccessLayer, bubble them up
            }
            //finally // DANI ADDED finally block
            //{
            //    conn.Close();
            //}
            return result;
        }

        public static User RetrieveUserByEmail(string email)
        {
            User user = null;

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_retrieve_user_names_by_email";
            string cmdText2 = @"sp_retrieve_employee_roles";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);
            var cmd2 = new SqlCommand(cmdText2, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd2.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd2.Parameters.Add("@Email", SqlDbType.NVarChar, 250);


            // values
            cmd1.Parameters["@Email"].Value = email;
            cmd2.Parameters["@Email"].Value = email;

            try
            {
                // open the connection
                conn.Open();

                int userID = 0;
                string firstName = null;
                string lastName = null;
                List<string> roles = new List<string>();

                // process cmd1
                SqlDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.HasRows)
                {
                    reader1.Read();     // reads the first line
                    userID = reader1.GetInt32(0);       // 0 represents the column number
                    firstName = reader1.GetString(1);   // 1 represents the column number
                    lastName = reader1.GetString(2);
                }
                else
                {
                    throw new ApplicationException("User not found.");      // only be possible if user was deleted while this is executed
                }
                reader1.Close();    // Only 1 reader can be opened at time

                // process cmd2
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        string role = reader2.GetString(0);     // grabbing the string
                        roles.Add(role);                        // build list of roles
                    }
                }
                reader2.Close();

                // build user object to be returned
                user = new User(userID, firstName, lastName, roles);

            }
            catch (Exception)
            {

                throw;
            }

            return user;
        }


        public static List<string> RetrieveRolesByEmail(string email)
        {
            // Start by creating your collection object, this line is important
            List<string> roles = new List<string>();

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText2 = @"sp_retrieve_employee_roles";

            // command objects
            var cmd2 = new SqlCommand(cmdText2, conn);

            // set the command type
            cmd2.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd2.Parameters.Add("@Email", SqlDbType.NVarChar, 250);

            // values
            cmd2.Parameters["@Email"].Value = email;

            try
            {
                // open the connection
                conn.Open();

                // process cmd2
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        string role = reader2.GetString(0);     // grabbing the string, 0 is the column number
                        roles.Add(role);                        // build list of roles
                    }
                }
                reader2.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return roles;
        }
        public static int UpdatePasswordHash(string email, string oldPassword, string newPassword)
        {
            int result = 0;

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = "sp_update_password_hash";

            // command object
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);

            // values
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPassword;
            cmd.Parameters["@NewPasswordHash"].Value = newPassword;

            // execute command
            try
            {
                // open the connection
                conn.Open();

                // execute the command
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




    }
}

