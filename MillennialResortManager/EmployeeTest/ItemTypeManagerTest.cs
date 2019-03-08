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
    public class ItemTypeManagerTest
    {
        private IItemTypeManager _itemTypeManager;
        private ItemTypeAccessorMock _mock;
        [TestInitialize]
        public void testSetup()
        {
            _mock = new ItemTypeAccessorMock();
            _itemTypeManager = new ItemTypeManagerMSSQL(_mock);
        }

        [TestMethod]
        public void TestRetrieveAllItemTypes()
        {
            //Arrange
            List<ItemType> itemTypes = null;

            //Act
            itemTypes = _itemTypeManager.RetrieveAllItemTypes();
            //Assert
            Assert.IsNotNull(itemTypes);
        }
    }
}
