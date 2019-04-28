using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    /// <summary>
    /// Author: Alisa Roehr
    /// Created : 2019/02/21
    /// Here are the Test Methods for GuestManager
    /// </summary>
    [TestClass]
    public class GuestManagerTests
    {
        private List<Guest> _guests;
        private GuestManager _guestManager;
        private GuestAccessorMock _gues;

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Setting up the TestInitializer
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _gues = new GuestAccessorMock();
            _guestManager = new GuestManager(_gues);
            _guests = new List<Guest>();
            _guests = _guestManager.ReadAllGuests();
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// creating a string
        /// </summary>
        /// <param name="length">lemgth of string to be created</param>
        private string createString(int length)
        {
            string testLength = "";
            for (int i = 0; i < length; i++)
            {
                testLength += "*";
            }
            return testLength;
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// setting a guest up
        /// </summary>
        /// <param name="newGuest">the new guest</param>
        /// <param name="oldGuest">the old guest</param>
        private void setGuest(Guest oldGuest, Guest newGuest)
        {
            newGuest.GuestID = oldGuest.GuestID;
            newGuest.MemberID = oldGuest.MemberID;
            newGuest.FirstName = oldGuest.FirstName;
            newGuest.LastName = oldGuest.LastName;
            newGuest.Email = oldGuest.Email;
            newGuest.PhoneNumber = oldGuest.PhoneNumber;
            newGuest.GuestTypeID = oldGuest.GuestTypeID;
            newGuest.Minor = oldGuest.Minor;
            newGuest.Active = oldGuest.Active;
            newGuest.ReceiveTexts = oldGuest.ReceiveTexts;
            newGuest.EmergencyFirstName = oldGuest.EmergencyFirstName; 
            newGuest.EmergencyLastName = oldGuest.EmergencyLastName;
            newGuest.EmergencyPhoneNumber = oldGuest.EmergencyPhoneNumber;
            newGuest.EmergencyRelation = oldGuest.EmergencyRelation;
        }

        // CREATE

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing valid input for creating a guest.
        /// </summary>
        [TestMethod]
        public void TestCreateGuestValidInput()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
            //Assert
            _guests = _guestManager.ReadAllGuests();

            Assert.IsNotNull(_guests.Find(x => x.GuestID == guest.GuestID &&
                                                x.MemberID == guest.MemberID &&
                                                x.FirstName == guest.FirstName &&
                                                x.LastName == guest.LastName &&
                                                x.Email == guest.Email &&
                                                x.PhoneNumber == guest.PhoneNumber &&
                                                x.GuestTypeID == guest.GuestTypeID &&
                                                x.Minor == guest.Minor &&
                                                x.Active == guest.Active &&
                                                x.ReceiveTexts == guest.ReceiveTexts &&
                                                x.EmergencyFirstName == guest.EmergencyFirstName &&
                                                x.EmergencyLastName == guest.EmergencyLastName &&
                                                x.EmergencyPhoneNumber == guest.EmergencyPhoneNumber &&
                                                x.EmergencyRelation == guest.EmergencyRelation));
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing invalid input for creating a guest.
        /// </summary>
        /*[TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidGuestID()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 9999999, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }*/
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidMemberID()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 0, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidGuestTypeID()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidFirstName()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidLastName()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidPhoneNumber()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidEmail()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidEmergencyFirstName()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "abc@def.com", ReceiveTexts = true, EmergencyFirstName = "", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidEmergencyLastName()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidEmergencyPhoneNumber()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "", EmergencyRelation = "Brother-in-law" };
            //Act
            _guestManager.CreateGuest(guest);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateGuestInValidEmergencyRelation()
        {
            //Arrange
            Guest guest = new Guest() { GuestID = 100010, MemberID = 100002, GuestTypeID = "Student", FirstName = "Johnny", LastName = "Johnson", PhoneNumber = "3192968018", Minor = true, Active = true, Email = "", ReceiveTexts = true, EmergencyFirstName = "Albion", EmergencyLastName = "Bumblebee", EmergencyPhoneNumber = "3192885567", EmergencyRelation = "" };
            //Act
            _guestManager.CreateGuest(guest);
        }

        // RETRIEVE

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the retrieve all guests works and does not return null. 
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllGuests()
        {
            //Arrange
            List<Guest> guests = null;
            //Act
            guests = _guestManager.ReadAllGuests();
            //Assert
            Assert.IsNotNull(guests);
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the retrieve guest works and does not return null. 
        /// </summary>
        [TestMethod]
        public void TestRetrieveGuest()
        {
            //Arrange
            int validGuestID = _guests[0].GuestID;
            //Act
            Guest guest = _guestManager.ReadGuestByGuestID(validGuestID);
            //Assert
            Assert.IsTrue(_guests[0].GuestID == guest.GuestID &&
                            _guests[0].MemberID == guest.MemberID &&
                            _guests[0].FirstName == guest.FirstName &&
                            _guests[0].LastName == guest.LastName &&
                            _guests[0].Email == guest.Email &&
                            _guests[0].PhoneNumber == guest.PhoneNumber &&
                            _guests[0].GuestTypeID == guest.GuestTypeID &&
                            _guests[0].Minor == guest.Minor &&
                            _guests[0].Active == guest.Active &&
                            _guests[0].ReceiveTexts == guest.ReceiveTexts &&
                            _guests[0].EmergencyFirstName == guest.EmergencyFirstName &&
                            _guests[0].EmergencyLastName == guest.EmergencyLastName &&
                            _guests[0].EmergencyPhoneNumber == guest.EmergencyPhoneNumber &&
                            _guests[0].EmergencyRelation == guest.EmergencyRelation);
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the retrieve guests by name works and does not return null. 
        /// </summary>
        [TestMethod]
        public void TestRetrieveGuestsSearchedBothNames()
        {
            //Arrange
            List<Guest> guests = null;
            string lname = _guests[0].LastName;
            string fname = _guests[0].FirstName;
            //Act
            guests = _guestManager.RetrieveGuestsSearched(lname, fname);
            //Assert
            Assert.IsNotNull(guests);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the retrieve guests by name works and does not return null. 
        /// </summary>
        [TestMethod]
        public void TestRetrieveGuestsSearchedFirstNameOnly()
        {
            //Arrange
            List<Guest> guests = null;
            string fname = _guests[0].FirstName;
            //Act
            guests = _guestManager.RetrieveGuestsSearched(null, fname);
            //Assert
            Assert.IsNotNull(guests);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the retrieve guests by name works and does not return null. 
        /// </summary>
        [TestMethod]
        public void TestRetrieveGuestsSearchedLastNameOnly()
        {
            //Arrange
            List<Guest> guests = null;
            string lname = _guests[0].LastName;
            //Act
            guests = _guestManager.RetrieveGuestsSearched(lname, null);
            //Assert
            Assert.IsNotNull(guests);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the retrieve guests by name works and does not return null. 
        /// </summary>
        [TestMethod]
        public void TestRetrieveGuestsSearchedNeitherName()
        {
            //Arrange
            List<Guest> guests = null;;
            //Act
            guests = _guestManager.RetrieveGuestsSearched(null, null);
            //Assert
            Assert.IsNotNull(guests);
        }

        // UPDATE

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the update guest works and does not return null. 
        /// </summary>
        [TestMethod]
        public void TestEditGuestValid()
        {
            //Arrange
            Guest oldGuest = _guests[0];
            Guest newGuest = _guests[0];
            newGuest.MemberID = 100002;
            newGuest.GuestTypeID = "Student";
            newGuest.LastName = "Johnson";
            newGuest.FirstName = "James";
            newGuest.EmergencyFirstName = "Albus";
            //Act
            _guestManager.EditGuest(newGuest, oldGuest);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.IsTrue(newGuest.GuestID == guest.GuestID &&
                            newGuest.MemberID == guest.MemberID &&
                            newGuest.FirstName == guest.FirstName &&
                            newGuest.LastName == guest.LastName &&
                            newGuest.Email == guest.Email &&
                            newGuest.PhoneNumber == guest.PhoneNumber &&
                            newGuest.GuestTypeID == guest.GuestTypeID &&
                            newGuest.Minor == guest.Minor &&
                            newGuest.Active == guest.Active &&
                            newGuest.ReceiveTexts == guest.ReceiveTexts &&
                            newGuest.EmergencyFirstName == guest.EmergencyFirstName &&
                            newGuest.EmergencyLastName == guest.EmergencyLastName &&
                            newGuest.EmergencyPhoneNumber == guest.EmergencyPhoneNumber &&
                            newGuest.EmergencyRelation == guest.EmergencyRelation);
        }
        
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the update guest works does not work when the guest id is different.
        /// </summary>
       /* [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidOldGuestID()
        {
            //Arrange
            Guest invalidGuest = _guests[0];
            Guest newGuest = _guests[0];
            invalidGuest.GuestID = 0;
            //Act
            _guestManager.EditGuest(newGuest, invalidGuest);
        }*/
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the update guest works does not work when the guest id is different.
        /// </summary>
       /* [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidNewGuestID()
        {
            //Arrange
            Guest oldGuest = _guests[0];
            Guest invalidGuest = _guests[0];
            invalidGuest.GuestID = 0;
            //Act
            _guestManager.EditGuest(invalidGuest, oldGuest);
        }*/
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for MemberID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidMemberIDZero()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.MemberID = 0;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for GuestTypeID.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidGuestTypeIDEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.GuestTypeID = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidGuestTypeIDNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.GuestTypeID = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidGuestTypeIDLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.GuestTypeID = createString(25);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.GuestTypeID, newGuest.GuestTypeID);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidGuestTypeIDTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.GuestTypeID = createString(26);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for First name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidFirstNameEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.FirstName = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidFirstNameNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.FirstName = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidFirstNameTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.FirstName = createString(51);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidFirstNameLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.FirstName = createString(50);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.FirstName, newGuest.FirstName);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for Last name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidLastNameEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.LastName = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidLastNameNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.LastName = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidLastNameTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.LastName = createString(101);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidLastNameLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.LastName = createString(100);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.LastName, newGuest.LastName);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for PhoneNumber.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidPhoneNumberEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.PhoneNumber = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidPhoneNumberNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.PhoneNumber = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidPhoneNumberTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.PhoneNumber = createString(12);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidPhoneNumberLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.PhoneNumber = createString(11);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.PhoneNumber, newGuest.PhoneNumber);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for Email.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmailEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.Email = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidEmailNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.Email = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmailTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.Email = createString(251);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidEmailLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.Email = createString(245);
            newGuest.Email += "@.com";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.Email, newGuest.Email);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for Emergency First name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyFirstNameEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyFirstName = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidEmergencyFirstNameNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyFirstName = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyFirstNameTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyFirstName = createString(51);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidEmergencyFirstNameLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyFirstName = createString(50);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.EmergencyFirstName, newGuest.EmergencyFirstName);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for Emergency Last name.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyLastNameEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyLastName = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidEmergencyLastNameNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyLastName = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyLastNameTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyLastName = createString(101);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidEmergencyLastNameLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyLastName = createString(100);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.EmergencyLastName, newGuest.EmergencyLastName);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for Emergency Phone Number.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyPhoneNumberEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyLastName = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidEmergencyPhoneNumberNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyPhoneNumber = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyPhoneNumberTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyPhoneNumber = createString(12);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidEmergencyPhoneNumberLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyPhoneNumber = createString(11);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.EmergencyPhoneNumber, newGuest.EmergencyPhoneNumber);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        /// 
        /// Testing that the update guest works does not work when there are invalid inputs, for Emergency Relation.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyRelationEmpty()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyRelation = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestEditGuestInvalidEmergencyRelationNull()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyRelation = null;
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyRelationTooLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyRelation = createString(26);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }
        [TestMethod]
        public void TestEditGuestValidEmergencyRelationLong()
        {
            //Arrange
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyRelation = createString(25);
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
            //Assert
            Guest guest = _guestManager.ReadGuestByGuestID(newGuest.GuestID);
            Assert.AreEqual(guest.EmergencyRelation, newGuest.EmergencyRelation);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEditGuestInvalidEmergencyRelation()
        {
            Guest newGuest = new Guest();
            setGuest(_guests[0], newGuest);
            newGuest.EmergencyRelation = "";
            //Act
            _guestManager.EditGuest(newGuest, _guests[0]);
        }

        // DELETE

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the deactivate guest works. 
        /// </summary>
        [TestMethod]
        public void TestDeactivateGuestValid()
        {
            //Arrange
            int validGuestID = _guests[0].GuestID;
            //Act
            _guestManager.DeactivateGuest(validGuestID);
            //Assert
            Assert.IsFalse(_guestManager.ReadGuestByGuestID(validGuestID).Active);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the deactivate guest does not work with invalid guest id. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateGuestInValidGuestID()
        {
            //Arrange
            int invalidGuestID = -1;
            //Act
            _guestManager.DeactivateGuest(invalidGuestID);

        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the delete guest works. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteGuestValid()
        {
            //Arrange
            int validGuestID = _guests[0].GuestID;
            _guestManager.DeactivateGuest(validGuestID);
            //Act
            _guestManager.DeleteGuest(validGuestID);
            //Assert
            _guestManager.ReadGuestByGuestID(validGuestID);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the delete guest does not work with invalid guest id. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteGuestInValidGuestID()
        {
            //Arrange
            int invalidGuestID = -1;
            //Act
            _guestManager.DeleteGuest(invalidGuestID);
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the reactivate guest works. 
        /// </summary>
        [TestMethod]
        public void TestReactivateGuestValid()
        {
            //Arrange
            int validGuestID = _guests[0].GuestID;
            _guestManager.DeactivateGuest(validGuestID);
            //Act
            _guestManager.ReactivateGuest(validGuestID);
            //Assert
            Assert.IsTrue(_guestManager.ReadGuestByGuestID(validGuestID).Active);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the reactivate guest does not work with invalid guest id. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestReactivateGuestInValidGuestID()
        {
            //Arrange
            int invalidGuestID = -1;
            //Act
            _guestManager.ReactivateGuest(invalidGuestID);

        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the check out guest works. 
        /// </summary>
        [TestMethod]
        public void TestCheckOutGuestValid()
        {
            //Arrange
            int validGuestID = _guests[0].GuestID;
            //Act
            _guestManager.CheckOutGuest(validGuestID);
            //Assert
            Assert.IsFalse(_guestManager.ReadGuestByGuestID(validGuestID).CheckedIn);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the check guest does not work with invalid guest id. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCheckOutGuestInValidGuestID()
        {
            //Arrange
            int invalidGuestID = -1;
            //Act
            _guestManager.CheckOutGuest(invalidGuestID);

        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the check in guest works. 
        /// </summary>
        [TestMethod]
        public void TestCheckInGuestValid()
        {
            //Arrange
            int validGuestID = _guests[0].GuestID;
            _guestManager.DeactivateGuest(validGuestID);
            //Act
            _guestManager.CheckInGuest(validGuestID);
            //Assert
            Assert.IsTrue(_guestManager.ReadGuestByGuestID(validGuestID).CheckedIn);
        }
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// 
        /// Testing that the check in guest does not work with invalid guest id. 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCheckInGuestInValidGuestID()
        {
            //Arrange
            int invalidGuestID = -1;
            //Act
            _guestManager.CheckInGuest(invalidGuestID);

        }
        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/04/23
        /// Testing  to retrieve all guest appointment info

        [TestMethod]
        public void TestRetrieveAllGuestAppointments()
        {
            //Arrange
            List<Guest> guests = null;
            //Act
            guests = _guestManager.RetrieveAllGuestAppointmentInfo();
            //Assert
            CollectionAssert.Equals(_guests, guests);

        }
    }
}
