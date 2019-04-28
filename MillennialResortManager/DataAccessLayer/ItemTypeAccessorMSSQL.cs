using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataObjects;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
	/// Kevin Broskow
	/// Created: 2019/01/20
	/// 
	/// ItemType Acccessor that utilizes SQL
	/// </summary>
    public class ItemTypeAccessorMSSQL : IItemTypeAccessor
    {
        public void CreateItemType(ItemType newItemType)
        {
            throw new NotImplementedException();
        }

        public void DeactivateItemType(ItemType deactivatingItemType)
        {
            throw new NotImplementedException();
        }

        public void PurgeItemType(ItemType purgingItemType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 2019/01/23
        /// 
        /// Method used to access the database and retrieve all item types.
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <returns>List<string> That contains all of the ItemTypeIDs</returns>	
        public List<string> RetrieveAllItemTypesString()
        {
            List<string> itemTypes = new List<string>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_itemtypes";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        itemTypes.Add(reader.GetString(0));
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
            return itemTypes;
        }

        public ItemType RetrieveItemType()
        {
            throw new NotImplementedException();
        }

        public void UpdateItemType(ItemType newItemType, ItemType oldItemType)
        {
            throw new NotImplementedException();
        }

        public List<ItemType> RetrieveAllItemTypes()
        {
            List<ItemType> itemTypes = new List<ItemType>();

            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_all_item_types";
            string itemTypeID = "";
            string description = "";
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

                        itemTypeID = reader.GetString(0);
                        if (reader.IsDBNull(1))
                        {
                            description = "";
                        }
                        else
                        {
                            description = reader.GetString(1);
                        }

                        ItemType itemType = new ItemType(itemTypeID, description);
                        itemTypes.Add(itemType);

                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return itemTypes;
        }
    }
}
