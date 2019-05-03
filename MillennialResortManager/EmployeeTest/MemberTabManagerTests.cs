using System;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
/// <summary>
/// Author: Jared Greenfield
/// Date Created: 2019-04-30
/// Unit tests for the MemberTabManager
/// </summary>
namespace UnitTests
{
    [TestClass]
    public class MemberTabManagerTests
    {
        private MemberTabManager _memberTabManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _memberTabManager = new MemberTabManager(new MemberTabAccessorMock());
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Date Created: 2019-04-30
        /// RetrieveLastMemberTabByMemberID Tests
        /// </summary>
        [TestMethod]
        public void TestRetrieveLastMemberTabByMemberIDValidID()
        {
            //Arrange
            int memberID = 100000;
            MemberTab tab = null;
            // Act 
            tab = _memberTabManager.RetrieveLastMemberTabByMemberID(memberID);
            //Assert
            Assert.IsNotNull(tab);
            Assert.AreEqual(memberID, tab.MemberID);
        }

        [TestMethod]
        public void TestRetrieveLastMemberTabByMemberIDIDNotValid()
        {
            //Arrange
            int memberID = -1;
            MemberTab tab = null;
            // Act 
            tab = _memberTabManager.RetrieveLastMemberTabByMemberID(memberID);
            //Assert
            Assert.IsNull(tab);
        }
    }
}
