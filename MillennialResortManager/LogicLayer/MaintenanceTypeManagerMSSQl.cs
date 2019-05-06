using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Dalton Cleveland" created="2019/03/05">
	/// MaintenanceTypeManagerMSSQL Is an implementation of the IMaintenanceTypeManager Interface meant to interact with the MSSQL MaintenanceType
	/// </summary>
	public class MaintenanceTypeManagerMSSQL : IMaintenanceTypeManager
    {

        private MaintenanceTypeAccessorMSSQL _maintenanceTypeAccessor;

		/// <summary author="Dalton Cleveland" created="2019/03/05">
		/// Constructor allowing for non-static method calls
		/// </summary>
		public MaintenanceTypeManagerMSSQL()
        {
            _maintenanceTypeAccessor = new MaintenanceTypeAccessorMSSQL();
        }

        public void AddMaintenanceType(MaintenanceType newMaintenanceType)
        {
            throw new NotImplementedException();
        }

        public void DeleteMaintenanceType()
        {
            throw new NotImplementedException();
        }

        public List<MaintenanceType> RetrieveAllMaintenanceTypes()
        {
            List<MaintenanceType> maintenanceTypes;
            try
            {
                maintenanceTypes = _maintenanceTypeAccessor.RetrieveAllMaintenanceTypes();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return maintenanceTypes;
        }

        public MaintenanceType RetrieveMaintenanceType()
        {
            throw new NotImplementedException();
        }
    }
}
