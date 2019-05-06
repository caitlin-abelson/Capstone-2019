using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Matt LaMarche" created="2019/01/24">
	/// IMemberManager is an interface meant to be the standard for interacting with Members in a storage medium
	/// <remarks>
	/// <updates>
	/// <update author="Ramesh Adhikari" date="2019/03/08">
	/// Added the Deactivate Member Method
	/// </update>
	/// </updates>
	/// </summary>
	public interface IMemberManager
    {

        void CreateMember(Member member);
        void UpdateMember(Member newMember, Member oldMember);
        Member RetrieveMember(int id);
		/// <summary author="Matt LaMarche" created="2019/01/24">
		/// SelectAllMembers returns a list of all the Active Members currently being stored in this data storage mechanism
		/// </summary>
		List<Member> RetrieveAllMembers();
        void DeactivateMember(Member member);
        void DeleteMember(Member member);
        int RetrieveMemberByEmail(string email);
    }
}
