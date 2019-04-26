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
    public class SetupListAccessor : ISetupListAccessor
    {

        public SetupListAccessor()
        {

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-03-19
        /// 
        /// This method deactivates a setupList based on the ID
        /// </summary>
        /// <param name="setupListID">The id of the setupList to be deactivated.</param>
        public void DeactiveSetupList(int setupListID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_setuplist";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupListID", setupListID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-03-19
        /// 
        /// This method deletes a setupList based on the ID
        /// </summary>
        /// <param name="setupListID"></param>
        public void DeleteSetupList(int setupListID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_setuplist";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupListID", setupListID);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// The accessor method that creates a new setup list.
        /// </summary>
        /// <param name="newSetupList">The object that is being passed in to be created</param>
        /// <returns></returns>
        public void InsertSetupList(SetupList newSetupList)
        {
            //int result = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_SetupList";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupID", newSetupList.SetupID);
            cmd.Parameters.AddWithValue("@Completed", newSetupList.Completed);
            cmd.Parameters.AddWithValue("@Description", newSetupList.Description);
            cmd.Parameters.AddWithValue("@Comments", newSetupList.Comments);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            //return result;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// Selects all active setupLists for the view model list
        /// </summary>
        /// <returns></returns>
        public List<VMSetupList> SelectActiveSetupLists()
        {
            List<VMSetupList> setupLists = new List<VMSetupList>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_completed_setuplists";
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
                        VMSetupList setupList = new VMSetupList();
                        setupList.EventTitle = reader.GetString(0);
                        setupList.SetupListID = reader.GetInt32(1);
                        setupList.SetupID = reader.GetInt32(2);
                        setupList.Completed = reader.GetBoolean(3);
                        setupList.Description = reader.GetString(4);
                        setupList.Comments = reader.GetString(5);
                        setupLists.Add(setupList);
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

            return setupLists;
        }

        //Eduardo
        public List<SetupList> SelectAllSetupLists()
        {
            List<SetupList> setupLists = new List<SetupList>();
            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_all_setuplists";

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

                        setupLists.Add(new SetupList()
                        {
                            SetupListID = reader.GetInt32(0),
                            SetupID = reader.GetInt32(1),
                            Completed = reader.GetBoolean(2),
                            Description = reader.GetString(3),
                            Comments = reader.GetString(4)
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


            return setupLists;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// Selects all inactive setupLists for the view model list
        /// </summary>
        /// <returns></returns>
        public List<VMSetupList> SelectInactiveSetupLists()
        {
            List<VMSetupList> setupLists = new List<VMSetupList>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_incomplete_setuplists";
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
                        VMSetupList setupList = new VMSetupList();
                        setupList.EventTitle = reader.GetString(0);
                        setupList.SetupListID = reader.GetInt32(1);
                        setupList.SetupID = reader.GetInt32(2);
                        setupList.Completed = reader.GetBoolean(3);
                        setupList.Description = reader.GetString(4);
                        setupList.Comments = reader.GetString(5);
                        setupLists.Add(setupList);
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

            return setupLists;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 3/6/19
        /// 
        /// Selecting all of the SetupLists and EventTitles
        /// </summary>
        /// <returns></returns>
        public List<VMSetupList> SelectVMSetupLists()
        {
            List<VMSetupList> setupLists = new List<VMSetupList>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_select_setupList_and_event_title";
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

                        VMSetupList setupList = new VMSetupList();
                        setupList.EventTitle = reader.GetString(0);
                        setupList.SetupListID = reader.GetInt32(1);
                        setupList.SetupID = reader.GetInt32(2);
                        setupList.Completed = reader.GetBoolean(3);
                        setupList.Description = reader.GetString(4);
                        setupList.Comments = reader.GetString(5);
                        setupLists.Add(setupList);

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

            return setupLists;
        }

        //Eduardo
        /// <summary>
        /// Updated By: Caitilin Abelson
        /// Date: 2019-03-14
        /// 
        /// reader.Read() was not called making the method not able to read through rows of data.
        /// Added reader.Read()
        /// </summary>
        /// <param name="setupListID"></param>
        /// <returns></returns>
        public SetupList SelectSetupList(int setupListID)
        {
            SetupList setupList = new SetupList();
            var cmdText = @"sp_select_setuplist_by_id";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupListID", setupListID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.HasRows)
                    {
                        setupList.SetupListID = reader.GetInt32(0);
                        setupList.SetupID = reader.GetInt32(1);
                        setupList.Completed = reader.GetBoolean(2);
                        setupList.Description = reader.GetString(3);
                        setupList.Comments = reader.GetString(4);
                        break;
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
            return setupList;
        }


        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// This method updates the setup list.
        /// </summary>
        /// <param name="newSetupList">The new object to be created.</param>
        /// <param name="oldSetupList">The old object to be read.</param>
        public int UpdateSetupList(SetupList newSetupList, SetupList oldSetupList)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_setupList_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SetupListID", oldSetupList.SetupListID);
            cmd.Parameters.AddWithValue("@Completed", newSetupList.Completed);
            cmd.Parameters.AddWithValue("@Description", newSetupList.Description);
            cmd.Parameters.AddWithValue("@Comments", newSetupList.Comments);
            cmd.Parameters.AddWithValue("@OldCompleted", oldSetupList.Completed);
            cmd.Parameters.AddWithValue("@OldDescription", oldSetupList.Description);
            cmd.Parameters.AddWithValue("@OldComments", oldSetupList.Comments);

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



    }
}
