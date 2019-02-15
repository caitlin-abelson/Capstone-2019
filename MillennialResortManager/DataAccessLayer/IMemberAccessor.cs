using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// The IMemberAccessor is an interface meant to be the standard for interacting with Data in a storage medium regarding Members
    /// </summary>
    interface IMemberAccessor
    {
        void CreateMember(Member newMember);
        Member RetrieveMember();
        List<Member> RetrieveAllMembers();
        void UpdateMember(Member oldMember, Member newMember);
        void DeactivateMember(Member deactivatingMember);
        void PurgeMember(Member purgedMember);
    }
}
