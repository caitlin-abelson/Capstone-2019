using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    interface IMaintenanceTypeAccessor
    {
        void CreateMaintenanceType(MaintenanceType newMaintenanceType);
        MaintenanceType RetrieveMaintenanceType();
        List<MaintenanceType> RetrieveAllMaintenanceTypes();
        void DeleteMaintenanceType(MaintenanceType deletedMaintenanceType);
    }
}
