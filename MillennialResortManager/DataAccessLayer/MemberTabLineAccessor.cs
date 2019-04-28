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
    /// Added by Matt H. on 4/26/19
    /// Accessor class that calls sp to retrieve a list of MemberTabLine.
    /// Implements the IMemberTabLineAccessor Interface.
    /// </summary>
    public class MemberTabLineAccessor : IMemberTabLineAccessor
    {
        public List<MemberTabLine> SelectMemberTabLineByMemberTabID(int id)
        {
            List<MemberTabLine> memberTabLines = new List<MemberTabLine>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_member_tab_line_by_member_tab_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberTabID", id);

            try
            {
                conn.Open();

                var rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        memberTabLines.Add(new MemberTabLine
                        {
                            MemberTabLineID = rdr.GetInt32(0),
                            MemberTabID = rdr.GetInt32(1),
                            OfferingTypeID = rdr.GetString(2),
                            Description = rdr.GetString(3),
                            Quantity = rdr.GetInt32(4),
                            Price = rdr.GetDecimal(5),
                            EmployeeID = rdr.GetInt32(6),
                            GuestID = rdr.GetInt32(7),
                            PurchasedDate = rdr.GetDateTime(8)
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

            return memberTabLines;
        }
    }
}
