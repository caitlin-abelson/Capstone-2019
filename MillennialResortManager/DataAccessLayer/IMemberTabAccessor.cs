using DataObjects;
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
    }
}
