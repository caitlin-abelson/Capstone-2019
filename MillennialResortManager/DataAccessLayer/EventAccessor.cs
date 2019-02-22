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
    public class EventAccessor : IEventAccessor
    {
        /// <summary>
        ///  @Author Phillip Hansen
        ///  @Created 1/23/2019
        ///  
        /// Class for the stored procedure data for Event Requests
        /// </summary>


        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Constructor for calling non-static methods
        /// </summary>
        public EventAccessor()
        {

        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Method for inserting a new event
        /// </summary>
        /// <param name="newEvent"></param> allows a new Event object to be created, called 'newEvent'
        public void insertEvent(Event newEvent)
        {

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_insert_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request
            cmd.Parameters.AddWithValue("@EventTitle", newEvent.EventTitle);
            cmd.Parameters.AddWithValue("@EmployeeID", newEvent.EmployeeID);
            cmd.Parameters.AddWithValue("@EventTypeID", newEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@Description", newEvent.Description);
            cmd.Parameters.AddWithValue("@EventStartDate", newEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@EventEndDate", newEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@KidsAllowed", newEvent.KidsAllowed);
            cmd.Parameters.AddWithValue("@NumGuests", newEvent.NumGuests);
            cmd.Parameters.AddWithValue("@Location", newEvent.Location);
            cmd.Parameters.AddWithValue("@Sponsored", newEvent.Sponsored);
            cmd.Parameters.AddWithValue("@Approved", newEvent.Approved);

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
        /// @Author Phillip Hansen
        /// 
        /// Method for updating an event object from old to new
        /// </summary>
        /// <param name="oldEvent"></param> Needs the old event object
        /// <param name="newEvent"></param> The new event object is the updated version of the old one
        public void updateEvent(Event oldEvent, Event newEvent)
        {

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_update_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Parameters for new Event Request
            cmd.Parameters.AddWithValue("@EventID", oldEvent.EventID);
            cmd.Parameters.AddWithValue("@EventTitle", newEvent.EventTitle);
            cmd.Parameters.AddWithValue("@EmployeeID", newEvent.EmployeeID);
            cmd.Parameters.AddWithValue("@EventTypeID", newEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@Description", newEvent.Description);
            cmd.Parameters.AddWithValue("@EventStartDate", newEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@EventEndDate", newEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@KidsAllowed", newEvent.KidsAllowed);
            cmd.Parameters.AddWithValue("@NumGuests", newEvent.NumGuests);
            cmd.Parameters.AddWithValue("@Location", newEvent.Location);
            cmd.Parameters.AddWithValue("@Sponsored", newEvent.Sponsored);
            cmd.Parameters.AddWithValue("@SponsorID", oldEvent.SponsorID);
            cmd.Parameters.AddWithValue("@Approved", newEvent.Approved);
            //Parameters for old Event Request
            //The PK ID should not change
            cmd.Parameters.AddWithValue("@OldEventTitle", oldEvent.EventTitle);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldEvent.EmployeeID);
            cmd.Parameters.AddWithValue("@OldEventTypeID", oldEvent.EventTypeID);
            cmd.Parameters.AddWithValue("@OldDescription", oldEvent.Description);
            cmd.Parameters.AddWithValue("@OldEventStartDate", oldEvent.EventStartDate);
            cmd.Parameters.AddWithValue("@OldEventEndDate", oldEvent.EventEndDate);
            cmd.Parameters.AddWithValue("@OldKidsAllowed", oldEvent.KidsAllowed);
            cmd.Parameters.AddWithValue("@OldNumGuests", oldEvent.NumGuests);
            cmd.Parameters.AddWithValue("@OldLocation", oldEvent.Location);
            cmd.Parameters.AddWithValue("@OldSponsored", oldEvent.Sponsored);
            cmd.Parameters.AddWithValue("@OldSponsorID", oldEvent.SponsorID);
            cmd.Parameters.AddWithValue("@OldApproved", oldEvent.Approved);

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
        /// @Author Phillip Hansen
        /// 
        /// Method for retrieving all Events
        /// </summary>
        /// <returns></returns> returns all events
        public List<Event> selectAllEvents()
        {
            List<Event> Events = new List<Event>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_events";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                
                var r = cmd.ExecuteReader();
                if(r.HasRows)
                {
                    while (r.Read())
                    {
                        Events.Add(new Event()
                        {
                            EventID = r.GetInt32(0),
                            EventTitle = r.GetString(1),
                            EmployeeID = r.GetInt32(2),
                            EmployeeName = r.GetString(3),
                            EventTypeID = r.GetString(4),
                            Description = r.GetString(5),
                            EventStartDate = r.GetDateTime(6),
                            EventEndDate = r.GetDateTime(7),
                            KidsAllowed = r.GetBoolean(8),
                            NumGuests = r.GetInt32(9),
                            Location = r.GetString(10),
                            Sponsored = r.GetBoolean(11),
                            SponsorID = r.GetInt32(12),
                            SponsorName = r.GetString(13),
                            Approved = r.GetBoolean(14)
                        });
                              
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

            return Events;
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Method for retrieving a specific Event
        /// </summary>
        /// <param name="eventReqID"></param> needs the unique ID of the event to be retrieved
        /// <returns></returns>
        public Event selectEventById(int eventReqID)
        {
           Event _event = new Event();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_event";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@EventID", eventReqID);

            try
            {
                conn.Open();
                


                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        _event = new Event()
                        {
                            //EventID = r.getInt32(0),
                            EventTitle = r.GetString(1),
                            EmployeeName = r.GetString(2),
                            EventTypeID = r.GetString(3),
                            Description = r.GetString(4),
                            EventStartDate = r.GetDateTime(5),
                            EventEndDate = r.GetDateTime(6),
                            KidsAllowed = r.GetBoolean(7),
                            NumGuests = r.GetInt32(8),
                            Location = r.GetString(9),
                            Sponsored = r.GetBoolean(10),
                            SponsorName = r.GetString(11),
                            Approved = r.GetBoolean(12)
                        };
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

            return _event;
        }

        /// <summary>
        /// @Author Phillip Hansen
        /// 
        /// Method for purging an event
        /// NOTE: The event must be approved as 'false' or the default value '0' in SQL to be purged
        /// </summary>
        /// <param name="EventID"></param> //needs the unique ID of the event to be deleted
        public void deleteEventByID(int EventID)
        {

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_delete_event", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventID", EventID);

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
