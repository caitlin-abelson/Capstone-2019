using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created On: 2019-05-01
    /// Unit tests for CheckoutReceiptManager
    /// </summary>
    [TestClass]
    public class CheckoutReceiptManagerTests
    {
        private CheckoutReceiptManager _receiptManager;
        [TestInitialize]
        public void TestSetup()
        {
            _receiptManager = new CheckoutReceiptManager(new CheckoutReceiptAccessorMock());
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        public void TestGenerateMemberTabReceiptValid()
        {
            //Arrange
            bool result = false;
            Reservation reservation = new Reservation();
            Member member = new Member();
            List<OfferingVM> offerings = new List<OfferingVM>();
            MemberTab tab = new MemberTab();
            string filePath = "C:\\Users";
            List<GuestRoomAssignmentVM> guests = new List<GuestRoomAssignmentVM>();
            // Act 
            result = _receiptManager.generateMemberTabReceipt(reservation, member, offerings, tab, filePath, guests);
            // Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateMemberTabReceiptNullReservation()
        {
            //Arrange
            bool result = false;
            Member member = new Member();
            List<OfferingVM> offerings = new List<OfferingVM>();
            MemberTab tab = new MemberTab();
            string filePath = "C:\\Users";
            List<GuestRoomAssignmentVM> guests = new List<GuestRoomAssignmentVM>();
            // Act 
            result = _receiptManager.generateMemberTabReceipt(null, member, offerings, tab, filePath, guests);
            
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateMemberTabReceiptNullMember()
        {
            //Arrange
            bool result = false;
            Reservation reservation = new Reservation();
            List<OfferingVM> offerings = new List<OfferingVM>();
            MemberTab tab = new MemberTab();
            string filePath = "C:\\Users";
            List<GuestRoomAssignmentVM> guests = new List<GuestRoomAssignmentVM>();
            // Act 
            result = _receiptManager.generateMemberTabReceipt(reservation, null, offerings, tab, filePath, guests);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateMemberTabReceiptNullOfferings()
        {
            //Arrange
            bool result = false;
            Reservation reservation = new Reservation();
            Member member = new Member();
            MemberTab tab = new MemberTab();
            string filePath = "C:\\Users";
            List<GuestRoomAssignmentVM> guests = new List<GuestRoomAssignmentVM>();
            // Act 
            result = _receiptManager.generateMemberTabReceipt(reservation, member, null, tab, filePath, guests);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateMemberTabReceiptNullTab()
        {
            //Arrange
            bool result = false;
            Reservation reservation = new Reservation();
            Member member = new Member();
            List<OfferingVM> offerings = new List<OfferingVM>();
            string filePath = "C:\\Users";
            List<GuestRoomAssignmentVM> guests = new List<GuestRoomAssignmentVM>();
            // Act 
            result = _receiptManager.generateMemberTabReceipt(reservation, member, offerings, null, filePath, guests);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateMemberTabReceiptNullFilePath()
        {
            //Arrange
            bool result = false;
            Reservation reservation = new Reservation();
            Member member = new Member();
            List<OfferingVM> offerings = new List<OfferingVM>();
            MemberTab tab = new MemberTab();
            List<GuestRoomAssignmentVM> guests = new List<GuestRoomAssignmentVM>();
            // Act 
            result = _receiptManager.generateMemberTabReceipt(reservation, member, offerings, tab, null, guests);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateMemberTabReceiptEmptyFilePath()
        {
            //Arrange
            bool result = false;
            Reservation reservation = new Reservation();
            Member member = new Member();
            List<OfferingVM> offerings = new List<OfferingVM>();
            MemberTab tab = new MemberTab();
            string filePath = "";
            List<GuestRoomAssignmentVM> guests = new List<GuestRoomAssignmentVM>();
            // Act 
            result = _receiptManager.generateMemberTabReceipt(reservation, member, offerings, tab, filePath, guests);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-05-01
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGenerateMemberTabReceiptNullGuests ()
        {
            //Arrange
            bool result = false;
            Reservation reservation = new Reservation();
            Member member = new Member();
            List<OfferingVM> offerings = new List<OfferingVM>();
            MemberTab tab = new MemberTab();
            string filePath = "C:\\Users";
            // Act 
            result = _receiptManager.generateMemberTabReceipt(reservation, member, offerings, tab, filePath, null);

        }
    }
}
