using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class ShopManagerMSSQL : IShopManager
    {
        private Shop _shop = new Shop();
        private IShopAccessor _shopAccessor;
        /// <summary>
        /// Author: Kevin Broskow
        /// Created Date: 2/27/2019
        /// The constructor for the ShopManager class
        /// </summary>
        public ShopManagerMSSQL()
        {
            _shopAccessor = new ShopAccessorMSSQL();
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created Date: 2/27/2019
        /// Constructor for the mock accessor
        /// </summary>
        /// <param name="employeeAccessorMock"></param>
        public ShopManagerMSSQL(IShopAccessor shopAccessorMock)
        {
            _shopAccessor = shopAccessorMock;
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created Date: 2/27/2019
        /// Method used for inserting a shop into the database.
        /// </summary>
        /// <param name="employeeAccessorMock"></param>
        public int InsertShop(Shop shop)
        {
            int result = 0;
            if (shop.IsValid())
            {
                result = _shopAccessor.CreateShop(shop);
            }
            else
            {
                throw new ArgumentException();
            }

            return result;
        }

        public bool DeactivateShop(Shop shop)
        {
            throw new NotImplementedException();
        }

        public bool DeleteShop(Shop shop)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Retrieve an IEnumerable of Shop objects from
        /// the database.
        /// </summary>
        /// <returns>IEnumerable of Shops</returns>
        public IEnumerable<Shop> RetrieveAllShops()
        {
            List<Shop> shops = null;

            try
            {
                shops = (List<Shop>)_shopAccessor.SelectShops();
            }
            catch (Exception)
            {

                throw;
            }

            return shops;
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-02-28
        /// 
        /// Retrieve the View Model Shop Objects via 
        /// the ShopAccessorMSSQL.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMBrowseShop> RetrieveAllVMShops()
        {
            List<VMBrowseShop> shops = null;

            try
            {
                shops = (List<VMBrowseShop>)_shopAccessor.SelectVMShops();
            }
            catch (Exception)
            {

                throw;
            }

            return shops;
        }

        public Shop RetrieveShopByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateShop(Shop newShop, Shop oldShop)
        {
            throw new NotImplementedException();
        }
    }
}
