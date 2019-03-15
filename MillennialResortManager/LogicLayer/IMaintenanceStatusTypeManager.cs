using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/5/2019
    /// </summary>
    public interface IMaintenanceStatusTypeManager
    {
        void AddMaintenanceStatusType(MaintenanceStatusType newMaintenanceStatusType);
        MaintenanceStatusType RetrieveMaintenanceStatusType();
        List<MaintenanceStatusType> RetrieveAllMaintenanceStatusTypes();
        void DeleteMaintenanceStatusType();
    }
}
