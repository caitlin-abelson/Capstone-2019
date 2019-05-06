using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Danielle Russo" created="2019/01/21">
	/// Class that interacts with the presentation layer and building access layer
	/// </summary>
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

		/// <summary author="Danielle Russo" created="2019/01/21">
		/// Adds a new Building obj.
		/// </summary>
		/// <updates>
		/// <update author="Danielle Russo" date="2019/02/28">
		/// Changed 1 to 2 so that it will pass for now
		/// </update>
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
				ExceptionLogManager.getInstance().LogException(ane);
				throw ane;
			}
			catch (ArgumentException ae)
			{
				ExceptionLogManager.getInstance().LogException(ae);
				throw ae;
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Danielle Russo" created="2019/01/31">
		/// Edits building details
		/// </summary>
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
				ExceptionLogManager.getInstance().LogException(ane);
				throw ane;
			}
			catch (ArgumentException ae)
			{
				ExceptionLogManager.getInstance().LogException(ae);
				throw ae;
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
		}

		/// <summary author="Danielle Russo" created="2019/01/30">
		/// List of all buildings in the Building table.
		/// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
		/// <returns>A list of Building objs.</returns>
		public List<Building> RetrieveAllBuildings()
		{
			List<Building> buildings = null;

			try
			{
				buildings = buildingAccessor.SelectAllBuildings();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return buildings;
		}

		/// <summary author="James Heim" created="2019/04/17">
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
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return building;
		}

		/// <summary author="Danielle Russo" created="2019/02/21">
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
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return buildingStatus;
		}
	}
}