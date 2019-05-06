using System.Data;
using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;

namespace LogicLayer
{
	/// <summary author="Carlos Arzu" created="2019/01/31">
	/// Interface
	/// </summary
	public interface ISpecialOrderManager
	{
		bool CreateSpecialOrder(CompleteSpecialOrder SpecialOrder);
		bool CreateSpecialOrderLine(SpecialOrderLine SpecialOrderLine);
		List<SpecialOrderLine> RetrieveOrderLinesByID(int orderID);
		List<CompleteSpecialOrder> retrieveAllOrders();
		List<int> employeeID();
		bool isValid(CompleteSpecialOrder SpecialOrder);
		bool EditSpecialOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew);
		bool EditSpecialOrderLine(SpecialOrderLine Order, SpecialOrderLine Ordernew);
		bool DeactivateSpecialOrder(int specialOrderID);
		bool DeleteItem(int ID, string ItemName);
		int retrieveSpecialOrderID(CompleteSpecialOrder selected);
		bool AuthenticatedBy(int ID, string username);
	}
}