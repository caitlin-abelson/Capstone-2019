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
            var cmdText = @"sp_select_membertablines";
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
                        var tab = new MemberTabLine()
                        {
                            MemberTabLineID = rdr.GetInt32(0),
                            MemberTabID = rdr.GetInt32(1),
                            OfferingID = rdr.GetInt32(2),
                            Quantity = rdr.GetInt32(3),
                            Price = (decimal)rdr.GetSqlMoney(4),
                            EmployeeID = rdr.GetInt32(5),
                            Discount = rdr.GetDecimal(6),
                            GuestID = rdr.GetInt32(7),
                            PurchasedDate = rdr.GetDateTime(8)
                        };
                        memberTabLines.Add(tab);
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

            return memberTabLines;
        }
    }
}
