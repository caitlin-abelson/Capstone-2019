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
    /// The MaintenanceStatusTypeAccessorMSSQL is an implementation of the IMaintenanceStatusTypeAccessor interface and  is designed to access a MSSQL database and work with data related to MaintenanceStatusTypes
    /// </summary>
    public class MaintenanceStatusTypeAccessorMSSQL : IMaintenanceStatusTypeAccessor
    {
        public MaintenanceStatusTypeAccessorMSSQL()
        {

        }
        public void CreateMaintenanceStatusType(MaintenanceStatusType newMaintenanceStatusType)
        {
            throw new NotImplementedException();
        }

        public void DeleteMaintenanceStatusType(MaintenanceStatusType deletedMaintenanceStatusType)
        {
            throw new NotImplementedException();
        }
    /// <summary>
    /// Author: Dalton Cleveland
    /// Created : 3/5/2019
    /// RetrieveAllMaintenanceStatusTypes will select all of the MaintenanceStatusTypes from our Database
    /// </summary>
    public List<MaintenanceStatusType> RetrieveAllMaintenanceStatusTypes()
        {
            List<MaintenanceStatusType> maintenanceStatusTypes = new List<MaintenanceStatusType>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText2 = @"sp_retrieve_maintenance_status_types";

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
                        MaintenanceStatusType maintenanceStatusType = new MaintenanceStatusType();
                        maintenanceStatusType.MaintenanceStatusID = reader2.GetString(0);
                        maintenanceStatusType.Description = reader2.GetString(1);
                        maintenanceStatusTypes.Add(maintenanceStatusType);
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
            return maintenanceStatusTypes;
        }

        public MaintenanceStatusType RetrieveMaintenanceStatusType()
        {
            throw new NotImplementedException();
        }
    }
}