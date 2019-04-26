//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DataObjects;

//namespace DataAccessLayer
//{

//    /// <summary>
//    /// Eduardo Colon
//    /// Created: 2019/03/05
//    /// 
//    /// SetupListAccessorMock to be used for testing
//    /// 
//    /// the class SetupListAccessorMock deriveds ISetupListManager
//    /// </summary>
//    public class SetupListAccessorMock : ISetupListAccessor
//    {
//        private List<SetupList> _setupList;
//        private List<int> _allSetupLists;

//        public SetupListAccessorMock()
//        {
//            _setupList = new List<SetupList>();
//            _setupList.Add(new SetupList() { SetupListID= 100000,SetupID= 100000,Completed= false, Description = "Prior to Guest Arrival: Registration Desk,signs,banners ", Comments = "Banners are not ready yet" });
//            _setupList.Add(new SetupList() { SetupListID = 100001, SetupID = 100001, Completed = false, Description = "Display Equipment: Prepares for display boards ", Comments = "Badges are not ready yet" });
//            _setupList.Add(new SetupList() { SetupListID = 100002, SetupID = 100002, Completed = true, Description = "Check Av Equipment: Laptop,projectors :Ensure all cables,leads,laptop: ", Comments = "Av Equipment is ready" });
//            _setupList.Add(new SetupList() { SetupListID = 100003, SetupID = 100003, Completed = true, Description = "Confirm that all decor and linen is in place ", Comments = "decor and linen are  ready" });
//            _setupList.Add(new SetupList() { SetupListID = 100004, SetupID = 100004, Completed = true, Description = "Walk through to make sure bathrooms are clean and stocked  ", Comments = "bathrooms are  ready" });
//            _allSetupLists = new List<int>();
//            foreach (var item in _setupList)
//            {
//                _allSetupLists.Add(item.SetupListID);
//            }
//        }

//        public List<SetupList> RetrieveAllSetupLists()
//        {
//            return _setupList;
//        }

       

//        public SetupList RetrieveSetupListByRoleId(int setupListID)
//        {
//           SetupList setupList = new SetupList();
//            setupList = _setupList.Find(b => b.SetupListID == setupListID);
//            if (setupList == null)
//            {
//                throw new ArgumentException("SetupListID doesn't match.");
//            }

//            return setupList;
//        }
//    }
//}
