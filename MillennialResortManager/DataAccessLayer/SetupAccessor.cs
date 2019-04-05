using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 2/28/19
    /// 
    /// The public accessor for the Setup table
    /// </summary>
    public class SetupAccessor : ISetupAccessor
    {
        public SetupAccessor()
        {

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-03-19
        /// 
        /// This deletes a setup by the ID as well as a setupList because it is a transaction.
        /// A setupList can't exist without the corresponding setup so when the setup is
        /// deleted, the setupList must be deleted as well.
        /// </summary>
        /// <param name="setupID"></param>
        public void DeleteSetup(int setupID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_setup_and_setup_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupID", setupID);

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
        /// Author: Caitlin Abelson
        /// Created Date: 2019-02-28
        /// 
        /// This is the method for when we are inserting a new setup.
        /// </summary>
        /// <param name="newSetup"></param>
        /// <returns></returns>
        public int InsertSetup(Setup newSetup)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_setup";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventID", newSetup.EventID);
            cmd.Parameters.AddWithValue("@DateEntered", newSetup.DateEntered);
            cmd.Parameters.AddWithValue("@DateRequired", newSetup.DateRequired);
            cmd.Parameters.AddWithValue("@Comments", newSetup.Comments);

            try
            {
                conn.Open();
                var tempResult = cmd.ExecuteScalar();
                result = Convert.ToInt32(tempResult);
                newSetup.SetupID = result;
            }
            catch (SqlException)
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
        /// Author: Caitlin Abelson
        /// Created Date: 2019-02-28
        /// 
        /// Retrieves a list of all of the setups 
        /// </summary>
        /// <returns></returns>
        public List<Setup> SelectAllSetup()
        {
            List<Setup> setups = new List<Setup>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_setups";
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
                        Setup setup = new Setup();
                        setup.SetupID = reader.GetInt32(0);
                        setup.EventID = reader.GetInt32(1);
                        setup.DateEntered = reader.GetDateTime(2);
                        setup.DateRequired = reader.GetDateTime(3);
                        setup.Comments = reader.GetString(4);
                        setups.Add(setup);
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

            return setups;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-02-28
        /// 
        /// 
        /// </summary>
        /// <param name="setupID"></param>
        /// <returns></returns>
        public Setup SelectSetup(int setupID)
        {
            Setup setup = new Setup();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_setup_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupID", setupID);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        setup.SetupID = reader.GetInt32(0);
                        setup.EventID = reader.GetInt32(1);
                        setup.DateEntered = reader.GetDateTime(2);
                        setup.DateRequired = reader.GetDateTime(3);
                        setup.Comments = reader.GetString(4);
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

            return setup;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-02-28
        /// 
        /// 
        /// </summary>
        /// <param name="newSetup"></param>
        /// <param name="oldSetup"></param>
        public void UpdateSetup(Setup newSetup, Setup oldSetup)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_setup_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupID", oldSetup.SetupID);
            cmd.Parameters.AddWithValue("@EventID", newSetup.EventID);
            cmd.Parameters.AddWithValue("@DateRequired", newSetup.DateRequired);
            cmd.Parameters.AddWithValue("@Comments", newSetup.Comments);
            cmd.Parameters.AddWithValue("@OldEventID", oldSetup.EventID);
            cmd.Parameters.AddWithValue("@OldDateRequired", oldSetup.DateRequired);
            cmd.Parameters.AddWithValue("@OldComments", oldSetup.Comments);

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
        /// Author: Caitlin Abelson
        /// Created Date: 2019-03-15
        /// 
        /// Reading through all of the Setup items as well as the EventTitles that 
        /// coincide with that Setup
        /// </summary>
        /// <returns></returns>
        public List<VMSetup> SelectVMSetups()
        {
            List<VMSetup> vmSetup = new List<VMSetup>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_setup_and_event_title";
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
                        VMSetup setup = new VMSetup();
                        setup.SetupID = reader.GetInt32(0);
                        setup.EventID = reader.GetInt32(1);
                        setup.DateEntered = reader.GetDateTime(2);
                        setup.DateRequired = reader.GetDateTime(3);
                        setup.EventTitle = reader.GetString(4);
                        vmSetup.Add(setup);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return vmSetup;
        }

        public List<VMSetup> SelectDateEntered(DateTime dateEntered)
        {
            List<VMSetup> vmSetup = new List<VMSetup>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_date_entered";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DateEntered", dateEntered);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VMSetup setup = new VMSetup();
                        setup.SetupID = reader.GetInt32(0);
                        setup.EventID = reader.GetInt32(1);
                        setup.DateEntered = reader.GetDateTime(2);
                        setup.DateRequired = reader.GetDateTime(3);
                        setup.EventTitle = reader.GetString(4);
                        vmSetup.Add(setup);
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

            return vmSetup;
        }

        public List<VMSetup> SelectDateRequired(DateTime dateRequired)
        {
            List<VMSetup> vmSetup = new List<VMSetup>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_date_required";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DateRequired", dateRequired);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VMSetup setup = new VMSetup();
                        setup.SetupID = reader.GetInt32(0);
                        setup.EventID = reader.GetInt32(1);
                        setup.DateEntered = reader.GetDateTime(2);
                        setup.DateRequired = reader.GetDateTime(3);
                        setup.EventTitle = reader.GetString(4);
                        vmSetup.Add(setup);
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

            return vmSetup;
        }

        public List<VMSetup> SelectSetupEventTitle(string eventTitle)
        {
            List<VMSetup> vmSetup = new List<VMSetup>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_setup_event_title";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventTitle", eventTitle);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VMSetup setup = new VMSetup();
                        setup.SetupID = reader.GetInt32(0);
                        setup.EventID = reader.GetInt32(1);
                        setup.DateEntered = reader.GetDateTime(2);
                        setup.DateRequired = reader.GetDateTime(3);
                        setup.EventTitle = reader.GetString(4);
                        vmSetup.Add(setup);
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

            return vmSetup;
        }
    }
}
