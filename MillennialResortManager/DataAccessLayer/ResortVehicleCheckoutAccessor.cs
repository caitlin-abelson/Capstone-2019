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
    /// Vehicle Checkout Accessor
    /// </summary>
    public class ResortVehicleCheckoutAccessor : IResortVehicleCheckoutAccessor
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Adds a vehicle checkout to database
        /// </summary>
        /// <param name="resortVehicleCheckout">resort vehicle checkout</param>
        /// <returns>returns an id for vehicle checkout</returns>
        public int AddVehicleCheckout(ResortVehicleCheckout resortVehicleCheckout)
        {
            int id;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_create_vehicle_checkout";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleCheckoutId", resortVehicleCheckout.VehicleCheckoutId);
            cmd.Parameters.AddWithValue("@EmployeeId", resortVehicleCheckout.EmployeeId);
            cmd.Parameters.AddWithValue("@DateCheckedOut	", resortVehicleCheckout.DateCheckedOut);
            cmd.Parameters.AddWithValue("@DateReturned", resortVehicleCheckout.DateReturned);
            cmd.Parameters.AddWithValue("@DateExpectedBack", resortVehicleCheckout.DateExpectedBack);
            cmd.Parameters.AddWithValue("@Returned", resortVehicleCheckout.Returned);
            cmd.Parameters.AddWithValue("@ResortVehicleId", resortVehicleCheckout.ResortVehicleId);

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
        /// Retrieves vehicle checkout from database by vehicle checkout id
        /// </summary>
        /// <param name="resortVehicleCheckoutId">resort vehicle checkout id</param>
        /// <returns>resort vehicle checkout object</returns>
        public ResortVehicleCheckout RetrieveVehicleCheckoutById(int resortVehicleCheckoutId)
        {
            ResortVehicleCheckout resortVehicleCheckout;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_vehicle_checkout_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleCheckoutId", resortVehicleCheckoutId);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    resortVehicleCheckout = new ResortVehicleCheckout()
                    {
                        VehicleCheckoutId = reader.GetInt32(0),
                        EmployeeId = reader.GetInt32(1),
                        DateCheckedOut = reader.GetDateTime(2),
                        DateReturned = reader.GetDateTime(3),
                        DateExpectedBack = reader.GetDateTime(4),
                        Returned = reader.GetBoolean(5),
                        ResortVehicleId = reader.GetInt32(6)
                    };
                }
                else
                {
                    throw new ApplicationException("Vehicle Checkout record not found.");
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return resortVehicleCheckout;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves all vehicle checkouts from database
        /// </summary>
        /// <returns>Resort Vehicle Checkout Collection</returns>
        public IEnumerable<ResortVehicleCheckout> RetrieveVehicleCheckouts()
        {
            var vehicleCheckouts = new List<ResortVehicleCheckout>();

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_vehicle_checkouts";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicleCheckouts.Add(new ResortVehicleCheckout
                        {
                            VehicleCheckoutId = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            DateCheckedOut = reader.GetDateTime(2),
                            DateReturned = reader.GetDateTime(3),
                            DateExpectedBack = reader.GetDateTime(4),
                            Returned = reader.GetBoolean(5),
                            ResortVehicleId = reader.GetInt32(6)

                        });

                    }
                }
                else
                {
                    throw new ApplicationException("Vehicle Checkout record not found.");
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return vehicleCheckouts;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Updates vehicle checkout in database
        /// </summary>
        /// <param name="old">old vehicle checkout (current database copy)</param>
        /// <param name="newResortVehicleCheckOut">new vehicle checkout (updated copy)</param>
        public void UpdateVehicleCheckouts(ResortVehicleCheckout old, ResortVehicleCheckout newResortVehicleCheckOut)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_update_vehicle_checkout";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleCheckoutId", old.VehicleCheckoutId);

            cmd.Parameters.AddWithValue("@OldEmployeeId", old.EmployeeId);
            cmd.Parameters.AddWithValue("@OldDateCheckedOut	", old.DateCheckedOut);
            cmd.Parameters.AddWithValue("@OldDateReturned", old.DateReturned);
            cmd.Parameters.AddWithValue("@OldDateExpectedBack", old.DateExpectedBack);
            cmd.Parameters.AddWithValue("@OldReturned", old.Returned);
            cmd.Parameters.AddWithValue("@OldResortVehicleId", old.ResortVehicleId);

            cmd.Parameters.AddWithValue("@NewEmployeeId", newResortVehicleCheckOut.EmployeeId);
            cmd.Parameters.AddWithValue("@NewDateCheckedOut	", newResortVehicleCheckOut.DateCheckedOut);
            cmd.Parameters.AddWithValue("@NewDateReturned", newResortVehicleCheckOut.DateReturned);
            cmd.Parameters.AddWithValue("@NewDateExpectedBack", newResortVehicleCheckOut.DateExpectedBack);
            cmd.Parameters.AddWithValue("@NewReturned", newResortVehicleCheckOut.Returned);
            cmd.Parameters.AddWithValue("@NewResortVehicleId", newResortVehicleCheckOut.ResortVehicleId);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // .. resort property wasn't found or old and database copy did not match
                    throw new ApplicationException("Database: Vehicle resortVehicleCheckout not updated");
                }
                else if (result > 1)
                {
                    // .. protection against change in expected stored stored procedure behaviour
                    throw new ApplicationException("Fatal Error: More than one record updated!");
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
        /// Deletes vehicle checkout in database
        /// </summary>
        /// <param name="vehicleCheckoutId">Resort Vehicle Checkout</param>
        public void DeleteVehicleCheckout(int vehicleCheckoutId)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_delete_vehicle_checkout_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@VehicleCheckoutId", vehicleCheckoutId);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    throw new ApplicationException("Error: Failed to delete vehicle resortVehicleCheckout property");
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