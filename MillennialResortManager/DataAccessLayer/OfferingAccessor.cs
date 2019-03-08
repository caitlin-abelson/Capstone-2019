/// <summary>
/// Jared Greenfield
/// Created: 2019/01/22
/// 
/// The concrete implementation of IOfferingAccessor. Handles storage and collection of
/// Offering objects to and from the database.
/// </summary>
///
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
    public class OfferingAccessor : IOfferingAccessor
    {
        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/29
        ///
        /// Adds an Offering record to the database.
        /// </summary>
        /// <param name="offering">An Offering object to be added to the database.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows affected.</returns>
        public int InsertOffering(Offering offering)
        {
           
            int returnedID = 0;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_insert_offering";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@OfferingTypeID", offering.OfferingTypeID);
                cmd1.Parameters.AddWithValue("@EmployeeID", offering.EmployeeID);
                cmd1.Parameters.AddWithValue("@Description", offering.Description);
                cmd1.Parameters.AddWithValue("@Price", offering.Price);
                try
                {
                    conn.Open();
                    var temp = cmd1.ExecuteScalar();
                    returnedID = Convert.ToInt32(temp);
                }
                catch (Exception)
                {

                    throw;
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
            return returnedID;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/29
        ///
        /// Retrieves an Offering based on an ID of an Offering.
        /// </summary>
        /// <param name="offeringID">The ID of the Offering.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Offering Object</returns>
        public Offering SelectOfferingByID(int offeringID)
        {
            Offering offering = null; 
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_offering";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@OfferingID", offeringID);
                try
                {
                    conn.Open();
                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string offeringTypeID = reader.GetString(1);
                            int employeeID = reader.GetInt32(2);
                            string description = reader.GetString(3);
                            decimal price = reader.GetDecimal(4);
                            bool active = reader.GetBoolean(5);
                            offering = new Offering(offeringID, offeringTypeID, employeeID, description, price, active);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
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
            return offering;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/09
        ///
        /// Updates an Offering with a new Offering.
        /// </summary>
        /// 
        /// <param name="oldOffering">The old Offering.</param>
        /// <param name="newOffering">The updated Offering.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows affected.</returns>
        public int UpdateOffering(Offering oldOffering, Offering newOffering)
        {
            int result = 0;
            string cmdText = @"sp_update_offering";
            var conn = DBConnection.GetDbConnection();
            SqlCommand cmd1 = new SqlCommand(cmdText, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@OfferingID", oldOffering.OfferingID);

            cmd1.Parameters.AddWithValue("@OldOfferingTypeID", oldOffering.OfferingTypeID);
            cmd1.Parameters.AddWithValue("@OldEmployeeID", oldOffering.EmployeeID);
            cmd1.Parameters.AddWithValue("@OldDescription", oldOffering.Description);
            cmd1.Parameters.AddWithValue("@OldPrice", oldOffering.Price);
            cmd1.Parameters.AddWithValue("@OldActive", oldOffering.Active);

            cmd1.Parameters.AddWithValue("@NewOfferingTypeID", newOffering.OfferingTypeID);
            cmd1.Parameters.AddWithValue("@NewEmployeeID", newOffering.EmployeeID);
            cmd1.Parameters.AddWithValue("@NewDescription", newOffering.Description);
            cmd1.Parameters.AddWithValue("@NewPrice", newOffering.Price);
            cmd1.Parameters.AddWithValue("@NewActive", newOffering.Active);
            try
            {     
                conn.Open();
                var temp = cmd1.ExecuteNonQuery();
                result = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
