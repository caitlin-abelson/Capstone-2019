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

    public class EventTypeAccessor : IEventTypeAccessor
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
        /// <summary>
        ///  @Author Craig Barkley
        ///  @Created 1/23/2019
        ///  
        /// Class for the stored procedure data for Event Types
        /// </summary>
        /// <param name="newEventType"></param>
        /// <returns></returns>

        //For creating a new Event RequestC:\Users\Craig\Desktop\MillennialResortManager\MillennialResortManager\DataAccessLayer\EventTypeAccessor.cs
        public int CreateEventType(EventType newEventType)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_create_event_type", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request          
            cmd.Parameters.AddWithValue("@EventTypeID", newEventType.EventTypeID);
            cmd.Parameters.AddWithValue("@Description", newEventType.Description);
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

        //SelectAllEventTypeID()
        /// <summary>
        /// Method that Retrieves All Event Types.
        /// </summary>
        /// <param name="">The ID of the Event Types are retrieved.</param>
        /// <returns> eventTypes </returns>
        public List<string> SelectAllEventTypeID()
        {
            var eventTypes = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_select_event_type_by_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        eventTypes.Add(read.GetString(0));
                    }
                }
                read.Close();
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

        //RetrievetAllEventTypes(string status)
        /// <summary>
        /// Method that retrieves the Event types table and stores it as a list by status.
        /// </summary>
        /// <returns>eventTypes</returns>	
        public List<EventType> RetrievetAllEventTypes(string status)
        {
            List<EventType> eventTypes = new List<EventType>();

            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand("sp_retrieve_all_event_type", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var read = cmd.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        eventTypes.Add(new EventType()
                        {
                            EventTypeID = read.GetString(0),
                            Description = read.GetString(1)
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
            return eventTypes;
        }

        //DeleteEventType(string eventTypeID)
        /// <summary>
        /// Method that deletes an Event Type and removes it from the table
        /// </summary>
        /// <param name="eventTypeID">The ID of the Event Type being deleted</param>
        /// <returns> Row Count </returns>
        public int DeleteEventType(string eventTypeID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_delete_event_type_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventTypeID", eventTypeID);

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
