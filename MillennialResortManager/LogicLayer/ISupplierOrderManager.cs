using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;


namespace LogicLayer
{
    public interface ISupplierOrderManager
    {
        int CreateSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines);

        List<VMItemSupplierItem> RetrieveAllItemSuppliersBySupplierID(int supplierID);

        List<SupplierOrder> RetrieveAllSupplierOrders();

        List<SupplierOrderLine> RetrieveAllSupplierOrderLinesBySupplierOrderID(int supplierOrderID);

        int UpdateSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines);

        int DeleteSupplierOrder(int supplierOrderID);

    }
}
