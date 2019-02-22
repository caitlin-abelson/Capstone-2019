using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;


/// <summary>
/// Author: Eric Bostwick
/// Created 2/18/2019
/// Unit Tests For Testing ItemSupplier Manager
/// </summary>


namespace UnitTests
{
    [TestClass]
    public class ItemSupplierTests
    {
        private List<ItemSupplier> _itemSuppliers;
        private List<Supplier> _suppliers;
        private IItemSupplierManager _itemSupplierManager;
        private ItemSupplierAccessorMock _itemSupplierAccessorMock;

        [TestInitialize]

        public void testSetup()
        {
            _itemSupplierAccessorMock = new ItemSupplierAccessorMock();
            _itemSupplierManager = new ItemSupplierManager(_itemSupplierAccessorMock);
            _suppliers = new List<Supplier>();
            //there two items that will be returned here for later testing
            _itemSuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(100000);
        }

        private string createString(int length)
        {
            string testLength = "";
            for (int i = 0; i < length; i++)
            {
                testLength += "*";
            }
            return testLength;
        }
        private void setItemSupplier(ItemSupplier oldItemSupplier, ItemSupplier newItemSupplier)
        {
            oldItemSupplier.ItemSupplierActive = newItemSupplier.ItemSupplierActive;
            oldItemSupplier.PrimarySupplier = newItemSupplier.PrimarySupplier;
            oldItemSupplier.UnitPrice = newItemSupplier.UnitPrice;
            oldItemSupplier.ItemID = newItemSupplier.ItemID;
            oldItemSupplier.SupplierID = newItemSupplier.SupplierID;
            oldItemSupplier.LeadTimeDays = newItemSupplier.LeadTimeDays;
            oldItemSupplier.Active = newItemSupplier.Active;
            oldItemSupplier.Address = newItemSupplier.Address;
            oldItemSupplier.City = newItemSupplier.City;
            oldItemSupplier.ContactFirstName = newItemSupplier.ContactFirstName;
            oldItemSupplier.ContactLastName = newItemSupplier.ContactLastName;
            oldItemSupplier.Country = newItemSupplier.Country;
            oldItemSupplier.DateAdded = newItemSupplier.DateAdded;
            oldItemSupplier.Description = newItemSupplier.Description;
            oldItemSupplier.Email = newItemSupplier.Email;
            oldItemSupplier.Name = newItemSupplier.Name;
            oldItemSupplier.PhoneNumber = newItemSupplier.PhoneNumber;
            oldItemSupplier.PostalCode = newItemSupplier.PostalCode;
            oldItemSupplier.State = newItemSupplier.State;

        }

        [TestMethod]
        public void TestCreateItemSupplierValidInput()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 5,
                UnitPrice = 1.00M
            };

            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
            //Assert
            _itemSuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(newItemSupplier.ItemID);

            Assert.IsNotNull(_itemSuppliers.Find(x => x.ItemID == newItemSupplier.ItemID && x.SupplierID == newItemSupplier.SupplierID &&
                x.PrimarySupplier == newItemSupplier.PrimarySupplier && x.ItemSupplierActive == newItemSupplier.ItemSupplierActive &&
                x.LeadTimeDays == newItemSupplier.LeadTimeDays && x.UnitPrice == newItemSupplier.UnitPrice
            ));
        }
        [TestMethod]
        public void TestCreateItemSupplierValidInputLeadTime1()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 1,
                UnitPrice = 1.00M
            };

            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
            //Assert
            _itemSuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(newItemSupplier.ItemID);

            Assert.IsNotNull(_itemSuppliers.Find(x => x.ItemID == newItemSupplier.ItemID && x.SupplierID == newItemSupplier.SupplierID &&
                x.PrimarySupplier == newItemSupplier.PrimarySupplier && x.ItemSupplierActive == newItemSupplier.ItemSupplierActive &&
                x.LeadTimeDays == newItemSupplier.LeadTimeDays && x.UnitPrice == newItemSupplier.UnitPrice
            ));
        }
        [TestMethod]
        public void TestCreateItemSupplierValidInputLeadTime365()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 365,
                UnitPrice = 1.00M
            };

            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
            //Assert
            _itemSuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(newItemSupplier.ItemID);

            Assert.IsNotNull(_itemSuppliers.Find(x => x.ItemID == newItemSupplier.ItemID && x.SupplierID == newItemSupplier.SupplierID &&
                x.PrimarySupplier == newItemSupplier.PrimarySupplier && x.ItemSupplierActive == newItemSupplier.ItemSupplierActive &&
                x.LeadTimeDays == newItemSupplier.LeadTimeDays && x.UnitPrice == newItemSupplier.UnitPrice
            ));
        }
        [TestMethod]
        public void TestCreateItemSupplierValidInputUnitPriceOneCent()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 180,
                UnitPrice = 0.01M
            };

            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
            //Assert
            _itemSuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(newItemSupplier.ItemID);

            Assert.IsNotNull(_itemSuppliers.Find(x => x.ItemID == newItemSupplier.ItemID && x.SupplierID == newItemSupplier.SupplierID &&
                x.PrimarySupplier == newItemSupplier.PrimarySupplier && x.ItemSupplierActive == newItemSupplier.ItemSupplierActive &&
                x.LeadTimeDays == newItemSupplier.LeadTimeDays && x.UnitPrice == newItemSupplier.UnitPrice
            ));
        }
        [TestMethod]
        public void TestCreateItemSupplierValidInputUnitPrice9999Dot99()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 180,
                UnitPrice = 9999.99M
            };

            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
            //Assert
            _itemSuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(newItemSupplier.ItemID);

            Assert.IsNotNull(_itemSuppliers.Find(x => x.ItemID == newItemSupplier.ItemID && x.SupplierID == newItemSupplier.SupplierID &&
                x.PrimarySupplier == newItemSupplier.PrimarySupplier && x.ItemSupplierActive == newItemSupplier.ItemSupplierActive &&
                x.LeadTimeDays == newItemSupplier.LeadTimeDays && x.UnitPrice == newItemSupplier.UnitPrice
            ));
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemSupplierInValidInputLeadTimeDaysNegative()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = -1,
                UnitPrice = 1.00M
            };
            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemSupplierInValidInputLeadTimeDaysGreaterThan365()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 366,
                UnitPrice = 1.00M
            };
            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemSupplierInValidInputUnitPriceNegative()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 10,
                UnitPrice = -0.01M
            };
            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemSupplierInValidInputUnitPriceGreaterThan9999()
        {
            //arrange
            ItemSupplier newItemSupplier = new ItemSupplier()
            {
                ItemID = 100004,
                SupplierID = 100009,
                Active = true,
                Address = "2455 Staggering Home St",
                City = "North Liberty",
                State = "IA",
                PostalCode = "52446",
                Country = "USA",
                ContactFirstName = "Joe",
                ContactLastName = "Smith",
                PhoneNumber = "13193353333",
                DateAdded = DateTime.Parse("2011-05-15"),
                Description = "",
                Email = "",
                ItemSupplierActive = true,
                Name = "Alcohol Whole Supply",
                PrimarySupplier = true,
                LeadTimeDays = 10,
                UnitPrice = 10000M
            };
            //Act
            _itemSupplierManager.CreateItemSupplier(newItemSupplier);
        }

        [TestMethod]
        public void TestRetrieveAllSuppliersForItemSupplierManagement()
        {
            //Arrange
            List<Supplier> suppliers = null;
            //Act
            suppliers = _itemSupplierManager.RetrieveAllSuppliersForItemSupplierManagement(100000);
            //Assert
            Assert.AreEqual(suppliers.Count, 8);
        }

        [TestMethod]
        public void TestRetrieveAllItemSuppliersByItemID()
        {
            //Arrange
            List<ItemSupplier> _itemsuppliers = null;
            //Act
            _itemsuppliers = _itemSupplierManager.RetrieveAllItemSuppliersByItemID(100000);
            //Assert
            Assert.AreEqual(_itemsuppliers[0].ItemID, 100000);
        }

        [TestMethod]
        public void TestRetrieveItemSupplier()
        {
            //Arrange
            ItemSupplier _itemsupplier = null;
            //Act
            _itemsupplier = _itemSupplierManager.RetrieveItemSupplier(100002, 100005);
            //Assert
            Assert.AreEqual(_itemsupplier.ItemID, 100002);
            Assert.AreEqual(_itemsupplier.SupplierID, 100005);
        }

        [TestMethod]
        public void TestUpdateItemSupplierMakeActive()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.ItemSupplierActive = true;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).ItemSupplierActive, true);
        }

        [TestMethod]
        public void TestUpdateItemSupplierMakeInActive()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.ItemSupplierActive = false;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).ItemSupplierActive, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemSupplierInvalidLeadTimeDaysNegative()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.LeadTimeDays = -1;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemSupplierInvalidLeadTimeDays366()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.LeadTimeDays = 366;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
        }
        [TestMethod]
        public void TestUpdateItemSupplierValidLeadTimeDays1()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.LeadTimeDays = 1;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).LeadTimeDays, 1);
        }

        [TestMethod]
        public void TestUpdateItemSupplierValidLeadTimeDays365()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.LeadTimeDays = 365;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).LeadTimeDays, 365);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemSupplierInvalidUnitPriceNegative()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.UnitPrice = -0.01M;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemSupplierInvalidUnitPriceZero()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.UnitPrice = 0.00M;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemSupplierInvalidUnitPriceTooHigh()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.UnitPrice = 10000.00M;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
        }
        [TestMethod]
        public void TestUpdateItemSupplierLeadTimeDays365()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.LeadTimeDays = 365;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).LeadTimeDays, 365);
        }

        [TestMethod]
        public void TestUpdateItemSupplierValidUnitPriceOneCent()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.UnitPrice = 0.01M;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).UnitPrice, 0.01M);
        }

        [TestMethod]

        public void TestUpdateItemSupplierValidUnit9999Dot99()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.UnitPrice = 9999.99M;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).UnitPrice, 9999.99M);
        }

        [TestMethod]
        public void TestUpdateItemSupplierValidUnit5000()
        {
            //Arrange
            ItemSupplier newItemSupplier = new ItemSupplier();
            setItemSupplier(newItemSupplier, _itemSuppliers[0]);
            newItemSupplier.UnitPrice = 5000.00M;
            //Act
            _itemSupplierManager.UpdateItemSupplier(newItemSupplier, _itemSuppliers[0]);
            //Assert
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(_itemSuppliers[0].ItemID, _itemSuppliers[0].SupplierID).UnitPrice, 5000.00M);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteItemSupplier()
        {
            //Arrange This is a known item supplier 
            int itemID = 100002;
            int supplierID = 100005;
            ItemSupplier itemSupplier = new ItemSupplier();
            //Act
            _itemSupplierManager.DeleteItemSupplier(itemID, supplierID);
            //Assert should throw exception 
            itemSupplier = _itemSupplierManager.RetrieveItemSupplier(itemID, supplierID);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateItemSupplier()
        {
            //Arrange This is a known item supplier 
            int itemID = 100002;
            int supplierID = 100005;
            ItemSupplier itemSupplier = new ItemSupplier();
            //Act
            _itemSupplierManager.DeactivateItemSupplier(itemID, supplierID);
            //Assert should throw exception 
            itemSupplier = _itemSupplierManager.RetrieveItemSupplier(itemID, supplierID);
            Assert.AreEqual(_itemSupplierManager.RetrieveItemSupplier(itemID, supplierID).ItemSupplierActive, false);

        }

    }

}

