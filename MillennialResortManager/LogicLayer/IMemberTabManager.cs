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
    /// </summary>
    public interface IMemberTabManager
    {
        MemberTab RetrieveMemberTabByID(int id);

        MemberTab RetrieveActiveMemberTabByMemberID(int memberID);

        bool CreateMemberTab(int memberID);

        bool UpdateMemberTabSetInactive(int memberTabID);
    }
}
