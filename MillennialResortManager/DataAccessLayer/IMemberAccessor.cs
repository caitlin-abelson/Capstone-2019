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
   public interface IMemberAccessor
    {
        void InsertMember(Member newMember);
        Member SelectMember(int id);
        List<Member> SelectAllMembers();
        void UpdateMember(Member newMember, Member oldMember);
        void DeactivateMember(Member member);
        void DeleteMember(Member member);
    }
}
