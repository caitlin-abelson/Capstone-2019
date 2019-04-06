/// <summary>
/// Danielle Russo
/// Created: 2019/02/28
/// 
/// Class that interacts with the Building Table data
/// </summary>
/// 
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
    public class InspectionAccessor : IInspectionAccessor
    {
        /// <summary>
        /// Danielle Russo
        /// Created: 2019/02/28
        /// 
        /// Creates a new Inspection 
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newInspection">The Inspection obj. to be added.</param>
        /// <returns>Rows created</returns>
        public int InsertInspection(Inspection newInspection)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_inspection";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ResortPropertyID", newInspection.ResortPropertyID);
            cmd.Parameters.AddWithValue("@Name", newInspection.Name);
            cmd.Parameters.AddWithValue("@DateInspected", newInspection.DateInspected);
            cmd.Parameters.AddWithValue("@Rating", newInspection.Rating);
            cmd.Parameters.AddWithValue("@ResortInspectionAffiliation", newInspection.ResortInspectionAffiliation);
            cmd.Parameters.AddWithValue("@InspectionProblemNotes", newInspection.InspectionProblemNotes);
            cmd.Parameters.AddWithValue("@InspectionFixNotes", newInspection.InspectionFixNotes);

            try
            {
                conn.Open();

                newInspection.InspectionID = cmd.ExecuteNonQuery();
                rows++;
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
        /// Created: 2019/03/14
        /// 
        /// Returns a list of inspections based off of the resort property
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newInspection">The Inspection obj. to be added.</param>
        /// <returns>List of inspections</returns>
        public List<Inspection> SelectAllInspectionsByResortPropertyID(int resortProperyId)
        {
            List<Inspection> inspections = new List<Inspection>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_inspection_by_resortpropertyid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ResortPropertyID", resortProperyId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        inspections.Add(new Inspection
                        {
                            InspectionID = reader.GetInt32(0),
                            ResortPropertyID = resortProperyId,
                            Name = reader.GetString(1),
                            DateInspected = reader.GetDateTime(2),
                            Rating = reader.GetString(3),
                            ResortInspectionAffiliation = reader.GetString(4),
                            InspectionProblemNotes = reader.GetString(5),
                            InspectionFixNotes = reader.GetString(6)
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return inspections;
        }
    }
}
