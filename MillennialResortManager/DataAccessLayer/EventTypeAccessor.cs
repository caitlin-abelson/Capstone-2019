using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// NOTE: This code is done correctly by someone else
    /// </summary>
    /// 

    public class EventTypeAccessor
    {
        /// <summary>
        /// Method for retrieving all event types
        /// </summary>
        /// <returns></returns>
        public static List<string> RetrieveAllEventTypes()
        {
            List<string> eventTypes = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_event_types";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                

                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        eventTypes.Add(r.GetString(0));
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

            return eventTypes;
        }
    }
}
