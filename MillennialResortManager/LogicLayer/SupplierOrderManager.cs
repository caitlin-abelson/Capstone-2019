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
        /// <summary>
        /// Eric Bostwick
        /// Created: 2/26/19
        /// 
        /// This is the interface for the Item Supplier Accessor
        /// for managing relationship between items and supplier 
        /// </summary>

        private ISupplierOrderAccessor _supplierOrderManager;
        public SupplierOrderManager()
        {
            _supplierOrderManager = new SupplierOrderAccessor();
        }

        public SupplierOrderManager(SupplierOrderAccessorMock _supplierOrderAccessorMock)
        {
            _supplierOrderManager = _supplierOrderAccessorMock;
        }

        /// <summary>
        /// Eric Bostwick
        /// 2/27/19
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
                result = _supplierOrderManager.InsertSupplierOrder(supplierOrder, supplierOrderLines);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }



        /// <summary>
        /// Eric Bostwick
        /// 2/27/19
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
                _itemSuppliers = _supplierOrderManager.SelectItemSuppliersBySupplierID(supplierID);

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
                _supplierOrderLines = _supplierOrderManager.SelectSupplierOrderLinesBySupplierOrderID(supplierOrderID);

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
                _supplierOrders = _supplierOrderManager.SelectAllSupplierOrders();

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
                result = _supplierOrderManager.UpdateSupplierOrder(supplierOrder, supplierOrderLines);
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
                result = _supplierOrderManager.DeleteSupplierOrder(supplierOrderID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public SupplierOrder RetrieveSupplierOrderByID(int supplierOrderID)
        {
            SupplierOrder order = new SupplierOrder();
            try
            {
                order = _supplierOrderManager.RetrieveSupplierOrderByID(supplierOrderID);
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
                _supplierOrderManager.CompleteSupplierOrder(supplierOrderID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
