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
    }
}
