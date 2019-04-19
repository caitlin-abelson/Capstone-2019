using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        int InsertMemberTab(int memberID);

        int UpdateMemberTabSetInactive(int memberTabID);
    }
}
