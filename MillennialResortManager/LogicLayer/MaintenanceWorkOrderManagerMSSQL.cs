using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 2/21/2019
    /// MaintenanceWorkOrderManagerMSSQL Is an implementation of the IMaintenanceWorkOrderManager Interface meant to interact with the MSSQL MaintenanceWorkOrderAccessor
    /// </summary>
    public class MaintenanceWorkOrderManagerMSSQL : IMaintenanceWorkOrderManager
    {
        private IMaintenanceWorkOrderAccessor _maintenanceWorkOrderAccessor;
        /// <summary>
        /// Constructor which allows us to implement the MaintenanceWorkOrder Accessor methods
        /// </summary>
        public MaintenanceWorkOrderManagerMSSQL()
        {
            _maintenanceWorkOrderAccessor = new MaintenanceWorkOrderAccessorMSSQL();
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// Constructor which allows us to implement which ever work order Accessor we need to use
        /// </summary>
        public MaintenanceWorkOrderManagerMSSQL(MaintenanceWorkOrderAccessorMock maintenanceWorkOrderAccessor)
        {
            _maintenanceWorkOrderAccessor = maintenanceWorkOrderAccessor;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Passes along a MaintenanceWorkOrder object to our MaintenanceWorkOrderAccessorMSSQL to be stored in our database
        public void AddMaintenanceWorkOrder(MaintenanceWorkOrder newMaintenanceWorkOrder)
        {
            try
            {
                //Double Check the MaintenanceWorkOrder is Valid
                if (!newMaintenanceWorkOrder.IsValid())
                {
                    throw new ArgumentException("Data for this MaintenanceWorkOrder is not valid");
                }
                _maintenanceWorkOrderAccessor.CreateMaintenanceWorkOrder(newMaintenanceWorkOrder);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Delete MaintenanceWorkOrder will determine whether the MaintenanceWorkOrder needs to be deleted or deactivated and request deactivation or deletion from a MaintenanceWorkOrder Accessor
        /// </summary>
        public void DeleteMaintenanceWorkOrder(int MaintenanceWorkOrderID, bool isActive)
        {
            if (isActive)
            {
                //Is Active so we just deactivate it
                try
                {
                    _maintenanceWorkOrderAccessor.DeactivateMaintenanceWorkOrder(MaintenanceWorkOrderID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                //Is Deactive so we purge it
                try
                {
                    _maintenanceWorkOrderAccessor.PurgeMaintenanceWorkOrder(MaintenanceWorkOrderID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Sends Existing MaintenanceWorkOrder data along with the new MaintenanceWorkOrder data to MaintenanceWorkOrder Accessor. Returns an error if update fails 
        /// </summary>
        public void EditMaintenanceWorkOrder(MaintenanceWorkOrder oldMaintenanceWorkOrder, MaintenanceWorkOrder newMaintenanceWorkOrder)
        {
            try
            {
                if (!newMaintenanceWorkOrder.IsValid())
                {
                    throw new ArgumentException("Data for this new MaintenanceWorkOrder is not valid");
                }
                _maintenanceWorkOrderAccessor.UpdateMaintenanceWorkOrder(oldMaintenanceWorkOrder, newMaintenanceWorkOrder);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 1/29/2019
        /// Retrieves all the MaintenanceWorkOrder in our system from a MaintenanceWorkOrder Accessor or an error if there was a problem
        /// </summary>
        public List<MaintenanceWorkOrder> RetrieveAllMaintenanceWorkOrders()
        {
            List<MaintenanceWorkOrder> maintenanceWorkOrders = new List<MaintenanceWorkOrder>();
            try
            {
                maintenanceWorkOrders = _maintenanceWorkOrderAccessor.RetrieveAllMaintenanceWorkOrders();
            }
            catch (Exception)
            {
                throw;
            }
            return maintenanceWorkOrders;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Returns a MaintenanceWorkOrder from a MaintenanceWorkOrderAccessor or throws an error if there was a problem
        /// </summary>
        public MaintenanceWorkOrder RetrieveMaintenanceWorkOrder(int MaintenanceWorkOrderID)
        {
            MaintenanceWorkOrder maintenanceWorkOrder = new MaintenanceWorkOrder();
            try
            {
                maintenanceWorkOrder = _maintenanceWorkOrderAccessor.RetrieveMaintenanceWorkOrder(MaintenanceWorkOrderID);
            }
            catch (Exception)
            {
                throw new ArgumentException("MaintenanceWorkOrderID did not match any MaintenanceWorkOrders in our System");
            }
            return maintenanceWorkOrder;

        }
    }
}
