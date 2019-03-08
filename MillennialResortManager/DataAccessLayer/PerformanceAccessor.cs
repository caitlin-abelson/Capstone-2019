using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Jacob Miller
    /// Created: 2019/01/22
    /// </summary>
    public class PerformanceAccessor : IPerformanceAccessor
    {
        /// <summary>
        /// Author: Jacob Miller
        /// Created: 2019/1/22
        /// Adds a new Performance to the DataBase
        /// Note: Even though a Performance object is passed this method will ignore the give object's ID as it is not necessary,
        /// the Database will assign an auto generated number for it.
        /// </summary>
        /// <param name="perf">The Performance to be added</param>
        /// <returns>Rows affected... should just be one</returns>
        public int InsertPerformance(Performance perf)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_insert_performance";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PerformanceName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PerformanceDate", SqlDbType.Date);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1000);
            cmd.Parameters["@PerformanceName"].Value = perf.Name;
            cmd.Parameters["@PerformanceDate"].Value = perf.Date;
            cmd.Parameters["@Description"].Value = perf.Description;
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
        /// Author: Jacob Miller
        /// Created: 2019/1/22
        /// Retrieves a performance based on its ID
        /// </summary>
        /// <param name="id">The ID of the targe peroformance</param>
        /// <returns>The performance with the matching ID</returns>
        public Performance SelectPerformanceByID(int id)
        {
            Performance performance = null;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_performance_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PerformanceID", SqlDbType.Int);
            cmd.Parameters["@PerformanceID"].Value = id;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        DateTime date = reader.GetDateTime(2);
                        string desc = reader.GetString(3);
                        performance = new Performance(ID, name, date, desc);
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
            return performance;
        }
        /// <summary>
        /// Author: Jacob Miller
        /// Created: 2019/1/22
        /// </summary>
        /// <param name="perf">The performance containing updated information</param>
        /// <returns></returns>
        public int UpdatePerformance(Performance perf)
        {
            int rows = 0;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_update_performance";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PerformanceID", SqlDbType.Int);
            cmd.Parameters.Add("@PerformanceName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PerformanceDate", SqlDbType.Date);
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 1000);
            cmd.Parameters["@PerformanceID"].Value = perf.ID;
            cmd.Parameters["@PerformanceName"].Value = perf.Name;
            cmd.Parameters["@PerformanceDate"].Value = perf.Date;
            cmd.Parameters["@Description"].Value = perf.Description;
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
        /// Author: Jacob Miller
        /// Created: 2019/1/22
        /// </summary>
        /// <returns>All Performances in the DB</returns>
        public List<Performance> SelectAllPerformance()
        {
            List<Performance> performances = new List<Performance>();
            var conn = DBConnection.GetDbConnection();
            string cmdText = "sp_select_all_performance";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        DateTime date = reader.GetDateTime(2);
                        string desc = reader.GetString(3);
                        performances.Add(new Performance(ID, name, date, desc));
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
            return performances;
        }
        /// <summary>
        /// Author: Jacob Miller
        /// Created: 2019/1/22
        /// </summary>
        /// <param name="term">What you would like to search for</param>
        /// <returns>Performances that contain the search term in thier name</returns>
        public List<Performance> SearchPerformances(string term)
        {
            List<Performance> performances = new List<Performance>();
            var conn = DBConnection.GetDbConnection();
            string cmdText = "sp_search_performances";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SearchTerm", SqlDbType.NVarChar, 50);
            cmd.Parameters["@SearchTerm"].Value = term;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int ID = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        DateTime date = reader.GetDateTime(2);
                        string desc = reader.GetString(3);
                        performances.Add(new Performance(ID, name, date, desc));
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
            return performances;
        }
        /// <summary>
        /// Author: Jacob Miller
        /// Created: 2019/1/22
        /// This method takes a Performance object for simplicity and uniformality but will only use the ID
        /// </summary>
        /// <param name="perf">The performance to be deleted</param>
        public void DeletePerformance(Performance perf)
        {
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_delete_performance";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PerformanceID", SqlDbType.Int);
            cmd.Parameters["@PerformanceID"].Value = perf.ID;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
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
