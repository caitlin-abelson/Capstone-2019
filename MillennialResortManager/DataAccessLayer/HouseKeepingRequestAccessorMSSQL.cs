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
    /// Created : 3/27/2019
    /// The HouseKeepingRequestAccessorMSSQL is an implementation of the IHouseKeepingRequestAccessor and is designed to access
    /// a MSSQL database and work with data related to HouseKeepingRequests
    /// </summary>
    public class HouseKeepingRequestAccessorMSSQL : IHouseKeepingRequestAccessor
    {
        public HouseKeepingRequestAccessorMSSQL()
        {
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Creates a HouseKeepingRequest for the HouseKeepingRequest Table in our Microsoft SQL Server
        /// </summary>
        public void CreateHouseKeepingRequest(HouseKeepingRequest newHouseKeepingRequest)
        {

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_create_house_keeping_request";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            //We do not have a HouseKeepingRequestID since we have not created the Request in our database yet
            cmd1.Parameters.AddWithValue("@BuildingNumber",newHouseKeepingRequest.BuildingNumber);
            cmd1.Parameters.AddWithValue("@RoomNumber", newHouseKeepingRequest.RoomNumber);
            cmd1.Parameters.AddWithValue("@Description", newHouseKeepingRequest.Description);
            cmd1.Parameters.AddWithValue("@Active", newHouseKeepingRequest.Active);

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
        /// Created : 3/27/2019
        /// DeactivateHouseKeepingRequest will attempt to change the active field to false for the Work Order with a matching HouseKeepingRequestID in our MSSQL database
        /// </summary>
        public void DeactivateHouseKeepingRequest(int HouseKeepingRequestID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_deactivate_house_keeping_request";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@HouseKeepingRequestID", HouseKeepingRequestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                if (cmd1.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("No HouseKeepingRequest could be found with that ID");
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
        /// Created : 3/27/2019
        /// Purge HouseKeepingRequest will attempt to delete the HouseKeepingRequest with a matching HouseKeepingRequestID in our MSSQL database
        /// </summary>
        public void PurgeHouseKeepingRequest(int HouseKeepingRequestID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_delete_house_keeping_request";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@HouseKeepingRequestID", HouseKeepingRequestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                if (cmd1.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("No HouseKeepingRequest could be found with that ID");
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
        /// Created : 3/27/2019
        /// Retrieve All HouseKeepingRequests will attempt to select and return every single HouseKeepingRequest from our MSSQL database in the form of a List
        /// </summary>
        public List<HouseKeepingRequest> RetrieveAllHouseKeepingRequests()
        {
            List<HouseKeepingRequest> houseKeepingRequests = new List<HouseKeepingRequest>();

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_select_all_house_keeping_requests";

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
                        HouseKeepingRequest houseKeepingRequest = new HouseKeepingRequest();
                        houseKeepingRequest.HouseKeepingRequestID = reader.GetInt32(0);
                        houseKeepingRequest.BuildingNumber = reader.GetInt32(1);
                        houseKeepingRequest.RoomNumber = reader.GetInt32(2);
                        houseKeepingRequest.Description = reader.GetString(3);
                        if (reader.IsDBNull(4))
                        {
                            houseKeepingRequest.WorkingEmployeeID = null;
                        }
                        else
                        {
                            houseKeepingRequest.WorkingEmployeeID = reader.GetInt32(4);
                        }
                        houseKeepingRequest.Active = reader.GetBoolean(5);
                        houseKeepingRequests.Add(houseKeepingRequest);
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
            return houseKeepingRequests;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Select HouseKeepingRequest by ID will attempt to select and return a HouseKeepingRequest from our MSSQL database with the matching ID
        /// </summary>
        public HouseKeepingRequest RetrieveHouseKeepingRequest(int HouseKeepingRequestID)
        {
            HouseKeepingRequest houseKeepingRequest = new HouseKeepingRequest();

            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_select_house_keeping_request_by_id";

            // command objects
            var cmd1 = new SqlCommand(cmdText, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            //We do not have a WorkOrderID since we have not created the WorkOrder in our database yet
            cmd1.Parameters.AddWithValue("@HouseKeepingRequestID", HouseKeepingRequestID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        houseKeepingRequest.HouseKeepingRequestID = reader.GetInt32(0);
                        houseKeepingRequest.BuildingNumber = reader.GetInt32(1);
                        houseKeepingRequest.RoomNumber = reader.GetInt32(2);
                        houseKeepingRequest.Description = reader.GetString(3);
                        if (reader.IsDBNull(4))
                        {
                            houseKeepingRequest.WorkingEmployeeID = null;
                        }
                        else
                        {
                            houseKeepingRequest.WorkingEmployeeID = reader.GetInt32(4);
                        }
                        houseKeepingRequest.Active = reader.GetBoolean(5);
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
            return houseKeepingRequest;
        }

        /// <summary>
        /// Author: Dalton Cleveland
        /// Created : 3/27/2019
        /// Update HouseKeepingRequest will attempt to update the HouseKeepingRequest from our MSSQL database
        /// </summary>
        public void UpdateHouseKeepingRequest(HouseKeepingRequest oldHouseKeepingRequest, HouseKeepingRequest newHouseKeepingRequest)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_update_house_keeping_request";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@HouseKeepingRequestID", oldHouseKeepingRequest.HouseKeepingRequestID);

            cmd1.Parameters.AddWithValue("@oldBuildingNumber", oldHouseKeepingRequest.BuildingNumber);
            cmd1.Parameters.AddWithValue("@oldRoomNumber", oldHouseKeepingRequest.RoomNumber);
            cmd1.Parameters.AddWithValue("@oldDescription", oldHouseKeepingRequest.Description);
            cmd1.Parameters.AddWithValue("@oldActive", oldHouseKeepingRequest.Active);

            cmd1.Parameters.AddWithValue("@newBuildingNumber", newHouseKeepingRequest.BuildingNumber);
            cmd1.Parameters.AddWithValue("@newRoomNumber", newHouseKeepingRequest.RoomNumber);
            cmd1.Parameters.AddWithValue("@newDescription", newHouseKeepingRequest.Description);
            cmd1.Parameters.AddWithValue("@newWorkingEmployeeID", newHouseKeepingRequest.WorkingEmployeeID);
            cmd1.Parameters.AddWithValue("@newActive", newHouseKeepingRequest.Active);
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
