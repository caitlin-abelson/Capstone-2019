using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
/// <summary>
/// Author: Kevin Broskow
/// Created : 2/27/2019
/// The ShopAccessorMock is used in creating test data for unit testing.
/// </summary>
namespace DataAccessLayer
{
    public class ShopAccessorMock : IShopAccessor
    {
        private List<Shop> _shops = new List<Shop>();

        /// <summary>
        /// Author: Kevin Broskow
        /// Created Date: 2/28/19
        /// 
        /// Inserting a shop for testing purposes
        /// </summary>
        /// <param name="shop"></param>
        public int CreateShop(Shop shop)
        {
            _shops.Add(shop);

            return shop.RoomID;
        }
    }
}
