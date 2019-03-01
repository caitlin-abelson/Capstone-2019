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
    public class SupplierOrderTests
    {

        private List<VMItemSupplierItem> _itemSuppliers;
        private ISupplierOrderManager _supplierOrderManager;
        private SupplierOrderAccessorMock _supplierOrderAccessorMock;
        private List<SupplierOrder> _supplierOrders;
        private List<SupplierOrderLine> _supplierOrderLines;

        [TestInitialize]

        public void testSetup()
        {
            _supplierOrderAccessorMock = new SupplierOrderAccessorMock();
            _supplierOrderManager = new SupplierOrderManager(_supplierOrderAccessorMock);
            _itemSuppliers = new List<VMItemSupplierItem>();
            _supplierOrders = new List<SupplierOrder>();
            _supplierOrderLines = new List<SupplierOrderLine>();
            //there two items that will be returned here for later testing
            _itemSuppliers = _supplierOrderAccessorMock.SelectItemSuppliersBySupplierID(100000);
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

        [TestMethod]
        public void TestCreateSupplierOrderValidInput()
        {
            //arrange
            SupplierOrder newSupplierOrder = new SupplierOrder()
            {
                SupplierID = 100005,
                SupplierOrderID = 100010,
                DateOrdered = DateTime.Today,
                Description = "test order",
                EmployeeID = 100000,
                OrderComplete = false
            };

            SupplierOrderLine supplierOrderLine1 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 100,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            SupplierOrderLine supplierOrderLine2 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 100,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            List<SupplierOrderLine> supplierOrderLines = new List<SupplierOrderLine>();

            supplierOrderLines.Add(supplierOrderLine1);
            supplierOrderLines.Add(supplierOrderLine2);

            //Act
            _supplierOrderManager.CreateSupplierOrder(newSupplierOrder, supplierOrderLines);

            //Assert
            _supplierOrders = _supplierOrderManager.RetrieveAllSupplierOrders();

            Assert.IsNotNull(_supplierOrders.Find(x => x.SupplierOrderID == newSupplierOrder.SupplierOrderID && x.DateOrdered == newSupplierOrder.DateOrdered &&
                x.Description == newSupplierOrder.Description && x.EmployeeID == newSupplierOrder.EmployeeID && x.OrderComplete == newSupplierOrder.OrderComplete)
            );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSupplierOrderInValidInputOrderQtyNegative()
        {
            //arrange
            SupplierOrder newSupplierOrder = new SupplierOrder()
            {
                SupplierID = 100005,
                SupplierOrderID = 100010,
                DateOrdered = DateTime.Today,
                Description = "test order",
                EmployeeID = 100000,
                OrderComplete = false
            };

            SupplierOrderLine supplierOrderLine1 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 100,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            SupplierOrderLine supplierOrderLine2 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = -5,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            List<SupplierOrderLine> supplierOrderLines = new List<SupplierOrderLine>();

            supplierOrderLines.Add(supplierOrderLine1);
            supplierOrderLines.Add(supplierOrderLine2);
            //Act
            _supplierOrderManager.CreateSupplierOrder(newSupplierOrder, supplierOrderLines);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSupplierOrderInValidInputOrderQtyHigh()
        {
            //arrange
            SupplierOrder newSupplierOrder = new SupplierOrder()
            {
                SupplierID = 100005,
                SupplierOrderID = 100010,
                DateOrdered = DateTime.Today,
                Description = "test order",
                EmployeeID = 100000,
                OrderComplete = false
            };

            SupplierOrderLine supplierOrderLine1 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 100,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            SupplierOrderLine supplierOrderLine2 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 100000,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            List<SupplierOrderLine> supplierOrderLines = new List<SupplierOrderLine>();

            supplierOrderLines.Add(supplierOrderLine1);
            supplierOrderLines.Add(supplierOrderLine2);
            //Act
            _supplierOrderManager.CreateSupplierOrder(newSupplierOrder, supplierOrderLines);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSupplierOrderInValidDescriptionTooLong()
        {
            string testString = createString(1001);
            //arrange
            SupplierOrder newSupplierOrder = new SupplierOrder()
            {
                SupplierID = 100005,
                SupplierOrderID = 100010,
                DateOrdered = DateTime.Today,
                Description = testString,
                EmployeeID = 100000,
                OrderComplete = false
            };

            SupplierOrderLine supplierOrderLine1 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 100,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            SupplierOrderLine supplierOrderLine2 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 500,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            List<SupplierOrderLine> supplierOrderLines = new List<SupplierOrderLine>();

            supplierOrderLines.Add(supplierOrderLine1);
            supplierOrderLines.Add(supplierOrderLine2);
            //Act
            _supplierOrderManager.CreateSupplierOrder(newSupplierOrder, supplierOrderLines);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSupplierOrderInValidLineDescriptionTooLong()
        {
            string testString = createString(1001);
            //arrange
            SupplierOrder newSupplierOrder = new SupplierOrder()
            {
                SupplierID = 100005,
                SupplierOrderID = 100010,
                DateOrdered = DateTime.Today,
                Description = "test",
                EmployeeID = 100000,
                OrderComplete = false
            };

            SupplierOrderLine supplierOrderLine1 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = "test item 1",
                ItemID = 100015,
                OrderQty = 100,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            SupplierOrderLine supplierOrderLine2 = new SupplierOrderLine()
            {
                SupplierOrderID = 100010,
                Description = testString,
                ItemID = 100015,
                OrderQty = 500,
                QtyReceived = 0,
                UnitPrice = 1.00M
            };

            List<SupplierOrderLine> supplierOrderLines = new List<SupplierOrderLine>();

            supplierOrderLines.Add(supplierOrderLine1);
            supplierOrderLines.Add(supplierOrderLine2);
            //Act
            _supplierOrderManager.CreateSupplierOrder(newSupplierOrder, supplierOrderLines);

        }



    }
}
