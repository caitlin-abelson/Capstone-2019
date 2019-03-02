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
    [TestClass]
    public class ShopManagerTest
    {

        private IShopManager _shopManager;
        private ShopAccessorMock _shopMock;

        [TestInitialize]
        public void testSetupMSSQL()
        {
            _shopMock = new ShopAccessorMock();
            _shopManager = new ShopManagerMSSQL(_shopMock);
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
        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 2/27/2019
        /// Here starts the CreateShop Unit Tests
        /// </summary>
        [TestMethod]
        public void TestCreateShopValidInput()
        {
            int addWorked = 0;
            //Arrange
            Shop newShop = new Shop() { ShopID = 14441, RoomID = 15, Name="Jose's Taco Shop", Description="For the best taco see Luis!"};
            //Act
            addWorked = _shopManager.InsertShop(newShop);
            //Assert
            Assert.IsNotNull(addWorked == newShop.ShopID);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShopInvalidNameNull()
        {
            //Arrange
            Shop newShop = new Shop() { ShopID = 14441, RoomID = 15, Name = null, Description = "For the best taco see Luis!" };
            //Act
            _shopManager.InsertShop(newShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShopInvalidNameEmptyString()
        {
            //Arrange
            Shop newShop = new Shop() { ShopID = 14441, RoomID = 15, Name = "", Description = "For the best taco see Luis!" };
            //Act
            _shopManager.InsertShop(newShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShopInvalidNameTooLong()
        {
            //Arrange
            Shop newShop = new Shop() { ShopID = 14441, RoomID = 15, Name = createString(51), Description = "For the best taco see Luis!" };
            //Act
            _shopManager.InsertShop(newShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShopInvalidDescriptionNull()
        {
            //Arrange
            Shop newShop = new Shop() { ShopID = 14441, RoomID = 15, Name = "Jose's Taco Shop", Description = null };
            //Act
            _shopManager.InsertShop(newShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShopInvalidDescriptionEmptyString()
        {
            //Arrange
            Shop newShop = new Shop() { ShopID = 14441, RoomID = 15, Name = "Jose's Taco Shop", Description = "" };
            //Act
            _shopManager.InsertShop(newShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateShopInvalidDescriptionTooLong()
        {
            //Arrange
            Shop newShop = new Shop() { ShopID = 14441, RoomID = 15, Name = "Jose's Taco Shop", Description = createString(1001) };
            //Act
            _shopManager.InsertShop(newShop);
        }


        /// <summary>
        /// James Heim
        /// Created 2019-02-28
        /// 
        /// Test RetrieveShops(). Assumes CreateShop tests have all passed.
        /// </summary>
        [TestMethod]
        public void RetrieveAllShopsTest()
        {
            // Arrange.

            // Create the list of shop test data.
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });
            shops.Add(new Shop() { ShopID = 100001, RoomID = 245255, Name = "Club Fun", Description = "Party Supplies", Active = true });
            shops.Add(new Shop() { ShopID = 100002, RoomID = 245620, Name = "Groceries R Us", Description = "Self Explanatory", Active = true });
            shops.Add(new Shop() { ShopID = 100003, RoomID = 205313, Name = "Peppers", Description = "Hot Sauce Shop in Key West", Active = true });
            shops.Add(new Shop() { ShopID = 100004, RoomID = 252113, Name = "Jitters", Description = "Coffee served by Tweek", Active = true });

            // Add those shops into the database.
            foreach (var shop in shops)
            {
                _shopManager.InsertShop(shop);
            }

            // Act.

            // Retrieve the shops from the database.
            List<Shop> retrievedShops = (List<Shop>)_shopManager.RetrieveAllShops();

            // Assert.

            // Make sure the shops we created and added to the database were retrieved properly.
            CollectionAssert.AreEqual(shops, retrievedShops);
        }


        /// <summary>
        /// James Heim
        /// Created 2019-02-28
        /// 
        /// Test RetrieveVMShops(). Assumes CreateShop tests have all passed.
        /// </summary>
        [TestMethod]
        public void RetrieveAllVMShopsTest()
        {
            // Arrange.

            // Create the list of shop test data.
            List<VMBrowseShop> vmBrowseShops = new List<VMBrowseShop>();

            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true, BuildingID = "B1", RoomNumber = "12" });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100001, RoomID = 245255, Name = "Club Fun", Description = "Party Supplies", Active = true, BuildingID = "B2", RoomNumber = "12" });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100002, RoomID = 245620, Name = "Groceries R Us", Description = "Self Explanatory", Active = true, BuildingID = "B1", RoomNumber = "13" });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100003, RoomID = 205313, Name = "Peppers", Description = "Hot Sauce Shop in Key West", Active = true, BuildingID = "B3", RoomNumber = "10" });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100004, RoomID = 252113, Name = "Jitters", Description = "Coffee served by Tweek", Active = true, BuildingID = "B5", RoomNumber = "10" });

            // Add those shops into the database.
            foreach (var shop in vmBrowseShops)
            {
                _shopManager.InsertShop(new Shop
                {
                    ShopID = shop.ShopID,
                    RoomID = shop.RoomID,
                    Name = shop.Name,
                    Description = shop.Description,
                    Active = shop.Active
                });
            }

            // Act.

            // Retrieve the shops from the database.
            List<Shop> retrievedShops = (List<Shop>)_shopManager.RetrieveAllShops();

            // Assert.

            foreach (var shop in retrievedShops)
            {
                Assert.IsNotNull(vmBrowseShops.Find(x =>
                    x.ShopID == shop.ShopID &&
                    x.RoomID == shop.RoomID &&
                    x.Name == shop.Name &&
                    x.Description == shop.Description &&
                    x.Active == shop.Active
                ));
            }

        }
    }
}
