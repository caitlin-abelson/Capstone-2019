/// <summary>
/// Wes Richardson
/// Created: 2019/01/24
/// 
/// Data Access Related to the Hotel's Rooms
/// </summary>
using System;
using System.Collections.Generic;
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
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/04/04
        /// 
        /// Updated to accomidate the number of rooms to be added
        /// </remarks>
        /// <param name="room"></param>
        /// <returns>Rows affted</returns>
        public int InsertNewRoom(Room room, int employeeID, int numOfRooms)
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
                for (int i = 0; i <= numOfRooms; i++)
                {
                    cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber + 1);
                    rows = cmd.ExecuteNonQuery();
                }

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
                        RoomNumber = reader.GetString(0),
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
        public int UpdateRoom(Room room)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_room";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
            cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
            cmd.Parameters.AddWithValue("@BuildingID", room.Building);
            cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomType);
            cmd.Parameters.AddWithValue("@Description", room.Description);
            cmd.Parameters.AddWithValue("@Price", room.Price);
            cmd.Parameters.AddWithValue("@Capacity", room.Capacity);

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
                            RoomNumber = reader.GetString(1),
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
    }
}
