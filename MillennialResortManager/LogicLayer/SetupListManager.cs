using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using ExceptionLoggerLogic;

namespace LogicLayer
{

    public class SetupListManager : ISetupListManager
    {
        private ISetupListAccessor _setupListAccessor;

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// Constructor for SetupListManager
		/// </summary>
		public SetupListManager()
        {
            _setupListAccessor = new SetupListAccessor();
        }

		/// <summary author="James Heim" created="2019/05/03">
		/// Constructor that takes allows a different accessor.
		/// Created specificially for supplying the mock accessor for unit testing.
		/// </summary>
		/// <param name="accessor"></param>
		public SetupListManager(ISetupListAccessor accessor)
        {
            _setupListAccessor = accessor;
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// The method for deactivating and deleting a setupList. 
		/// </summary>
		/// <param name="setupListID"></param>
		/// <param name="isActive"></param>
		public void DeleteSetupList(int setupListID, bool isActive)
        {
            // If the setupList is completed, the setuplist needs to be deactivated or made incomplete
            // before it can be deleted.
            if (isActive == true)
            {
                try
                {
                    _setupListAccessor.DeactiveSetupList(setupListID);
                }
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
            // If the setuplist is incomplete then the setupList can be deleted.
            else
            {
                try
                {
                    _setupListAccessor.DeleteSetupList(setupListID);
                }
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// </summary>
		/// <param name="newSetupList">The object that the new SetupList is being created to</param>
		/// <returns></returns>
		public void InsertSetupList(SetupList newSetupList)
        {

            try
            {
                _setupListAccessor.InsertSetupList(newSetupList);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// Get a list of all of the SetupLists.
		/// </summary>
		/// <returns></returns>
		public List<VMSetupList> SelectAllVMSetupLists()
        {
            List<VMSetupList> setupLists = new List<VMSetupList>();

            try
            {
                setupLists = _setupListAccessor.SelectVMSetupLists();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return setupLists;
        }

        /// <summary author="Eduardo Colon">
        /// Updated By: Caitlin Abelson
        /// Updated Date: 2019-03-19
        /// 
        /// The active SetupLists needs to be a list of the the VMSetupList objects not the SetupList objects.
        /// With that, I also changed the accessor method that is being called.
        /// </summary>
        /// <returns></returns>
        public List<VMSetupList> SelectAllActiveSetupLists()
        {
            List<VMSetupList> setupLists;
            try
            {
                setupLists = _setupListAccessor.SelectActiveSetupLists();
            }
            catch (Exception ex)
            {
				ExceptionLogManager.getInstance().LogException(ex);
                throw ex;
            }

            return setupLists;
        }

		/// <summary author="Caitlin Abelson" created="2019/03/19">
		/// </summary>
		/// <returns></returns>
		public List<VMSetupList> SelectAllInActiveSetupLists()
        {
            List<VMSetupList> setupList;
            try
            {
                setupList = _setupListAccessor.SelectInactiveSetupLists();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return setupList;
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// </summary>
		/// <returns></returns>
		public List<SetupList> SelectAllSetupLists()
        {
            List<SetupList> setupLists = new List<SetupList>();
            try
            {
                setupLists = _setupListAccessor.SelectAllSetupLists();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return setupLists;
        }

        public SetupList SelectSetupList(int setupListID)
        {
            SetupList setupList;
            try
            {
                setupList = _setupListAccessor.SelectSetupList(setupListID);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return setupList;
        }

		/// <summary author="Caitlin Abelson" created="2019/02/28">
		/// </summary>
		/// <param name="newSetupList"></param>
		/// <param name="oldSetupList"></param>
		public void UpdateSetupList(SetupList newSetupList, SetupList oldSetupList)
        {
            try
            {
                _setupListAccessor.UpdateSetupList(newSetupList, oldSetupList);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}
    }
}