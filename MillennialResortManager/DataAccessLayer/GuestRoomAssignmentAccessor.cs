using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created On: 2019-04-25
    /// Class for interaction with database for GuestRoomAssignment operations
    /// </summary>
    public class GuestRoomAssignmentAccessor : IGuestRoomAssignmentAccessor
    {
        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Retrieves a list of guest Room Assignment View Models
        /// 
        /// </summary>
        /// <param name="reservationID">ID of Reservation you want Guests of</param>
        /// <returns>List of Room Assignment View Models</returns>
        public List<GuestRoomAssignmentVM> SelectGuestRoomAssignmentVMSByReservationID(int reservationID)
        {
            List<GuestRoomAssignmentVM> assignments = new List<GuestRoomAssignmentVM>();
            var conn = DBConnection.GetDbConnection();
            
            string cmdText1 = @"sp_select_all_guestroomassignvms_by_reservationID";
            
            var cmd1 = new SqlCommand(cmdText1, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@ReservationID", reservationID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateTime? checkinDate;
                        DateTime? checkoutDate;
                        if (reader.IsDBNull(6))
                        {
                            checkinDate = null;
                        }
                        else
                        {
                            checkinDate = reader.GetDateTime(6);
                        }
                        if (reader.IsDBNull(7))
                        {
                            checkoutDate = null;
                        }
                        else
                        {
                            checkoutDate = reader.GetDateTime(7);
                        }
                        assignments.Add(new GuestRoomAssignmentVM()
                        {
                            GuestID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            BuildingName = reader.GetString(3),
                            RoomNumber = reader.GetInt32(4),
                            RoomReservationID = reader.GetInt32(5),
                            CheckinDate = checkinDate,
                            CheckOutDate = checkoutDate
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
            return assignments;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Updates a guest room assignment to checkedout and sets the date
        /// 
        /// </summary>
        /// <param name="roomReservationID">ID of Room Reservation </param>
        /// <param name="guestID">ID of Guest</param>
        /// <returns>0 if failed, greater if successful</returns>
        public int UpdateGuestRoomAssignmentToCheckedOut(int guestID, int roomReservationID)
        {
            int result = 0;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_guestroomassignment_checkoutdate";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GuestID", guestID);
            cmd.Parameters.AddWithValue("@RoomReservationID", roomReservationID);
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
