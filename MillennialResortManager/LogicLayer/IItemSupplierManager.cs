using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Eric Bostwick" created="2019/02/04">
	/// Interface For ItemSupplyManagement
	/// </summary>
	public interface IItemSupplierManager
    {
        int CreateItemSupplier(ItemSupplier itemSupplier);
        List<ItemSupplier> RetrieveAllItemSuppliersByItemID(int itemID);
        ItemSupplier RetrieveItemSupplier(int ItemID, int SupplierID);
        int UpdateItemSupplier(ItemSupplier itemSupplier, ItemSupplier oldItemSupplier);
        List<Supplier> RetrieveAllSuppliersForItemSupplierManagement(int itemID);
        int DeleteItemSupplier(int itemID, int supplierID);
        int DeactivateItemSupplier(int itemID, int supplierID);
    }
}