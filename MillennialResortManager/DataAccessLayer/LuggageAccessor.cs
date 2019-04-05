using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Jacob Miller
    /// Created: 3/28/19
    /// </summary>
    public class LuggageAccessor : ILuggageAccessor
    {
        public bool CreateLuggage(Luggage l)
        {
            bool result = false;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_insert_luggage";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@GuestID", SqlDbType.Int);
            cmd.Parameters.Add("@LuggageStatusID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@GuestID"].Value = l.GuestID;
            cmd.Parameters["@LuggageStatusID"].Value = l.Status;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() == 1;
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
        public Luggage RetrieveLuggageByID(int id)
        {
            Luggage l = null;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_luggage_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LuggageID", SqlDbType.Int);
            cmd.Parameters["@LuggageID"].Value = id;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int luggageID = reader.GetInt32(0);
                        int guestID = reader.GetInt32(1);
                        string status = reader.GetString(2);
                        l = new Luggage() { LuggageID = luggageID, GuestID = guestID, Status = status};
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
            return l;
        }
        public List<Luggage> RetrieveAllLuggage()
        {
            List<Luggage> l = new List<Luggage>();
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_all_luggage";
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
                        int luggageID = reader.GetInt32(0);
                        int guestID = reader.GetInt32(1);
                        string status = reader.GetString(2);
                        l.Add(new Luggage() { LuggageID = luggageID, GuestID = guestID, Status = status });
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
            return l;
        }
        public bool UpdateLuggage(Luggage oldLuggage, Luggage newLuggage)
        {
            bool result = false;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_update_Luggage";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LuggageID", SqlDbType.Int);
            cmd.Parameters.Add("@OldGuestID", SqlDbType.Int);
            cmd.Parameters.Add("@NewGuestID", SqlDbType.Int);
            cmd.Parameters.Add("@OldLuggageStatusID", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewLuggageStatusID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@LuggageID"].Value = oldLuggage.LuggageID;
            cmd.Parameters["@OldGuestID"].Value = oldLuggage.GuestID;
            cmd.Parameters["@NewGuestID"].Value = newLuggage.GuestID;
            cmd.Parameters["@OldLuggageStatusID"].Value = oldLuggage.Status;
            cmd.Parameters["@NewLuggageStatusID"].Value = newLuggage.Status;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() == 1;
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
        public bool DeleteLuggage(Luggage l)
        {
            bool result = false;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_delete_luggage";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LuggageID", SqlDbType.Int);
            cmd.Parameters["@LuggageID"].Value = l.LuggageID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() == 1;
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
        public List<LuggageStatus> RetrieveAllLuggageStatus()
        {
            List<LuggageStatus> status = new List<LuggageStatus>();
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_all_luggage_status";
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
                        status.Add(new LuggageStatus() { LuggageStatusID = reader.GetString(0)});
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
            return status;
        }
    }
}
