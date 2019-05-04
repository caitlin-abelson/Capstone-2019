using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayer.Tests
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created : 02/15/2019
    /// 
    /// Here are the Test Methods for ItemManagerTest
    /// 
    /// Updated By: Jared Greenfield
    /// Updated : 02/19/2019
    /// Added more tests and corrected errors in old tests.
    /// </summary>
    [TestClass]
    public class ItemManagerTest
    {
        private IItemManager _itemManager;
        private List<Item> _items;
        private ItemAccessorMock _mock;

        [TestInitialize]
        public void TestSetup()
        {
            _mock = new ItemAccessorMock();
            _itemManager = new ItemManager(_mock);
            _items = new List<Item>();
            _items = _itemManager.RetrieveAllItems();

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

        private void setItem(Item old, Item newItem)
        {
            newItem.Name = old.Name;
            newItem.ItemType = old.ItemType;
            newItem.ItemID = old.ItemID;
            newItem.Description = old.Description;
            newItem.CustomerPurchasable = old.CustomerPurchasable;
            newItem.DateActive = old.DateActive;
            newItem.Active = old.Active;
            newItem.OnHandQty = old.OnHandQty;
            newItem.RecipeID = old.RecipeID;
            newItem.ReorderQty = old.ReorderQty;
        }

        [TestMethod]
        public void TestRetrieveAllItems()
        {
            //Arrange
            List<Item> items = null;
            //Act
            items = _itemManager.RetrieveAllItems();
            //Assert
            CollectionAssert.Equals(_items, items);
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 2/11/2019
        /// Here starts the RetrieveProduct Unit Tests
        /// </summary>

        [TestMethod]
        public void TestRetrieveItemValidInput()
        {
            //Arrange
            Item newItem = new Item();
            //Act
            newItem = _itemManager.RetrieveItem(_items[1].ItemID);
            //Assert
            Assert.AreEqual(newItem.ItemID, _items[1].ItemID);
            Assert.AreEqual(newItem.ItemType, _items[1].ItemType);
            Assert.AreEqual(newItem.RecipeID, _items[1].RecipeID);
            Assert.AreEqual(newItem.CustomerPurchasable, _items[1].CustomerPurchasable);
            Assert.AreEqual(newItem.Description, _items[1].Description);
            Assert.AreEqual(newItem.OnHandQty, _items[1].OnHandQty);
            Assert.AreEqual(newItem.Name, _items[1].Name);
            Assert.AreEqual(newItem.ReorderQty, _items[1].ReorderQty);
            Assert.AreEqual(newItem.DateActive, _items[1].DateActive);
            Assert.AreEqual(newItem.Active, _items[1].Active);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRetrieveProductInvalidInput()
        {
            //Arrange
            Item newItem = new Item();
            int invalidItemID = -1;
            //Act
            newItem = _itemManager.RetrieveItem(invalidItemID);
        }

        [TestMethod]
        public void TestRetrieveItemByRecipeIDValidID()
        {
            //Arrange
            Item item = null;
            //Act
            item = _itemManager.RetrieveItemByRecipeID((int)_items[1].RecipeID);
            //Assert
            Assert.AreEqual(_items[1], item);
        }

        [TestMethod]
        public void TestRetrieveItemByRecipeIDInvalidID()
        {
            //Arrange
            Item item = null;
            //Act
            item = _itemManager.RetrieveItemByRecipeID(-1);
            //Assert
            //There is no Item with this recipeID, so it returns null;
            Assert.IsNull(item);
        }

        [TestMethod]
        public void TestRetrieveLineItemsByRecipeIDValidID()
        {
            //Arrange
            List<Item> testItems = null;
            //Act 
            testItems = _itemManager.RetrieveLineItemsByRecipeID(100000);

            //Assert
            Assert.IsNotNull(testItems);
        }

        [TestMethod]
        public void TestRetrieveLineItemsByRecipeIDInValidID()
        {
            //Arrange
            List<Item> testItems = null;
            //Act 
            testItems = _itemManager.RetrieveLineItemsByRecipeID(-1);
            //Assert
            Assert.IsNull(testItems);
        }

        [TestMethod]
        public void TestCreateItemValidInput()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, "Food", "It's a calzone!", 0, "Calzone-Supremo", 0, DateTime.Now, true);
            //Act 
            _itemManager.CreateItem(newItem);
            //Assert
            // Update list of items
            _items = _itemManager.RetrieveAllItems();
            Assert.IsNotNull(_items.Find(x => x.ItemID == newItem.ItemID && x.OfferingID == newItem.OfferingID &&
                x.CustomerPurchasable == newItem.CustomerPurchasable && x.RecipeID == newItem.RecipeID && x.ItemType == newItem.ItemType &&
                x.Description == newItem.Description && x.OnHandQty == newItem.OnHandQty && x.Name == newItem.Name &&
                x.ReorderQty == newItem.ReorderQty && x.DateActive == newItem.DateActive && x.Active == newItem.Active));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputItemTypeNull()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, null, "It's a calzone!", 0, "Calzone-Supremo", 0, DateTime.Now, true);
            //Act 
            //Since the ItemType is null, this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputItemTypeTooLong()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, createLongString(16), "It's a calzone!", 0, "Calzone-Supremo", 0, DateTime.Now, true);
            //Act 
            //Since the ItemType is too long (greater than 15 characters), this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputItemTypeTooShort()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, "", "It's a calzone!", 0, "Calzone-Supremo", 0, DateTime.Now, true);
            //Act 
            //Since the ItemType is too short (0 characters), this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputDescriptionTooLong()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, "Food", "It's a calzone!" + createLongString(1000), 0, "Calzone-Supremo", 0, DateTime.Now, true);
            //Act 
            //Since the Description is too long (greater than 1000 characters), this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputNameTooLong()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, "Food", "It's a calzone!", 0, "Calzone-Supremo" + createLongString(1000), 0, DateTime.Now, true);
            //Act 
            //Since the Name is too long (greater than 1000 characters), this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputNameTooShort()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, "Food", "It's a calzone!", 0, "", 0, DateTime.Now, true);
            //Act 
            //Since the Name is too short (0 characters), this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputNameNull()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, "Food", "It's a calzone!", 0, null, 0, DateTime.Now, true);
            //Act 
            //Since the Name is too short (0 characters), this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateItemInvalidInputDateTooEarly()
        {
            //Arrange
            Item newItem = new Item(100050, 1000050, false, 100050, "Food", "It's a calzone!", 0, "Calzone-Supremo", 0, new DateTime(1899, 12, 2), true);
            //Act 
            //Since the Date is too early (Year < 1900), this should throw an exception.
            _itemManager.CreateItem(newItem);
        }

        [TestMethod]
        public void TestUpdateItemValidInput()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.Name = "Big Test Onion";

            //Act
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
            //Arrange
            _items = _itemManager.RetrieveAllItems();
            Assert.AreEqual(true, isUpdated);
            Assert.AreEqual(_items[0], newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidInputItemTypeNull()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.ItemType = null;

            //Act
            //Since the ItemType is null, this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidInputItemTypeTooLong()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.ItemType = createLongString(1005);

            //Act
            //Since the ItemType is too long (greater than 1000 characters), this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidInputItemTypeTooShort()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.ItemType = "";

            //Act
            //Since the ItemType is too short (0 characters ), this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidDescriptionTooLong()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.Description = createLongString(1006);

            //Act
            //Since the Description is too long (greater than 1000 characters ), this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidInputNameNull()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.Name = null;

            //Act
            //Since the Name is null, this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidInputNameTooShort()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.Name = "";

            //Act
            //Since the Name is too short (0 characters), this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidInputNameTooLong()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.Name = createLongString(51);

            //Act
            //Since the Name is too long (greater than 50 characters ), this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestUpdateItemInvalidInputDateActiveTooEarly()
        {
            //Arrange
            Item oldItem = _items[0];
            Item newItem = _items[0];
            newItem.DateActive = new DateTime(1899, 11, 3);

            //Act
            //Since the DateActive is too early (Before 1900), this should throw an exception.
            bool isUpdated = _itemManager.UpdateItem(oldItem, newItem);
        }

        /// <summary>
        /// Author: Kevin Broskow
        /// Created : 2/11/2019
        /// Here starts the DeactivateItem Unit Tests
        /// </summary>
        [TestMethod]
        public void TestDeactivateItemValid()
        {
            //Arrange
            Item validItem = _items[0];
            //Act
            _itemManager.DeactivateItem(validItem);
            //Assert
            Assert.IsFalse(_itemManager.RetrieveItem(validItem.ItemID).Active);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeactivateItemInvalid()
        {
            //Arrange
            Item invalidItem = _items[0];
            invalidItem.ItemID = -1;
            //Act
            _itemManager.DeactivateItem(invalidItem);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDeleteItemValid()
        {
            //Arrange
            Item invalidItem = _items[0];
            //Act
            _itemManager.DeleteItem(invalidItem);
            //Assert

        }
    }
}
