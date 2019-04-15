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
    /// Resort Property Accessor
    /// </summary>
    public class ResortPropertyAccessor : IResortPropertyAccessor
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        /// 
        /// Adds Resort Property to database
        /// </summary>
        /// <param name="resortProperty">resort property</param>
        /// <returns>resort property id</returns>
        public int AddResortProperty(ResortProperty resortProperty)
        {
            int id;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_create_resort_property";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyTypeId", resortProperty.ResortPropertyTypeId);

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
        /// Retrieves Resort Property from database by resort property id
        /// </summary>
        /// <param name="id">resort property id</param>
        /// <returns>Resort Property Object</returns>
        public ResortProperty RetrieveResortPropertyById(string id)
        {
            ResortProperty resortProperty;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_resort_property_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyId", id);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    resortProperty = new ResortProperty()
                    {
                        ResortPropertyId = reader.GetInt32(0),
                        ResortPropertyTypeId = reader.GetString(1)
                    };
                }
                else
                {
                    throw new ApplicationException("Resort Property not found.");
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return resortProperty;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves Resort Properties from database
        /// </summary>
        /// <returns>Resort Property Collection</returns>
        public IEnumerable<ResortProperty> RetrieveResortProperties()
        {
            var resortProperties = new List<ResortProperty>();

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_resort_properties";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resortProperties.Add(new ResortProperty()
                        {
                            ResortPropertyId = reader.GetInt32(0),
                            ResortPropertyTypeId = reader.GetString(1)
                        });
                    }
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return resortProperties;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Updates resort property in database
        /// </summary>
        /// <param name="oldResortProperty">old resort property</param>
        /// <param name="newResortProperty">new resort property</param>
        public void UpdateResortProperty(ResortProperty oldResortProperty, ResortProperty newResortProperty)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_update_resort_property";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyId", oldResortProperty.ResortPropertyId);
            cmd.Parameters.AddWithValue("@OldResortPropertyTypeId", oldResortProperty.ResortPropertyTypeId);

            cmd.Parameters.AddWithValue("@NewResortPropertyTypeId", newResortProperty.ResortPropertyTypeId);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // .. resort property wasn't found or old and database copy did not match
                    throw new ApplicationException("Database: Resort property not updated");
                }
                else if (result > 1)
                {
                    // .. protection against change in expected stored stored procedure behaviour
                    throw new ApplicationException("Fatal Error: More than one property updated");
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
        /// Deletes resort property from database
        /// </summary>
        /// <param name="id">resort property id</param>
        public void DeleteResortProperty(int id)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_delete_resort_property_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyId", id);

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