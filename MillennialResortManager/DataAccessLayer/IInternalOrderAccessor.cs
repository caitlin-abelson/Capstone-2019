using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IInternalOrderAccessor
    {
        int InsertItemOrder(InternalOrder internalOrder, List<VMInternalOrderLine> lines);
        List<VMInternalOrder> SelectAllInternalOrders();
        List<VMInternalOrderLine> SelectOrderLinesByID(int orderID);
        int UpdateOrderStatusToComplete(int orderID, bool orderComplete);
    }
}