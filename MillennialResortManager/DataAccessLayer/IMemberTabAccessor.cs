using DataObjects;
using System.Data;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-18
    /// </summary>
    public interface IMemberTabAccessor
    {
        MemberTab SelectMemberTabByID(int id);

        MemberTab SelectActiveMemberTabByMemberID(int memberID);

        IEnumerable<MemberTab> SelectMemberTabsByMemberID(int memberID);

        IEnumerable<MemberTab> SelectMemberTabs();

        int InsertMemberTab(int memberID);

        int DeactivateMemberTab(int memberTabID);

        int ReactivateMemberTab(int memberTabID);

        int InsertMemberTabLine(MemberTabLine memberTabLine);

        IEnumerable<MemberTabLine> SelectMemberTabLinesByMemberTabID(int memberTabID);

        MemberTabLine SelectMemberTabLineByID(int memberTabLineID);

        int DeleteMemberTab(int memberTabID);

        int DeleteMemberTabLine(int memberTabLineID);

        /// <summary>
        /// Jared Greenfield
        /// Created 2019-04-30
        /// 
        /// Select last tab member had.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        MemberTab SelectLastMemberTabByMemberID(int memberID);
       
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/25
        /// 
        /// User inputs a search criteria, if exits, method retrieves the list of 
        /// members that meet the criteria
        /// </summary
        List<string> SelectShop();
        DataTable selectOfferings(int shopID);
        int SelectShopID(string name);
        DataTable SelectSearchMember(string data);

    }
}
