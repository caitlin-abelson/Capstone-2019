using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
/// <summary>
/// Author: Jared Greenfield
/// Date Created: 2019-04-30
/// A mock accessor for Unit-testing
/// </summary>
namespace DataAccessLayer
{
    public class MemberTabAccessorMock : IMemberTabAccessor
    {
        private List<MemberTab> _memberTabs;
        /// <summary>
        /// Author: Jared Greenfield
        /// Date Created: 2019-04-30
        /// Sets up values for testing.
        /// </summary>
        public MemberTabAccessorMock()
        {
            _memberTabs = new List<MemberTab>();
            _memberTabs.Add(new MemberTab() {
                MemberTabID = 100000,
                MemberID = 100000,
                MemberTabLines = null,
                Active = true,
                TotalPrice = 1000
            });
            _memberTabs.Add(new MemberTab()
            {
                MemberTabID = 100001,
                MemberID = 100000,
                MemberTabLines = null,
                Active = true,
                TotalPrice = 10001
            });
            _memberTabs.Add(new MemberTab()
            {
                MemberTabID = 100002,
                MemberID = 100001,
                MemberTabLines = null,
                Active = true,
                TotalPrice = 10.00M
            });
        }

        public int DeactivateMemberTab(int memberTabID)
        {
            throw new NotImplementedException();
        }

        public int DeleteMemberTab(int memberTabID)
        {
            throw new NotImplementedException();
        }

        public int DeleteMemberTabLine(int memberTabLineID)
        {
            throw new NotImplementedException();
        }

        public int InsertMemberTab(int memberID)
        {
            throw new NotImplementedException();
        }

        public int InsertMemberTabLine(MemberTabLine memberTabLine)
        {
            throw new NotImplementedException();
        }

        public int ReactivateMemberTab(int memberTabID)
        {
            throw new NotImplementedException();
        }

        public MemberTab SelectActiveMemberTabByMemberID(int memberID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Date Created: 2019-04-30
        /// Returns last tab of a member
        /// </summary>
        public MemberTab SelectLastMemberTabByMemberID(int memberID)
        {
            MemberTab tab = null;
            foreach (MemberTab Tab in _memberTabs)
            {
                if (Tab.MemberID == memberID)
                {
                    tab = Tab;
                }
            }
            return tab;
        }

        public MemberTab SelectMemberTabByID(int id)
        {
            throw new NotImplementedException();
        }

        public MemberTabLine SelectMemberTabLineByID(int memberTabLineID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberTabLine> SelectMemberTabLinesByMemberTabID(int memberTabID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberTab> SelectMemberTabs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberTab> SelectMemberTabsByMemberID(int memberID)
        {
            throw new NotImplementedException();
        }
    }
}
