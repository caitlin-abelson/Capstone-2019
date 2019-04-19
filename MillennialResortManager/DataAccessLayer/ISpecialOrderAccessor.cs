using System.Collections.Generic;
using System.Data;
using DataObjects;

namespace DataAccessLayer
{
    public interface ISpecialOrderAccessor
    {
        int InsertSpecialOrder(CompleteSpecialOrder newSpecialOrder);
        int InsertSpecialOrderLine(SpecialOrderLine newSpecialOrderline);
        List<CompleteSpecialOrder> SelectSpecialOrder();
        List<SpecialOrderLine> SelectSpecialOrderLinebySpecialID(int Item);
        List<int> listOfEmployeesID();
        int UpdateOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew);
        int UpdateOrderLine(SpecialOrderLine Order, SpecialOrderLine Ordernew);
        int DeactivateSpecialOrder(int specialOrderID);
        int DeleteItemFromOrder(int ID, string ItemName);
        int retrieveSpecialOrderIDbyDetails(CompleteSpecialOrder selected);
        int insertAuthenticateBy(int SpecialOrderID, string Authorized);


    }
}