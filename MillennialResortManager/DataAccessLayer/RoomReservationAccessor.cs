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
    public class RoomReservationAccessor : IRoomReservationAccessor
    {


        public int DeleteGuestRoomAssignment(int guestID, int roomReservationID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_unassign_room_reservation";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", guestID);
            cmd.Parameters.AddWithValue("@RoomReservationID", roomReservationID);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public int InsertGuestRoomAssignment(int guestID, int roomReservationID)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_assign_room_reservation";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", guestID);
            cmd.Parameters.AddWithValue("@RoomReservationID", roomReservationID);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-11
        /// 
        /// Select the Room Reservation by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomReservation SelectRoomReservationByID(int id)
        {
            RoomReservation roomReservation = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_roomreservation_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomReservationID", id);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    roomReservation = new RoomReservation()
                    {
                        RoomReservationID = reader.GetInt32(0),
                        RoomID = reader.GetInt32(1),
                        ReservationID = reader.GetInt32(2)
                    };

                    // CheckIn, CheckOut can be NULL
                    if (reader.IsDBNull(3))
                    {
                        roomReservation.CheckInDate = null;
                    } 
                    else
                    {
                        roomReservation.CheckInDate = reader.GetDateTime(3);
                    }

                    if (reader.IsDBNull(4))
                    {
                        roomReservation.CheckOutDate = null;
                    }
                    else
                    {
                        roomReservation.CheckOutDate = reader.GetDateTime(4);
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


            return roomReservation;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-16
        /// 
        /// Select the RoomReservation that the Guest is assigned to.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomReservation SelectRoomReservationByGuestID(int id)
        {
            RoomReservation roomReservation = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_roomreservation_by_guest_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", id);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    roomReservation = new RoomReservation()
                    {
                        RoomReservationID = reader.GetInt32(0),
                        RoomID = reader.GetInt32(1),
                        ReservationID = reader.GetInt32(2)
                    };

                    // CheckIn, CheckOut can be NULL
                    if (reader.IsDBNull(3))
                    {
                        roomReservation.CheckInDate = null;
                    }
                    else
                    {
                        roomReservation.CheckInDate = reader.GetDateTime(3);
                    }

                    if (reader.IsDBNull(4))
                    {
                        roomReservation.CheckOutDate = null;
                    }
                    else
                    {
                        roomReservation.CheckOutDate = reader.GetDateTime(4);
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }


            return roomReservation;
        }

        public List<VMRoomRoomReservation> SelectAvailableVMRoomRoomReservations(int reservationId)
        {
            List<VMRoomRoomReservation> vmRooms = new List<VMRoomRoomReservation>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_room_roomreservation_viewmodel_by_reservation_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReservationID", reservationId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var vmRoom = new VMRoomRoomReservation()
                        {
                            ReservationID = reservationId
                        };

                        vmRoom.RoomReservationID = reader.GetInt32(0);
                        vmRoom.RoomID = reader.GetInt32(1);
                        vmRoom.RoomNumber = reader.GetInt32(2);
                        vmRoom.BuildingName = reader.GetString(3);
                        vmRoom.CurrentlyAssigned = reader.GetInt32(4);
                        vmRoom.Capacity = reader.GetInt32(5);

                        // Dates are nullable.
                        if (reader.IsDBNull(6))
                        {
                            vmRoom.CheckInDate = null;
                        }
                        else
                        {
                            vmRoom.CheckInDate = reader.GetDateTime(6);
                        }
                        if (reader.IsDBNull(7))
                        {
                            vmRoom.CheckOutDate = null;
                        }
                        else
                        {
                            vmRoom.CheckOutDate = reader.GetDateTime(7);
                        }


                        vmRooms.Add(vmRoom);
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

            return vmRooms;
        }

        public int UpdateCheckInDateToNow(RoomReservation roomReservation)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_checkindate";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomReservationID", roomReservation.ReservationID);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }
    }
}
