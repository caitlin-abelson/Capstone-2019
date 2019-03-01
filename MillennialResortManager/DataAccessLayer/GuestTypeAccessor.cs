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
	/// GuestTypeAccessor class is used to access the Guest Type table
	/// and the stored procedures as well
	/// </summary>
    public class GuestTypeAccessor : IGuestTypeAccessor
    {
        /// <summary>
        /// Method that retrieves the guest types table and stores it as a list
        /// </summary>
        /// <returns>List of Guest Types </returns>	
        public List<GuestType> SelectGuestTypes(string status)
        {
            List<GuestType> guestTypes = new List<GuestType>();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_guest_types";

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
                        guestTypes.Add(new GuestType()
                        {
                            GuestTypeID = reader.GetString(0),
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

            return guestTypes;
        }

        /// <summary>
        /// Method that creates a new guest type and stores it in the table
        /// </summary>
        /// <param name="guestType">Object holding the data to add to the table</param>
        /// <returns> Row Count </returns>	
        public int InsertGuestType(GuestType guestType)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_guest_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestTypeID", guestType.GuestTypeID);
            cmd.Parameters.AddWithValue("@Description", guestType.Description);

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
        /// Method that retrieves the guest type IDs to store into a combo box
        /// </summary>
        /// <returns>List of Guest Types </returns>	
        public List<string> SelectAllTypes()
        {
            var types = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_guestTypes", conn);
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

        /// <summary>
        /// Method that deletes a guest type and removes it from the table
        /// </summary>
        /// <param name="guestTypeID">The ID of the guest type being deleted</param>
        /// <returns> Row Count </returns>
        public int DeleteGuestType(string guestTypeID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_guest_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestTypeID", guestTypeID);

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
