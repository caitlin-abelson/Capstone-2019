using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Richard Carroll" created="2019/01/30">
	/// This class is used to retrieve Item Order data for the
	/// presentation layer.
	/// </summary>
	public class InternalOrderManager : IInternalOrderManager
    {
        private IInternalOrderAccessor _itemOrderAccessor;

		/// <summary author="Richard Carroll" created="2019/01/30">
		/// The Basic Constructor.
		/// Creates a normal Accessor Object to interact with the Data
		/// Access Layer.
		/// </summary>
		public InternalOrderManager()
        {
            _itemOrderAccessor = new InternalOrderAccessor();
        }

		/// <summary author="Richard Carroll" created="2019/01/30">
		/// The Secondary Constructor.
		/// Creates a mock Accessor Object so the following methods can
		/// be tested without needing the database.
		/// </summary>
		public InternalOrderManager(InternalOrderAccessorMock internalOrderAccessorMock)
        {
            _itemOrderAccessor = internalOrderAccessorMock;
        }

		/// <summary author="Richard Carroll" created="2019/01/30">
		/// Passes information from the Presentation Layer to the Data Access
		/// Layer and returns the result of the Insertion attempt.
		/// </summary>
		public bool CreateInternalOrder(InternalOrder itemOrder, List<VMInternalOrderLine> lines)
        {
            bool result = false;
            try
            {
                if (!itemOrder.isValid())
                {
                    throw new ArgumentException("Data entered for this order is invalid\n " + 
                        itemOrder.ToString());
                }
                foreach (var line in lines)
                {
                    if (!line.isValid())
                    {
                        throw new ArgumentException("Data entered for this order is invalid\n" +
                            line.ToString());

                    }
                }
                result = (_itemOrderAccessor.InsertItemOrder(itemOrder, lines) > 0);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="Richard Carroll" created="2019/01/30">
		/// Requests all available order information from the Data Access
		/// Layer and returns it to the Presentation Layer, if Possible.
		/// </summary>
		public List<VMInternalOrder> RetrieveAllInternalOrders()
        {
            List<VMInternalOrder> orders = new List<VMInternalOrder>();

            try
            {
                orders = _itemOrderAccessor.SelectAllInternalOrders();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return orders;
        }

		/// <summary author="Richard Carroll" created="2019/01/30">
		/// Requests all Order Line information from the Data Access
		/// Layer and returns it to the Presentation Layer, if Possible.
		/// </summary>
		public List<VMInternalOrderLine> RetrieveOrderLinesByID(int orderID)
        {
            List<VMInternalOrderLine> lines = new List<VMInternalOrderLine>();

            try
            {
                lines = _itemOrderAccessor.SelectOrderLinesByID(orderID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return lines;
        }

		/// <summary author="Richard Carroll" created="2019/01/30">
		/// Passes information from the Presentation Layer to the Data Access
		/// Layer and returns the result of the Update attempt.
		/// </summary>
		public bool UpdateOrderStatusToComplete(int orderID, bool orderComplete)
        {
            bool result = false;

            try
            {
                result = (_itemOrderAccessor.UpdateOrderStatusToComplete(orderID, orderComplete) > 0);
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
