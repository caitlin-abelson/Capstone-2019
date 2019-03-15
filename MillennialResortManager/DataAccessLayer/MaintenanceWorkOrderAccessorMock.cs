using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/07/2019
    /// This is a mock Data accessor which implements the IMaintenanceWorkOrderAccessor interface.
    /// </summary>
    public class MaintenanceWorkOrderAccessorMock : IMaintenanceWorkOrderAccessor
    {
        private List<MaintenanceWorkOrder> _maintenanceWorkOrders;
        private List<string> _AllMaintenanceTypes;
        private List<string> _AllMaintenanceStatusTypes;
        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public MaintenanceWorkOrderAccessorMock()
        {
            _maintenanceWorkOrders = new List<MaintenanceWorkOrder>();
            _maintenanceWorkOrders.Add(new MaintenanceWorkOrder() { MaintenanceWorkOrderID = 100000, MaintenanceTypeID = "Plumbing",  DateRequested = new DateTime(2019, 1, 1), DateCompleted = new DateTime(2019, 1, 2), RequestingEmployeeID = 100000, WorkingEmployeeID = 100001, Description = "Test Description", Comments = "Test Comment", MaintenanceStatusID = "Open", ResortPropertyID = 1, Complete = true});
            _maintenanceWorkOrders.Add(new MaintenanceWorkOrder() { MaintenanceWorkOrderID = 100001, MaintenanceTypeID = "Electrical", DateRequested = new DateTime(2018, 1, 1), DateCompleted = new DateTime(2018, 1, 2), RequestingEmployeeID = 100000, WorkingEmployeeID = 100001, Description = "Test Description", Comments = "Test Comment", MaintenanceStatusID = "Closed", ResortPropertyID = 1, Complete = true });
            _maintenanceWorkOrders.Add(new MaintenanceWorkOrder() { MaintenanceWorkOrderID = 100002, MaintenanceTypeID = "Repairs", DateRequested = new DateTime(2017, 1, 1), DateCompleted = new DateTime(2017, 1, 2), RequestingEmployeeID = 100000, WorkingEmployeeID = 100001, Description = "Test Description", Comments = "Test Comment", MaintenanceStatusID = "Working", ResortPropertyID = 1, Complete = false });
            _maintenanceWorkOrders.Add(new MaintenanceWorkOrder() { MaintenanceWorkOrderID = 100003, MaintenanceTypeID = "Other", DateRequested = new DateTime(2016, 1, 1), DateCompleted = new DateTime(2016, 1, 2), RequestingEmployeeID = 100000, WorkingEmployeeID = 100001, Description = "Test Description", Comments = "Test Comment", MaintenanceStatusID = "Awaiting Materials", ResortPropertyID = 1, Complete = false });
            _AllMaintenanceTypes = new List<string>();
            _AllMaintenanceStatusTypes = new List<string>();
            foreach (var wo in _maintenanceWorkOrders)
            {
                _AllMaintenanceTypes.Add(wo.MaintenanceTypeID);
                _AllMaintenanceStatusTypes.Add(wo.MaintenanceStatusID);
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// This will create a work order using the data provided in the newWorkOrder
        /// </summary>
        public void CreateMaintenanceWorkOrder(MaintenanceWorkOrder newMaintenanceWorkOrder)
        {
            _maintenanceWorkOrders.Add(newMaintenanceWorkOrder);
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// This will search for a work order with a matching work order ID from our mock data
        /// </summary>
        public MaintenanceWorkOrder RetrieveMaintenanceWorkOrder(int MaintenanceWorkOrderID)
        {
            MaintenanceWorkOrder w = new MaintenanceWorkOrder();
            w = _maintenanceWorkOrders.Find(x => x.MaintenanceWorkOrderID == MaintenanceWorkOrderID);
            if (w == null)
            {
                throw new ArgumentException("MaintenanceWorkOrderID did not match any MaintenanceWorkOrders in our System");
            }

            return w;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// This will simply return a list with all of our Work Order data
        /// </summary>
        public List<MaintenanceWorkOrder> RetrieveAllMaintenanceWorkOrders()
        {
            return _maintenanceWorkOrders;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// This will update an existing work order or throw a new ArgumentException
        /// </summary>
        public void UpdateMaintenanceWorkOrder(MaintenanceWorkOrder oldMaintenanceWorkOrder, MaintenanceWorkOrder newMaintenanceWorkOrder)
        {
            bool updated = false;
            foreach (var wo in _maintenanceWorkOrders)
            {
                if (wo.MaintenanceWorkOrderID == oldMaintenanceWorkOrder.MaintenanceWorkOrderID)
                {
                    wo.MaintenanceTypeID = newMaintenanceWorkOrder.MaintenanceTypeID;
                    wo.MaintenanceStatusID = newMaintenanceWorkOrder.MaintenanceStatusID;
                    wo.DateRequested = newMaintenanceWorkOrder.DateRequested;
                    wo.DateCompleted = newMaintenanceWorkOrder.DateCompleted;
                    wo.RequestingEmployeeID = newMaintenanceWorkOrder.RequestingEmployeeID;
                    wo.WorkingEmployeeID = newMaintenanceWorkOrder.WorkingEmployeeID;
                    wo.Description = newMaintenanceWorkOrder.Description;
                    wo.Comments = newMaintenanceWorkOrder.Comments;
                    wo.ResortPropertyID = newMaintenanceWorkOrder.ResortPropertyID;
                    wo.Complete = newMaintenanceWorkOrder.Complete;
                    updated = true;
                    break;
                }
            }
            if (!updated)
            {
                throw new ArgumentException("No Work Order was found to update");
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// This will deactivate the work order which has a matching ID to the one that was provided 
        /// </summary>
        public void DeactivateMaintenanceWorkOrder(int MaintenanceWorkOrderID)
        {
            bool foundWorkOrder = false;
            foreach (var wo in _maintenanceWorkOrders)
            {
                if (wo.MaintenanceWorkOrderID == MaintenanceWorkOrderID)
                {
                    wo.Complete = false;
                    foundWorkOrder = true;
                    break;
                }
            }
            if (!foundWorkOrder)
            {
                throw new ArgumentException("No Work Order was found with that ID");
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/07/2019
        /// This will completely purge a work order from our system or throw an exception if we cannot find it
        public void PurgeMaintenanceWorkOrder(int MaintenanceWorkOrderID)
        {
            try
            {
                RetrieveMaintenanceWorkOrder(MaintenanceWorkOrderID);
            }
            catch (Exception)
            {
                throw new ArgumentException("No Work Order was found with that ID");
            }
            _maintenanceWorkOrders.Remove(_maintenanceWorkOrders.Find(x => x.MaintenanceWorkOrderID == MaintenanceWorkOrderID));
        }

    }
}
