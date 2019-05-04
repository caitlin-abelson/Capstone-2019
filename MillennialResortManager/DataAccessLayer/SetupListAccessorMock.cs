using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class SetupListAccessorMock : ISetupListAccessor
    {
        private List<SetupList> _setupLists;
        private int _setupListID;


        public SetupListAccessorMock()
        {
            _setupLists = new List<SetupList>();
            _setupListID = 0;
        }



        /// <summary>
        /// Author James Heim
        /// Created 2019-03-15
        /// 
        /// Return all Setup Lists.
        /// </summary>
        /// <returns>All Setup Lists.</returns>
        public List<SetupList> SelectAllSetupLists()
        {
            return _setupLists;
        }

        public List<SetupList> SelectIncompleteSetupLists()
        {
            return _setupLists.FindAll(x => x.Completed == false);
        }

        /// <summary>
        /// Eduardo
        /// 
        /// Create a new mock setupList.
        /// </summary>
        /// <param name="newSetup"></param>
        /// <returns></returns>
        public void InsertSetupList(SetupList newSetupList)
        {

            newSetupList.SetupListID = _setupListID;
            _setupListID++;

            _setupLists.Add(newSetupList);

            //return newSetupList.SetupListID;

        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-15
        /// 
        /// Update the specified SetupList by overwriting it with the new SetupList.
        /// Throw an exception if the IDs of both SetupLists do not match.
        /// </summary>
        /// <param name="newSetupList"></param>
        /// <param name="oldSetupList"></param>
        public int UpdateSetupList(SetupList newSetupList, SetupList oldSetupList)
        {
            int rows = 0;

            if (newSetupList.SetupListID == oldSetupList.SetupListID)
            {
                _setupLists.Remove(_setupLists.Find(x => x.SetupListID == oldSetupList.SetupListID));
                _setupLists.Add(newSetupList);
                rows = 1;
            }
            else
            {
                throw new Exception("ID of old SetupList and new SetupList do not match.");
            }

            return rows;
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-15
        /// 
        /// Return the SetupList by the specified ID.
        /// </summary>
        /// <param name="setupListID"></param>
        /// <returns></returns>
        public SetupList SelectSetupList(int setupListID)
        {
            return _setupLists.Find(x => x.SetupListID == setupListID);
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-15
        /// 
        /// Delete the SetupList at the Specified ID.
        /// </summary>
        /// <param name="setupListID">Setup List ID</param>
        public void DeleteSetupList(int setupListID)
        {
            _setupLists.Remove(_setupLists.Find(x => x.SetupListID == setupListID));
        }

        /// <summary>
        /// Author James Heim
        /// Created 2019-03-15
        /// 
        /// Mark the specified Setup List as complete.
        /// </summary>
        /// <param name="setupListID">Setup List ID</param>
        public void CompleteSetupList(int setupListID)
        {
            _setupLists.Find(x => x.SetupListID == setupListID).Completed = true;
        }

        /// <summary>
        /// Author James Heim
        /// 
        /// Retrieve only the completed Setup Lists.
        /// </summary>
        /// <returns>Completed Setup Lists.</returns>
        public List<SetupList> SelectCompleteSetupLists()
        {
            return _setupLists.FindAll(x => x.Completed == true);
        }

        public List<VMSetupList> SelectActiveSetupLists()
        {
            List<VMSetupList> vmSetups = new List<VMSetupList>();

            foreach (var setupList in _setupLists)
            {
                vmSetups.Add(new VMSetupList()
                {
                    EventTitle = "Debug Data",
                    SetupListID = setupList.SetupListID,
                    SetupID = setupList.SetupID,
                    Comments = setupList.Comments,
                    Completed = setupList.Completed,
                    Description = setupList.Description
                });
            }

            return vmSetups.Where(x => x.Completed == false).ToList();
        }

        public List<VMSetupList> SelectInactiveSetupLists()
        {
            List<VMSetupList> vmSetups = new List<VMSetupList>();

            foreach (var setupList in _setupLists)
            {
                vmSetups.Add(new VMSetupList()
                {
                    EventTitle = "Debug Data",
                    SetupListID = setupList.SetupListID,
                    SetupID = setupList.SetupID,
                    Comments = setupList.Comments,
                    Completed = setupList.Completed,
                    Description = setupList.Description
                });
            }

            return vmSetups.Where(x => x.Completed == true).ToList();
        }

        public List<VMSetupList> SelectVMSetupLists()
        {
            List<VMSetupList> vmSetups = new List<VMSetupList>();

            foreach (var setupList in _setupLists)
            {
                vmSetups.Add(new VMSetupList()
                {
                    EventTitle = "Debug Data",
                    SetupListID = setupList.SetupListID,
                    SetupID = setupList.SetupID,
                    Comments = setupList.Comments,
                    Completed = setupList.Completed,
                    Description = setupList.Description
                });
            }

            return vmSetups;
        }

        /// <summary>
        /// Author James Heim
        /// 
        /// Retrieve only the completed Setup Lists.
        /// </summary>
        /// <returns></returns>
        public void DeactiveSetupList(int setupListID)
        {
            _setupLists.Find(x => x.SetupListID == setupListID).Completed = true;
        }
    }
}
