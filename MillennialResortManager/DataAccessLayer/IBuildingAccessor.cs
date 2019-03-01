/// <summary>
/// Danielle Russo
/// Created: 2019/01/21
/// 
/// Interface that implements CRUD functions for Building objs.
/// for accessor classes.
/// </summary>
///
/// <remarks>
/// </remarks>
/// 

using DataObjects;
using System.Collections.Generic;

// Create, Retrieve, Update, Deactivate, Purge
namespace DataAccessLayer
{
    public interface IBuildingAccessor
    {
        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Creates a new Building
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="newBuilding">The Building obj. to be added.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows created</returns>
        int InsertBuilding(Building newBuilding);
        int SelectBuildingByID(int buildingID);
        List<Building> SelectAllBuildings();

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/18
        /// 
        /// Finds all buildings by StatusID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="statusID">The status to be searched.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows created</returns>
        List<Building> SelectAllBuildingsByStatusID(string statusID);
        int UpdateBuilding(Building oldBuilding, Building updatedBuilding);
        List<string> SelectAllBuildingStatus();

    }
}