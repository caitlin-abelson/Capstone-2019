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
    public class ShuttleReservationAccessor : IShuttleReservationAccessor
    {
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to insert shuttlereservation
        /// </summary>


        public int InsertShuttleReservation(ShuttleReservation newShuttleReservation)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_shuttle_reservation";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", newShuttleReservation.GuestID);
            cmd.Parameters.AddWithValue("@EmployeeID", newShuttleReservation.EmployeeID);
            cmd.Parameters.AddWithValue("@PickupLocation", newShuttleReservation.PickupLocation);
            cmd.Parameters.AddWithValue("@DropoffDestination", newShuttleReservation.DropoffDestination);
            cmd.Parameters.AddWithValue("@PickupDateTime", newShuttleReservation.PickupDateTime);
          
            if(newShuttleReservation.DropoffDateTime == null) //null
            {
                 cmd.Parameters.AddWithValue("@DropoffDateTime", System.DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@DropoffDateTime", newShuttleReservation.DropoffDateTime);
             }
            
            
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
        /// Created: 2019/03/20
        /// 
        /// method to retrieve all shuttlereservation
        /// </summary>
        public List<ShuttleReservation> RetrieveAllShuttleReservations()
        {
           
            List<ShuttleReservation> shuttleReservations = new List<ShuttleReservation>();
            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_all_shuttle_reservations";

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
                        object test = reader[6];
                        DateTime? dropoff;
                        if (test == DBNull.Value)
                        {
                            dropoff = null;
                        }
                        else
                        {
                            dropoff = reader.GetDateTime(6);
                        }
                        var shuttleReservation = new ShuttleReservation()
                        {
                            ShuttleReservationID = reader.GetInt32(0),
                            GuestID = reader.GetInt32(1),
                            EmployeeID = reader.GetInt32(2),
                            PickupLocation = reader.GetString(3),
                            DropoffDestination =reader.GetString(4),
                             PickupDateTime=  reader.GetDateTime(5),
                           DropoffDateTime= dropoff,

                           Active = reader.GetBoolean(7)
                           
                        };

                        shuttleReservation.Employee = new EmployeeAccessor().RetrieveEmployeeInfo(shuttleReservation.EmployeeID);

                        shuttleReservation.Guest = new GuestAccessor().RetrieveGuestInfo(shuttleReservation.GuestID);

                        shuttleReservations.Add(shuttleReservation);
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


            return shuttleReservations;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method to update shttlereservation
        /// </summary>
        public void UpdateShuttleReservation(ShuttleReservation oldShuttleReservation, ShuttleReservation newShuttleReservation)
        {
           

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = "sp_update_shuttle_reservation_by_id";

            // command object
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ShuttleReservationID", oldShuttleReservation.ShuttleReservationID);
            cmd.Parameters.AddWithValue("@OldGuestID", oldShuttleReservation.GuestID);
            cmd.Parameters.AddWithValue("@NewGuestID", newShuttleReservation.GuestID);
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldShuttleReservation.EmployeeID);
            cmd.Parameters.AddWithValue("@NewEmployeeID", newShuttleReservation.EmployeeID);
            cmd.Parameters.AddWithValue("@OldPickupLocation", oldShuttleReservation.PickupLocation);
            cmd.Parameters.AddWithValue("@NewPickupLocation", newShuttleReservation.PickupLocation);
            cmd.Parameters.AddWithValue("@OldDropoffDestination", oldShuttleReservation.DropoffDestination);
            cmd.Parameters.AddWithValue("@NewDropoffDestination", newShuttleReservation.DropoffDestination);
            cmd.Parameters.AddWithValue("@OldPickupDateTime", oldShuttleReservation.PickupDateTime);
            cmd.Parameters.AddWithValue("@NewPickupDateTime", newShuttleReservation.PickupDateTime);
            
           
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
        /// Created: 2019/03/20
        /// 
        /// method to update shttlereservation
        /// </summary>
        public void UpdateShuttleReservationDropoff(ShuttleReservation oldShuttleReservation)
        {
          

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = "sp_shuttle_dropoff_time_now";

            // command object
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ShuttleReservationID", oldShuttleReservation.ShuttleReservationID);
            cmd.Parameters.AddWithValue("@OldGuestID", oldShuttleReservation.GuestID);
           
            cmd.Parameters.AddWithValue("@OldEmployeeID", oldShuttleReservation.EmployeeID);
           
            cmd.Parameters.AddWithValue("@OldPickupLocation", oldShuttleReservation.PickupLocation);
           
            cmd.Parameters.AddWithValue("@OldDropoffDestination", oldShuttleReservation.DropoffDestination);
        
            cmd.Parameters.AddWithValue("@OldPickupDateTime", oldShuttleReservation.PickupDateTime);
           

        
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
        /// Created: 2019/03/20
        /// 
        /// method to select shuttle reservation by shuttlereservationID
        /// </summary>

        public ShuttleReservation RetrieveShuttleReservationByShuttleReservationID(int shuttleReservationID)
        {
           ShuttleReservation shuttleReservation = new ShuttleReservation();
            var cmdText = @"sp_retrieve_shuttle_reservation_by_id";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);



            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ShuttleReservationID", shuttleReservationID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {

                    shuttleReservation.ShuttleReservationID = reader.GetInt32(0);
                    shuttleReservation.GuestID = reader.GetInt32(1);
                    shuttleReservation.EmployeeID = reader.GetInt32(2);
                    shuttleReservation.PickupLocation = reader.GetString(3);
                    shuttleReservation.DropoffDestination = reader.GetString(4);
                    shuttleReservation.PickupDateTime = reader.GetDateTime(5);
                    shuttleReservation.DropoffDateTime = reader.GetDateTime(6);
              

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
            return shuttleReservation;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method  to retrieve all guestids 
        /// </summary>
        public List<int> RetrieveGuestIDs()
        {
            List<int> guestIDs = new List<int>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_guest_info";
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
                        guestIDs.Add(reader.GetInt32(0));

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

           return guestIDs;
        }



        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method  to retrieve all employeeids 
        /// </summary>
        public List<int> RetrieveEmployeeIDs()
        {
            List<int> employeeIDs = new List<int>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_employee_ids";
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
                        employeeIDs.Add(reader.GetInt32(0));

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

            return employeeIDs;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method  to deactivate shuttlereservation by id
        /// </summary>

        public void DeactivateShuttleReservation(int shuttleReservationID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_shuttle_reservation_by_id ";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ShuttleReservationID", shuttleReservationID);

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
        /// Created: 2019/03/20
        /// 
        /// method  to retrieve all active shuttle reservations 
        /// </summary>
        public List<ShuttleReservation> RetrieveActiveShuttleReservations()
        {
            List<ShuttleReservation> shuttleReservations = new List<ShuttleReservation>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_active_shuttle_reservation";
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
                        ShuttleReservation shuttleReservation = new ShuttleReservation();
                        shuttleReservation.ShuttleReservationID = reader.GetInt32(0);
                        shuttleReservation.GuestID = reader.GetInt32(1);
                        shuttleReservation.EmployeeID = reader.GetInt32(2);
                        shuttleReservation.PickupLocation = reader.GetString(3);
                        shuttleReservation.DropoffDestination = reader.GetString(4);
                        shuttleReservation.PickupDateTime = reader.GetDateTime(5);
                        shuttleReservation.DropoffDateTime = reader.GetDateTime(6);
                        shuttleReservation.Active= reader.GetBoolean(7);
                        shuttleReservations.Add(shuttleReservation);
                      
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

            return shuttleReservations;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/20
        /// 
        /// method  to retrieve all inactive shuttle reservations 
        /// </summary>
        public List<ShuttleReservation> RetrieveInactiveShuttleReservations()
        {
            List<ShuttleReservation> shuttleReservations = new List<ShuttleReservation>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_inactive_shuttle_reservation";
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
                        ShuttleReservation shuttleReservation = new ShuttleReservation();
                        shuttleReservation.ShuttleReservationID = reader.GetInt32(0);
                        shuttleReservation.GuestID = reader.GetInt32(1);
                        shuttleReservation.EmployeeID = reader.GetInt32(2);
                        shuttleReservation.PickupLocation = reader.GetString(3);
                        shuttleReservation.DropoffDestination = reader.GetString(4);
                        shuttleReservation.PickupDateTime = reader.GetDateTime(5);
                        shuttleReservation.DropoffDateTime = reader.GetDateTime(6);
                        shuttleReservation.Active = reader.GetBoolean(7);
                        shuttleReservations.Add(shuttleReservation);
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

            return shuttleReservations;
        }

    }
}
