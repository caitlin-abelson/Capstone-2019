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
    }
}
