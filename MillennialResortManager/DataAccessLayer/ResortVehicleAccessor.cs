using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    /// 
    /// Resort Vehicle Status Accessor
    /// </summary>
    public class ResortVehicleAccessor : IResortVehicleAccessor
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Adds a new vehicle to the
        /// database
        /// </summary>
        /// <param name="resortVehicle">resort vehicle object</param>
        /// <returns>new vehicle id</returns>
        public int AddVehicle(ResortVehicle resortVehicle)
        {
            int id;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_create_vehicle";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@Make", resortVehicle.Make);
            cmd.Parameters.AddWithValue("@Model", resortVehicle.Model);
            cmd.Parameters.AddWithValue("@Year", resortVehicle.Year);
            cmd.Parameters.AddWithValue("@License", resortVehicle.License);
            cmd.Parameters.AddWithValue("@Mileage", resortVehicle.Mileage);
            cmd.Parameters.AddWithValue("@Capacity", resortVehicle.Capacity);
            cmd.Parameters.AddWithValue("@Color", resortVehicle.Color);
            cmd.Parameters.AddWithValue("@PurchaseDate", resortVehicle.PurchaseDate);
            cmd.Parameters.AddWithValue("@Description", resortVehicle.Description);
            cmd.Parameters.AddWithValue("@Active", resortVehicle.Active);
            cmd.Parameters.AddWithValue("@DeactivationDate", (object)resortVehicle.DeactivationDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Available", resortVehicle.Available);
            cmd.Parameters.AddWithValue("@ResortVehicleStatusId", resortVehicle.ResortVehicleStatusId);
            cmd.Parameters.AddWithValue("@ResortPropertyId", resortVehicle.ResortPropertyId);

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
        /// Created: 2019/04/03
        ///
        /// Deactivates a vehicle in database
        /// active bit is set low
        /// </summary>
        /// <param name="vehicleId">vehicle id</param>
        public void DeactivateVehicle(int vehicleId)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_deactivate_vehicle_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    throw new ApplicationException("Error: Vehicle failed to deactivate");
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Deletes a vehicle from database
        /// </summary>
        /// <param name="vehicleId"></param>
        public void DeleteVehicle(int vehicleId)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_delete_vehicle_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    throw new ApplicationException("Error: Failed to delete vehicle");
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

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves vehicle by vehicle id
        /// </summary>
        /// <param name="id">vehicle id</param>
        /// <returns>Resort Vehicle object</returns>
        public ResortVehicle RetrieveVehicleById(int id)
        {
            ResortVehicle resortVehicle;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_vehicle_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleId", id);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    resortVehicle = ParseReaderIntoNewVehicle(reader);
                }
                else
                {
                    throw new ApplicationException("Vehicle not found.");
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return resortVehicle;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves all vehicles
        /// </summary>
        /// <returns>generic collection of vehicles</returns>
        public IEnumerable<ResortVehicle> RetrieveVehicles()
        {
            List<ResortVehicle> vehicles = new List<ResortVehicle>();

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_vehicles";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicles.Add(ParseReaderIntoNewVehicle(reader));
                    }
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return vehicles;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Updates vehicle contents
        /// </summary>
        /// <param name="oldResortVehicle">old resort vehicle (mirror to database copy)</param>
        /// <param name="newResortVehicle">new updated resort vehicle</param>
        public void UpdateVehicle(ResortVehicle oldResortVehicle, ResortVehicle newResortVehicle)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_update_vehicle";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleId", oldResortVehicle.Id);
            cmd.Parameters.AddWithValue("@DeactivationDate", (object)newResortVehicle.DeactivationDate ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@OldMake", oldResortVehicle.Make);
            cmd.Parameters.AddWithValue("@OldModel", oldResortVehicle.Model);
            cmd.Parameters.AddWithValue("@OldYear", oldResortVehicle.Year);
            cmd.Parameters.AddWithValue("@OldLicense", oldResortVehicle.License);
            cmd.Parameters.AddWithValue("@OldMileage", oldResortVehicle.Mileage);
            cmd.Parameters.AddWithValue("@OldCapacity", oldResortVehicle.Capacity);
            cmd.Parameters.AddWithValue("@OldColor", oldResortVehicle.Color);
            cmd.Parameters.AddWithValue("@OldPurchaseDate", oldResortVehicle.PurchaseDate);
            cmd.Parameters.AddWithValue("@OldDescription", oldResortVehicle.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldResortVehicle.Active);
            cmd.Parameters.AddWithValue("@OldAvailable", oldResortVehicle.Available);
            cmd.Parameters.AddWithValue("@OldResortVehicleStatusId", oldResortVehicle.ResortVehicleStatusId);
            cmd.Parameters.AddWithValue("@OldResortPropertyId", oldResortVehicle.ResortPropertyId);

            cmd.Parameters.AddWithValue("@NewMake", newResortVehicle.Make);
            cmd.Parameters.AddWithValue("@NewModel", newResortVehicle.Model);
            cmd.Parameters.AddWithValue("@NewYear", newResortVehicle.Year);
            cmd.Parameters.AddWithValue("@NewLicense", newResortVehicle.License);
            cmd.Parameters.AddWithValue("@NewMileage", newResortVehicle.Mileage);
            cmd.Parameters.AddWithValue("@NewCapacity", newResortVehicle.Capacity);
            cmd.Parameters.AddWithValue("@NewColor", newResortVehicle.Color);
            cmd.Parameters.AddWithValue("@NewPurchaseDate", newResortVehicle.PurchaseDate);
            cmd.Parameters.AddWithValue("@NewDescription", newResortVehicle.Description);
            cmd.Parameters.AddWithValue("@NewActive", newResortVehicle.Active);
            cmd.Parameters.AddWithValue("@NewAvailable", newResortVehicle.Available);
            cmd.Parameters.AddWithValue("@NewResortVehicleStatusId", newResortVehicle.ResortVehicleStatusId);
            cmd.Parameters.AddWithValue("@NewResortPropertyId", newResortVehicle.ResortPropertyId);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // .. vehicle wasn't found or old and database copy did not match
                    throw new ApplicationException("Database: Vehicle not updated");
                }
                else if (result > 1)
                {
                    // .. protection against change in expected stored stored procedure behaviour
                    throw new ApplicationException("Fatal Error: More than one vehicle updated");
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Helper method to parse reader input
        /// </summary>
        /// <param name="reader">reader object containing database data</param>
        /// <returns>new resort vehicle object</returns>
        private static ResortVehicle ParseReaderIntoNewVehicle(SqlDataReader reader)
        {
            return new ResortVehicle
            {
                Id = reader.GetInt32(0),
                Make = reader.GetString(1),
                Model = reader.GetString(2),
                Year = reader.GetInt32(3),
                License = reader.GetString(4),
                Mileage = reader.GetInt32(5),
                Capacity = reader.GetInt32(6),
                Color = reader.GetString(7),
                PurchaseDate = reader.GetDateTime(8),
                Description = reader.GetString(9),
                Active = reader.GetBoolean(10),
                DeactivationDate = reader.IsDBNull(11) ? (DateTime?)null : reader.GetDateTime(11),
                Available = reader.GetBoolean(12),
                ResortVehicleStatusId = reader.GetString(13),
                ResortPropertyId = reader.GetInt32(14)
            };
        }
    }
}
