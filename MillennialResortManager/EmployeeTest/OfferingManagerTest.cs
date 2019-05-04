using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer.Tests
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
        private List<OfferingVM> _viewModels;
        private OfferingAccessorMock _mock;

        [TestInitialize]
        public void TestSetup()
        {
            _mock = new OfferingAccessorMock();
            _offeringManager = new OfferingManager(_mock);
            _offerings = new List<Offering>();
            _viewModels = new List<OfferingVM>();
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

        //Select Tests

        /// <summary>
        /// Created By: Jared Greenfield
        /// Created Date: 2019/03/28
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllOfferingViewModelValidInput()
        {
            //Arrange
            List<OfferingVM> viewModels = null;
            //Act
            viewModels = _offeringManager.RetrieveAllOfferingViewModels();
            //Assert
            Assert.IsNotNull(viewModels);

        }

        /// <summary>
        /// Created By: Jared Greenfield
        /// Created Date: 2019/03/28
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllOfferingTypesValidInput()
        {
            //Arrange
            List<string> types = null;
            //Act
            types = _offeringManager.RetrieveAllOfferingTypes();
            //Assert
            Assert.IsNotNull(types);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        // Create Tests

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateOfferingInvalidInputOfferingTypeNull()
        {
            //Arrange
            Offering offering = new Offering(100050, null, 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type cannot be null, this should throw an exception.
            _offeringManager.CreateOffering(offering); ;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        //Update tests

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateOfferingInvalidInputIDOfferingTypeNull()
        {
            //Arrange
            Offering offering = new Offering(100000, null, 100000, "Beach front room with a view of sharks.", (Decimal)300.99, true);
            //Act
            //Because Offering Type cannot be null, this should throw an exception.
            _offeringManager.UpdateOffering(_offeringManager.RetrieveOfferingByID(100000), offering);
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 02/20/2019
        /// </summary>
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

        //Delete, Deactivate, Reactivate tests

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/29/2019
        /// </summary>
        [TestMethod]
        public void TestDeleteOfferingByIDValidInput()
        {
            //Arrange
            int offeringID = 100000;
            int countOfRecords = 0;
            //Act
            bool resultValue = _offeringManager.DeleteOfferingByID(offeringID);
            //Assert
            countOfRecords = 0;
            if (_offeringManager.RetrieveOfferingByID(offeringID) != null)
            {
                countOfRecords = 1;
            }
            Assert.IsTrue(resultValue);
            Assert.AreEqual(0, countOfRecords);

        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/29/2019
        /// </summary>
        [TestMethod]
        public void TestDeleteOfferingByIDDoesntExist()
        {
            //Arrange
            int offeringID = 200000;
            int countOfRecords = 0;
            //Act
            bool resultValue = _offeringManager.DeleteOfferingByID(offeringID);
            //Assert
            countOfRecords = 0;
            if (_offeringManager.RetrieveOfferingByID(offeringID) != null)
            {
                countOfRecords = 1;
            }
            Assert.IsFalse(resultValue);
            Assert.AreEqual(0, countOfRecords);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/29/2019
        /// </summary>
        [TestMethod]
        public void TestDeactivateOfferingByIDValidInput()
        {
            //Arrange
            int offeringID = 100000;
            int countOfRecords = 0;
            //Act
            bool resultValue = _offeringManager.DeactivateOfferingByID(offeringID);
            //Assert
            countOfRecords = 0;
            if (_offeringManager.RetrieveOfferingByID(offeringID) != null)
            {
                if (_offeringManager.RetrieveOfferingByID(offeringID).Active == false)
                {
                    countOfRecords = 1;
                }
            }
            Assert.IsTrue(resultValue);
            Assert.AreEqual(1, countOfRecords);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/29/2019
        /// </summary>
        [TestMethod]
        public void TestDeactivateOfferingByIDDoesntExist()
        {
            //Arrange
            int offeringID = 200000;
            int countOfRecords = 0;
            //Act
            bool resultValue = _offeringManager.DeactivateOfferingByID(offeringID);
            //Assert
            countOfRecords = 0;
            if (_offeringManager.RetrieveOfferingByID(offeringID) != null)
            {
                if (_offeringManager.RetrieveOfferingByID(offeringID).Active == false)
                {
                    countOfRecords = 1;
                }
            }
            Assert.IsFalse(resultValue);
            Assert.AreEqual(0, countOfRecords);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/29/2019
        /// </summary>
        [TestMethod]
        public void TestReactivateOfferingByIDValidInput()
        {
            //Arrange
            int offeringID = 100003;
            int countOfRecords = 0;
            //Act
            bool resultValue = _offeringManager.ReactivateOfferingByID(offeringID);
            //Assert
            countOfRecords = 0;
            if (_offeringManager.RetrieveOfferingByID(offeringID) != null)
            {
                if (_offeringManager.RetrieveOfferingByID(offeringID).Active == true)
                {
                    countOfRecords = 1;
                }
            }
            Assert.IsTrue(resultValue);
            Assert.AreEqual(1, countOfRecords);

        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 03/29/2019
        /// </summary>
        [TestMethod]
        public void TestReactivateOfferingByIDDoesntExist()
        {
            //Arrange
            int offeringID = 200000;
            int countOfRecords = 0;
            //Act
            bool resultValue = _offeringManager.ReactivateOfferingByID(offeringID);
            //Assert
            countOfRecords = 0;
            if (_offeringManager.RetrieveOfferingByID(offeringID) != null)
            {
                if (_offeringManager.RetrieveOfferingByID(offeringID).Active == true)
                {
                    countOfRecords = 1;
                }
            }
            Assert.IsFalse(resultValue);
            Assert.AreEqual(0, countOfRecords);

        }

    }
}
