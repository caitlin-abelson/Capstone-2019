using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-18
    /// 
    /// <remarks>
    /// James Heim
    /// Modified 2019-04-25
    /// Added more methods.
    /// </remarks>
    /// </summary>
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
    }
}
