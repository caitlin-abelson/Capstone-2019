/// <summary>
/// James Heim
/// Created: 2019/01/24
/// 
/// Manipulate the Supplier table in the database.
/// </summary>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class SupplierAccessor : ISupplierAccessor
    {


        public int ActivateSupplier(Supplier supplier)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            String cmdText = @"sp_activate_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
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



        /// <summary>
        /// James Heim
        /// Created: 2019/01/24
        /// 
        /// Insert a Supplier into the database.
        /// </summary>
        /// <param name="supplierName">Human friendly name of the Supplier.</param>
        /// <param name="address">Address of the Supplier.</param>
        /// <param name="city">City the Supplier is based in.</param>
        /// <param name="state">State the Supplier is based in.</param>
        /// <param name="zip">Zip code of the Supplier.</param>
        /// <param name="country">Country the Supplier is based in.</param>
        /// <param name="contactFirstName">The first name of our contact for the Supplier.</param>
        /// <param name="contactLastName">The last name of our contact for the Supplier.</param>
        /// <param name="phoneNumber">The phone number of our contact for the Supplier.</param>
        /// <param name="email">The email of our contact for the Supplier.</param>
        /// <returns>Returns rows affected. Returns -1 if something went wrong but an exception is not thrown.</returns>
        public void InsertSupplier(Supplier newSupplier)
        {

            var conn = DBConnection.GetDbConnection();
            var procedure = @"sp_insert_supplier";
            var cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Name", newSupplier.Name);
            cmd.Parameters.AddWithValue("@Address", newSupplier.Address);
            cmd.Parameters.AddWithValue("@City", newSupplier.City);
            cmd.Parameters.AddWithValue("@State", newSupplier.State);
            cmd.Parameters.AddWithValue("@PostalCode", newSupplier.PostalCode);
            cmd.Parameters.AddWithValue("@Country", newSupplier.Country);
            cmd.Parameters.AddWithValue("@PhoneNumber", newSupplier.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newSupplier.Email);
            cmd.Parameters.AddWithValue("@ContactFirstName", newSupplier.ContactFirstName);
            cmd.Parameters.AddWithValue("@ContactLastName", newSupplier.ContactLastName);
            cmd.Parameters.AddWithValue("@Description", newSupplier.Description);

            try
            {
                conn.Open();

                Convert.ToInt32(cmd.ExecuteScalar());


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return;
        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/15
        /// 
        /// Select a Supplier by their ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Supplier SelectSupplier(int id)
        {
            Supplier supplier = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_supplier_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierID", id);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    supplier = new Supplier()
                    {
                        SupplierID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        ContactFirstName = reader.GetString(2),
                        ContactLastName = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Email = reader.GetString(5),
                        DateAdded = reader.GetDateTime(6),
                        Address = reader.GetString(7),
                        City = reader.GetString(8),
                        State = reader.GetString(9),
                        Country = reader.GetString(10),
                        PostalCode = reader.GetString(11),
                        Description = reader.GetString(12),
                        Active = reader.GetBoolean(13),

                    };
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

            return supplier;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 1/22/19
        /// 
        /// BrowseSupplier reads in the data objects from the stored procedure sp_retrieve_suppliers
        /// so that they can be listed onto a data grid.
        /// </summary>
        /// 
        /// <remarks>
        /// James Heim
        /// Updated: 2019/01/30
        /// Updated the built Supplier object to match the new database field names.
        /// </remarks>
        /// 
        /// <returns></returns>
        public List<Supplier> SelectAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_suppliers";
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

                        Supplier supplier = new Supplier();
                        supplier.SupplierID = reader.GetInt32(0);
                        supplier.Name = reader.GetString(1);
                        supplier.ContactFirstName = reader.GetString(2);
                        supplier.ContactLastName = reader.GetString(3);
                        supplier.PhoneNumber = reader.GetString(4);
                        supplier.Email = reader.GetString(5);
                        supplier.DateAdded = reader.GetDateTime(6);
                        supplier.Address = reader.GetString(7);
                        supplier.City = reader.GetString(8);
                        supplier.State = reader.GetString(9);
                        supplier.Country = reader.GetString(10);
                        supplier.PostalCode = reader.GetString(11);
                        supplier.Description = reader.GetString(12);
                        supplier.Active = reader.GetBoolean(13);

                        suppliers.Add(supplier);
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

            return suppliers;


        }

        /// <summary>
        /// Author James Heim
        /// Created 2019/01/30
        /// 
        /// Update the Supplier with new information. Pass in the old as well as the new
        /// to prevent concurrency errors.
        /// </summary>
        /// <param name="newSupplier"></param>
        /// <param name="oldSupplier"></param>
        public void UpdateSupplier(Supplier newSupplier, Supplier oldSupplier)
        {
            var conn = DBConnection.GetDbConnection();
            var procedure = @"sp_update_supplier";
            var cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierID", oldSupplier.SupplierID);

            cmd.Parameters.AddWithValue("@OldName", oldSupplier.Name);
            cmd.Parameters.AddWithValue("@OldAddress", oldSupplier.Address);
            cmd.Parameters.AddWithValue("@OldCity", oldSupplier.City);
            cmd.Parameters.AddWithValue("@OldState", oldSupplier.State);
            cmd.Parameters.AddWithValue("@OldPostalCode", oldSupplier.PostalCode);
            cmd.Parameters.AddWithValue("@OldCountry", oldSupplier.Country);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldSupplier.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldSupplier.Email);
            cmd.Parameters.AddWithValue("@OldContactFirstName", oldSupplier.ContactFirstName);
            cmd.Parameters.AddWithValue("@OldContactLastName", oldSupplier.ContactLastName);
            cmd.Parameters.AddWithValue("@OldDescription", oldSupplier.Description);
            cmd.Parameters.AddWithValue("@OldActive", oldSupplier.Active);

            cmd.Parameters.AddWithValue("@NewName", newSupplier.Name);
            cmd.Parameters.AddWithValue("@NewAddress", newSupplier.Address);
            cmd.Parameters.AddWithValue("@NewCity", newSupplier.City);
            cmd.Parameters.AddWithValue("@NewState", newSupplier.State);
            cmd.Parameters.AddWithValue("@NewPostalCode", newSupplier.PostalCode);
            cmd.Parameters.AddWithValue("@NewCountry", newSupplier.Country);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newSupplier.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewEmail", newSupplier.Email);
            cmd.Parameters.AddWithValue("@NewContactFirstName", newSupplier.ContactFirstName);
            cmd.Parameters.AddWithValue("@NewContactLastName", newSupplier.ContactLastName);
            cmd.Parameters.AddWithValue("@NewDescription", newSupplier.Description);
            cmd.Parameters.AddWithValue("@NewActive", newSupplier.Active);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// James Heim
        /// Created 2019/02/15
        /// 
        /// Delete the specified Supplier if the record is already deactivated.
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>Rows affected</returns>
        public int DeleteSupplier(Supplier supplier)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);

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
        /// James Heim
        /// Created 2019/02/15
        /// 
        /// Deactivate the specified Supplier.
        /// Throw an exception if the supplier is already inactive.
        /// </summary>
        /// <param name="supplier"></param>
        public int DeactivateSupplier(Supplier supplier)
        {
            int rows = 0;

            if (supplier.Active == false)
            {
                throw new Exception("Supplier is already deactivated.");
            }

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);

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
