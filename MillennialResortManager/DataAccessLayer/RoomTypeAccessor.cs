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
	/// Austin Berquam
	/// Created: 2019/01/26
	/// 
	/// RoomTypeAccessor class is used to access the Room Type table
	/// and the stored procedures as well
	/// </summary>
    public class RoomTypeAccessor : IRoomTypeAccessor
    {
        
        /// <summary>
        /// Method that retrieves the room types table and stores it as a list
        /// </summary>
        /// <returns>List of Room Types </returns>	
        public List<RoomType> SelectRoomTypes(string status)
        {
            List<RoomType> roomTypes = new List<RoomType>();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_room_types";

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
                        roomTypes.Add(new RoomType()
                        {
                            RoomTypeID = reader.GetString(0),
                            Description = reader.GetString(1)

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

            return roomTypes;
        }

        /// <summary>
        /// Method that creates a new room type and stores it in the table
        /// </summary>
        /// <param name="roomType">Object holding the data to add to the table</param>
        /// <returns> Row Count </returns>	
        public int InsertRoomType(RoomType roomType)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_room_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomTypeID", roomType.RoomTypeID);
            cmd.Parameters.AddWithValue("@Description", roomType.Description);

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
        /// Method that deletes a room type and removes it from the table
        /// </summary>
        /// <param name="roomTypeID">The ID of the room type being deleted</param>
        /// <returns> Row Count </returns>	
        public int DeleteRoomType(string roomTypeID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_room_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

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
        /// Method that retrieves the room type IDs to store into a combo box
        /// </summary>
        /// <returns>List of Room Types </returns>	
        public List<string> SelectAllTypes()
        {
            var types = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_roomTypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        types.Add(r.GetString(0));
                    }
                }
                r.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return types;
        }
    }
}
