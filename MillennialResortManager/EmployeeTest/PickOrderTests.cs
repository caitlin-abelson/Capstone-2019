using System;
using System.Text;
using System.Collections.Generic;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayer.Tests
{
    /// <summary>
    /// Eric Bostwick
    /// 4/9/2019
    ///Tests the PickSheet and PickOrder Functions
    /// </summary>
    [TestClass]
    public class PickOrderTests
    {
        /// <summary>
        /// Eric Bostwick
        /// 4/10/2019
        /// Tests for the PickManager
        ///</summary>
        /// 

        private List<PickOrder> _pickOrders;
        private List<PickSheet> _pickSheets;
        private List<PickOrder> _tmpPickOrders;
        private PickSheet _tmpPickSheet;
        private IPickManager _pickManager;
        private PickAccessorMock _pickAccessorMock;
        private PickOrder _tmpPickOrder;
        private DateTime _dt;
        
        [TestInitialize]

        public void TestSetup()
        {
            _pickAccessorMock = new PickAccessorMock();
            _pickManager = new PickManager(_pickAccessorMock);

            _pickOrders = new List<PickOrder>();
            _pickSheets = new List<PickSheet>();
            _tmpPickOrders = new List<PickOrder>();
            _tmpPickSheet = new PickSheet();
            _tmpPickOrder = new PickOrder();
            _dt = new DateTime();
            _dt = DateTime.Now.AddDays(-60);

            //there two items that will be returned here for later testing
            _pickSheets = _pickAccessorMock.Select_All_PickSheets();
            _pickOrders = _pickAccessorMock.Select_Orders_For_Acknowledgement(_dt, false);
            _tmpPickOrders = _pickAccessorMock.Select_All_Temp_PickOrders();
            //_pickOrders = _pickAccessorMock.
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


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void TestInsertRecordToTempPickSheet()
        {
            //arrange
            PickOrder tmpPickOrder = new PickOrder()
            {
                PickSheetID = "10000012345",                
                ItemID = 100000,
                InternalOrderID = 100000,
                OrderReceivedDate = DateTime.Now              
            };

            //act
            _pickManager.Insert_Record_To_TmpPicksheet(tmpPickOrder);
            _tmpPickOrders = _pickManager.Select_All_Tmp_PickOrders();            

            //assert            
            Assert.IsNotNull(_tmpPickOrders.Find(x => x.PickSheetID == "10000012345"));       
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void TestDeleteTmpPickSheetItem()
        {
            //arrange
            PickOrder tmpPickOrder = new PickOrder()
            {
                PickSheetID = "10000099999",
                ItemID = 100000,
                InternalOrderID = 100000,
                OrderReceivedDate = DateTime.Now
            };
            //act
            _pickManager.Insert_Record_To_TmpPicksheet(tmpPickOrder);
            _pickManager.Delete_TmpPickSheet_Item(tmpPickOrder);
            _tmpPickOrders = _pickManager.Select_All_Tmp_PickOrders();

            //assert
            Assert.IsNull(_tmpPickOrders.Find(x => x.PickSheetID == "10000099999"));
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void DeleteTmpPickSheet()
        {
            //arrange
            string pickSheetID = "10000012345";
            PickSheet pickSheet;          

            //act            
            _pickManager.Delete_TmpPickSheet(pickSheetID);
            pickSheet = _pickManager.Select_TmpPickSheet(pickSheetID);

            //arrange
            Assert.AreEqual(pickSheet, null);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void SelectAllClosedPickSheets()
        {
            //Arrange
            List<PickSheet> pickSheets = null;
            //Act
            pickSheets = _pickManager.Select_All_Closed_PickSheets_By_Date(_dt);
            //Assert
            CollectionAssert.Equals(_pickSheets, pickSheets);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void SelectAllPickSheets()
        {
            //Arrange
            List<PickSheet> pickSheets = null;
            //Act
            pickSheets = _pickManager.Select_All_PickSheets();
            //Assert
            CollectionAssert.Equals(_pickSheets, pickSheets);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void SelectAllPickSheetsByDateTime()
        {
            //Arrange
            List<PickSheet> pickSheets = null;
            //Act
            pickSheets = _pickManager.Select_All_PickSheets_By_Date(_dt);
            //Assert
            CollectionAssert.Equals(_pickSheets, pickSheets);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void SelectAllTmpPickOrders()
        {
            //Arrange
            List<PickOrder> pickOrders = null;
            //Act
            pickOrders = _pickManager.Select_All_Tmp_PickOrders();
            //Assert
            CollectionAssert.Equals(_tmpPickOrders, pickOrders);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void SelectAllOrdersForAcknowledgment()
        {
            //Arrange
            List<PickOrder> pickOrders = null;
            //Act
            pickOrders = _pickManager.Select_Orders_For_Acknowledgement(_dt, false);
            //Assert
            CollectionAssert.Equals(_pickOrders, pickOrders);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void SelectPickSheetByPickSheetID()
        {
            //Arrange
            string pickSheetID = "10000011111";
            List<PickOrder> pickOrders = null;
            //Act
            pickOrders = _pickManager.Select_PickSheet_By_PickSheetID(pickSheetID);
            //Assert
            Assert.IsTrue(pickOrders.FindAll(x => x.PickSheetID == pickSheetID).Count > 0);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void SelectPickSheetNumber()
        {
            //Arrange
            string pickSheetID = "";
            string newPickSheetID = "";
           
            //Act
            newPickSheetID = _pickManager.Select_Pick_Sheet_Number();
            //Assert
            Assert.AreNotEqual(pickSheetID, newPickSheetID);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void UpdatePickSheet()
        {
            PickSheet pickSheet;
            pickSheet = _pickManager.Select_All_PickSheets().Find(p => p.PickSheetID == "10000022222");
            PickSheet newPickSheet = new PickSheet();
            newPickSheet = CopyPickSheet(pickSheet);
            newPickSheet.NumberOfOrders = 500;
            int result;
            //Act
            result = _pickManager.UpdatePickSheet(newPickSheet, pickSheet);
            //Assert
            Assert.AreNotEqual(pickSheet, newPickSheet);
        }

        [TestMethod]
        /// Eric Bostwick
        /// 4/10/2019
        public void UpdatePickOrder()
        {
            PickOrder pickOrder;

            
            pickOrder = _pickOrders.Find(o => (o.PickSheetID == "1000011111") && (o.ItemOrderID == 100000) & (o.ItemID == 100000));

            PickOrder newPickOrder = new PickOrder();
            newPickOrder = CopyOrder(pickOrder);
            newPickOrder.EmployeeID = 999999;
            int result;
            //Act
            result = _pickManager.Update_PickOrder(newPickOrder, pickOrder);
            //Assert
            Assert.AreNotEqual(pickOrder, newPickOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]       
        /// Eric Bostwick
        /// 4/10/2019
        public void UpdateQtyReceivedLessThanZero()
        {
            PickOrder pickOrder;


            pickOrder = _pickOrders.Find(o => (o.PickSheetID == "1000011111") && (o.ItemOrderID == 100000) & (o.ItemID == 100000));

            PickOrder newPickOrder = new PickOrder();
            newPickOrder = CopyOrder(pickOrder);
            newPickOrder.QtyReceived = -1;
            int result;
            //Act
            result = _pickManager.Update_PickOrder(newPickOrder, pickOrder);
            //Assert
            //Assert.AreNotEqual(pickOrder, newPickOrder);
        }



        private static PickOrder CopyOrder(PickOrder order)
        {
            PickOrder _newOrder = new PickOrder()
            {
                EmployeeID = order.EmployeeID,
                DeptID = order.DeptID,
                DeptDescription = order.DeptDescription,
                DeliveryDate = order.DeliveryDate,
                DeliveryDateView = order.DeliveryDateView,
                InternalOrderID = order.InternalOrderID,
                ItemDescription = order.ItemDescription,
                ItemID = order.ItemID,
                ItemOrderID = order.ItemOrderID,
                OrderDateView = order.OrderDateView,
                OrderDate = order.OrderDate,
                Orderer = order.Orderer,
                OrderQty = order.OrderQty,
                OrderReceivedDate = order.OrderReceivedDate,
                OrderReceivedDateView = order.OrderReceivedDateView,
                OrderStatus = order.OrderStatus,
                OrderStatusView = order.OrderStatusView,
                PickCompleteDateView = order.PickCompleteDateView,
                PickCompleteDate = order.PickCompleteDate,
                PickSheetID = order.PickSheetID,
                PickSheetIDView = order.PickSheetIDView,
                QtyReceived = order.QtyReceived,
                UnitPrice = order.UnitPrice
            };
            return _newOrder;
        }
        private static PickSheet CopyPickSheet(PickSheet pickSheet)
        {
            PickSheet _newPickSheet = new PickSheet()
            {
                PickSheetID = pickSheet.PickSheetID,
                CreateDate = pickSheet.CreateDate,
                NumberOfOrders = pickSheet.NumberOfOrders,
                PickCompletedBy = pickSheet.PickCompletedBy,
                PickCompletedByName = pickSheet.PickCompletedByName,
                PickCompletedDate = pickSheet.PickCompletedDate,
                PickCompletedDateView = pickSheet.PickCompletedDateView,
                PickDeliveredBy = pickSheet.PickDeliveredBy,
                PickDeliveredByName = pickSheet.PickDeliveredByName,
                PickDeliveryDate = pickSheet.PickDeliveryDate,
                PickDeliveryDateView = pickSheet.PickDeliveryDateView,
                PickSheetCreatedBy = pickSheet.PickSheetCreatedBy,
                PickSheetCreatedByName = pickSheet.PickSheetCreatedByName,
                PickSheetIDView = pickSheet.PickSheetIDView,
                PickSheetInternalOrderID = pickSheet.PickSheetInternalOrderID,
                PickSheetStatus = pickSheet.PickSheetStatus,
                PickSheetStatusView = pickSheet.PickSheetStatusView,
                TempPickSheetID = pickSheet.TempPickSheetID                
            };
            return _newPickSheet;
        }

    }
}
