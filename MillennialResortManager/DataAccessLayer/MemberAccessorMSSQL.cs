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
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// The MemberAccessorMSSQL is an implementation of the IMemberAccessor interface and  is designed to access a MSSQL database and work with data related to Members
    /// </summary>
    public class MemberAccessorMSSQL : IMemberAccessor
    {
        public MemberAccessorMSSQL()
        {

        }
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// </summary>
        public void InsertMember(Member newMember)
        {
            // int rowsAffected = -1;

            var conn = DBConnection.GetDbConnection();
            var procedure = @"sp_insert_member";
            var cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", newMember.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newMember.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", newMember.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newMember.Email);
            cmd.Parameters.AddWithValue("@Password", newMember.Password);

            try
            {
                conn.Open();
                Convert.ToInt32(cmd.ExecuteScalar());

                //  rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return;
        }
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/02/2019
        /// </summary>
        public void DeactivateMember(Member member)
        {
            if (member.Active == false)
            {
                throw new Exception("Member is already deactivated");
            }

            var conn = DBConnection.GetDbConnection();
            var procedure = @"sp_deactivate_member";
            var cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberID", member.MemberID);

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
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/02/2019
        /// </summary>
        public void DeleteMember(Member member)
        {
            if (member.Active == true)
            {
                throw new Exception("Cannot delete an active");
            }
            var conn = DBConnection.GetDbConnection();
            var procedure = @"sp_delete_member";
            var cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberID", member.MemberID);

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
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// SelectAllMembers will select all of the Members from our Database who have an Active Status of 1 and return them
        /// </summary>
        /// <returns>Returns a List of all Members</returns>
        public List<Member> SelectAllMembers()
        {
            List<Member> members = new List<Member>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText2 = @"sp_retrieve_all_members";

            // command objects
            var cmd2 = new SqlCommand(cmdText2, conn);

            // set the command type
            cmd2.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        Member member = new Member();
                        member.MemberID = reader2.GetInt32(0);
                        member.FirstName = reader2.GetString(1);
                        member.LastName = reader2.GetString(2);
                        member.PhoneNumber = reader2.GetString(3);
                        member.Email = reader2.GetString(4);
                        member.Active = reader2.GetBoolean(5);
                        members.Add(member);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally{
                conn.Close();
            }
            return members;
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// 
        /// Retrieve member by their id
        /// </summary>
        public Member SelectMember(int id)
        {
            Member member = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_retrieve_member_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("MemberID", id);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    member = new Member()
                    {
                        MemberID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Email = reader.GetString(4),
                        Password = reader.GetString(5),
                        Active = reader.GetBoolean(6)


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
            return member;
        }

        public void UpdateMember(Member newMember, Member oldMember)
        {
            var conn = DBConnection.GetDbConnection();
            var procedure = @"sp_update_member";
            var cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberID", oldMember.MemberID);
            cmd.Parameters.AddWithValue("@OldFirstName", oldMember.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldMember.LastName);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldMember.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldMember.Email);
            cmd.Parameters.AddWithValue("@OldPassword", oldMember.Password);
            cmd.Parameters.AddWithValue("@OldActive", oldMember.Active);

            cmd.Parameters.AddWithValue("@NewFirstName", newMember.FirstName);
            cmd.Parameters.AddWithValue("@NewLastName", newMember.LastName);
            cmd.Parameters.AddWithValue("@NewPhoneNumber", newMember.PhoneNumber);
            cmd.Parameters.AddWithValue("@NewEmail", newMember.Email);
            cmd.Parameters.AddWithValue("@NewPassword", newMember.Password);
            cmd.Parameters.AddWithValue("@NewActive", newMember.Active);

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

        public int SelectMemberByEmail(string email)
        {
            int id = 0;

            var conn = DBConnection.GetDbConnection();
            var procedure = @"sp_select_member_by_email";
            var cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@Email", email);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    id = reader.GetInt32(0);
                    
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
            return id;
        }
    }
}