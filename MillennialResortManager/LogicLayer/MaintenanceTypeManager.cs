using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Austin Berquam
    /// Created: 2019/03/04
    /// 
    /// Used to manage the MaintenanceTypes table
    /// and the stored procedures as well
    /// </summary>
    public class MaintenanceTypeManager : IMaintenanceTypeManager
    {

        IMaintenanceTypeAccessor maintenanceTypeAccessor;

        public MaintenanceTypeManager()
        {
            maintenanceTypeAccessor = new MaintenanceTypeAccessor();
        }
        public MaintenanceTypeManager(MockMaintenanceTypeAccessor mock)
        {
            maintenanceTypeAccessor = new MockMaintenanceTypeAccessor();
        }
        /// <summary>
        /// Method that collects the MaintenanceType from the accessor
        /// </summary>
        /// <returns> List of MaintenanceTypes </returns>
        public List<MaintenanceTypes> RetrieveMaintenanceTypes(string status)
        {
            List<MaintenanceTypes> types = null;
            if (status != "")
            {
                try
                {
                    types = maintenanceTypeAccessor.SelectAllMaintenanceTypes(status);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return types;
        }

        /// <summary>
        /// Method that sends the created maintenanceType to the accessor
        /// </summary>
        /// <param name="maintenanceType">Object holding the new maintenanceType to add to the table</param>
        /// <returns> bool on if the role was created </returns>
        public bool CreateMaintenanceType(MaintenanceTypes maintenanceType)
        {

            ValidationExtensionMethods.ValidateID(maintenanceType.MaintenanceTypeID);
            ValidationExtensionMethods.ValidateDescription(maintenanceType.Description);
            bool result = false;

            try
            {
                result = (1 == maintenanceTypeAccessor.InsertMaintenanceType(maintenanceType));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Method that deletes a maintenanceType through the accessor
        /// </summary>
        /// <param name="maintenanceTypeID">String of maintenanceTypeId to delete</param>
        /// <returns> bool on if the guest was deleted </returns>
        public bool DeleteMaintenanceType(string maintenanceTypeID)
        {
            bool result = false;

            try
            {
                result = (1 == maintenanceTypeAccessor.DeleteMaintenanceType(maintenanceTypeID));
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Method that retrieves all MaintenanceTypes and stores it in a list
        /// </summary>
        /// <returns> MaintenanceTypes in a List returns>
        public List<string> RetrieveAllMaintenanceTypes()
        {
            List<string> types = null;
            try
            {
                types = maintenanceTypeAccessor.SelectAllMaintenanceTypeID();
            }
            catch (Exception)
            {
                throw;
            }
            return types;
        }
    }
}

