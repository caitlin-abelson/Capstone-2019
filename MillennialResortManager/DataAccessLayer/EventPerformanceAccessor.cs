using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;



namespace DataAccessLayer
{
    /// <summary>
    /// @Author: Phillip Hansen
    /// @Created 4/10/2019
    /// 
    /// Accessor for EventPerformance in Database
    /// </summary>
    public class EventPerformanceAccessor
    {

        /// <summary>
        /// 
        /// </summary>
        public EventPerformanceAccessor()
        {

        }

        public void insertEventPerformance(int eventID, int performanceID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_event_performance", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Sponsor record
            cmd.Parameters.AddWithValue("@EventID", eventID);
            cmd.Parameters.AddWithValue("@PerformanceID", performanceID);

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

        public List<EventPerformance> selectAllEventPerformances()
        {
            List<EventPerformance> EventPerformances = new List<EventPerformance>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_event_performances";
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
                        EventPerformances.Add(new EventPerformance()
                        {
                            EventID = r.GetInt32(0),
                            EventTitle = r.GetString(1),
                            PerformanceTitle = r.GetString(2),
                            PerformanceID = r.GetInt32(3)
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

            return EventPerformances;
        }




    }
}
