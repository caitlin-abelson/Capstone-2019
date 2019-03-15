using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    interface IMaintenanceStatusTypeAccessor
    {
        void CreateMaintenanceStatusType(MaintenanceStatusType newMaintenanceStatusType);
        MaintenanceStatusType RetrieveMaintenanceStatusType();
        List<MaintenanceStatusType> RetrieveAllMaintenanceStatusTypes();
        void DeleteMaintenanceStatusType(MaintenanceStatusType deletedMaintenanceStatusType);
    }
}
