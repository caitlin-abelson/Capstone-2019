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
    /// Created : 02/14/2019
    /// The MaintenanceWorkOrderAccessorMSSQL is an implementation of the IMaintenanceWorkOrderAccessor and is designed to access
    /// a MSSQL database and work with data related to Maintenance Work Orders
    /// </summary>
    public class MaintenanceWorkOrderAccessorMSSQL : IMaintenanceWorkOrderAccessor
    {
        public MaintenanceWorkOrderAccessorMSSQL()
        {
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 02/14/2019
        /// Creates a Work Order for the MaintenanceWorkOrder Table in our Microsoft SQL Server
        /// </summary>
        public void CreateMaintenanceWorkOrder(MaintenanceWorkOrder newMaintenanceWorkOrder)
        {

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_create_work_order";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            //We do not have a MaintenanceWorkOrderID since we have not created the Work Order in our database yet
            cmd1.Parameters.AddWithValue("@MaintenanceTypeID", newMaintenanceWorkOrder.MaintenanceTypeID);
            cmd1.Parameters.AddWithValue("@DateRequested", DateTime.Now);
            cmd1.Parameters.AddWithValue("@RequestingEmployeeID", newMaintenanceWorkOrder.RequestingEmployeeID);
            cmd1.Parameters.AddWithValue("@WorkingEmployeeID", newMaintenanceWorkOrder.WorkingEmployeeID);
            cmd1.Parameters.AddWithValue("@Description", newMaintenanceWorkOrder.Description);
            cmd1.Parameters.AddWithValue("@MaintenanceStatusID", newMaintenanceWorkOrder.MaintenanceStatusID);
            cmd1.Parameters.AddWithValue("@ResortPropertyID", newMaintenanceWorkOrder.ResortPropertyID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd1.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Deactivate Work Order will attempt to change the active field to false for the Work Order with a matching WorkOrderID in our MSSQL database
        /// </summary>
        public void DeactivateMaintenanceWorkOrder(int MaintenanceWorkOrderID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_deactivate_work_order";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@MaintenanceWorkOrderID", MaintenanceWorkOrderID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                if (cmd1.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("No Maintenance Work Order found with that ID");
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
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Deactivate Work Order will attempt to delete the Work Order with a matching WorkOrderID in our MSSQL database
        /// </summary>
        public void PurgeMaintenanceWorkOrder(int MaintenanceWorkOrderID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_delete_work_order";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@MaintenanceWorkOrderID", MaintenanceWorkOrderID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                if (cmd1.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("No Maintenance Work Order found with that ID");
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
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Retrieve All Work Orders will attempt to select and return every single Work Order from our MSSQL database in the form of a List
        /// </summary>
        public List<MaintenanceWorkOrder> RetrieveAllMaintenanceWorkOrders()
        {
            List<MaintenanceWorkOrder> maintenanceWorkOrders = new List<MaintenanceWorkOrder>();

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_select_all_work_orders";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MaintenanceWorkOrder maintenanceWorkOrder = new MaintenanceWorkOrder();
                        maintenanceWorkOrder.MaintenanceWorkOrderID = reader.GetInt32(0);
                        maintenanceWorkOrder.MaintenanceTypeID = reader.GetString(1);
                        maintenanceWorkOrder.DateRequested = reader.GetDateTime(2);

                        if(reader.IsDBNull(3))
                        {
                            maintenanceWorkOrder.DateCompleted = null;
                        }
                        else
                        {
                            maintenanceWorkOrder.DateCompleted = reader.GetDateTime(3);
                        }
                        maintenanceWorkOrder.RequestingEmployeeID = reader.GetInt32(4);
                        maintenanceWorkOrder.WorkingEmployeeID = reader.GetInt32(5);
                        maintenanceWorkOrder.Description = reader.GetString(6);
                        if (reader.IsDBNull(7))
                        {
                            maintenanceWorkOrder.Comments = null;
                        }
                        else
                        {
                            maintenanceWorkOrder.Comments = reader.GetString(7);
                        }
                        maintenanceWorkOrder.MaintenanceStatusID = reader.GetString(8);
                        maintenanceWorkOrder.ResortPropertyID = reader.GetInt32(9);
                        maintenanceWorkOrder.Complete = reader.GetBoolean(10);
                        maintenanceWorkOrders.Add(maintenanceWorkOrder);
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
            return maintenanceWorkOrders;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Retrieve All Work Orders will attempt to select and return every single Work Order from our MSSQL database in the form of a List
        /// </summary>
        public MaintenanceWorkOrder RetrieveMaintenanceWorkOrder(int MaintenanceWorkOrderID)
        {
            MaintenanceWorkOrder maintenanceWorkOrder = new MaintenanceWorkOrder();

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_select_work_order_by_id";

            // command objects
            var cmd1 = new SqlCommand(cmdText, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            //We do not have a WorkOrderID since we have not created the WorkOrder in our database yet
            cmd1.Parameters.AddWithValue("@MaintenanceWorkOrderID", MaintenanceWorkOrderID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        maintenanceWorkOrder.MaintenanceWorkOrderID = reader.GetInt32(0);
                        maintenanceWorkOrder.MaintenanceTypeID = reader.GetString(1);
                        maintenanceWorkOrder.DateRequested = reader.GetDateTime(2);
                        if (reader.IsDBNull(3))
                        {
                            maintenanceWorkOrder.DateCompleted = null;
                        }
                        else
                        {
                            maintenanceWorkOrder.DateCompleted = reader.GetDateTime(3);
                        }
                        maintenanceWorkOrder.RequestingEmployeeID = reader.GetInt32(4);
                        maintenanceWorkOrder.WorkingEmployeeID = reader.GetInt32(5);
                        maintenanceWorkOrder.Description = reader.GetString(6);
                        if (reader.IsDBNull(7))
                        {
                            maintenanceWorkOrder.Comments = null;
                        }
                        else
                        {
                            maintenanceWorkOrder.Comments = reader.GetString(7);
                        }
                        maintenanceWorkOrder.MaintenanceStatusID = reader.GetString(8);
                        maintenanceWorkOrder.ResortPropertyID = reader.GetInt32(9);
                        maintenanceWorkOrder.Complete = reader.GetBoolean(10);
                        break;
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
            return maintenanceWorkOrder;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 2/21/2019
        /// Update Work Order will attempt to update the Work Order from our MSSQL database
        /// </summary>
        public void UpdateMaintenanceWorkOrder(MaintenanceWorkOrder oldMaintenanceWorkOrder, MaintenanceWorkOrder newMaintenanceWorkOrder)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_update_work_order";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@MaintenanceWorkOrderID", oldMaintenanceWorkOrder.MaintenanceWorkOrderID);
            cmd1.Parameters.AddWithValue("@oldDateRequested", oldMaintenanceWorkOrder.DateRequested);
            cmd1.Parameters.AddWithValue("@oldMaintenanceTypeID", oldMaintenanceWorkOrder.MaintenanceTypeID);
            cmd1.Parameters.AddWithValue("@oldRequestingEmployeeID", oldMaintenanceWorkOrder.RequestingEmployeeID);
            cmd1.Parameters.AddWithValue("@oldWorkingEmployeeID", oldMaintenanceWorkOrder.WorkingEmployeeID);
            cmd1.Parameters.AddWithValue("@oldDescription", oldMaintenanceWorkOrder.Description);
            cmd1.Parameters.AddWithValue("@oldMaintenanceStatusID", oldMaintenanceWorkOrder.MaintenanceStatusID);
            cmd1.Parameters.AddWithValue("@oldResortPropertyID", oldMaintenanceWorkOrder.ResortPropertyID);
            cmd1.Parameters.AddWithValue("@oldComplete", oldMaintenanceWorkOrder.Complete);

            cmd1.Parameters.AddWithValue("@newMaintenanceTypeID", newMaintenanceWorkOrder.MaintenanceTypeID);
            cmd1.Parameters.AddWithValue("@newRequestingEmployeeID", newMaintenanceWorkOrder.RequestingEmployeeID);
            cmd1.Parameters.AddWithValue("@newWorkingEmployeeID", newMaintenanceWorkOrder.WorkingEmployeeID);
            cmd1.Parameters.AddWithValue("@newDescription", newMaintenanceWorkOrder.Description);
            cmd1.Parameters.AddWithValue("@newComments", newMaintenanceWorkOrder.Comments);
            cmd1.Parameters.AddWithValue("@newMaintenanceStatusID", newMaintenanceWorkOrder.MaintenanceStatusID);
            cmd1.Parameters.AddWithValue("@newResortPropertyID", newMaintenanceWorkOrder.ResortPropertyID);
            cmd1.Parameters.AddWithValue("@newComplete", newMaintenanceWorkOrder.Complete);
            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd1.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
