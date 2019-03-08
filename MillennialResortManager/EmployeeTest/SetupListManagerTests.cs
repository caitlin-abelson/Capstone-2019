using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataObjects;
using DataAccessLayer;

namespace LogicLayerUnitTests
{


    ///  /// <summary>
    /// Eduardo Colon
    /// Created: 2019/02/09
    /// 
    /// description for RoleManagerTests
    /// </summary>
   
    [TestClass]
    public class SetupListManagerTests
    {
        private List<SetupList> _setupLists;
        private ISetupListManager _setupListManager;
        private SetupListAccessorMock _setupListMock;

        [TestInitialize]
        public void testSetup()
        {
            _setupListMock = new SetupListAccessorMock();
            _setupListManager = new SetupListManager(_setupListMock);
            _setupLists = new List<SetupList>();
            _setupLists = _setupListManager.RetrieveAllSetupLists();
        }


        private string createStringLength(int length)
        {
            string testingString = "";
            for (int i = 0; i < length; i++)
            {
                testingString += "X";
            }
            return testingString;
        }

        /*
        private void setSetupList(SetupList newSetupList, SetupList oldSetupList)
        {
            newSetupList. = oldRole.RoleID;
            newRole.Description = oldRole.Description;

        }
        */

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// Testing  to retrieve all setuplist

        [TestMethod]
        public void TestRetrieveAllSetupList()
        {
            //Arrange
            List<SetupList> setupLists = null;
            //Act
            setupLists = _setupListManager.RetrieveAllSetupLists();
            //Assert
            CollectionAssert.Equals(_setupLists, setupLists);
        }

      

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// Testing valid  retrieving all setuplist
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllSetupListWithValidInput()
        {
            //Arrange
            List<SetupList> setupLists = new List<SetupList>();

            //Act
            setupLists = _setupListManager.RetrieveAllSetupLists();

            //Assert
            Assert.IsNotNull(setupLists);
        }


      

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// 
        /// Testing retrieving all setup list with at least one object
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllSetupList_RetrieveAtLeastOneObject()
        {
            var roles = _setupListManager.RetrieveAllSetupLists();

            bool hasAtLeastOneElement = roles.Count > 0;

            Assert.IsTrue(hasAtLeastOneElement);
        }

       

       /// <summary>
       /// Eduardo Colon
       /// Created: 2019/02/09
       /// 
       /// 
       /// Testing retrieving all roles with  zero  object
       /// </summary>

       [TestMethod]

       public void TestRetrieveAllRoles_RetrieveZeroObject()
       {
           var roles = _setupListManager.RetrieveAllSetupLists();

           bool hasAtLeastZeroElement = roles.Count < 0;

           Assert.IsFalse(hasAtLeastZeroElement);
       }



       
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/03/05
        /// 
        /// 
        /// Testing retrieving setuplist by Invalid id
        /// </summary>
        [TestMethod]
  [ExpectedException(typeof(ArgumentException))]

        public void TestRetrieveAllSetupListBywithInValidID()
        {


            SetupList setupList = new SetupList();
            int setupListID = setupList.SetupListID;
            const int setupListIDTest = 1000011;
            var setupLists = _setupListManager.RetrieveSetupListBySetupListID(setupListID);

            int expected = setupLists.SetupListID;

            Assert.AreEqual(expected, setupListIDTest);
        }

  



    }
}
