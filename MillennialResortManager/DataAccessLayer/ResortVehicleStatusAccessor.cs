using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Vehicle Status Accessor
    /// </summary>
    public class ResortVehicleStatusAccessor : IResortVehicleStatusAccessor
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/30
        ///
        /// Adds new vehicle status to database from database
        /// </summary>
        /// <param name="status">current vehicle status</param>
        /// <returns>returns primary key</returns>
        public int AddResortVehicleStatus(ResortVehicleStatus status)
        {
            int id;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_create_resort_vehicle_status";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortVehicleStatusId", status.Id);
            cmd.Parameters.AddWithValue("@Description", status.Description);

            try
            {
                conn.Open();

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally
            {
                conn.Close();
            }

            return id;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/30
        ///
        /// Retrieve vehicle status by id from database
        /// </summary>
        /// <param name="id">resort vehicle status id</param>
        /// <returns>resort vehicle status object</returns>
        public ResortVehicleStatus RetrieveResortVehicleStatusById(string id)
        {
            ResortVehicleStatus resortVehicleStatus;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_resort_vehicle_status_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortVehicleStatusId", id);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    resortVehicleStatus = new ResortVehicleStatus()
                    {
                        Id = reader.GetString(0),
                        Description = reader.GetString(1)
                    };
                }
                else
                {
                    throw new ApplicationException("Resort Vehicle Status not found.");
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return resortVehicleStatus;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/30
        ///
        /// Retrieves all vehicle statuses from database
        /// </summary>
        /// <returns>collection of resort vehicle statuses</returns>
        public IEnumerable<ResortVehicleStatus> RetrieveResortVehicleStatuses()
        {
            var resortVehicleStatuses = new List<ResortVehicleStatus>();

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_resort_vehicle_statuses";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resortVehicleStatuses.Add(new ResortVehicleStatus()
                        {
                            Id = reader.GetString(0),
                            Description = reader.GetString(1)
                        });
                    }
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return resortVehicleStatuses;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/30
        ///
        /// Updates Resort Vehicle Status in database
        /// </summary>
        /// <param name="oldStatus">old status</param>
        /// <param name="newStatus">new status</param>
        public void UpdateResortVehicleStatus(ResortVehicleStatus oldStatus, ResortVehicleStatus newStatus)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_update_resort_vehicle_status";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyId", oldStatus.Id);
            cmd.Parameters.AddWithValue("@OldDescription", oldStatus.Description);

            cmd.Parameters.AddWithValue("@NewDescription", newStatus.Description);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // .. resort property wasn't found or old and database copy did not match
                    throw new ApplicationException("Database: Vehicle status not updated");
                }
                else if (result > 1)
                {
                    // .. protection against change in expected stored stored procedure behaviour
                    throw new ApplicationException("Fatal Error: More than one vehicle status updated");
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/30
        ///
        /// Deletes Resort Vehicle Status from database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteResortVehicleStatus(string id)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_delete_resort_vehicle_status_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortVehicleStatusId", id);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    throw new ApplicationException("Error: Failed to delete resort property");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
