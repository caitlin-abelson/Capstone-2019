using DataObjects;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Richard Carroll
/// Created: 2019/01/30
/// 
/// This class is used for Database Interactions for 
/// Item Data
/// </summary>

namespace DataAccessLayer
{
    public class ItemAccessor : IItemAccessor
    {
        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// This Method Requests Item data from the database 
        /// and returns it to the Logic Layer if Possible.
        /// </summary>
        public List<Item> SelectItemNamesAndIDs()
        {
            List<Item> items = new List<Item>();

            var cmdText = "sp_select_all_item_names_and_ids";
            var conn = DBConnection.GetDbConnection();
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
                        Item item = new Item();
                        item.Name = reader.GetString(0);
                        item.ItemID = reader.GetInt32(1);
                        items.Add(item);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return items;
        }
        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        /// 
        /// Retrieves all items from the database.
        /// </summary>
        /// <returns>A List of all Items</returns>
        public List<Item> SelectAllItems()
        {
            List<Item> items = new List<Item>();

            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_all_items";
            int itemID = 0;
            string itemTypeID = "";
            string description = "";
            int onHandQty = 0;
            string name = "";
            int reorderQty = 0;
            int? offeringID = 0;
            bool customerPurchaseable = false;
            int? recipeID = 0;
            DateTime dateActive = DateTime.Now;
            bool active;
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                var reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        itemID = reader.GetInt32(0);
                        itemTypeID = reader.GetString(1);
                        if (reader.IsDBNull(2))
                        {
                            description = "";
                        }
                        else
                        {
                            description = reader.GetString(2);
                        }
                        onHandQty = reader.GetInt32(3);
                        name = reader.GetString(4);
                        reorderQty = reader.GetInt32(5);
                        dateActive = reader.GetDateTime(6);
                        active = reader.GetBoolean(7);
                        if (reader.IsDBNull(8))
                        {
                            offeringID = null;
                        }
                        else
                        {
                            offeringID = reader.GetInt32(8);
                        }
                        customerPurchaseable = reader.GetBoolean(9);
                        if (reader.IsDBNull(10))
                        {
                            recipeID = null;
                        }
                        else
                        {
                            recipeID = reader.GetInt32(10);
                        }
                        Item item = new Item(itemID, offeringID, customerPurchaseable, recipeID, itemTypeID, description, onHandQty, name, reorderQty, dateActive, active);
                        items.Add(item);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return items;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/06
        ///
        /// Adds an Item to the Database.
        /// </summary>
        /// <param name="recipe">A recipe object.</param>
        /// <param name="recipeID">The List of RecipeLines to be added under the given recipe.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>List of Items belonging to a Recipe.</returns>
        public int InsertItem(Item item)
        {
            int returnedID = 0;
            string cmdText = @"sp_insert_item";
            var conn = DBConnection.GetDbConnection();
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                if (item.OfferingID == null)
                {
                    cmd1.Parameters.AddWithValue("@OfferingID", DBNull.Value);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@OfferingID", item.OfferingID);
                }
                cmd1.Parameters.AddWithValue("@ItemTypeID", item.ItemType);
                if (item.RecipeID == null)
                {
                    cmd1.Parameters.AddWithValue("@RecipeID", DBNull.Value);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@RecipeID", item.RecipeID);
                }
                cmd1.Parameters.AddWithValue("@CustomerPurchasable", item.CustomerPurchasable);
                cmd1.Parameters.AddWithValue("@Description", item.Description);
                cmd1.Parameters.AddWithValue("@OnHandQty", item.OnHandQty);
                cmd1.Parameters.AddWithValue("@Name", item.Name);
                cmd1.Parameters.AddWithValue("@ReorderQty", item.ReorderQty);
                var temp = cmd1.ExecuteScalar();
                returnedID = Convert.ToInt32(temp);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return returnedID;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/25
        ///
        /// Retrieves a List of Items belonging to a Recipe.
        /// </summary>
        /// <param name="recipeID">The ID of the recipe that the items are to be retrieved from.</param>
        /// <returns>List of Items belonging to a Recipe.</returns>
        public List<Item> SelectLineItemsByRecipeID(int recipeID)
        {
            List<Item> items = new List<Item>();

            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_line_items_by_recipeid";
            int itemID = 0;
            string itemTypeID = "";
            string description = "";
            int onHandQty = 0;
            string name = "";
            int reorderQty = 0;
            int? offeringID = 0;
            bool customerPurchaseable = false;
            int? recipeId = recipeID;
            DateTime dateActive = DateTime.Now;
            bool active;
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RecipeID", recipeID);
                var reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        itemID = reader.GetInt32(0);
                        itemTypeID = reader.GetString(1);
                        if (reader.IsDBNull(2))
                        {
                            description = "";
                        }
                        else
                        {
                            description = reader.GetString(2);
                        }
                        onHandQty = reader.GetInt32(3);
                        name = reader.GetString(4);
                        reorderQty = reader.GetInt32(5);
                        dateActive = reader.GetDateTime(6);
                        active = reader.GetBoolean(7);
                        if (reader.IsDBNull(8))
                        {
                            offeringID = null;
                        }
                        else
                        {
                            offeringID = reader.GetInt32(8);
                        }
                        customerPurchaseable = reader.GetBoolean(9);
                        Item item = new Item(itemID, offeringID, customerPurchaseable, recipeID, itemTypeID, description, onHandQty, name, reorderQty, dateActive, active);
                        items.Add(item);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return items;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/25
        ///
        /// Retrieves a List of Items belonging to a Recipe.
        /// </summary>
        /// <param name="recipeID">The ID of the recipe that the items are to be retrieved from.</param>
        /// <returns>List of Items belonging to a Recipe.</returns>
        public Item SelectItemByRecipeID(int recipeID)
        {
            Item item = null;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_item_by_recipeid";
            int itemID = 0;
            string itemTypeID = "";
            string description = "";
            int onHandQty = 0;
            string name = "";
            int reorderQty = 0;
            int? offeringID = 0;
            bool customerPurchaseable = false;
            int? recipeId = recipeID;
            DateTime dateActive = DateTime.Now;
            bool active;
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@RecipeID", recipeID);
                var reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        itemID = reader.GetInt32(0);
                        itemTypeID = reader.GetString(1);
                        if (reader.IsDBNull(2))
                        {
                            description = "";
                        }
                        else
                        {
                            description = reader.GetString(2);
                        }
                        onHandQty = reader.GetInt32(3);
                        name = reader.GetString(4);
                        reorderQty = reader.GetInt32(5);
                        dateActive = reader.GetDateTime(6);
                        active = reader.GetBoolean(7);
                        if (reader.IsDBNull(8))
                        {
                            offeringID = null;
                        }
                        else
                        {
                            offeringID = reader.GetInt32(8);
                        }
                        customerPurchaseable = reader.GetBoolean(9);
                        item = new Item(itemID, offeringID, customerPurchaseable, recipeID, itemTypeID, description, onHandQty, name, reorderQty, dateActive, active);

                    }
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return item;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/09
        ///
        /// Updates an Item with a new Item.
        /// </summary>
        /// 
        /// <param name="oldItem">The old Item.</param>
        /// <param name="newItem">The updated Item.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows affected</returns>
        public int UpdateItem(Item oldItem, Item newItem)
        {
            int result = 0;
            string cmdText = @"sp_update_item";
            var conn = DBConnection.GetDbConnection();
            SqlCommand cmd1 = new SqlCommand(cmdText, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@ItemID", oldItem.ItemID);

            if (oldItem.OfferingID == null)
            {
                cmd1.Parameters.AddWithValue("@OldOfferingID", DBNull.Value);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@OldOfferingID", oldItem.OfferingID);
            }
            cmd1.Parameters.AddWithValue("@OldItemTypeID", oldItem.ItemType);
            if (oldItem.RecipeID == null)
            {
                cmd1.Parameters.AddWithValue("@OldRecipeID", DBNull.Value);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@OldRecipeID", oldItem.RecipeID);
            }
            cmd1.Parameters.AddWithValue("@OldCustomerPurchasable", oldItem.CustomerPurchasable);
            if (oldItem.Description == null)
            {
                cmd1.Parameters.AddWithValue("@OldDescription", DBNull.Value);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@OldDescription", oldItem.Description);
            }
            cmd1.Parameters.AddWithValue("@OldOnHandQty", oldItem.OnHandQty);
            cmd1.Parameters.AddWithValue("@OldName", oldItem.Name);
            cmd1.Parameters.AddWithValue("@OldReorderQty", oldItem.ReorderQty);
            cmd1.Parameters.AddWithValue("@OldActive", oldItem.Active);


            if (newItem.OfferingID == null)
            {
                cmd1.Parameters.AddWithValue("@NewOfferingID", DBNull.Value);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@NewOfferingID", newItem.OfferingID);
            }
            cmd1.Parameters.AddWithValue("@NewItemTypeID", newItem.ItemType);
            if (newItem.RecipeID == null)
            {
                cmd1.Parameters.AddWithValue("@NewRecipeID", DBNull.Value);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@NewRecipeID", newItem.RecipeID);
            }
            cmd1.Parameters.AddWithValue("@NewCustomerPurchasable", newItem.CustomerPurchasable);
            if (newItem.Description == null)
            {
                cmd1.Parameters.AddWithValue("@NewDescription", DBNull.Value);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@NewDescription", newItem.Description);
            }
            cmd1.Parameters.AddWithValue("@NewOnHandQty", newItem.OnHandQty);
            cmd1.Parameters.AddWithValue("@NewName", newItem.Name);
            cmd1.Parameters.AddWithValue("@NewReorderQty", newItem.ReorderQty);
            cmd1.Parameters.AddWithValue("@NewActive", newItem.Active);
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

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/30
        /// 
        /// Used to retrieve a specific Item from inventory
        /// </summary>
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Converted to Item from Product
        /// </remarks>
        /// <param name="itemID">ID of an item</param>
        ///// <returns>Product</returns>
        public Item SelectItem(int itemID)
        {
            Item item = new Item();
            var cmdText = @"sp_select_item_by_itemid";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemID", itemID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.HasRows)
                {
                    item.ItemID = reader.GetInt32(0);
                    item.ItemType = reader.GetString(1);
                    if (reader.IsDBNull(2))
                    {
                        item.Description = null;
                    }
                    else
                    {
                        item.Description = reader.GetString(2);
                    }
                    item.OnHandQty = reader.GetInt32(3);
                    item.Name = reader.GetString(4);
                    item.ReorderQty = reader.GetInt32(5);
                    item.DateActive = reader.GetDateTime(6);
                    item.Active = reader.GetBoolean(7);
                    item.CustomerPurchasable = reader.GetBoolean(8);
                    if (reader.IsDBNull(9))
                    {
                        item.RecipeID = null;
                    }
                    else
                    {
                        item.RecipeID = reader.GetInt32(9);
                    }
                    if (reader.IsDBNull(10))
                    {
                        item.OfferingID = null;
                    }
                    else
                    {
                        item.OfferingID = reader.GetInt32(10);
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
            return item;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Deactivating a item in the database
        /// </summary>
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Converted to Item from Product
        /// </remarks>
        /// <param name="deactivatingItem">The item to be deactivated</param>
        /// <returns>void</returns>
        public void DeactivateItem(Item deactivatingItem)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_deactivate_item";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ItemID", deactivatingItem.ItemID);
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
        /// Purging a item from the database
        /// </summary>
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Converted to Item from Product
        /// </remarks>
        /// <param name="purgingItem">The item to be purged</param>
        /// <returns>void</returns>
        public void DeleteItem(Item purgingItem)
        {
            {
                var conn = DBConnection.GetDbConnection();
                var cmdText = @"sp_delete_item";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemID", purgingItem.ItemID);
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
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Retrieves all active items in the database
        /// </summary>
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Converted to Item from Product
        /// </remarks>
        /// <returns>List<Item></returns>
        public List<Item> SelectActiveItems()
        {

            {
                List<Item> activeItems = new List<Item>();
                var conn = DBConnection.GetDbConnection();
                var cmdText = @"sp_select_all_active_items";

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
                            Item item = new Item();
                            item.ItemID = reader.GetInt32(0);
                            item.ItemType = reader.GetString(1);
                            if (reader.IsDBNull(2))
                            {
                                item.Description = null;
                            }
                            else
                            {
                                item.Description = reader.GetString(2);
                            }
                            item.OnHandQty = reader.GetInt32(3);
                            item.Name = reader.GetString(4);
                            item.ReorderQty = reader.GetInt32(5);
                            item.DateActive = reader.GetDateTime(6);
                            item.Active = reader.GetBoolean(7);
                            item.CustomerPurchasable = reader.GetBoolean(8);
                            if (reader.IsDBNull(9))
                            {
                                item.RecipeID = null;
                            }
                            else
                            {
                                item.RecipeID = reader.GetInt32(9);
                            }
                            if (reader.IsDBNull(10))
                            {
                                item.OfferingID = null;
                            }
                            else
                            {
                                item.OfferingID = reader.GetInt32(10);
                            }
                            activeItems.Add(item);
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

                return activeItems;
            }
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/02/5
        /// 
        /// Retrieves all deactive items in the database
        /// </summary>
        ///
        /// <remarks>
        /// Jared Greenfield
        /// Updated: 2019/04/03
        /// Converted to Item from Product
        /// </remarks>
        /// <returns>List<Item></returns>
        public List<Item> SelectDeactiveItems()
        {

            {
                List<Item> deactiveItems = new List<Item>();
                var conn = DBConnection.GetDbConnection();
                var cmdText = @"sp_select_all_deactivated_items";

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
                            Item item = new Item();
                            item.ItemID = reader.GetInt32(0);
                            item.ItemType = reader.GetString(1);
                            if (reader.IsDBNull(2))
                            {
                                item.Description = null;
                            }
                            else
                            {
                                item.Description = reader.GetString(2);
                            }
                            item.OnHandQty = reader.GetInt32(3);
                            item.Name = reader.GetString(4);
                            item.ReorderQty = reader.GetInt32(5);
                            item.DateActive = reader.GetDateTime(6);
                            item.Active = reader.GetBoolean(7);
                            item.CustomerPurchasable = reader.GetBoolean(8);
                            if (reader.IsDBNull(9))
                            {
                                item.RecipeID = null;
                            }
                            else
                            {
                                item.RecipeID = reader.GetInt32(9);
                            }
                            if (reader.IsDBNull(10))
                            {
                                item.OfferingID = null;
                            }
                            else
                            {
                                item.OfferingID = reader.GetInt32(10);
                            }
                            deactiveItems.Add(item);
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

                return deactiveItems;
            }
        }
    }
}

