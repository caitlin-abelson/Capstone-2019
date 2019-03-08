using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Richard Carroll
    /// Created Date: 3/1/19
    /// 
    /// This Class contains methods for interacting with the Database
    /// with GuestVehicle Data Objects and 
    /// returns the results of calling those methods.
    /// </summary>
    public class GuestVehicleAccessor : IGuestVehicleAccessor
    {
        /// <summary>
        /// Richard Carroll
        /// Created: 3/1/19
        /// 
        /// Takes a Vehicle argument and uses it for parameters
        /// in a stored procedure for Inserting a GuestVehicle 
        /// and calls the Stored Procedure
        /// Returns the number of rows affected.
        /// </summary>
        public int InsertGuestVehicle(GuestVehicle vehicle)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_insert_guest_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GuestID", vehicle.GuestID);
            cmd.Parameters.AddWithValue("@Make", vehicle.Make);
            cmd.Parameters.AddWithValue("@Model", vehicle.Model);
            cmd.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
            cmd.Parameters.AddWithValue("@Color", vehicle.Color);
            cmd.Parameters.AddWithValue("@ParkingLocation", vehicle.ParkingLocation);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }


        /// <summary>
        /// Richard Carroll
        /// Created: 3/7/19
        /// 
        /// Requests a List of GuestVehicles from the 
        /// Database and returns the result.
        /// </summary>
        public List<VMGuestVehicle> SelectAllGuestVehicles()
        {
            List<VMGuestVehicle> vehicles = new List<VMGuestVehicle>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_select_all_guest_vehicles";
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
                        VMGuestVehicle vehicle = new VMGuestVehicle()
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            GuestID = reader.GetInt32(2),
                            Make = reader.GetString(3),
                            Model = reader.GetString(4),

                            PlateNumber = reader.GetString(5),
                            Color = reader.GetString(6),
                            ParkingLocation = reader.GetString(7)
                        };
                        vehicles.Add(vehicle);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return vehicles;
        }


        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Takes two Vehicle arguments and uses them for parameters
        /// in a stored procedure for Updating a GuestVehicle 
        /// and calls the Stored Procedure
        /// Returns the number of rows affected.
        /// </summary>
        public int UpdateGuestVehicle(GuestVehicle oldVehicle, GuestVehicle vehicle)
        {
            int rowsAffected = 0;

            var cmdText = "sp_update_guest_vehicle";
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@OldGuestID", oldVehicle.GuestID);
            cmd.Parameters.AddWithValue("@OldMake", oldVehicle.Make);
            cmd.Parameters.AddWithValue("@OldModel", oldVehicle.Model);
            cmd.Parameters.AddWithValue("@OldPlateNumber", oldVehicle.PlateNumber);
            cmd.Parameters.AddWithValue("@OldColor", oldVehicle.Color);
            cmd.Parameters.AddWithValue("@OldParkingLocation", oldVehicle.ParkingLocation);

            cmd.Parameters.AddWithValue("@GuestID", vehicle.GuestID);
            cmd.Parameters.AddWithValue("@Make", vehicle.Make);
            cmd.Parameters.AddWithValue("@Model", vehicle.Model);
            cmd.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
            cmd.Parameters.AddWithValue("@Color", vehicle.Color);
            cmd.Parameters.AddWithValue("@ParkingLocation", vehicle.ParkingLocation);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }
    }
}
