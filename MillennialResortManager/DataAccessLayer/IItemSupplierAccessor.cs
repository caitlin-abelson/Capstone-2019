/// <summary>
/// Eric Bostwick
/// Created: 2/4/19
/// 
/// This is the interface for the Item Supplier Accessor
/// for managing relationship between items and supplier 
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccessLayer
{
    public  interface IItemSupplierAccessor
    {
        int InsertItemSupplier(ItemSupplier itemSupplier);
        List<ItemSupplier> SelectItemSuppliersByItemID(int itemID);
        ItemSupplier SelectItemSupplierByItemIDandSupplierID(int itemID, int supplierID);
        int UpdateItemSupplier(ItemSupplier itemSupplier, ItemSupplier oldItemSupplier);
        List<Supplier> SelectAllSuppliersForItemSupplierManagement(int itemID);
        int DeleteItemSupplier(int itemID, int supplierID);
        int DeactivateItemSupplier(int itemID, int supplierID);
        
    }
}
