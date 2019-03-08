using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{

    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/03/05
    /// 
    /// Concrete class for ISetupManager.
    /// </summary>
    public class SetupListManager : ISetupListManager
    {
        private ISetupListAccessor _setupListAccessor;


        public SetupListManager()
        {
            _setupListAccessor = new SetupListAccessor();
        }


        public SetupListManager(SetupListAccessorMock setupListAccessorMock)
        {
            _setupListAccessor = setupListAccessorMock;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// method to get a list of setup
        /// </summary>
        public List<SetupList> RetrieveAllSetupLists()
        {
            List<SetupList> setupLists;
            try
            {
               setupLists = _setupListAccessor.RetrieveAllSetupLists();
            }
            catch (Exception)
            {
                throw;
            }

            return setupLists;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// method to retrieve setupList by setuplistid
        /// </summary>

       
        public SetupList RetrieveSetupListBySetupListID(int setupListID)
        {
            SetupList setupList;
            try
            {
                setupList = _setupListAccessor.RetrieveSetupListByRoleId(setupListID);
            }
            catch (Exception)
            {
                throw;
            }
            return setupList;
        }


    }
}
