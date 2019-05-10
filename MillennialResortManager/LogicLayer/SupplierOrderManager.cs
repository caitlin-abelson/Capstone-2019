using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class SupplierOrderManager : ISupplierOrderManager
    {
		/// <summary author="Eric Bostwick" created="2019/02/26">
		/// This is the interface for the Item Supplier Accessor
		/// for managing relationship between items and supplier 
		/// </summary>
		private ISupplierOrderAccessor _supplierOrderAccessor;
        public SupplierOrderManager()
        {
            _supplierOrderAccessor = new SupplierOrderAccessor();
        }

        public SupplierOrderManager(SupplierOrderAccessorMock _supplierOrderAccessorMock)
        {
            _supplierOrderAccessor = _supplierOrderAccessorMock;
        }

		/// <summary author="Eric Bostwick" created="2019/02/27">
		/// Inserts a Supplier Order
		/// using a SupplierOrder Object and a list of SupplierOrderLines
		/// </summary>
		/// <returns>
		/// List of ItemSupplers
		/// </returns>
		public int CreateSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines)
        {
            int result;
            try
            {
                if (!supplierOrder.IsValid())
                {
                    throw new ArgumentException("Data for this supplier order record is invalid");
                }
                //check each of the supplier order lines for valid inputs
                foreach (var line in supplierOrderLines)
                {
                    if (!line.IsValid())
                    {
                        throw new ArgumentException("Data for this supplier line item record is invalid");
                    }
                }
                result = _supplierOrderAccessor.InsertSupplierOrder(supplierOrder, supplierOrderLines);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

		/// <summary author="Eric Bostwick" created="2019/02/27">
		/// Gets list of itemsuppliers from itemsupplier table
		/// based upon the supplierID
		/// </summary>
		/// <returns>
		/// List of ItemSupplers
		/// </returns>
		public List<VMItemSupplierItem> RetrieveAllItemSuppliersBySupplierID(int supplierID)
        {

            List<VMItemSupplierItem> _itemSuppliers;
            try
            {
                _itemSuppliers = _supplierOrderAccessor.SelectItemSuppliersBySupplierID(supplierID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _itemSuppliers;
        }

        public List<SupplierOrderLine> RetrieveAllSupplierOrderLinesBySupplierOrderID(int supplierOrderID)
        {
            List<SupplierOrderLine> _supplierOrderLines;
            try
            {
                _supplierOrderLines = _supplierOrderAccessor.SelectSupplierOrderLinesBySupplierOrderID(supplierOrderID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _supplierOrderLines;
        }

        public List<SupplierOrder> RetrieveAllSupplierOrders()
        {
            List<SupplierOrder> _supplierOrders;
            try
            {
                _supplierOrders = _supplierOrderAccessor.SelectAllSupplierOrders();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _supplierOrders;

        }

		/// <summary author="Richard Carroll" created="2019/04/26">
		/// Gets list of Generated Supplier Orders from the SupplierOrder table
		/// </summary>
		/// <returns>
		/// List of SupplierOrders
		/// </returns>
		public List<SupplierOrder> RetrieveAllGeneratedOrders()
        {
            List<SupplierOrder> _supplierOrders;
            try
            {
                _supplierOrders = _supplierOrderAccessor.SelectAllGeneratedOrders();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _supplierOrders;
        }

        public int UpdateSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines)
        {
            int result;
            try
            {
                if (!supplierOrder.IsValid())
                {
                    throw new ArgumentException("Data for this supplier order record is invalid");
                }

                foreach (var line in supplierOrderLines)
                {
                    if (!line.IsValid())
                    {
                        throw new ArgumentException("Data for this supplier line item record is invalid");
                    }
                }
                result = _supplierOrderAccessor.UpdateSupplierOrder(supplierOrder, supplierOrderLines);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public int DeleteSupplierOrder(int supplierOrderID)
        {
            int result;
            try
            {
                result = _supplierOrderAccessor.DeleteSupplierOrder(supplierOrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int RetrieveSupplierItemID(int ItemID, int SupplierID)
        {
            int supplierItemID;
            try
            {
                supplierItemID = _supplierOrderAccessor.SelectSupplierItemIDByItemAndSupplier(ItemID, SupplierID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return supplierItemID;
        }

        public SupplierOrder RetrieveSupplierOrderByID(int supplierOrderID)
        {
            SupplierOrder order = new SupplierOrder();
            try
            {
                order = _supplierOrderAccessor.RetrieveSupplierOrderByID(supplierOrderID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return order;
        }

        public void CompleteSupplierOrder(int supplierOrderID)
        {
            try
            {
                _supplierOrderAccessor.CompleteSupplierOrder(supplierOrderID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateGeneratedOrder(int supplierOrderID, int employeeID)
        {
            bool result = false;

            try
            {
                result = _supplierOrderAccessor.UpdateGeneratedOrder(supplierOrderID, employeeID) == 1;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Author: Jared Greenfield, James Heim, Richard Caroll
        /// Date: 2019-05-09
        /// </summary>
        public void GenerateOrders()
        {
            try
            {
                _supplierOrderAccessor.GenerateOrders();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}