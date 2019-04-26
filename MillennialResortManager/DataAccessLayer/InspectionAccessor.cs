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

        /// <summary>
        /// Danielle Russo
        /// Created: 2019/04/26
        /// 
        /// Updates inspection details
        /// </summary>
        /// <param name="oldInspection">The old inspection details</param>
        /// <param name="newInspection">The updated inspection</param>
        /// <returns></returns>
        public int UpdateInspection(Inspection oldInspection, Inspection newInspection)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_inspection";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ResortPropertyID", oldInspection.ResortPropertyID);
            cmd.Parameters.AddWithValue("@InspectionID", oldInspection.InspectionID);

            cmd.Parameters.AddWithValue("@OldName", oldInspection.Name);
            cmd.Parameters.AddWithValue("@OldDateInspected", oldInspection.DateInspected);
            cmd.Parameters.AddWithValue("@OldRating", oldInspection.Rating);
            cmd.Parameters.AddWithValue("@OldResortInspectionAffiliation", oldInspection.ResortInspectionAffiliation);
            cmd.Parameters.AddWithValue("@OldInspectionProblemNotes", oldInspection.InspectionProblemNotes);
            cmd.Parameters.AddWithValue("@OldInspectionFixNotes", oldInspection.InspectionFixNotes);

            cmd.Parameters.AddWithValue("@NewName", newInspection.Name);
            cmd.Parameters.AddWithValue("@NewDateInspected", newInspection.DateInspected);
            cmd.Parameters.AddWithValue("@NewRating", newInspection.Rating);
            cmd.Parameters.AddWithValue("@NewResortInspectionAffiliation", newInspection.ResortInspectionAffiliation);
            cmd.Parameters.AddWithValue("@NewInspectionProblemNotes", newInspection.InspectionProblemNotes);
            cmd.Parameters.AddWithValue("@NewInspectionFixNotes", newInspection.InspectionFixNotes);

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
