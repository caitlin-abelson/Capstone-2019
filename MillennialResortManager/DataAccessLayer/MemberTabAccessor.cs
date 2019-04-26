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
        /// <returns>Rows affected.</returns>
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
        /// <returns>The only active Tab for the specified Member.</returns>
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

            // If there is an active tab for the Member, 
            // Retrieve the tab lines.
            if (memberTab != null)
            {
               
                memberTab.MemberTabLines = SelectMemberTabLinesByMemberTabID(memberTab.MemberTabID).ToList();
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
            var cmdText1 = @"sp_select_membertab_by_id";
            SqlCommand cmd1 = new SqlCommand(cmdText1, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@MemberTabID", id);

            try
            {
                conn.Open();

                // Retrieve the MemberTab Attributes.
                var reader1 = cmd1.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();

                    
                    memberTab = new MemberTab()
                    {
                        MemberTabID = reader1.GetInt32(0),
                        MemberID = reader1.GetInt32(1),
                        Active = reader1.GetBoolean(2),
                        TotalPrice = (decimal)reader1.GetSqlMoney(3)
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

            // Retrieve the MemberTab Lines.
            if (memberTab != null)
            {
                memberTab.MemberTabLines = SelectMemberTabLinesByMemberTabID(memberTab.MemberTabID).ToList();
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
        public int DeactivateMemberTab(int memberTabID)
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

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Reactivate a MemberTab.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns></returns>
        public int ReactivateMemberTab(int memberTabID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_set_active_membertab_by_id";
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

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Create a new tabline.
        /// </summary>
        /// <param name="memberTabLine"></param>
        /// <returns></returns>
        public int InsertMemberTabLine(MemberTabLine memberTabLine)
        {
            int memberTabLineID = -1;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_membertabline";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MemberTabLineID", SqlDbType.Int);
            cmd.Parameters["@MemberTabLineID"].Direction = ParameterDirection.Output;

            cmd.Parameters.AddWithValue("@MemberTabID", memberTabLine.MemberTabID);
            cmd.Parameters.AddWithValue("@OfferingID", memberTabLine.OfferingID);
            cmd.Parameters.AddWithValue("@Quantity", memberTabLine.Quantity);
            cmd.Parameters.AddWithValue("@Price", memberTabLine.Price);
            cmd.Parameters.AddWithValue("@EmployeeID", memberTabLine.EmployeeID);
            cmd.Parameters.AddWithValue("@Discount", memberTabLine.Discount);
            cmd.Parameters.AddWithValue("@GuestID", memberTabLine.GuestID);
            cmd.Parameters.AddWithValue("@DatePurchased", memberTabLine.DatePurchased);

            try
            {
                conn.Open();

                memberTabLineID = cmd.ExecuteNonQuery();
                memberTabLine.MemberTabLineID = memberTabLineID;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return memberTabLineID;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Select all tablines for the specified Tab.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns></returns>
        public IEnumerable<MemberTabLine> SelectMemberTabLinesByMemberTabID(int memberTabID)
        {
            List<MemberTabLine> tabLines = new List<MemberTabLine>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_membertablines";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberTabID", memberTabID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MemberTabLine tabLine = new MemberTabLine()
                        {
                            MemberTabLineID = reader.GetInt32(0),
                            MemberTabID = reader.GetInt32(1),
                            OfferingID = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            Price = (decimal) reader.GetSqlMoney(4),
                            DatePurchased = reader.GetDateTime(8)
                        };

                        // Employee ID is nullable.
                        if (reader.IsDBNull(5))
                        {
                            tabLine.EmployeeID = null;
                        }
                        else
                        {
                            tabLine.EmployeeID = reader.GetInt32(5);
                        }


                        // Discount is nullable.
                        if (reader.IsDBNull(6))
                        {
                            tabLine.Discount = 0;
                        }
                        else
                        {
                            tabLine.Discount = reader.GetDecimal(6);
                        }

                        // GuestID is nullable.
                        if (reader.IsDBNull(7))
                        {
                            tabLine.GuestID = null;
                        }
                        else
                        {
                            tabLine.GuestID = reader.GetInt32(7);
                        }

                        
                        tabLines.Add(tabLine);
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

            return tabLines;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Select a TabLine by the specified ID.
        /// </summary>
        /// <param name="memberTabLineID"></param>
        /// <returns></returns>
        public MemberTabLine SelectMemberTabLineByID(int memberTabLineID)
        {
            MemberTabLine tabLine = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_membertabline";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberTabLineID", memberTabLineID);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    
                    tabLine = new MemberTabLine()
                    {
                        MemberTabLineID = reader.GetInt32(0),
                        MemberTabID = reader.GetInt32(1),
                        OfferingID = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        Price = (decimal)reader.GetSqlMoney(4),
                        DatePurchased = reader.GetDateTime(8)
                    };

                    // Employee ID is nullable.
                    if (reader.IsDBNull(5))
                    {
                        tabLine.EmployeeID = null;
                    }
                    else
                    {
                        tabLine.EmployeeID = reader.GetInt32(5);
                    }


                    // Discount is nullable.
                    if (reader.IsDBNull(6))
                    {
                        tabLine.Discount = 0;
                    }
                    else
                    {
                        tabLine.Discount = reader.GetDecimal(6);
                    }

                    // GuestID is nullable.
                    if (reader.IsDBNull(7))
                    {
                        tabLine.GuestID = null;
                    }
                    else
                    {
                        tabLine.GuestID = reader.GetInt32(7);
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

            return tabLine;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Delete the specified Line.
        /// </summary>
        /// <param name="memberTabLineID"></param>
        /// <returns></returns>
        public int DeleteMemberTabLine(int memberTabLineID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_membertabline";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberTabLineID", memberTabLineID);


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
        /// Created 2019-04-25
        /// 
        /// Select all Tabs for all Members.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MemberTab> SelectMemberTabs()
        {
            List<MemberTab> tabs = new List<MemberTab>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_membertabs";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                // Retrieve the MemberTab Attributes.
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();


                    MemberTab tab = new MemberTab()
                    {
                        MemberTabID = reader.GetInt32(0),
                        MemberID = reader.GetInt32(1),
                        Active = reader.GetBoolean(2),
                        TotalPrice = (decimal)reader.GetSqlMoney(3)
                    };

                    tabs.Add(tab);

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

            // Get the tablines for each tab.
            foreach (var tab in tabs)
            {
                tab.MemberTabLines = SelectMemberTabLinesByMemberTabID(tab.MemberTabID).ToList();
            }


            return tabs;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Delete the specified Tab.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns></returns>
        public int DeleteMemberTab(int memberTabID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_membertab_by_id";
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

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Select all Tabs a Member has ever had.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public IEnumerable<MemberTab> SelectMemberTabsByMemberID(int memberID)
        {
            List<MemberTab> memberTabs = new List<MemberTab>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_membertabs_by_member_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", memberID);


            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        memberTabs.Add(new MemberTab()
                        {
                            MemberTabID = reader.GetInt32(0),
                            MemberID = reader.GetInt32(1),
                            Active = reader.GetBoolean(2),
                            TotalPrice = (decimal)reader.GetSqlMoney(3)
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


            // Get the tablines for each tab.
            foreach (var tab in memberTabs)
            {
                tab.MemberTabLines = SelectMemberTabLinesByMemberTabID(tab.MemberTabID).ToList();
            }

            return memberTabs;
        }
    }
}
