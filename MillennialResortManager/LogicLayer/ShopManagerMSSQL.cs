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
    }
}
