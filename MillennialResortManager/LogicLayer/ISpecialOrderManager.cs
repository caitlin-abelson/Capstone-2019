using System.Data;
using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface ISpecialOrderManager
    {
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Interface
        /// </summary
        bool CreateSpecialOrder(CompleteSpecialOrder SpecialOrder, SpecialOrderLine SpecialOrderLine);
        List<SpecialOrderLine> RetrieveOrderLinesByID(int orderID);
        List<CompleteSpecialOrder> retrieveAllOrders();
        List<int> employeeID();
        List<int> listOfitemID();
        bool isValid(CompleteSpecialOrder SpecialOrder);
        bool EditSpecialOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew);
        bool DeactivateSpecialOrder(int specialOrderID);
            }
}