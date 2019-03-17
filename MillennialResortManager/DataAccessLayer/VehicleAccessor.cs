using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/2/23
    /// 
    /// Data Accessor for Shuttel Véhicule
    /// </summary>
    public class VehicleAccessor : IVehicleAccessor
    {
        /// <summary>
        ///     Adds a new vehicle to the database
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns>New Vehicle Id</returns>
        public int AddVehicle(Vehicle vehicle)
        {
            int id;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_create_vehicle";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@Make", vehicle.Make);
            cmd.Parameters.AddWithValue("@Model", vehicle.Model);
            cmd.Parameters.AddWithValue("@YearOfManufacture", vehicle.YearOfManufacture);
            cmd.Parameters.AddWithValue("@License", vehicle.License);
            cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
            cmd.Parameters.AddWithValue("@Vin", vehicle.Vin);
            cmd.Parameters.AddWithValue("@Capacity", vehicle.Capacity);
            cmd.Parameters.AddWithValue("@Color", vehicle.Color);
            cmd.Parameters.AddWithValue("@PurchaseDate", vehicle.PurchaseDate);
            cmd.Parameters.AddWithValue("@Description", vehicle.Description);
            cmd.Parameters.AddWithValue("@Active", vehicle.Active);
            cmd.Parameters.AddWithValue("@DeactivationDate", (object)vehicle.DeactivationDate ?? DBNull.Value);

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
            catch(Exception ex)
            {
                throw new ApplicationException("Database Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public Vehicle RetrieveVehicleById(int id)
        {
            Vehicle vehicle;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_retrieve_vehicle_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleId", id);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    vehicle = new Vehicle
                    {
                        Id = reader.GetInt32(0),
                        Make = reader.GetString(1),
                        Model = reader.GetString(2),
                        YearOfManufacture = reader.GetInt32(3),
                        License = reader.GetString(4),
                        Mileage = reader.GetInt32(5),
                        Vin = reader.GetString(6),
                        Capacity = reader.GetInt32(7),
                        Color = reader.GetString(8),
                        PurchaseDate = reader.GetDateTime(9),
                        Description = reader.GetString(10),
                        Active = reader.GetBoolean(11),
                        DeactivationDate = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12)
                    };
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

            return vehicle;
        }

        public IEnumerable<Vehicle> RetrieveVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_retrieve_vehicles";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicles.Add(new Vehicle
                        {
                            Id = reader.GetInt32(0),
                            Make = reader.GetString(1),
                            Model = reader.GetString(2),
                            YearOfManufacture = reader.GetInt32(3),
                            License = reader.GetString(4),
                            Mileage = reader.GetInt32(5),
                            Vin = reader.GetString(6),
                            Capacity = reader.GetInt32(7),
                            Color = reader.GetString(8),
                            PurchaseDate = reader.GetDateTime(9),
                            Description = reader.GetString(10),
                            Active = reader.GetBoolean(11),
                            DeactivationDate = reader.IsDBNull(12) ? (DateTime?)null : reader.GetDateTime(12)
                        });
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

        public void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_update_vehicle";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };


            cmd.Parameters.AddWithValue("@VehicleId", oldVehicle.Id);
            cmd.Parameters.AddWithValue("@DeactivationDate", (object)newVehicle.DeactivationDate ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@OldMake", oldVehicle.Make);
            cmd.Parameters.AddWithValue("@OldModel", oldVehicle.Model);
            cmd.Parameters.AddWithValue("@OldYearOfManufacture", oldVehicle.YearOfManufacture);
            cmd.Parameters.AddWithValue("@OldLicense", oldVehicle.License);
            cmd.Parameters.AddWithValue("@OldMileage", oldVehicle.Mileage);
            cmd.Parameters.AddWithValue("@OldVin", oldVehicle.Vin);
            cmd.Parameters.AddWithValue("@OldCapacity", oldVehicle.Capacity);
            cmd.Parameters.AddWithValue("@OldColor", oldVehicle.Color);
            cmd.Parameters.AddWithValue("@OldPurchaseDate", oldVehicle.PurchaseDate);
            cmd.Parameters.AddWithValue("@OldDescription", oldVehicle.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldVehicle.Active);

            cmd.Parameters.AddWithValue("@NewMake", newVehicle.Make);
            cmd.Parameters.AddWithValue("@NewModel", newVehicle.Model);
            cmd.Parameters.AddWithValue("@NewYearOfManufacture", newVehicle.YearOfManufacture);
            cmd.Parameters.AddWithValue("@NewLicense", newVehicle.License);
            cmd.Parameters.AddWithValue("@NewMileage", newVehicle.Mileage);
            cmd.Parameters.AddWithValue("@NewVin", newVehicle.Vin);
            cmd.Parameters.AddWithValue("@NewCapacity", newVehicle.Capacity);
            cmd.Parameters.AddWithValue("@NewColor", newVehicle.Color);
            cmd.Parameters.AddWithValue("@NewPurchaseDate", newVehicle.PurchaseDate);
            cmd.Parameters.AddWithValue("@NewDescription", newVehicle.Description);
            cmd.Parameters.AddWithValue("@NewActive", newVehicle.Active);

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
    }
}
