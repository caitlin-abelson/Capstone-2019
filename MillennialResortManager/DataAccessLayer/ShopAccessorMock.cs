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
        private List<VMBrowseShop> _vmShops;

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

        public int DeactivateShop(Shop shop)
        {
            throw new NotImplementedException();
        }

        public int DeleteShop(Shop shop)
        {
            throw new NotImplementedException();
        }

        public Shop SelectShopByID(int id)
        {
            return _shops.Find(x => x.ShopID == id);
        }

        /// <summary>
        /// Author: James Heim
        /// Created 2019-03-01
        /// 
        /// Return the shops.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Shop> SelectShops()
        {
            return _shops;
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-01
        /// 
        /// Return the shop view models.
        /// </summary>
        /// <returns></returns>
        public List<VMBrowseShop> SelectVMShops()
        {
            return _vmShops;
        }

        public int UpdateShop(Shop newShop, Shop oldShop)
        {
            throw new NotImplementedException();
        }
    }
}
