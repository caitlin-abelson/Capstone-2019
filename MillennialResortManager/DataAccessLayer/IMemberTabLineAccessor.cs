using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Added by Matt H. on 4/26/19
    /// Interface for MemberTabLineAccessor.
    /// </summary>
    public interface IMemberTabLineAccessor
    {
        List<MemberTabLine> SelectMemberTabLineByMemberTabID(int id);
    }
}
