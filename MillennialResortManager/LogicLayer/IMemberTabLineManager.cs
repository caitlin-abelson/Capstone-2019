using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Matthew Hill" created="2019/04/26">
	/// Manager Interface for MemberTabLine
	/// </summary>
	public interface IMemberTabLineManager
    {
        List<MemberTabLine> RetrieveMemberTabLineByMemberID(int id);
    }
}