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
    /// </summary>
    public interface IMemberManager
    {

        void AddMember(Member newMember);
        void EditMember(Member oldMember, Member newMember);
        Member RetrieveMember();
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// RetrieveAllMembers returns a list of all the Active Members currently being stored in this data storage mechanism
        /// </summary>
        List<Member> RetrieveAllMembers();
        void DeleteMember();
    }
}
