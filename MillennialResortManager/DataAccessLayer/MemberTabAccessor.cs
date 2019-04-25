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
    /// James Heim
    /// Created 2019-04-18
    /// </summary>
    public class MemberTabAccessor : IMemberTabAccessor
    {
        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Insert a new MemberTab for the Member if no other
        /// active tab exists.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public int InsertMemberTab(int memberID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_membertab";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", memberID);


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
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Select the only active MemberTab for the specified MemberID.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public MemberTab SelectActiveMemberTabByMemberID(int memberID)
        {
            MemberTab memberTab = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_active_membertab_by_member_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", memberID);


            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    memberTab = new MemberTab()
                    {
                        MemberTabID = reader.GetInt32(0),
                        MemberID = reader.GetInt32(1),
                        Active = reader.GetBoolean(2),
                        TotalPrice = (decimal) reader.GetSqlMoney(3)
                    };
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

            return memberTab;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Select the MemberTab by the specified MemberTabID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MemberTab SelectMemberTabByID(int id)
        {
            MemberTab memberTab = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_membertab_by_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberTabID", id);


            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    memberTab = new MemberTab()
                    {
                        MemberTabID = reader.GetInt32(0),
                        MemberID = reader.GetInt32(1),
                        Active = reader.GetBoolean(2),
                        TotalPrice = (decimal)reader.GetSqlMoney(3)
                    };
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

            return memberTab;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Set the MemberTab inactive if it's active.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns></returns>
        public int UpdateMemberTabSetInactive(int memberTabID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_set_inactive_membertab_by_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberTabID", memberTabID);


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
