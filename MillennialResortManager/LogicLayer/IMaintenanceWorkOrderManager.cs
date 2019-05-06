using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="Dalton Cleveland" created="2019/02/21">
	/// IMaintenanceWorkOrderManager is an interface meant to be the standard for interacting with WorkOrders in a storage medium
	/// </summary>
	public interface IMaintenanceWorkOrderManager
    {
        void AddMaintenanceWorkOrder(MaintenanceWorkOrder newMaintenanceWorkOrder);
        void EditMaintenanceWorkOrder(MaintenanceWorkOrder oldMaintenanceWorkOrder, MaintenanceWorkOrder newMaintenanceWorkOrder);
        MaintenanceWorkOrder RetrieveMaintenanceWorkOrder(int MaintenanceWorkOrderID);
        List<MaintenanceWorkOrder> RetrieveAllMaintenanceWorkOrders();
        void DeleteMaintenanceWorkOrder(int MaintenanceWorkOrderID, bool isActive);
    }
}
