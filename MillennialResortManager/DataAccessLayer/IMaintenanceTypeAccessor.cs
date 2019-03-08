

using System.Collections.Generic;
using DataObjects;
/// Austin Berquam
/// Created: 2019/03/04
/// 
/// Interface that implements Create and Delete functions for Maintenance Type
/// accessor classes.
/// </summary>
namespace DataAccessLayer
{

    public interface IMaintenanceTypeAccessor
    {
        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Creates a new Maintenance type 
        /// </summary>
        int InsertMaintenanceType(MaintenanceTypes type);
        List<MaintenanceTypes> SelectAllMaintenanceTypes(string status);

        /// <summary>
        /// Austin Berquam
        /// Created: 2019/02/06
        /// 
        /// Deletes a Maintenance type
        /// </summary>
        List<string> SelectAllMaintenanceTypeID();
        int DeleteMaintenanceType(string id);
    }
}