using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class MemberTabManager : IMemberTabManager
    {

        private IMemberTabAccessor _memberTabAccessor;

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// The default constructor. Generates the default MemberTabAccessor (MSSQL.)
        /// </summary>
        public MemberTabManager()
        {
            _memberTabAccessor = new MemberTabAccessor();
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// The constructor that takes an object inheriting from IMemberTabAccessor.
        /// Allows the user to specify an accessor other than MSSQL or a Mock Accessor.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        public MemberTabManager(IMemberTabAccessor accessor)
        {
            _memberTabAccessor = accessor;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Create a new MemberTab for the specified Member.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns>Whether the tab was created.</returns>
        public bool CreateMemberTab(int memberID)
        {
            bool result = false;

            try
            {
                result = (1 == _memberTabAccessor.InsertMemberTab(memberID));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Retrieve the only active MemberTab by the specified Member's ID.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns>The only active MemberTab</returns>
        public MemberTab RetrieveActiveMemberTabByMemberID(int memberID)
        {
            MemberTab memberTab = null;

            try
            {
                memberTab = _memberTabAccessor.SelectActiveMemberTabByMemberID(memberID);
            }
            catch (Exception)
            {

                throw;
            }

            return memberTab;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Retrieve the MemberTab that matches the specified MemberTabID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The MemberTab.</returns>
        public MemberTab RetrieveMemberTabByID(int id)
        {
            MemberTab memberTab = null;

            try
            {
                memberTab = _memberTabAccessor.SelectMemberTabByID(id);
            }
            catch (Exception)
            {

                throw;
            }

            return memberTab;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-18
        /// 
        /// Set the MemberTab to inactive.
        /// Should not work if any guests are still checked in.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns>If the deactivation was successful.</returns>
        public bool DeactivateMemberTab(int memberTabID)
        {
            bool result = false;

            try
            {
                result = (1 == _memberTabAccessor.DeactivateMemberTab(memberTabID));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Reactivate the specified member tab.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns>If the reactivation was successful.</returns>
        public bool ReactivateMemberTab(int memberTabID)
        {
            bool result = false;

            try
            {
                result = (1 == _memberTabAccessor.ReactivateMemberTab(memberTabID));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Create a new line on the member's tab and return the ID of that line.
        /// Throws an exception if the ID is not updated by the database.
        /// </summary>
        /// <param name="memberTabLine"></param>
        /// <returns>The ID of the newly created MemberTab.</returns>
        public int CreateMemberTabLine(MemberTabLine memberTabLine)
        {
            int memberTabLineID = -1;

            try
            {
                memberTabLineID = _memberTabAccessor.InsertMemberTabLine(memberTabLine);

                if (memberTabLineID == -1)
                {
                    throw new ApplicationException("TabLine ID was not set by the database.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return memberTabLineID;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Retrieve all lines for the specified Tab.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns>A list of the tab's tablines.</returns>
        public IEnumerable<MemberTabLine> RetrieveMemberTabLinesByMemberTabID(int memberTabID)
        {
            List<MemberTabLine> tabLines = null;

            try
            {
                tabLines = _memberTabAccessor.SelectMemberTabLinesByMemberTabID(memberTabID).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return tabLines;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Retrieve the line on a tab by its unique ID.
        /// </summary>
        /// <param name="memberTabLineID"></param>
        /// <returns>The TabLine specific to the suppied ID.</returns>
        public MemberTabLine RetrieveMemberTabLineByID(int memberTabLineID)
        {
            MemberTabLine tabLine = null;

            try
            {
                tabLine = _memberTabAccessor.SelectMemberTabLineByID(memberTabLineID);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return tabLine;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Delete the specified tabline.
        /// </summary>
        /// <param name="memberTabLineID"></param>
        /// <returns>Whether the Delete was successful.</returns>
        public bool DeleteMemberTabLine(int memberTabLineID)
        {
            bool result = false;

            try
            {
                result = (1 == _memberTabAccessor.DeleteMemberTabLine(memberTabLineID));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Retrieve all member tabs.
        /// </summary>
        /// <returns>A list of all member tabs.</returns>
        public IEnumerable<MemberTab> RetrieveAllMemberTabs()
        {
            List<MemberTab> allTabs = null;

            try
            {
                allTabs = _memberTabAccessor.SelectMemberTabs().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return allTabs;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-25
        /// 
        /// Delete the specified Member Tab.
        /// </summary>
        /// <param name="memberTabID"></param>
        /// <returns>Whether the delete was successful.</returns>
        public bool DeleteMemberTab(int memberTabID)
        {
            bool result = false;

            try
            {
                result = (1 == _memberTabAccessor.DeleteMemberTab(memberTabID));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// James Heim
        /// Created 2019-04-26
        /// 
        /// Retrieve all tabs a Member has ever had.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public IEnumerable<MemberTab> RetrieveMemberTabsByMemberID(int memberID)
        {
            List<MemberTab> memberTabs = new List<MemberTab>();

            try
            {
                memberTabs = _memberTabAccessor.SelectMemberTabsByMemberID(memberID).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return memberTabs;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created 2019-04-30
        /// 
        /// Select last tab member had.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public MemberTab RetrieveLastMemberTabByMemberID(int memberID)
        {
            MemberTab tab = null;
            try
            {
                tab = _memberTabAccessor.SelectLastMemberTabByMemberID(memberID);
            }
            catch (Exception)
            {

                throw;
            }
            return tab;
        }
    }
}
