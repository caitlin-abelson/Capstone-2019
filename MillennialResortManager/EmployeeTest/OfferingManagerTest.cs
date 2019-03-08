using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayerTest
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created : 02/20/2019
    /// Here are the Test Methods for ItemManager
    /// </summary>
    [TestClass]
    public class OfferingManagerTest
    {
        private IOfferingManager _offeringManager;
        private List<Offering> _offerings;
        private OfferingAccessorMock _mock;

        [TestInitialize]
        public void TestSetup()
        {
            _mock = new OfferingAccessorMock();
            _offeringManager = new OfferingManager(_mock);
            _offerings = new List<Offering>();
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

        [TestMethod]
        public void TestRetrieveOfferingByIDValidInput()
        {
            //Arrange
            Offering offering = null;
            //Act
            offering = _offeringManager.RetrieveOfferingByID(100000);
            //Assert
            Assert.IsNotNull(offering);
            Assert.AreEqual(100000, offering.OfferingID);
        }

        [TestMethod]
        public void TestRetrieveOfferingByIDInvalidInput()
        {
            //Arrange
            Offering offering = null;
            //Act
            offering = _offeringManager.RetrieveOfferingByID(1);
            //Assert
            Assert.IsNull(offering);
        }

        [TestMethod]
        public void TestCreateOfferingValidInput()
        {
            //Arrange
            Offering offering = new Offering(100050, "Room", 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            _offeringManager.CreateOffering(offering);
            //Assert
            Offering retrievedOffering = _offeringManager.RetrieveOfferingByID(100050);
            Assert.AreEqual(retrievedOffering, offering);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateOfferingInvalidInputOfferingTypeNull()
        {
            //Arrange
            Offering offering = new Offering(100050, null, 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type cannot be null, this should throw an exception.
            _offeringManager.CreateOffering(offering);;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateOfferingInvalidInputOfferingTypeTooShort()
        {
            //Arrange
            Offering offering = new Offering(100050, "", 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type cannot be 0 characters, this should throw an exception.
            _offeringManager.CreateOffering(offering); ;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateOfferingInvalidInputOfferingTypeTooLong()
        {
            //Arrange
            Offering offering = new Offering(100050, createLongString(16), 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type hs a maximum of 15 characters, this should throw an exception.
            _offeringManager.CreateOffering(offering); ;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateOfferingInvalidInputPriceNegative()
        {
            //Arrange
            Offering offering = new Offering(100050, "Room", 100000, "Beach front room with a view of sharks.", (Decimal)(-300.99), true);
            //Act
            //Because price cannot be negative, this should throw an exception.
            _offeringManager.CreateOffering(offering); ;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateOfferingInvalidInputDescriptionTooLong()
        {
            //Arrange
            Offering offering = new Offering(100050, "Room", 100000, "Beach front room with a view of sharks." + createLongString(1000), (Decimal)300.99, true);
            //Act
            //Because Description has a maximum of 1000 characters, this should throw an exception.
            _offeringManager.CreateOffering(offering); ;
        }

        [TestMethod]
        public void TestUpdateOfferingValidInput()
        {
            //Arrange
            Offering newOffering = new Offering(100000, "Room", 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            bool isSuccessful = _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000), newOffering);
            //Assert
            Assert.IsTrue(isSuccessful);
        }

        [TestMethod]
        public void TestUpdateOfferingValidInputIDNotInUse()
        {
            //Arrange
            Offering newOffering = new Offering(100500, "Room", 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            bool isSuccessful = _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000), newOffering);
            //Assert
            Assert.IsFalse(isSuccessful);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateOfferingInvalidInputIDOfferingTypeNull()
        {
            //Arrange
            Offering offering = new Offering(100000, null, 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type cannot be null, this should throw an exception.
            _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000),offering);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateOfferingInvalidInputIDOfferingTypeTooShort()
        {
            //Arrange
            Offering offering = new Offering(100000, "", 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type cannot be 0 characters, this should throw an exception.
            _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000), offering);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateOfferingInvalidInputIDOfferingTypeTooLong()
        {
            //Arrange
            Offering offering = new Offering(100000, createLongString(1001), 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type hs a maximum of 15 characters, this should throw an exception.
            _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000), offering);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateOfferingInvalidInputIDPriceNegative()
        {
            //Arrange
            Offering offering = new Offering(100000, "Room", 100000, "Beach front room with a view of sharks.", (Decimal)(-300.99), true);
            //Act
            //Because price cannot be negative, this should throw an exception.
            _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000), offering);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateOfferingInvalidInputIDDescriptionTooLong()
        {
            //Arrange
            Offering offering = new Offering(100000, "Room", 100000, "Beach front room with a view of sharks." + createLongString(1000), (Decimal)300.99, true);
            //Act
            //Because Description has a maximum of 1000 characters, this should throw an exception.
            _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000), offering);
        }
    }
}
