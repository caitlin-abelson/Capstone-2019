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
  public   class SetupListAccessor : ISetupListAccessor
    {
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// method to retrieve all roles
        /// </summary>

        public List<SetupList> RetrieveAllSetupLists()
        {
            //return new List<SetupList>();  // will fail test
            List<SetupList> setupLists = new List<SetupList>();
            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_all_setuplists";

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
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// method to select setuplist by setuplistID
        /// </summary>

        public SetupList RetrieveSetupListByRoleId(int setupListID)
{
    SetupList setupList = new SetupList();
    var cmdText = @"sp_retrieve_setuplist_by_id";
    var conn = DBConnection.GetDbConnection();

    var cmd = new SqlCommand(cmdText, conn);

    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    cmd.Parameters.AddWithValue("@SetupListID", setupListID);
    try
    {
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.HasRows)
        {
            setupList.SetupListID = reader.GetInt32(0);
            setupList.SetupID = reader.GetInt32(1);
            setupList.Completed = reader.GetBoolean(2);
            setupList.Description = reader.GetString(3);
            setupList.Comments = reader.GetString(4);

          

          
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

       


    }
}
