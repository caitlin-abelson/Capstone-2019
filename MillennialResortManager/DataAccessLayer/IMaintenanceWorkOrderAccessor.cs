using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 2/14/2019
    /// The IMaintenanceWorkOrderAccessor is an interface meant to be the standard for interacting with Data 
    /// in a storage medium regarding Work Orders
    /// </summary>
    public interface IMaintenanceWorkOrderAccessor
    {
        void CreateMaintenanceWorkOrder(MaintenanceWorkOrder newMaintenanceWorkOrder);
        MaintenanceWorkOrder RetrieveMaintenanceWorkOrder(int MaintenanceWorkOrderID);
        List<MaintenanceWorkOrder> RetrieveAllMaintenanceWorkOrders();
        void UpdateMaintenanceWorkOrder(MaintenanceWorkOrder oldMaintenanceWorkOrder, MaintenanceWorkOrder newMaintenanceWorkOrder);
        void DeactivateMaintenanceWorkOrder(int MaintenanceWorkOrderID);
        void PurgeMaintenanceWorkOrder(int MaintenanceWorkOrderID);

    }
}
