using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;
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
            cmd.Parameters.Add("@ShopID", SqlDbType.Int );
            cmd.Parameters["@ShopID"].Direction = ParameterDirection.Output;
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

        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-07
        /// 
        /// Activate the shop that was passed in.
        /// </summary>
        /// <param name="shop"></param>
        /// <returns>Rows affected</returns>
        public int ActivateShop(Shop shop)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_activate_shop";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShopID", shop.ShopID);

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
        /// Author James Heim
        /// Created 2019-03-07
        /// 
        /// Deactivates the shop that was passed in.
        /// </summary>
        /// <param name="shop"></param>
        /// <returns>Rows affected</returns>
        public int DeactivateShop(Shop shop)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_deactivate_shop";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShopID", shop.ShopID);

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
        /// Delete the shop that was passed in.
        /// </summary>
        /// <param name="shop"></param>
        /// <returns>Rows affected</returns>
        public int DeleteShop(Shop shop)
        {
            int result = 0;

            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_delete_shop";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShopID", shop.ShopID);

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
        /// Author: James Heim
        /// Created: 2019-03-07
        /// 
        /// Retrieve the shop with the provided ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Shop SelectShopByID(int id)
        {
            Shop shop = null;


            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_shop_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShopID", id);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    shop = new Shop()
                    {
                        ShopID = id,
                        RoomID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Active = reader.GetBoolean(3)
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

            return shop;
        }


        /// <summary>
        /// James Heim
        /// Created 2019-02-28
        /// 
        /// Select all shops from the database.
        /// </summary>
        /// <returns>All Shops</returns>
        public IEnumerable<Shop> SelectShops()
        {
            List<Shop> shops = new List<Shop>();

            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_shops";
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
                        shops.Add(new Shop()
                        {
                            ShopID = reader.GetInt32(0),
                            RoomID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3),
                            Active = reader.GetBoolean(4)
                        });
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

            return shops;
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-02-28
        /// 
        /// Select all shops from table as well as their
        /// corresponding RoomNumber and BuildingID from
        /// the Room Table.
        /// </summary>
        /// <returns></returns>
        public List<VMBrowseShop> SelectVMShops()
        {
            List<VMBrowseShop> shops = new List<VMBrowseShop>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_view_model_shops";
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
                        VMBrowseShop shop = new VMBrowseShop()
                        {
                            ShopID = reader.GetInt32(0),
                            RoomID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3),
                            Active = reader.GetBoolean(4),
                            RoomNumber = reader.GetInt32(5),
                            BuildingID = reader.GetString(6)
                        };
                        shops.Add(shop);
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

            return shops;
        }

        public int UpdateShop(Shop newShop, Shop oldShop)
        {
            int result = 0;

            var cmdText = @"sp_update_shop";
            var conn = DBConnection.GetDbConnection();

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ShopID", oldShop.ShopID);
            cmd.Parameters.AddWithValue("@oldRoomID", oldShop.RoomID);
            cmd.Parameters.AddWithValue("@oldName", oldShop.Name);
            cmd.Parameters.AddWithValue("@oldDescription", oldShop.Description);
            cmd.Parameters.AddWithValue("@newRoomID", newShop.RoomID);
            cmd.Parameters.AddWithValue("@newName", newShop.Name);
            cmd.Parameters.AddWithValue("@newDescription", newShop.Description);
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
                result = newShop.ShopID;
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
    }
}
