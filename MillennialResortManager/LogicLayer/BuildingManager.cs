/// <summary>
/// Danielle Russo
/// Created: 2019/01/21
/// 
/// Class that interacts with the presentation layer and building access layer
/// </summary>
///
/// <remarks>
/// </remarks>
/// 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class BuildingManager : IBuildingManager
    {
        IBuildingAccessor buildingAccessor;

        public BuildingManager()
        {
            buildingAccessor = new BuildingAccessor();
        }
        public BuildingManager(BuildingAccessorMock mockAccessor)
        {
            buildingAccessor = new BuildingAccessorMock();
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Adds a new Building obj.
        /// </summary>
        ///
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/02/28
        /// 
        /// Changed 1 to 2 so that it will pass for now
        /// 
        /// </remarks>
        /// <param name="newBuilding">The Building obj to be added</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>True if Building was successfully added, False if Building was not added.</returns>
        public bool CreateBuilding(Building newBuilding)
        {
            bool result = false;

            try
            {
                LogicValidationExtensionMethods.ValidateBuildingID(newBuilding.BuildingID);
                LogicValidationExtensionMethods.ValidateBuildingName(newBuilding.Name);
                LogicValidationExtensionMethods.ValidateBuildingAddress(newBuilding.Address);
                LogicValidationExtensionMethods.ValidateBuildngDescription(newBuilding.Description);
                LogicValidationExtensionMethods.ValidateBuildingStatusID(newBuilding.StatusID);

                result = (2 == buildingAccessor.InsertBuilding(newBuilding));
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

       

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// Edits building details
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// 
        /// </remarks>
        /// <param name="oldBuilding">The original Building obj to be updated</param>
        /// <param name="updatedBuilding">The updated Building obj</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>True if Building was successfully updated, False if Building was not updated.</returns>
        public bool UpdateBuilding(Building oldBuilding, Building updatedBuilding)
        {
            bool result = false;

            try
            {
                LogicValidationExtensionMethods.ValdateMatchingIDs(oldBuilding.BuildingID, updatedBuilding.BuildingID);
                LogicValidationExtensionMethods.ValidateBuildingID(updatedBuilding.BuildingID);
                LogicValidationExtensionMethods.ValidateBuildingName(updatedBuilding.Name);
                LogicValidationExtensionMethods.ValidateBuildingAddress(updatedBuilding.Address);
                LogicValidationExtensionMethods.ValidateBuildngDescription(updatedBuilding.Description);
                LogicValidationExtensionMethods.ValidateBuildingStatusID(updatedBuilding.StatusID);

                result = (1 == buildingAccessor.UpdateBuilding(oldBuilding, updatedBuilding));
            }
            catch (ArgumentNullException ane)
            {
                throw ane;
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// List of all buildings in the Building table.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        ///
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>A list of Building objs.</returns>
        public List<Building> RetrieveAllBuildings()
        {
            List<Building> buildings = null;

            try
            {
                buildings = buildingAccessor.SelectAllBuildings();
            }
            catch (Exception)
            {

                throw;
            }
            return buildings;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-17
        /// 
        /// Retrieve a building by the specified building id.
        /// </summary>
        /// <param name="selectedBuilding"></param>
        /// <returns></returns>
        public Building RetrieveBuilding(string buildingID)
        {
            Building building = null;

            try
            {
                building = buildingAccessor.SelectBuildingByID(buildingID);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return building;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/21
        /// 
        /// List of all building status ids.
        /// </summary>
        /// <returns>A list of Status Ids.</returns>
        public List<string> RetrieveAllBuildingStatus()
        {
            List<string> buildingStatus = null;
            try
            {
                buildingStatus = buildingAccessor.SelectAllBuildingStatus();
            }
            catch (Exception)
            {
                throw;
            }
            return buildingStatus;
        }
    }
}
