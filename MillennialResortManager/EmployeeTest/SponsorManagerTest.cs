using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessLayer;

using DataObjects;

namespace UnitTests
{
    /// <summary>
    /// Author: Gunardi Saputra
    /// Created : 2/28/2019
    /// Here are the Test Methods for SponsorManager
    /// </summary>
    [TestClass]
    public class SponsorManagerTests
    {
        private ISponsorManager _sponsorsManager;
        private List<Sponsor> _sponsors;
        private SponsorAccessorMock _sponsorsAccessorMock;

        [TestInitialize]
        public void testSetup()
        {
            _sponsorsAccessorMock = new SponsorAccessorMock();
            _sponsorsManager = new SponsorManager(_sponsorsAccessorMock);
            _sponsors = new List<Sponsor>();
            _sponsors = _sponsorsManager.SelectAllSponsors();
        }

        private string createLongString(int length)
        {
            string longString = "";
            for (int i = 0; i < length; i++)
            {
                longString += "I";
            }
            return longString;
        }

        private void setSponsorss(Sponsor oldSponsor, Sponsor newSponsor)
        {
            newSponsor.SponsorID = oldSponsor.SponsorID;
            newSponsor.Name = oldSponsor.Name;
            newSponsor.Address = oldSponsor.Address;
            newSponsor.City = oldSponsor.City;
            newSponsor.State = oldSponsor.State;
            newSponsor.PhoneNumber = oldSponsor.PhoneNumber;
            newSponsor.Email = oldSponsor.Email;
            newSponsor.ContactFirstName = oldSponsor.ContactFirstName;
            newSponsor.ContactLastName = oldSponsor.ContactLastName;
            //newSponsor.StatusID = oldSponsor.StatusID;
            newSponsor.DateAdded = oldSponsor.DateAdded;
            newSponsor.Active = oldSponsor.Active;
        }





        [TestMethod]
        public void TestSelectAllSponsors()
        {
            //Arange
            List<Sponsor> sponsors = null;
            //Act
            sponsors = _sponsorsManager.SelectAllSponsors();
            //Assert
            CollectionAssert.Equals(_sponsors, sponsors);
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created : 2/28/2019
        /// Here starts the InsertSponsor Unit Tests
        /// </summary>
        [TestMethod]
        public void TestCreateSponsorValidInput()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor()
            {
                SponsorID = 100004,
                Name = "ABC",
                Address = "123 ABC",
                City = "Cedar",
                State = "IA",
                PhoneNumber = "13195551234",
                Email = "adam@abc.com",
                ContactFirstName = "Adam",
                ContactLastName = "Smith",
                //StatusID = "New",
                DateAdded = DateTime.Now,
                Active = true
            };
            //Act
            _sponsorsManager.InsertSponsor(newSponsor);
            //Assert
            //Updates the list of Sponsors
            _sponsors = _sponsorsManager.SelectAllSponsors();
            //Checks to see if the new Sponsor is in the updated list of sponsors
            Assert.IsNotNull(_sponsors.Find(x => x.SponsorID == newSponsor.SponsorID &&
                                                x.Name == newSponsor.Name &&
                                                x.Address == newSponsor.Address &&
                                                x.City == newSponsor.City &&
                                                x.State == newSponsor.State &&
                                                x.PhoneNumber == newSponsor.PhoneNumber &&
                                                x.Email == newSponsor.Email &&
                                                x.ContactFirstName == newSponsor.ContactFirstName &&
                                                x.ContactLastName == newSponsor.ContactLastName &&
                                                //x.StatusID == newSponsor.StatusID &&
                                                x.DateAdded.Day == newSponsor.DateAdded.Day &&
                                                x.Active == newSponsor.Active));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSponsorInValidInputAddress()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor()
            {
                SponsorID = 100004,
                Name = "ABC",
                Address = "123",
                City = "Cedar",
                State = "IA",
                PhoneNumber = "13195551234",
                Email = "adam@abc.com",
                ContactFirstName = "Adam",
                ContactLastName = "Smith",
                //StatusID = "New",
                DateAdded = DateTime.Now,
                Active = true
            };
            // Act
            //Since Address is out of the current range of valid Guests 
            // This should throw an Exception
            _sponsorsManager.InsertSponsor(newSponsor);
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSponsorInValidInputCity()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor()
            {
                SponsorID = 100004,
                Name = "ABC",
                Address = "123 ABC",
                City = "",
                State = "IA",
                PhoneNumber = "13195551234",
                Email = "adam@abc.com",
                ContactFirstName = "Adam",
                ContactLastName = "Smith",
                //StatusID = "New",
                DateAdded = DateTime.Now,
                Active = true
            };

            //Since City is out of the current range of valid Sponsor 
            //This should throw an Exception
            _sponsorsManager.InsertSponsor(newSponsor);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSponsorInValidInputState()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor()
            {
                SponsorID = 100004,
                Name = "ABC",
                Address = "123 ABC",
                City = "Cedar",
                State = "",
                PhoneNumber = "13195551234",
                Email = "adam@abc.com",
                ContactFirstName = "Adam",
                ContactLastName = "Smith",
                //StatusID = "New",
                DateAdded = DateTime.Now,
                Active = true
            };

            //Act
            //Since State is out of the current range of valid States This should throw an Exception
            _sponsorsManager.InsertSponsor(newSponsor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSponsorInValidInputPhoneNumber()
        {

            //Arrange
            Sponsor newSponsor = new Sponsor()
            {
                SponsorID = 100004,
                Name = "ABC",
                Address = "123 ABC",
                City = "Cedar",
                State = "",
                PhoneNumber = "ABC",
                Email = "adam@abc.com",
                ContactFirstName = "Adam",
                ContactLastName = "Smith",
                //StatusID = "New",
                DateAdded = DateTime.Now,
                Active = true
            };
            //Act
            //Since PhoneNumber is out of the current range of valid PhoneNumbers This should throw an Exception
            _sponsorsManager.InsertSponsor(newSponsor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSponsorInValidInputContactLastName()
        {

            //Arrange
            Sponsor newSponsor = new Sponsor()
            {
                SponsorID = 100004,
                Name = "ABC",
                Address = "123 ABC",
                City = "Cedar",
                State = "IA",
                PhoneNumber = "ABC",
                Email = "adam@abc.com",
                ContactFirstName = "Adam",
                ContactLastName = "123",
                //StatusID = "New",
                DateAdded = DateTime.Now,
                Active = true
            };

            //Act
            //Since ContactLastName is longer than out currently acceptable range this should throw an Exception
            _sponsorsManager.InsertSponsor(newSponsor);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveSponsorInValidInput()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor();
            int invalidSponsorID = -1;
            //Act
            newSponsor = _sponsorsManager.SelectSponsor(invalidSponsorID);
            //Assert
            //Assert.AreEqual(newSponsor.SponsorID, 0);
        }

        /// <summary>
        /// Author: Gunardi Saputra
        /// Created : 2/28/2019
        /// Here starts the UpdateSponsor Unit Tests
        /// </summary>

        [TestMethod]

        public void TestUpdateSponsorValidInput()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor();
            setSponsorss(_sponsors[1], newSponsor);
            string newContactLastName = "This test";
            newSponsor.ContactLastName = newContactLastName;
            //Act
            _sponsorsManager.UpdateSponsor(_sponsors[0], newSponsor);
            //Assert
            _sponsors = _sponsorsManager.SelectAllSponsors();
            Assert.AreEqual(_sponsorsManager.SelectSponsor(_sponsors[0].SponsorID).ContactLastName, newContactLastName);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSponsorInValidInputContactLastName()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor();
            setSponsorss(_sponsors[0], newSponsor);
            string newContactLastName = "This test is updating the ContactLastName in TestUpdateSponsorValidInput()" + createLongString(1000);
            newSponsor.ContactLastName = newContactLastName;
            //Act
            _sponsorsManager.UpdateSponsor(_sponsors[0], newSponsor);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSponsorInValidAddress()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor();
            setSponsorss(_sponsors[0], newSponsor);
            newSponsor.Address = "500";
            //Act
            _sponsorsManager.UpdateSponsor(_sponsors[0], newSponsor);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSponsorInValidCity()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor();
            setSponsorss(_sponsors[0], newSponsor);
            newSponsor.City = "";
            //Act
            _sponsorsManager.UpdateSponsor(_sponsors[0], newSponsor);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateSponsorInValidName()
        {
            //Arrange
            Sponsor newSponsor = new Sponsor();
            setSponsorss(_sponsors[0], newSponsor);
            newSponsor.Name = "";
            //Act
            _sponsorsManager.UpdateSponsor(_sponsors[0], newSponsor);
        }


        /// <summary>
        /// Author: Gunardi Saputra
        /// Created : 2/28/2019
        /// Here starts the DeactivateSponsor Unit Tests
        /// </summary>

        [TestMethod]
        public void TestDeactivateSponsorValid()
        {
            //Arrange
            int validSponsorID = _sponsors[0].SponsorID;
            bool activeStatus = _sponsors[0].Active;
            //Act
            _sponsorsManager.DeleteSponsor(validSponsorID, activeStatus);
            //Assert
            Assert.IsFalse(_sponsorsManager.SelectSponsor(validSponsorID).Active);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateSponsorInValidSponsorID()
        {
            //Arrange
            int invalidSponsorID = -1;
            bool activeStatus = true;
            //Act
            _sponsorsManager.DeleteSponsor(invalidSponsorID, activeStatus);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteSponsorValid()
        {
            //Arrange
            int validSponsorID = _sponsors[0].SponsorID;
            bool activeStatus = _sponsors[0].Active;
            //Act
            _sponsorsManager.DeleteSponsor(validSponsorID, false);
            //Assert
            _sponsorsManager.SelectSponsor(validSponsorID);
        }

        [TestMethod]
        public void TestRetrieveAllVMSponsors()
        {
            //Arrange
            List<BrowseSponsor> _vms = new List<BrowseSponsor>();
            //Act
            _vms = _sponsorsManager.SelectAllVMSponsors();
            //Assert
            Assert.IsNotNull(_vms);
        }


    }
}
