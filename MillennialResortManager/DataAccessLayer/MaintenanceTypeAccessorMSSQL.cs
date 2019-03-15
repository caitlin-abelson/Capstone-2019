using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/5/2019
    /// The MaintenanceTypeAccessorMSSQL is an implementation of the IMaintenanceTypeAccessor interface and  is designed to access a MSSQL database and work with data related to MaintenanceTypes
    /// </summary>
    public class MaintenanceTypeAccessorMSSQL : IMaintenanceTypeAccessor
    {
        public MaintenanceTypeAccessorMSSQL()
        {

        }
        public void CreateMaintenanceType(MaintenanceType newMaintenanceType)
        {
            throw new NotImplementedException();
        }

        public void DeleteMaintenanceType(MaintenanceType deletedMaintenanceType)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/5/2019
        /// RetrieveAllMaintenanceTypes will select all of the MaintenanceTypes from our Database who have an Complete Status of 1 and return them
        /// </summary>
        /// <returns>Returns a List of all Members</returns>
        public List<MaintenanceType> RetrieveAllMaintenanceTypes()
        {
            List<MaintenanceType> maintenanceTypes = new List<MaintenanceType>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText2 = @"sp_retrieve_maintenance_types";

            // command objects
            var cmd2 = new SqlCommand(cmdText2, conn);

            // set the command type
            cmd2.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        MaintenanceType maintenanceType = new MaintenanceType();
                        maintenanceType.MaintenanceTypeID = reader2.GetString(0);
                        maintenanceType.Description = reader2.GetString(1);
                        maintenanceTypes.Add(maintenanceType);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return maintenanceTypes;
        }

        public MaintenanceType RetrieveMaintenanceType()
        {
            throw new NotImplementedException();
        }
    }
}