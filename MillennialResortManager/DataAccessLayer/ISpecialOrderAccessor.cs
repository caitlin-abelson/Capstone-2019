using System.Collections.Generic;
using System.Data;
using DataObjects;

namespace DataAccessLayer
{
    public interface ISpecialOrderAccessor
    {
        int InsertSpecialOrder(CompleteSpecialOrder newSpecialOrder, SpecialOrderLine newSpecialOrderline);
        List<CompleteSpecialOrder> RetrieveSpecialOrder();
        List<int> retrieveitemID();
        List<SpecialOrderLine> retrieveSpecialOrderLinebySpecialID(int Item);
        List<int> listOfEmployeesID();
        int UpdateOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew);
        int DeactivateSpecialOrder(int specialOrderID);
        void UpdateSpecialOrderLine(List<SpecialOrderLine> specialOrderLines);
    }
}