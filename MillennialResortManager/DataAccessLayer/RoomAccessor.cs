/// <summary>
/// Wes Richardson
/// Created: 2019/01/24
/// 
/// Data Access Related to the Hotel's Rooms
/// </summary>
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
    public class RoomAccessor : IRoomAccessor
    {
        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// </summary>
        /// <returns>A list of Buildings</returns>
        public List<string> SelectBuildings()
        {
            var buildingList = new List<string>();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_buildings";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        buildingList.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("Building data not found");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return buildingList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// </summary>
        /// <returns>A list of RoomsTypes</returns>
        public List<string> SelectRoomTypes()
        {
            var roomTypeList = new List<string>();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_room_types";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roomTypeList.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ApplicationException("Room Type data not found");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }

            return roomTypeList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/24
        /// 
        /// </summary>
        /// <remarks>
        /// Danielle Russo
        /// 2019/04/01
        /// 
        /// Removed "Available" parameter
        /// Added "Offering Type ID" parameter needed for creating a new Offering ID
        /// Added offeringTypeID variable to match stored procedure
        /// </remarks>
        /// <param name="room"></param>
        /// <returns>Rows affted</returns>
        public int InsertNewRoom(Room room, int employeeID)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();
            string offeringTypeID = "Room";

            var cmdText = @"sp_insert_room";

            var cmd = new SqlCommand(cmdText, conn);
            // room needs to create a offering to get a offeringID
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
            cmd.Parameters.AddWithValue("@BuildingID", room.Building);
            cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomType);
            cmd.Parameters.AddWithValue("@Description", room.Description);
            cmd.Parameters.AddWithValue("@Capacity", room.Capacity);
            cmd.Parameters.AddWithValue("@RoomStatusID", room.RoomStatus);
            cmd.Parameters.AddWithValue("@OfferingTypeID", offeringTypeID);
            cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
            cmd.Parameters.AddWithValue("@Price", room.Price);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch
            {

                throw;
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/30
        /// </summary>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Removed  Available, Active fields
        /// Added ResortPropertyId and RoomStatus fields
        /// Updated sp name
        /// Converted SqlMoney data type to decimal for price
        /// </remarks>
        /// <param name="roomID"></param>
        /// <returns>A room</returns>
        public Room SelectRoomByID(int roomID)
        {
            Room room = new Room();
            Building bd = new Building();
            RoomType rt = new RoomType();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_select_room_by_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RoomID", roomID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    room = new Room
                    {
                        RoomNumber = reader.GetInt32(0),
                        Building = reader.GetString(1),
                        RoomType = reader.GetString(2),
                        Description = reader.GetString(3),
                        Capacity = reader.GetInt32(4),
                        Price = (decimal)reader.GetSqlMoney(5),
                        ResortPropertyID = reader.GetInt32(6),
                        OfferingID = reader.GetInt32(7),
                        RoomStatus = reader.GetString(8)
                    };
                    room.RoomID = roomID;
                }
                else
                {
                    throw new ApplicationException("Record not found");
                }

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }
            return room;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/01/30
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns>Rows affected</returns>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/05
        /// Removed Active and Available parameters
        /// </remarks>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/11
        /// Added OfferingID  and RoomStatusID parameters
        /// </remarks>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/11
        /// Added updatedRoom parameter
        /// Updated all parameters
        /// </remarks>
        public int UpdateRoom(Room selectedRoom, Room updatedRoom)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_room";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomID", selectedRoom.RoomID);
            cmd.Parameters.AddWithValue("@OfferingID", selectedRoom.OfferingID);

            cmd.Parameters.AddWithValue("@OldRoomNumber", selectedRoom.RoomNumber);
            cmd.Parameters.AddWithValue("@OldBuildingID", selectedRoom.Building);
            cmd.Parameters.AddWithValue("@OldDescription", selectedRoom.Description);
            cmd.Parameters.AddWithValue("@OldRoomTypeID", selectedRoom.RoomType);
            cmd.Parameters.AddWithValue("@OldRoomStatusID", selectedRoom.RoomStatus);
            cmd.Parameters.AddWithValue("@OldPrice", selectedRoom.Price);
            cmd.Parameters.AddWithValue("@OldCapacity", selectedRoom.Capacity);

            cmd.Parameters.AddWithValue("@NewRoomNumber", updatedRoom.RoomNumber);
            cmd.Parameters.AddWithValue("@NewBuildingID", updatedRoom.Building);
            cmd.Parameters.AddWithValue("@NewDescription", updatedRoom.Description);
            cmd.Parameters.AddWithValue("@NewRoomTypeID", updatedRoom.RoomType);
            cmd.Parameters.AddWithValue("@NewRoomStatusID", updatedRoom.RoomStatus);
            cmd.Parameters.AddWithValue("@NewPrice", updatedRoom.Price);
            cmd.Parameters.AddWithValue("@NewCapacity", updatedRoom.Capacity);

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

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/07
        /// 
        /// </summary>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Removed Price, Available, Active fields
        /// Added ResortPropertyId and RoomStatus fields
        /// Converted SqlMoney data type to decimal for price
        /// </remarks>
        /// <returns>List of Rooms</returns>
        public List<Room> SelectRoomList()
        {
            List<Room> roomList = new List<Room>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_room_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var rm = new Room()
                        {
                            RoomID = reader.GetInt32(0),
                            RoomNumber = reader.GetInt32(1),
                            Building = reader.GetString(2),
                            RoomType = reader.GetString(3),
                            Description = reader.GetString(4),
                            Capacity = reader.GetInt32(5),
                            Price = (decimal)reader.GetSqlMoney(6),
                            ResortPropertyID = reader.GetInt32(7),
                            OfferingID = reader.GetInt32(8),
                            RoomStatus = reader.GetString(9)
                        };
                        roomList.Add(rm);
                    }
                }
                else
                {
                    throw new ApplicationException("Data not found");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }


            return roomList;
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// Deletes a Room
        /// 
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public int DeleteRoom(Room room)
        {
            throw new NotImplementedException();
            // return DeleteRoomByID(room.RoomID);
        }

        /// <summary>
        /// Wes Richardson
        /// Created: 2019/02/12
        /// Deletes a room by the roomID
        /// 
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public int DeleteRoomByID(int roomID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/04/05
        /// 
        /// </summary>
        /// <returns>A list of Room Status</returns>
        public List<string> SelectRoomStatusList()
        {
            List<string> roomStatus = new List<string>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_room_status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roomStatus.Add(reader.GetString(0));

                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database access error", ex);
            }
            finally
            {
                conn.Close();
            }


            return roomStatus;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/04/10
        /// 
        /// </summary>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/11
        /// 
        /// Added Offering ID field
        /// </remarks>
        /// <returns>A list of Rooms in a selected building</returns>
        public List<Room> SelectRoomsByBuildingID(string buildingId)
        {
            List<Room> rooms = new List<Room>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_room_list_by_buildingid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BuildingID", buildingId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rooms.Add(new Room()
                        {
                            RoomID = reader.GetInt32(0),
                            RoomNumber = reader.GetInt32(1),
                            RoomType = reader.GetString(2),
                            Description = reader.GetString(3),
                            Capacity = reader.GetInt32(4),
                            OfferingID = reader.GetInt32(5),
                            Price = (decimal)reader.GetSqlMoney(6),
                            ResortPropertyID = reader.GetInt32(7),
                            RoomStatus = reader.GetString(8),
                            Building = buildingId
                        });
                    }
                }
            }
            catch (SqlException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return rooms;
        }
    }
}
