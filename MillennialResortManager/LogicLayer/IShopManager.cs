using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Authors: Kevin Broskow and James Heim
    /// </summary>
    public interface IShopManager
    {
        int InsertShop(Shop shop);

        Shop RetrieveShopByID(int id);

        IEnumerable<Shop> RetrieveAllShops();

        IEnumerable<VMBrowseShop> RetrieveAllVMShops();

        bool UpdateShop(Shop newShop, Shop oldShop);

        bool DeactivateShop(Shop shop);

        bool DeleteShop(Shop shop);
    }
}
