
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using System.Data;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Carlos Arzu" created="2019/01/31">
	/// Class that manages data from Presentation Layer to Data Access layer,
	/// and Data Access Layer to Presentation Layer.
	/// </summary
	public class SpecialOrderManagerMSSQL : ISpecialOrderManager
	{
		private ISpecialOrderAccessor specialOrderAccessor;
		private SpecialOrderAccessorMSSQL SpecialOrderAccessor;
		public List<int> data { get; set; }
		public List<string> dataString { get; set; }

		public SpecialOrderManagerMSSQL()
		{
			specialOrderAccessor = new SpecialOrderAccessorMSSQL();
		}

		public SpecialOrderManagerMSSQL(SpecialOrderAccessorMock supplierOrderAccessorMock)
		{
			specialOrderAccessor = supplierOrderAccessorMock;
		}

		/// <summary author="Carlos Arzu" created="2019/02/06">
		/// Has the information of a new Order, and will create it in our DB.
		/// </summary>
		public bool CreateSpecialOrder(CompleteSpecialOrder SpecialOrder)
		{
			bool result = false;

			try
			{
				if (!SpecialOrder.isValid())
				{
					throw new ArgumentException("Data entered for this order is invalid\n " +
						SpecialOrder.ToString());
				}

				result = (1 == specialOrderAccessor.InsertSpecialOrder(SpecialOrder));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Carlos Arzu" created="2019/03/26">
		/// Creates a new Order line in our DB.
		/// </summary>
		public bool CreateSpecialOrderLine(SpecialOrderLine SpecialOrderLine)
		{
			bool result = false;

			try
			{
				if (!SpecialOrderLine.isValid())
				{
					throw new ArgumentException("Data entered for this order is invalid\n" +
						SpecialOrderLine.ToString());
				}

				result = 1 == specialOrderAccessor.InsertSpecialOrderLine(SpecialOrderLine);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Carlos Arzu" created="2019/03/07">
		/// Requests all Supplier Order Line information from the Data Access
		/// Layer.
		/// </summary>
		public List<SpecialOrderLine> RetrieveOrderLinesByID(int orderID)
		{
			List<SpecialOrderLine> order = new List<SpecialOrderLine>();

			try
			{
				order = specialOrderAccessor.SelectSpecialOrderLinebySpecialID(orderID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return order;
		}

		/// <summary author="Carlos Arzu" created="2019/02/06">
		/// Retrieve the list all orders for browsing.
		/// </summary>
		public List<CompleteSpecialOrder> retrieveAllOrders()
		{
			List<CompleteSpecialOrder> order;
			try
			{
				order = specialOrderAccessor.SelectSpecialOrder();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return order;
		}

		/// <summary author="Carlos Arzu" created="2019/02/06">
		/// Retrieve list of EmployeeId from DB, for a combo box.
		/// </summary>
		public List<int> employeeID()
		{
			data = specialOrderAccessor.listOfEmployeesID();

			return data;
		}

		public bool isValid(CompleteSpecialOrder SpecialOrder)
		{
			return true;
		}

		/// <summary author="Carlos Arzu" created="2019/02/06">
		/// Called for updating the new input from the user, to the order in the DB.
		/// </summary>
		public bool EditSpecialOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew)
		{
			bool result = false;

			try
			{
				result = (1 == specialOrderAccessor.UpdateOrder(Order, Ordernew));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Carlos Arzu" created="2019/04/05">
		/// Called for updating the new input from the user, to the special order line in the DB.
		/// </summary>
		public bool EditSpecialOrderLine(SpecialOrderLine Order, SpecialOrderLine Ordernew)
		{
			bool result = false;

			try
			{
				result = (1 == specialOrderAccessor.UpdateOrderLine(Order, Ordernew));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Carlos Arzu" created="2019/02/27">
		/// Method to deactivate the order supplies.
		/// </summary
		public bool DeactivateSpecialOrder(int specialOrderID)
		{
			bool result = false;

			try
			{
				result = (1 == specialOrderAccessor.DeactivateSpecialOrder(specialOrderID));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Carlos Arzu" created="2019/04/05">
		/// Called for updating the new input from the user, to the special order line in the DB.
		/// </summary>
		public bool DeleteItem(int ID, string ItemName)
		{
			bool result = false;

			try
			{
				result = (1 == specialOrderAccessor.DeleteItemFromOrder(ID, ItemName));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Carlos Arzu" created="2019/04/05">
		/// Called for updating the new input from the user, to the special order line in the DB.
		/// </summary>
		public int retrieveSpecialOrderID(CompleteSpecialOrder selected)
		{

			int result = 0;

			try
			{
				result = specialOrderAccessor.retrieveSpecialOrderIDbyDetails(selected);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Carlos Arzu" created="2019/04/10">
		/// After the order has been authorized, it is added to the record the employee's username
		/// that Authorized it. 
		/// </summary>
		public bool AuthenticatedBy(int ID, string username)
		{
			bool result = false;

			try
			{
				result = (1 == specialOrderAccessor.insertAuthenticateBy(ID, username));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}
	}
}