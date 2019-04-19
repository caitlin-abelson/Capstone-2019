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
        /// <param name="accessor"></param>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
        public bool UpdateMemberTabSetInactive(int memberTabID)
        {
            bool result = false;

            try
            {
                result = (1 == _memberTabAccessor.UpdateMemberTabSetInactive(memberTabID));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
