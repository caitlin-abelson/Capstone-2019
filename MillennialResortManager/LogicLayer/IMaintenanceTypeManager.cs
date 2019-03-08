/// <summary>
/// Austin Berquam
/// Created: 2019/02/06
/// 
/// Interface that implements Create and Delete functions for Guest Types
/// for manager classes.
/// </summary>

using System.Collections.Generic;
using DataObjects;
namespace LogicLayer
{
    public interface IMaintenanceTypeManager
    {
        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new MaintenanceType
        /// </summary>
        bool CreateMaintenanceType(MaintenanceTypes guestType);
        List<string> RetrieveAllMaintenanceTypes();

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a MaintenanceType
        /// </summary>
        bool DeleteMaintenanceType(string guestTypeID);
        List<MaintenanceTypes> RetrieveMaintenanceTypes(string status);
    }
}