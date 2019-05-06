using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
	/// <summary author="Danielle Russo" created="2019/01/21">
	/// Interface that implements CRUD functions for Building objs.
	/// for manager classes.
	/// </summary>
	public interface IBuildingManager
    {
		/// <summary author="Danielle Russo" created="2019/01/21">
		/// Creates a new Building
		/// </summary>
		/// <param name="newBuilding">The Building obj. to be added.</param>
		/// DO BELOW STILL
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// DO ABOVE STILL
		/// <returns>Rows created</returns>
        bool CreateBuilding(Building newBuilding);
        List<Building> RetrieveAllBuildings();
        Building RetrieveBuilding(string buildingID);
        bool UpdateBuilding(Building oldBuilding, Building updatedBuilding);
        List<string> RetrieveAllBuildingStatus();
    }
}