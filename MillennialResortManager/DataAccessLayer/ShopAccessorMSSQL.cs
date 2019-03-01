using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
/// <summary>
/// Author: Kevin Broskow
/// Created : 2/27/2019
/// The ShopAccessorMSSQL is used to access shop data store in a microsoft SQL server.
/// </summary>
namespace DataAccessLayer
{
    public class ShopAccessorMSSQL : IShopAccessor
    {
        private List<Shop> _shops = new List<Shop>();
        public ShopAccessorMSSQL()
        {

        }
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 2/28/2019
        /// Creating a shop object to insert into the database for further use.
        /// </summary>
        /// <param name="shop">The data object of type shop to be added into the database</param>

        public int CreateShop(Shop shop)
        {
            int shopID=0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_shop";

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RoomID", shop.RoomID);
            cmd.Parameters.AddWithValue("@Name", shop.Name);
            cmd.Parameters.AddWithValue("@Description", shop.Description);

            try
            {
                conn.Open();
                shopID = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return shopID;

        }
    }
}
