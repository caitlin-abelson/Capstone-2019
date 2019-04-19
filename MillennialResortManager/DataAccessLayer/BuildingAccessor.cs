/// <summary>
/// Danielle Russo
/// Created: 2019/01/21
/// 
/// Class that interacts with the Building Table data
/// </summary>
/// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Transactions;

namespace DataAccessLayer
{

    public class BuildingAccessor : IBuildingAccessor
    {


        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/21
        /// 
        /// Creates a new Building 
        /// </summary>
        ///
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/02/13
        /// 
        /// Added additional parameters (@BuildingName and @Address).
        /// 
        /// </remarks>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/02/18
        /// 
        /// Added @BuildingStatusID
        /// 
        /// </remarks>
        /// <param name="newBuilding">The Building obj. to be added.</param>
        /// <returns>Rows created</returns>
        public int InsertBuilding(Building newBuilding)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_building";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BuildingID", newBuilding.BuildingID);
            cmd.Parameters.AddWithValue("@BuildingName", newBuilding.Name);
            cmd.Parameters.AddWithValue("@Address", newBuilding.Address);
            cmd.Parameters.AddWithValue("@Description", newBuilding.Description);
            cmd.Parameters.AddWithValue("@BuildingStatusID", newBuilding.StatusID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/30
        /// 
        /// List of all buildings in the Building table.
        /// </summary>
        ///
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/02/13
        /// 
        /// Added Name and Address to Building constructor arguments
        /// 
        /// </remarks>
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/02/18
        /// 
        /// Added StatusID to constructor arguments
        /// 
        /// </remarks>
        /// Danielle Russo
        /// Updated: 2019/03/01
        /// Added ResortPropertyID to constructor arguments
        /// 
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>A list of Building objs.</returns>
        public List<Building> SelectAllBuildings()
        {
            List<Building> buildings = new List<Building>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_buildings";
            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        buildings.Add(new Building()
                        {
                            BuildingID = reader.GetString(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            Description = reader.GetString(3),
                            StatusID = reader.GetString(4),
                            ResortPropertyID = reader.GetInt32(5)
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

            return buildings;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-17
        /// 
        /// Select a building object by the specified Building ID.
        /// </summary>
        /// <param name="buildingID"></param>
        /// <returns></returns>
        public Building SelectBuildingByID(string buildingID)
        {
            Building building = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_building_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@BuildingID", buildingID);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    building = new Building()
                    {
                        BuildingID = reader.GetString(0),

                        StatusID = reader.GetString(4),
                        ResortPropertyID = reader.GetInt32(5)

                    };

                    // Nullables:
                    // Name.
                    if (reader.IsDBNull(1))
                    {
                        building.Name = null;
                    }
                    else
                    {
                        building.Name = reader.GetString(1);
                    }

                    // Address.
                    if (reader.IsDBNull(2))
                    {
                        building.Address = null;
                    }
                    else
                    {
                        building.Address = reader.GetString(2);
                    }

                    // Description
                    if (reader.IsDBNull(3))
                    {
                        building.Description = null;
                    }
                    else
                    {
                        building.Description = reader.GetString(3);
                    }





                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return building;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/01/31
        /// 
        /// Updates Building details
        /// </summary>
        ///
        /// <remarks>
        /// Danielle Russo
        /// Updated: 2019/02/13
        /// 
        /// Added additional parameters
        /// 
        /// </remarks>
        ///
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Number of Buildings updated in the table</returns>
        public int UpdateBuilding(Building oldBuilding, Building updatedBuilding)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_building";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BuildingID", oldBuilding.BuildingID);

            cmd.Parameters.AddWithValue("@NewBuildingName", updatedBuilding.Name);
            cmd.Parameters.AddWithValue("@NewAddress", updatedBuilding.Address);
            cmd.Parameters.AddWithValue("@NewDescription", updatedBuilding.Description);
            cmd.Parameters.AddWithValue("@NewBuildingStatusID", updatedBuilding.StatusID);


            cmd.Parameters.AddWithValue("@OldBuildingName", oldBuilding.Name);
            cmd.Parameters.AddWithValue("@OldAddress", oldBuilding.Address);
            cmd.Parameters.AddWithValue("@OldDescription", oldBuilding.Description);
            cmd.Parameters.AddWithValue("@OldBuildingStatusID", oldBuilding.StatusID);

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


        public List<Building> SelectAllBuildingsByStatusID(string statusID)
        {
            List<Building> buildings = new List<Building>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_building_by_buildingstatusid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("BuildingStatusID", statusID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        buildings.Add(new Building()
                        {
                            BuildingID = reader.GetString(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            Description = reader.GetString(3),
                            StatusID = reader.GetString(4)
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return buildings;
        }

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/21
        /// 
        /// Populates a list of building status ids
        /// </summary>
        /// <returns>List of building status ids</returns>
        public List<string> SelectAllBuildingStatus()
        {
            var buildingStatus = new List<string>();

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_select_all_statusids", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        buildingStatus.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            } 

            return buildingStatus;
        }
    }
}
