using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// IMemberManager is an interface meant to be the standard for interacting with Members in a storage medium
    /// <remarks>
    /// Name: Ramesh Adhikari
    /// Update Date: 03/08/2019
    /// Description: Added the Deactivate Member Method
    /// </remarks>
    /// </summary>
    public interface IMemberManager
    {

        void CreateMember(Member member);
        void UpdateMember(Member newMember, Member oldMember);
        Member RetrieveMember(int id);
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// SelectAllMembers returns a list of all the Active Members currently being stored in this data storage mechanism
        /// </summary>
        List<Member> RetrieveAllMembers();
        void DeactivateMember(Member member);
        void DeleteMember(Member member);
    }
}
