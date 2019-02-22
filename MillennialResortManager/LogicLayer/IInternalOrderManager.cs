using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IInternalOrderManager
    {
        bool CreateInternalOrder(InternalOrder itemOrder, List<VMInternalOrderLine> lines);
        List<VMInternalOrder> RetrieveAllInternalOrders();
        List<VMInternalOrderLine> RetrieveOrderLinesByID(int orderID);
        bool UpdateOrderStatusToComplete(int orderID, bool orderComplete);
    }
}