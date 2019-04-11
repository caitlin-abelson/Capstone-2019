using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using System.Data;

namespace LogicLayer
{
    /// <summary>
    /// Carlos Arzu
    /// Created: 2019/01/31
    /// 
    /// Class that manages data from Presentation Layer to Data Access layer,
    /// and Data Access Layer to Presentation Layer.
    /// </summary
    public class SpecialOrderManagerMSSQL : ISpecialOrderManager
    {
        private ISpecialOrderAccessor specialOrderAccessor;
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

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        /// Has the information of a new Order, and will create it in our DB.
        /// </summary>
        public bool CreateSpecialOrder(CompleteSpecialOrder SpecialOrder, SpecialOrderLine SpecialOrderLine )
        {
            bool result = false;

            try
            {
                if (!SpecialOrder.isValid())
                {
                    throw new ArgumentException("Data entered for this order is invalid\n " +
                        SpecialOrder.ToString());
                }
                
                 if (!SpecialOrderLine.isValid())
                 {
                        throw new ArgumentException("Data entered for this order is invalid\n" +
                            SpecialOrderLine.ToString());

                 }
              

                result = (1 == specialOrderAccessor.InsertSpecialOrder(SpecialOrder, SpecialOrderLine));
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/03/07
        /// 
        /// Requests all Supplier Order Line information from the Data Access
        /// Layer.
        /// </summary>
        public List<SpecialOrderLine> RetrieveOrderLinesByID(int orderID)
        {
            List<SpecialOrderLine> order = new List<SpecialOrderLine>();

            try
            {
                order = specialOrderAccessor.retrieveSpecialOrderLinebySpecialID(orderID);
            }
            catch (Exception)
            {

                throw;
            }

            return order;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        /// Retrieve the list all orders for browsing.
        /// </summary>
        public List<CompleteSpecialOrder> retrieveAllOrders()
        {

            List<CompleteSpecialOrder> order;
            try
            {

                order = specialOrderAccessor.RetrieveSpecialOrder();

            }
            catch (Exception)
            {
                throw;
            }
            return order;
        }


        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        /// Retrieve list of EmployeeId from DB, for a combo box.
        /// </summary>
        public List<int> employeeID()
        {
             data= specialOrderAccessor.listOfEmployeesID();

            return data;
         }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        /// Retrieve list of ItemId from DB, for a combo box.
        /// </summary>
        public List<int> listOfitemID()
        {
            List<int> order = new List<int> ();
            order = specialOrderAccessor.retrieveitemID();

            return order;
        }
        
        public bool isValid(CompleteSpecialOrder SpecialOrder)
        {
            return true; 
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/06
        /// 
        /// Called for updating the new input from the user, to the order in the DB.
        /// </summary>
        public bool EditSpecialOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew)
        {
            bool result = false;

            try
            {
                result = (1 == specialOrderAccessor.UpdateOrder(Order, Ordernew));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/02/27
        /// 
        /// Method to deactivate the order supplies.
        /// </summary
        public bool DeactivateSpecialOrder(int specialOrderID)
        {
            bool result = false;

            try
            {
                result = (1 == specialOrderAccessor.DeactivateSpecialOrder(specialOrderID));
            }
            catch
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Kevin Broskow
        /// Created: 04/11/2019
        /// 
        /// Update a line in a special order.
        /// </summary>
        public void UpdateSpecialOrderLine(List<SpecialOrderLine> specialOrderLines)
        {
            try
            {
                specialOrderAccessor.UpdateSpecialOrderLine(specialOrderLines);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
