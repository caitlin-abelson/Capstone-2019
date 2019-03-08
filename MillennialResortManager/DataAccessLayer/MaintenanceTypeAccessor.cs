using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Austin Berquam
    /// Created: 2019/03/04
    /// 
    /// MaintenanceTypeAccessor class is used to access the Maintenance table
    /// and the stored procedures as well
    /// </summary>
    public class MaintenanceTypeAccessor : IMaintenanceTypeAccessor
    {
        /// <summary>
        /// Method that retrieves the Maintenance table and stores it as a list
        /// </summary>
        /// <returns>List of Types </returns>	
        public List<MaintenanceTypes> SelectAllMaintenanceTypes(string status)
        {
            List<MaintenanceTypes> types = new List<MaintenanceTypes>();

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_maintenance_types";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        types.Add(new MaintenanceTypes()
                        {
                            MaintenanceTypeID = reader.GetString(0),
                            Description = reader.GetString(1)

                        });
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

            return types;
        }

        /// <summary>
        /// Method that retrieves the MaintenanceTypes to store into a combo box
        /// </summary>
        /// <returns>List of Maintenance Types </returns>	
        public List<string> SelectAllMaintenanceTypeID()
        {
            var types = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_maintenanceTypes", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        types.Add(r.GetString(0));
                    }
                }
                r.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return types;
        }

        /// <summary>
        /// Method that creates a new MaintenanceType and stores it in the table
        /// </summary>
        /// <param name="maintenanceTypes">Object holding the data to add to the table</param>
        /// <returns> Row Count </returns>	
        public int InsertMaintenanceType(MaintenanceTypes maintenanceTypes)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_maintenance_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaintenanceTypeID", maintenanceTypes.MaintenanceTypeID);
            cmd.Parameters.AddWithValue("@Description", maintenanceTypes.Description);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;

        }

        /// <summary>
        /// Method that deletes a MaintenanceType and removes it from the table
        /// </summary>
        /// <param name="maintenanceTypeID">The ID of the MaintenanceType being deleted</param>
        /// <returns> Row Count </returns>
        public int DeleteMaintenanceType(string maintenanceTypeID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_maintenance_type";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaintenanceTypeID", maintenanceTypeID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
    }
}
