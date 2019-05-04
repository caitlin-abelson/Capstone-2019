using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Added by Matt H. on 4/26/19
    /// Manager Interface for MemberTabLine
    /// </summary>
    public interface IMemberTabLineManager
    {
        List<MemberTabLine> RetrieveMemberTabLineByMemberID(int id);
    }
}
