using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Dalton Cleveland" created="2019/03/05">
	/// </summary>
	public interface IMaintenanceTypeManager
    {
        void AddMaintenanceType(MaintenanceType newMaintenanceType);
        MaintenanceType RetrieveMaintenanceType();
        List<MaintenanceType> RetrieveAllMaintenanceTypes();
        void DeleteMaintenanceType();
    }
}
