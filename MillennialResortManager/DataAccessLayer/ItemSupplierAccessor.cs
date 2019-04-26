/// <summary>
/// Eric Bostwick
/// Created: 2/4/2019
/// 
/// Database methods for managing itemsupplier table
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ItemSupplierAccessor : IItemSupplierAccessor
    {

        /// <summary>
        /// Eric Bostwick
        /// Created 2/4/2019
        /// Inserts records to the itemsupplier table
        /// </summary>
        /// <returns>
        /// 1 if successful 0 if not
        /// </returns>
        /// 
        public int InsertItemSupplier(ItemSupplier itemSupplier)
        {

            int result;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_create_itemsupplier"; 
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemSupplier.ItemID);
            cmd.Parameters.AddWithValue("@SupplierID", itemSupplier.SupplierID);
            cmd.Parameters.AddWithValue("@PrimarySupplier", itemSupplier.PrimarySupplier);
            cmd.Parameters.AddWithValue("@LeadTimeDays", itemSupplier.LeadTimeDays);
            cmd.Parameters.AddWithValue("@UnitPrice", itemSupplier.UnitPrice);
            cmd.Parameters.AddWithValue("@SupplierItemID", itemSupplier.ItemSupplierID);

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

        public List<ItemSupplier> SelectItemSuppliersByItemID(int itemID)
        {
            /// <summary>
            /// Eric Bostwick
            /// Created 2/4/2019
            /// Gets list of itemsuppliers from itemsupplier table
            /// using the itemID
            /// </summary>
            /// <returns>
            /// List of ItemSupplier Objects
            /// </returns>            
            {
                List<ItemSupplier> itemSuppliers = new List<ItemSupplier>();
                var conn = DBConnection.GetDbConnection();
                var cmdText = @"sp_select_itemsuppliers_by_itemid";  //sp_retrieve_itemsuppliers_by_itemid
                var cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemID", itemID);

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ItemSupplier itemSupplier = new ItemSupplier();

                            itemSupplier.ItemID = reader.GetInt32(reader.GetOrdinal("ItemID"));
                            itemSupplier.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));
                            itemSupplier.PrimarySupplier = reader.GetBoolean(reader.GetOrdinal("PrimarySupplier"));
                            itemSupplier.LeadTimeDays = reader.GetInt32(reader.GetOrdinal("LeadTimeDays"));
                            itemSupplier.UnitPrice = (decimal)reader.GetSqlMoney(reader.GetOrdinal("UnitPrice"));                            
                            itemSupplier.Name = reader["Name"].ToString();
                            itemSupplier.Description = reader["Description"].ToString();  
                            itemSupplier.ItemSupplierActive = reader.GetBoolean(reader.GetOrdinal("ItemSupplierActive"));
                            itemSupplier.ItemSupplierID = reader.GetInt32(reader.GetOrdinal("SupplierItemID"));
                            itemSuppliers.Add(itemSupplier);

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

                return itemSuppliers;
            }
        }

        

        /// <summary>
        /// Eric Bostwick
        /// Created 2/4/2019
        /// Gets an ItemSupplier from itemsupplier table
        /// using the itemID and supplierid
        /// </summary>
        /// <returns>
        /// ItemSupplier Object
        /// </returns>
        public ItemSupplier SelectItemSupplierByItemIDandSupplierID(int itemID, int supplierID)
        {
            ItemSupplier itemSupplier = new ItemSupplier();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_itemsupplier_by_itemid_and_supplierid";  //sp_retrieve_itemsupplier_by_itemid_and_supplyid
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemID);
            cmd.Parameters.AddWithValue("@SupplierID", supplierID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        itemSupplier.ItemID = reader.GetInt32(reader.GetOrdinal("ItemID"));
                        itemSupplier.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));
                        itemSupplier.PrimarySupplier = reader.GetBoolean(reader.GetOrdinal("PrimarySupplier"));
                        itemSupplier.LeadTimeDays = reader.GetInt32(reader.GetOrdinal("LeadTimeDays"));
                        itemSupplier.UnitPrice = (decimal)reader.GetSqlMoney(reader.GetOrdinal("UnitPrice")); 
                        itemSupplier.Address = reader["Address"].ToString();
                        itemSupplier.City = reader["City"].ToString();
                        itemSupplier.ContactFirstName = reader["ContactFirstName"].ToString();
                        itemSupplier.ContactLastName = reader["ContactLastName"].ToString();
                        itemSupplier.Country = reader["Country"].ToString();                        
                        itemSupplier.DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"));
                        itemSupplier.Description = reader["Description"].ToString();
                        itemSupplier.Email = reader["Email"].ToString();
                        itemSupplier.Name = reader["Name"].ToString();
                        itemSupplier.Active = reader.GetBoolean(reader.GetOrdinal("SupplierActive"));
                        itemSupplier.PhoneNumber = reader["PhoneNumber"].ToString();
                        itemSupplier.PostalCode = reader["PostalCode"].ToString();
                        itemSupplier.State = reader["State"].ToString();       
                        itemSupplier.ItemSupplierActive = reader.GetBoolean(reader.GetOrdinal("ItemSupplierActive"));
                        itemSupplier.ItemSupplierID = reader.GetInt32(reader.GetOrdinal("SupplierItemID"));
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

            return itemSupplier;
        }

        /// <summary>
        /// Eric Bostwick
        /// Created 2/4/2019
        /// Updates the item supplier table
        /// </summary>
        /// <returns>
        /// 1 if successful 0 if not
        /// </returns>
        public int UpdateItemSupplier(ItemSupplier itemSupplier, ItemSupplier oldItemSupplier)
        {
            int result;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_itemsupplier";  
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemSupplier.ItemID);
            cmd.Parameters.AddWithValue("@SupplierID", itemSupplier.SupplierID);
            cmd.Parameters.AddWithValue("@PrimarySupplier", itemSupplier.PrimarySupplier);
            cmd.Parameters.AddWithValue("@LeadTimeDays", itemSupplier.LeadTimeDays);
            cmd.Parameters.AddWithValue("@UnitPrice", itemSupplier.UnitPrice);
            cmd.Parameters.AddWithValue("@Active", itemSupplier.ItemSupplierActive);
            cmd.Parameters.AddWithValue("@SupplierItemID", itemSupplier.ItemSupplierID);

            cmd.Parameters.AddWithValue("@OldItemID", oldItemSupplier.ItemID);
            cmd.Parameters.AddWithValue("@OldSupplierID", oldItemSupplier.SupplierID);
            cmd.Parameters.AddWithValue("@OldPrimarySupplier", oldItemSupplier.PrimarySupplier);
            cmd.Parameters.AddWithValue("@OldLeadTimeDays", oldItemSupplier.LeadTimeDays);
            cmd.Parameters.AddWithValue("@OldUnitPrice", oldItemSupplier.UnitPrice);
            cmd.Parameters.AddWithValue("@OldActive", oldItemSupplier.ItemSupplierActive);
            cmd.Parameters.AddWithValue("@OldSupplierItemID", itemSupplier.ItemSupplierID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        /// <summary>
        /// Author: Eric Bostwick
        /// Created Date: 2/7/19
        /// 
        /// Reads list of suppliers for use in itemsupplier management window
        /// for loading combo box.  Loads a list of suppliers that aren't 
        /// currently being used by that item
        /// </summary> 
        /// <remarks>
        /// </remarks>
        /// 
        /// <returns>
        /// List of suppliers that aren't assigned to that itemid
        /// </returns>
        public List<Supplier> SelectAllSuppliersForItemSupplierManagement(int itemID)
        {
            List<Supplier> suppliers = new List<Supplier>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_suppliers_for_itemsupplier_mgmt_by_itemid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Supplier supplier = new Supplier();                       
                        supplier.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));                        
                        supplier.Address = reader["Address"].ToString();
                        supplier.City = reader["City"].ToString();
                        supplier.ContactFirstName = reader["ContactFirstName"].ToString();
                        supplier.ContactLastName = reader["ContactLastName"].ToString();
                        supplier.Country = reader["Country"].ToString();
                        supplier.DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded"));
                        supplier.Description = reader["Description"].ToString();
                        supplier.Email = reader["Email"].ToString();
                        supplier.Name = reader["Name"].ToString();
                        supplier.Active = reader.GetBoolean(reader.GetOrdinal("Active"));
                        supplier.PhoneNumber = reader["PhoneNumber"].ToString();
                        supplier.PostalCode = reader["PostalCode"].ToString();
                        supplier.State = reader["State"].ToString();
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

        public int DeleteItemSupplier(int itemID, int supplierID)
        {
            int result;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_itemsupplier_by_itemid_and_supplierid";  
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemID);
            cmd.Parameters.AddWithValue("@SupplierID", supplierID);

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

        public int DeactivateItemSupplier(int itemID, int supplierID)
        {
            int result;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_itemsupplier_by_itemid_and_supplierid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemID);
            cmd.Parameters.AddWithValue("@SupplierID", supplierID);

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
    }
}
