using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
	/// <summary author="James Heim" created="2019/04/18">
	/// </summary>
	/// <updates>
	/// <update author="James Heim" date="2019/04/25">
	/// Added more methods.
	/// </update>
	/// <update author="Jared Greenfield" date="2019/04/30">
	/// Added RetrieveLastMemberTabByMemberID.
	/// </update>
	/// </updates>
	public interface IMemberTabManager
	{
		MemberTab RetrieveMemberTabByID(int id);
		MemberTab RetrieveActiveMemberTabByMemberID(int memberID);
		IEnumerable<MemberTab> RetrieveMemberTabsByMemberID(int memberID);
		IEnumerable<MemberTab> RetrieveAllMemberTabs();
		bool CreateMemberTab(int memberID);
		bool DeactivateMemberTab(int memberTabID);
		bool ReactivateMemberTab(int memberTabID);
		int CreateMemberTabLine(MemberTabLine memberTabLine);
		IEnumerable<MemberTabLine> RetrieveMemberTabLinesByMemberTabID(int memberTabID);
		MemberTabLine RetrieveMemberTabLineByID(int memberTabLineID);
		bool DeleteMemberTab(int memberTabID);
		bool DeleteMemberTabLine(int memberTabLineID);

		/// <summary author="Jared Greenfield" created="2019/04/30">
		/// Select last tab member had.
		/// </summary>
		/// <param name="memberID"></param>
		/// <returns></returns>
		MemberTab RetrieveLastMemberTabByMemberID(int memberID);
		/// <summary name="Carlos Arzu" created="2019/04/26">
		/// Retrieve list of members.
		/// </summary>
		List<string> retrieveShops();
		int retrieveShopID(string name);
		DataTable retrieveOfferings(int shopID);
		DataTable retrieveSearchMembers(string data);
	}
}