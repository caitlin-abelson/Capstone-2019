using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{

    public class SetupListManager : ISetupListManager
    {
        private ISetupListAccessor _setupListAccessor;

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// Constructor for SetupListManager
        /// </summary>
        public SetupListManager()
        {
            _setupListAccessor = new SetupListAccessor();
        }

        /// <summary>
        /// Author: James Heim
        /// Created Date: 5/3/19
        /// 
        /// Constructor that takes allows a different accessor.
        /// Created specificially for supplying the mock accessor for unit testing.
        /// </summary>
        /// <param name="accessor"></param>
        public SetupListManager(ISetupListAccessor accessor)
        {
            _setupListAccessor = accessor;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-02-28
        /// 
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
                catch (Exception)
                {
                    throw;
                }
            }
            // If the setuplist is incomplete then the setupList can be deleted.
            else
            {
                try
                {
                    _setupListAccessor.DeleteSetupList(setupListID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-02-28
        /// 
        /// 
        /// </summary>
        /// <param name="newSetupList">The object that the new SetupList is being created to</param>
        /// <returns></returns>
        public void InsertSetupList(SetupList newSetupList)
        {

            try
            {
                _setupListAccessor.InsertSetupList(newSetupList);
            }
            catch (Exception)
            {
                throw;
            }



        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-02-28
        /// 
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
            catch (Exception)
            {

                throw;
            }

            return setupLists;
        }

        //Eduardo
        /// <summary>
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
            catch (Exception)
            {
                throw;
            }

            return setupLists;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2019-03-19
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public List<VMSetupList> SelectAllInActiveSetupLists()
        {
            List<VMSetupList> setupList;
            try
            {
                setupList = _setupListAccessor.SelectInactiveSetupLists();
            }
            catch (Exception)
            {
                throw;
            }
            return setupList;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SetupList> SelectAllSetupLists()
        {
            List<SetupList> setupLists = new List<SetupList>();
            try
            {
                setupLists = _setupListAccessor.SelectAllSetupLists();
            }
            catch (Exception)
            {
                throw;
            }
            return setupLists;
        }

        //Eduardo
        public SetupList SelectSetupList(int setupListID)
        {
            SetupList setupList;
            try
            {
                setupList = _setupListAccessor.SelectSetupList(setupListID);
            }
            catch (Exception)
            {
                throw;
            }
            return setupList;
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Created Date: 2/28/19
        /// 
        /// 
        /// </summary>
        /// <param name="newSetupList"></param>
        /// <param name="oldSetupList"></param>
        public void UpdateSetupList(SetupList newSetupList, SetupList oldSetupList)
        {

            try
            {
                _setupListAccessor.UpdateSetupList(newSetupList, oldSetupList);
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
