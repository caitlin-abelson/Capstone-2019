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
        
        private MemberAccessorMSSQL _memberAccessor;
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// Constructor allowing for non-static method calls
        /// </summary>
        public MemberManagerMSSQL()
        {
            _memberAccessor = new MemberAccessorMSSQL();
        }

        public void AddMember(Member newMember)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember()
        {
            throw new NotImplementedException();
        }

        public void EditMember(Member oldMember, Member newMember)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// RetrieveAllMembers asks our MemberAccessorMSSQL for a List<Member> containing all of the Active Members in our system
        /// </summary>
        /// <returns>A list of Members retrieved from the Member Accessor</returns>
        public List<Member> RetrieveAllMembers()
        {
            List<Member> members;
            try
            {
                members = _memberAccessor.RetrieveAllMembers();
            }
            catch (Exception)
            {
                throw;
            }
            return members;
        }

        public Member RetrieveMember()
        {
            throw new NotImplementedException();
        }
    }
}
