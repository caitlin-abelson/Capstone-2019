using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// MemberManagerMSSQL Is an implementation of the IMemberManager Interface meant to interact with the MSSQL MemberAccessor
    /// </summary>
    public class MemberManagerMSSQL : IMemberManager
    {
        private IMemberAccessor _memberAccessor;

        //private MemberAccessorMSSQL _memberAccessor;
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// Constructor allowing for non-static method calls
        /// </summary>
        public MemberManagerMSSQL()
        {
            _memberAccessor = new MemberAccessorMSSQL();
        }

        public MemberManagerMSSQL(MemberAccessorMock mockMemberAccessor)
        {
            _memberAccessor = mockMemberAccessor;
        }
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// </summary>
        public void CreateMember(Member member)
        {
            member.Validate();
            try
            {
                _memberAccessor.InsertMember(member);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// </summary>
        public void DeleteMember(Member member)
        {
            try
            {
                if (member.Active)
                {
                    _memberAccessor.DeactivateMember(member);
                }
                else
                {
                    _memberAccessor.DeleteMember(member);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// </summary>
        public void UpdateMember(Member oldMember, Member newMember)
        {
            newMember.Validate();
            oldMember.Validate();

            try
            {
                _memberAccessor.UpdateMember(newMember, oldMember);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// SelectAllMembers asks our MemberAccessorMSSQL for a List<Member> containing all of the Active Members in our system
        /// </summary>
        /// <returns>A list of Members retrieved from the Member Accessor</returns>
        public List<Member> RetrieveAllMembers()
        {
            List<Member> members;
            try
            {
                members = _memberAccessor.SelectAllMembers();
            }
            catch (Exception)
            {
                throw;
            }
            return members;
        }
        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 01/30/2019
        /// </summary>
        public Member RetrieveMember(int id)
        {
            Member member = null;
            try
            {
                member = _memberAccessor.SelectMember(id);
                if (member == null)
                {
                    throw new NullReferenceException("Member not found");
                }

            }
            catch (Exception)
            {

                throw;
            }
            return member;
        }

        /// <summary>
        /// Author: Ramesh Adhikari
        /// Created On: 02/22/2019
        /// Deactivate the member from the member records
        /// </summary>
        public void DeactivateMember(Member selectedMember)
        {
            try
            {
                _memberAccessor.DeactivateMember(selectedMember);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
