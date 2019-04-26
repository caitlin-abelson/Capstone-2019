using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface ISupplierOrderAccessor
    {
        /// <summary>
        /// Author: Eric Bostwick
        /// Created : 2/26/2019
        /// The Interface for the SupplierOrder Accessor
        /// </summary>
        int InsertSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines);

        List<VMItemSupplierItem> SelectItemSuppliersBySupplierID(int supplierID);

        List<SupplierOrder> SelectAllSupplierOrders();
        List<SupplierOrder> SelectAllGeneratedOrders();

        List<SupplierOrderLine> SelectSupplierOrderLinesBySupplierOrderID(int supplierOrderID);

        int UpdateSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines);

        int DeleteSupplierOrder(int supplierOrderID);

        SupplierOrder RetrieveSupplierOrderByID(int supplierOrderID);

        void CompleteSupplierOrder(int supplierOrderID);
        int SelectSupplierItemIDByItemAndSupplier(int itemID, int supplierID);
    }
}
