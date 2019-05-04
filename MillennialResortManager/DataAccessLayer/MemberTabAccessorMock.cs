using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

/// <summary>
/// Author: Jared Greenfield and James Heim
/// Date Created: 2019-04-30
/// A mock accessor for Unit-testing
/// </summary>
namespace DataAccessLayer
{
    public class MemberTabAccessorMock : IMemberTabAccessor
    {
        /// <summary>
        /// Our fake DataStore.
        /// </summary>
        private List<MemberTab> _memberTabs;

        /// <summary>
        /// James Heim
        /// Created 2019-04-30
        /// 
        /// Build our fake datastore.
        /// </summary>
        public MemberTabAccessorMock()
        {

            // Build our list of test data.
            _memberTabs = new List<MemberTab>();

            _memberTabs.Add(new MemberTab()
            {
                MemberTabID = 100000,
                MemberID = 100000,
                MemberTabLines = new List<MemberTabLine>(),
                Active = true
            });

            _memberTabs[0].MemberTabLines.Add(new MemberTabLine()
            {
                MemberTabLineID = 100000,
                MemberTabID = 100000,
                Quantity = 1,
                Description = "Test Item",
                OfferingID = 100000,
                OfferingTypeID = "Room",
                Price = 299.99M,
                DatePurchased = DateTime.Now.AddDays(-1)
            });

            _memberTabs[0].MemberTabLines.Add(new MemberTabLine()
            {
                MemberTabLineID = 100001,
                MemberTabID = 100000,
                Quantity = 2,
                Description = "Test Item 2",
                OfferingID = 100001,
                OfferingTypeID = "Item",
                GuestID = 100000,
                EmployeeID = 100000,
                Price = 2.99M,
                DatePurchased = DateTime.Now.AddMinutes(-213)
            });

        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-30
        /// 
        /// The number of Rows Affected by the "SQL" script.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns></returns>
        public int DeactivateMemberTab(int memberTabID)
        {
            int rowsAffected = 0;

            // Find the matching memberTab in our list.
            var memberTab = _memberTabs.Find(x => x.MemberTabID == memberTabID);
            
            if (memberTab == null)
            {
                // If we can't find it, we can't perform our Deactivate. Return 0.
                rowsAffected = 0;
            }
            else
            {
                // Set the tab to inactive and return 1.
                memberTab.Active = true;
                rowsAffected = 1;
            }

            return rowsAffected;
        }

        public int DeleteMemberTab(int memberTabID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-30
        /// 
        /// Delete the specified MemberTabLine if it exists.
        /// Return the number of items deleted (1 if exists, 0 if not.)
        /// </summary>
        /// <param name="memberTabLineID"></param>
        /// <returns></returns>
        public int DeleteMemberTabLine(int memberTabLineID)
        {
            int rows = 0;

            MemberTabLine memberTabLine;

            // Find the matching memberTabLine in our list.
            foreach (var memberTab in _memberTabs)
            {
                memberTabLine = memberTab.MemberTabLines.Find(x => x.MemberTabLineID == memberTabLineID);

                if (memberTabLine != null)
                {
                    break;
                }
            }

            

            return rows;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-30
        /// 
        /// Return the ID assigned to the newly created MemberTab.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public int InsertMemberTab(int memberID)
        {
            int id = 0;

            // If the specified member does not currently have an open tab,
            // then we may proceed.
            List<MemberTab> results = 
                _memberTabs.Where(x => x.MemberID == memberID)
                           .Where( x => x.Active == true).ToList();

            if (results.Count == 0)
            {
                // No current tab; we're good to go.

                _memberTabs.Add(new MemberTab()
                {
                    MemberID = memberID,
                    MemberTabID = 100001,
                    MemberTabLines = new List<MemberTabLine>(),
                    TotalPrice = 0,
                    Active = true
                });
            }

            return id;
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
