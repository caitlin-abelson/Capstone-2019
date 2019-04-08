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
    public class EventSponsorAccessor
    {
        /// <summary>
        ///  @Author: Phillip Hansen
        ///  @Created 4/3/2019
        ///  
        /// Class for the stored procedure data for Event Sponsor Requests
        /// </summary>


        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Constructor for calling non-static methods
        /// </summary>
        public EventSponsorAccessor()
        {

        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Insert method using a stored procedure
        /// </summary>
        /// <param name="newEvSpons"></param> EventSponsor object must be supplied first
        public void insertEventSponsor(int eventID, int sponsorID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_event_sponsor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Sponsor record
            cmd.Parameters.AddWithValue("@EventID", eventID);
            cmd.Parameters.AddWithValue("@SponsorID", sponsorID);

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
        /// @Author: Phillip Hansen
        /// 
        /// Created: 4/3/2019
        /// 
        /// Method for retrieving all Event Sponsors
        /// </summary>
        /// <returns></returns> returns all event sponsors
        public List<EventSponsor> selectAllEventSponsors()
        {
            List<EventSponsor> EventSponsors = new List<EventSponsor>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_event_sponsors";
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
                        EventSponsors.Add(new EventSponsor()
                        {
                            EventID = r.GetInt32(0),
                            EventTitle = r.GetString(1),
                            SponsorName = r.GetString(2),
                            SponsorID = r.GetInt32(3)
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

            return EventSponsors;
        }

        /// <summary>
        /// @Author: Phillip Hansen
        /// 
        /// Method to delete a record with the associated event/sponsor id's
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="SponsorID"></param>
        public void deleteEventByID(int EventID, int SponsorID)
        {

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_delete_event_sponsor_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventID", EventID);
            cmd.Parameters.AddWithValue("@SponsorID", SponsorID);

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
    }
}
