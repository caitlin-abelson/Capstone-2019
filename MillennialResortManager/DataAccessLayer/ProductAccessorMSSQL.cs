using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// Product Acccessor that utilizes SQL
	/// </summary>
    public class ProductAccessorMSSQL : iProductAccessor
    {
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/23
        /// 
        /// Used to insert a newly created product into the database.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="newProduct">A newly created data object of type Product to be inserted into the database</param>
        /// <returns>Int that contains the newly created productID</returns>	
        public int InsertProduct(Product newProduct)
        {
            int productID;
            var cmdText = @"sp_insert_product";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemTypeID", newProduct.ItemType);
            cmd.Parameters.AddWithValue("@Description", newProduct.Description);
            cmd.Parameters.AddWithValue("@OnHandQuantity", newProduct.OnHandQty);
            cmd.Parameters.AddWithValue("@Name", newProduct.Name);
            cmd.Parameters.AddWithValue("@ReOrderQuantity", newProduct.ReorderQty);
            cmd.Parameters.AddWithValue("@DateActive", newProduct.DateActive);
            cmd.Parameters.AddWithValue("@CustomerPurchasable", newProduct.CustomerPurchasable);
            cmd.Parameters.AddWithValue("@RecipeID", newProduct.RecipeID);

            try
            {
                conn.Open();
                productID = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            } finally
            {
                conn.Close();
            }
            return productID;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Deactivating a product in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="deactivatingProduct">The product to be deactivated</param>
        /// <returns>void</returns>	
        public void DeactivateProduct(Product deactivatingProduct)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_product";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemID", deactivatingProduct.ProductID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Purging a product from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="purgingProduct">The product to be purged</param>
        /// <returns>void</returns>	
        public void DeleteProduct(Product purgingProduct)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_purge_product";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemID", purgingProduct.ProductID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Retrieves all products in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<Product></returns>	
        public List<Product> RetrieveAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_all_items";

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        allProducts.Add(new Product()
                        {
                            ProductID = reader.GetInt32(0),
                            ItemType = reader.GetString(1),
                            Description = reader.GetString(2),
                            OnHandQty = reader.GetInt32(3),
                            Name = reader.GetString(4),
                            ReorderQty = reader.GetInt32(5),
                            DateActive = reader.GetDateTime(6),
                            Active = reader.GetBoolean(7),
                            CustomerPurchasable = reader.GetBoolean(8),
                            RecipeID = reader.GetInt32(9)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return allProducts;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Retrieves all deactive products in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<Product></returns>	
        public List<Product> RetrieveDeactiveProducts()
        {
            List<Product> deactiveProducts = new List<Product>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_all_deactivated_items";

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deactiveProducts.Add(new Product()
                        {
                            ProductID = reader.GetInt32(0),
                            ItemType = reader.GetString(1),
                            Description = reader.GetString(2),
                            OnHandQty = reader.GetInt32(3),
                            Name = reader.GetString(4),
                            ReorderQty = reader.GetInt32(5),
                            DateActive = reader.GetDateTime(6),
                            Active = reader.GetBoolean(7),
                            CustomerPurchasable = reader.GetBoolean(8),
                            RecipeID = reader.GetInt32(9)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return deactiveProducts;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Retrieves all active products in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<Product></returns>
        public List<Product> RetrieveActiveProducts()
        {
            List<Product> activeProducts = new List<Product>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_all_active_items";

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        activeProducts.Add(new Product()
                        {
                            ProductID = reader.GetInt32(0),
                            ItemType = reader.GetString(1),
                            Description = reader.GetString(2),
                            OnHandQty = reader.GetInt32(3),
                            Name = reader.GetString(4),
                            ReorderQty = reader.GetInt32(5),
                            DateActive = reader.GetDateTime(6),
                            Active = reader.GetBoolean(7),
                            CustomerPurchasable = reader.GetBoolean(8),
                            RecipeID = reader.GetInt32(9)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return activeProducts;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to retrieve a specific Product from inventory
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="ProductID">A newly created data object of type Product to be inserted into the database</param>
        /// <returns>Product</returns>	
        public Product RetrieveProduct(int productID)
        {
            Product product = new Product();
            var cmdText = @"sp_retrieve_product";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", productID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    product.ItemType = reader.GetString(0);
                    product.Description = reader.GetString(1);
                    product.OnHandQty = reader.GetInt32(2);
                    product.Name = reader.GetString(3);
                    product.ReorderQty = reader.GetInt32(4);
                    product.DateActive = reader.GetDateTime(5);
                    product.Active = reader.GetBoolean(6);
                    product.CustomerPurchasable = reader.GetBoolean(8);
                    product.RecipeID = reader.GetInt32(9);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return product;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to update a specific product in the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="newProduct">A newly created data object of type Product to be inserted into the database</param>
        /// <returns>Void</returns>	
        public void UpdateProduct(Product newProduct, Product oldProduct)
        {
            var cmdText = @"sp_update_product";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemID", oldProduct.ProductID);
            cmd.Parameters.AddWithValue("@oldItemTypeID", oldProduct.ItemType);
            cmd.Parameters.AddWithValue("@oldDescription", oldProduct.Description);
            cmd.Parameters.AddWithValue("@oldOnHandQuantity", oldProduct.OnHandQty);
            cmd.Parameters.AddWithValue("@oldName", oldProduct.Name);
            cmd.Parameters.AddWithValue("@oldReOrderQuantity", oldProduct.ReorderQty);
            cmd.Parameters.AddWithValue("@oldDateActive", oldProduct.DateActive);
            cmd.Parameters.AddWithValue("@oldActive", oldProduct.Active);
            cmd.Parameters.AddWithValue("@oldCustomerPurchasable", oldProduct.CustomerPurchasable);
            cmd.Parameters.AddWithValue("@oldRecipeID", oldProduct.RecipeID);
            cmd.Parameters.AddWithValue("@newItemTypeID", newProduct.ItemType);
            cmd.Parameters.AddWithValue("@newDescription", newProduct.Description);
            cmd.Parameters.AddWithValue("@newOnHandQuantity", newProduct.OnHandQty);
            cmd.Parameters.AddWithValue("@newName", newProduct.Name);
            cmd.Parameters.AddWithValue("@newReOrderQuantity", newProduct.ReorderQty);
            cmd.Parameters.AddWithValue("@newDateActive", newProduct.DateActive);
            cmd.Parameters.AddWithValue("@newActive", newProduct.Active);
            cmd.Parameters.AddWithValue("@newCustomerPurchasable", newProduct.CustomerPurchasable);
            cmd.Parameters.AddWithValue("@newRecipeID", newProduct.RecipeID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
