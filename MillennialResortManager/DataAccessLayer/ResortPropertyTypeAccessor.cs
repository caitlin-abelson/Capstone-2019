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
    /// Resort Property Type Accessor
    /// </summary>
    public class ResortPropertyTypeAccessor : IResortPropertyTypeAccessor
    {
        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Adds Resort Property Type to database
        /// </summary>
        /// <param name="resortPropertyType">resort property type</param>
        /// <returns>id to new resort property type added to database</returns>
        public string AddResortPropertyType(ResortPropertyType resortPropertyType)
        {
            string id;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_create_resort_property_type";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyTypeId", resortPropertyType.ResortPropertyTypeId);

            try
            {
                conn.Open();

                id = Convert.ToString(cmd.ExecuteScalar());
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
        /// Retrieves Resort Property Type by Resort Property Type Id
        /// </summary>
        /// <param name="id">resort property type id</param>
        /// <returns>Resort Property Type object</returns>
        public ResortPropertyType RetrieveResortPropertyTypeById(string id)
        {
            ResortPropertyType resortPropertyType;

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_resort_property_type_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyTypeId", id);

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    resortPropertyType = new ResortPropertyType()
                    {
                        ResortPropertyTypeId = reader.GetString(0)
                    };
                }
                else
                {
                    throw new ApplicationException("Resort Property Type not found.");
                }

                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return resortPropertyType;
        }

        /// <summary>
        /// Francis Mingomba
        /// Created: 2019/04/03
        ///
        /// Retrieves all Resort Property Types from database
        /// </summary>
        /// <returns>Resort Property Type Collection</returns>
        public IEnumerable<ResortPropertyType> RetrieveResortPropertyTypes()
        {
            var resortProperties = new List<ResortPropertyType>();

            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_select_resort_property_types";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resortProperties.Add(new ResortPropertyType()
                        {
                            ResortPropertyTypeId = reader.GetString(0)
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
        /// Updates Resort Property Type in database
        /// </summary>
        /// <param name="old">old Resort Property Type (current database copy)</param>
        /// <param name="newResortPropertyType">new Resort Property Type (updated copy)</param>
        public void UpdateResortPropertyType(ResortPropertyType old, ResortPropertyType newResortPropertyType)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_update_resort_property_type";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@OldResortPropertyTypeId", old.ResortPropertyTypeId);

            cmd.Parameters.AddWithValue("@NewResortPropertyTypeId", newResortPropertyType.ResortPropertyTypeId);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    // .. resort property type wasn't found or old and database copy did not match
                    throw new ApplicationException("Database: Resort property type not updated");
                }
                else if (result > 1)
                {
                    // .. protection against change in expected stored stored procedure behaviour
                    throw new ApplicationException("Fatal Error: More than one resort property type updated");
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
        /// Deletes Resort Property Type from database
        /// </summary>
        /// <param name="id">Resort Property Type Id</param>
        public void DeleteResortPropertyType(string id)
        {
            var conn = DBConnection.GetDbConnection();

            const string cmdText = @"sp_delete_resort_property_type_by_id";

            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@ResortPropertyTypeId", id);

            try
            {
                conn.Open();

                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                    throw new ApplicationException("Error: Failed to delete resort property type");
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