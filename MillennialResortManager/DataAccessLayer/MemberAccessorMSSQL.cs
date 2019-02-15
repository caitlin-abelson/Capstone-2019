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
        public void CreateMember(Member newMember)
        {
            throw new NotImplementedException();
        }

        public void DeactivateMember(Member deactivatingMember)
        {
            throw new NotImplementedException();
        }

        public void PurgeMember(Member purgedMember)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// RetrieveAllMembers will select all of the Members from our Database who have an Active Status of 1 and return them
        /// </summary>
        /// <returns>Returns a List of all Members</returns>
        public List<Member> RetrieveAllMembers()
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

        public Member RetrieveMember()
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member oldMember, Member newMember)
        {
            throw new NotImplementedException();
        }
    }
}