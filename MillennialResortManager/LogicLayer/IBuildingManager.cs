/// <summary>
/// Danielle Russo
/// Created: 2019/01/21
/// 
/// Interface that implements CRUD functions for Building objs.
/// for manager classes.
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
/// 

using DataObjects;
using System.Collections.Generic;

// add, retreive, edit, delete
namespace LogicLayer
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
    /// DO BELOW STILL
    /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
    /// DO ABOVE STILL
    /// <returns>Rows created</returns>
    public interface IBuildingManager
    {
        bool CreateBuilding(Building newBuilding);

        List<Building> RetrieveAllBuildings();
        Building RetrieveBuilding(Building selectedBuilding);
        bool UpdateBuilding(Building oldBuilding, Building updatedBuilding);
        List<string> RetrieveAllBuildingStatus();
    }
}