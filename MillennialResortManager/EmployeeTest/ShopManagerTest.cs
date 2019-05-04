using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayer.Tests
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

            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true, BuildingID = "B1", RoomNumber = 12 });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100001, RoomID = 245255, Name = "Club Fun", Description = "Party Supplies", Active = true, BuildingID = "B2", RoomNumber = 12 });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100002, RoomID = 245620, Name = "Groceries R Us", Description = "Self Explanatory", Active = true, BuildingID = "B1", RoomNumber = 13 });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100003, RoomID = 205313, Name = "Peppers", Description = "Hot Sauce Shop in Key West", Active = true, BuildingID = "B3", RoomNumber = 10 });
            vmBrowseShops.Add(new VMBrowseShop() { ShopID = 100004, RoomID = 252113, Name = "Jitters", Description = "Coffee served by Tweek", Active = true, BuildingID = "B5", RoomNumber = 10 });

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


        /// <summary>
        /// James Heim
        /// Created 2019/03/08
        /// 
        /// Test that the deactivate method
        /// successfully deactivates a shop.
        /// </summary>
        [TestMethod]
        public void TestDeactivateShop()
        {
            // Arrange.
            int shopID = 100100;
            Shop shop = new Shop()
            {
                ShopID = shopID,
                RoomID = 100300,
                Name = "Test Shop",
                Description = "TESTING",
                Active = true
            };

            _shopMock.CreateShop(shop);

            // Act.
            _shopManager.DeactivateShop(shop);

            // Assert.
            Assert.IsFalse(_shopManager.RetrieveShopByID(shopID).Active);

        }


        /// <summary>
        /// James Heim
        /// Created 2019/03/08
        /// 
        /// Test that the activate shop method
        /// successfully activates a shop.
        /// </summary>
        [TestMethod]
        public void TestActivateShop()
        {
            // Arrange.
            int shopID = 100100;
            Shop shop = new Shop()
            {
                ShopID = shopID,
                RoomID = 100300,
                Name = "Test Shop",
                Description = "TESTING",
                Active = true
            };

            _shopMock.CreateShop(shop);
            _shopMock.DeactivateShop(shop);

            // Act.
            _shopManager.ActivateShop(shop);

            // Assert.
            Assert.IsTrue(_shopManager.RetrieveShopByID(shopID).Active);

        }


        /// <summary>
        /// James Heim
        /// Created 2019/03/08
        /// 
        /// Test that the delete method
        /// successfully deletes a disabled shop.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestDeleteShop()
        {
            // Arrange.
            int shopID = 100100;
            Shop shop = new Shop()
            {
                ShopID = shopID,
                RoomID = 100300,
                Name = "Test Shop",
                Description = "TESTING",
                Active = true
            };

            _shopMock.CreateShop(shop);
            _shopManager.DeactivateShop(shop);

            // Act.
            _shopManager.DeleteShop(shop);

            // Assert.
            _shopManager.RetrieveShopByID(shopID);

        }


        /// <summary>
        /// James Heim
        /// Created 2019/03/08
        /// 
        /// Test that the delete method
        /// throws NullReferenceException on
        /// non-existent shops.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestDeleteShopFailNull()
        {
            // Arrange.
            int shopID = 100100;
            Shop shop = new Shop()
            {
                ShopID = shopID,
                RoomID = 100300,
                Name = "Test Shop",
                Description = "TESTING",
                Active = true
            };



            // Act.
            _shopManager.DeleteShop(shop);

            // Assert.
            _shopManager.RetrieveShopByID(shopID);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/03/08
        /// 
        /// Test that the delete method
        /// does not delete active records.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDeleteShopFailActiveShop()
        {
            // Arrange.
            int shopID = 100100;
            Shop shop = new Shop()
            {
                ShopID = shopID,
                RoomID = 100300,
                Name = "Test Shop",
                Description = "TESTING",
                Active = true
            };

            _shopManager.InsertShop(shop);

            // Act.
            _shopManager.DeleteShop(shop);
        }

        /// <summary>
        /// James Heim
        /// Created 2019/03/08
        /// 
        /// Test that the activate method
        /// does not activate active records.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestActivateFailActiveAlready()
        {
            // Arrange.
            int shopID = 100100;
            Shop shop = new Shop()
            {
                ShopID = shopID,
                RoomID = 100300,
                Name = "Test Shop",
                Description = "TESTING",
                Active = true
            };

            _shopManager.InsertShop(shop);

            // Act.
            _shopManager.ActivateShop(shop);
        }


        /// <summary>
        /// James Heim
        /// Created 2019/03/08
        /// 
        /// Test that the deactivate method
        /// does not deactivate inactive records.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestDeactivateFailInactiveAlready()
        {
            // Arrange.
            int shopID = 100100;
            Shop shop = new Shop()
            {
                ShopID = shopID,
                RoomID = 100300,
                Name = "Test Shop",
                Description = "TESTING",
                Active = true
            };

            _shopManager.InsertShop(shop);
            _shopManager.DeactivateShop(shop);

            // Act.
            _shopManager.DeactivateShop(shop);
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 3/4/2019
        /// Here starts the Update Unit Tests
        /// </summary>
        [TestMethod]
        public void TestUpdateShopValidInput()
        {
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });

            bool addWorked = false;
            //Arrange
            Shop oldShop = shops[0];
            Shop newShop = new Shop() { ShopID = 100000, RoomID = 245213, Name = "Jose's Taco Shop", Description = "For the best taco see Luis!" };
            //Act
            addWorked = _shopManager.UpdateShop(newShop, oldShop);
            //Assert
            Assert.AreEqual(oldShop.ShopID, newShop.ShopID);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShopInvalidNameNull()
        {
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });

            bool addWorked = false;
            //Arrange
            Shop oldShop = shops[0];
            Shop newShop = new Shop() { ShopID = 100000, RoomID = 245213, Name = null, Description = "For the best taco see Luis!" };
            //Act
            addWorked = _shopManager.UpdateShop(newShop, oldShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShopInvalidNameEmptyString()
        {
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });

            bool addWorked = false;
            //Arrange
            Shop oldShop = shops[0];
            Shop newShop = new Shop() { ShopID = 100000, RoomID = 245213, Name = "", Description = "For the best taco see Luis!" };
            //Act
            addWorked = _shopManager.UpdateShop(newShop, oldShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShopInvalidNameTooLong()
        {
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });

            bool addWorked = false;
            //Arrange
            Shop oldShop = shops[0];
            Shop newShop = new Shop() { ShopID = 100000, RoomID = 245213, Name = createString(51), Description = "For the best taco see Luis!" };
            //Act
            addWorked = _shopManager.UpdateShop(newShop, oldShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShopInvalidDescriptionNull()
        {
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });

            bool addWorked = false;
            //Arrange
            Shop oldShop = shops[0];
            Shop newShop = new Shop() { ShopID = 100000, RoomID = 245213, Name = "Jose's Taco Shop", Description = null };
            //Act
            addWorked = _shopManager.UpdateShop(newShop, oldShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShopInvalidDescriptionEmptyString()
        {
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });

            bool addWorked = false;
            //Arrange
            Shop oldShop = shops[0];
            Shop newShop = new Shop() { ShopID = 100000, RoomID = 245213, Name = "Jose's Taco Shop", Description = "" };
            //Act
            addWorked = _shopManager.UpdateShop(newShop, oldShop);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateShopInvalidDescriptionTooLong()
        {
            List<Shop> shops = new List<Shop>();

            shops.Add(new Shop() { ShopID = 100000, RoomID = 245213, Name = "Awesome Pawesome", Description = "Pet Store", Active = true });

            bool addWorked = false;
            //Arrange
            Shop oldShop = shops[0];
            Shop newShop = new Shop() { ShopID = 100000, RoomID = 245213, Name = "Jose's Taco Shop", Description = createString(1001) };
            //Act
            addWorked = _shopManager.UpdateShop(newShop, oldShop);
        }

    }
}
