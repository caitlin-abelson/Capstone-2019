using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LogicLayer;
using DataObjects;
using DataAccessLayer;
using System;

namespace LogicLayer.Tests
{
    ///  /// <summary>
    /// Carlos Arzu
    /// Created: 2019/02/28
    /// 
    /// description for SupplierOrderManagerTests
    /// </summary>
    [TestClass]
    public class SpecialOrderManagerTests
    {
        private List<CompleteSpecialOrder> _compsupplierOrder;
        private List<SpecialOrderLine> _supplierOrderLine;
        private ISpecialOrderManager _supplierOrderManager;
        private SpecialOrderAccessorMock _supplierOrderMock;

        [TestInitialize]
        public void testSetup()
        {

            _supplierOrderMock = new SpecialOrderAccessorMock();
            _supplierOrderManager = new SpecialOrderManagerMSSQL(_supplierOrderMock);
            _compsupplierOrder = new List<CompleteSpecialOrder>();
            _compsupplierOrder = _supplierOrderManager.retrieveAllOrders();
        }

        private string createStringLength(int length)
        {
            string testingString = "";
            for (int i = 0; i < length; i++)
            {
                testingString += "X";
            }
            return testingString;
        }

        ///  /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/28
        /// 
        /// description all valid inputs to create an order
        /// </summary>
        [TestMethod]
        public void TestCreateSupplierOrderWithAllValidInputs()
        {

            //arrange
            CompleteSpecialOrder order = new CompleteSpecialOrder() {
                SpecialOrderID = 100008,
                EmployeeID = 100005,
                Description = "Escape Pod for Groom",
                DateOrdered = DateTime.Now,
                Supplier = "Backscratcher"
            };

            SpecialOrderLine orderline = new SpecialOrderLine()
            {
                NameID = "Short Relief",
                Description = "Darts to sleep the Bride",
                OrderQty = 40,
                QtyReceived = 0
            };


            //Act
            _supplierOrderManager.CreateSpecialOrder(order);

            //Assert
            _compsupplierOrder = _supplierOrderManager.retrieveAllOrders();
            _supplierOrderLine = _supplierOrderManager.RetrieveOrderLinesByID(order.SpecialOrderID);

            Assert.IsNotNull(_compsupplierOrder.Find(o => o.SpecialOrderID == order.SpecialOrderID
              && o.EmployeeID == order.EmployeeID && o.Description == order.Description
              && o.DateOrdered == order.DateOrdered
              && o.Supplier == order.Supplier));
            /*Assert.IsNotNull(_supplierOrderLine.Find(l => l.NameID == orderline.NameID
              && l.Description == orderline.Description && l.OrderQty == orderline.OrderQty
              && l.QtyReceived == orderline.QtyReceived));*/

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSupplierOrderInvalidDescriptionNull()
        {
            CompleteSpecialOrder order = new CompleteSpecialOrder()
            {
                SpecialOrderID = 100008,
                EmployeeID = 100005,
                Description = null,
                DateOrdered = DateTime.Now,
                Supplier = "Leap"
            };

            SpecialOrderLine orderline = new SpecialOrderLine()
            {
                NameID = "You get nothing",
                Description = null,
                OrderQty = 40,
                QtyReceived = 0
            };

             _supplierOrderManager.CreateSpecialOrder(order);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSupplierOrderInvalidDescriptionBlank()
        {
            CompleteSpecialOrder order = new CompleteSpecialOrder()
            {
                SpecialOrderID = 100008,
                EmployeeID = 100005,
                Description = "",
                DateOrdered = DateTime.Now,
                Supplier = "ShopFever"
            };

            SpecialOrderLine orderline = new SpecialOrderLine()
            {
                NameID = "Job",
                Description = "",
                OrderQty = 40,
                QtyReceived = 0
            };

             _supplierOrderManager.CreateSpecialOrder(order);
             _supplierOrderManager.CreateSpecialOrderLine(orderline);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCreateSupplierOrderInvalidDescriptionLength()
        {
            CompleteSpecialOrder order = new CompleteSpecialOrder()
            {
                SpecialOrderID = 100008,
                EmployeeID = 100005,
                Description = createStringLength(1001),
                DateOrdered = DateTime.Now,
                Supplier = "Fish n Dip"
            };

            SpecialOrderLine orderline = new SpecialOrderLine()
            {
                NameID = "Paint",
                Description = createStringLength(1001),
                OrderQty = 40,
                QtyReceived = 0
            };

            _supplierOrderManager.CreateSpecialOrder(order);
            _supplierOrderManager.CreateSpecialOrderLine(orderline);

        }


        //Retrieve All Order Tests
        [TestMethod()]
        public void RetrieveAllOrdersTest()
        {
            _compsupplierOrder = new List<CompleteSpecialOrder>();
            _compsupplierOrder = _supplierOrderManager.retrieveAllOrders();
            Assert.IsNotNull(_compsupplierOrder);
        }

        //Retrieve Order Lines By Supplier Tests
        [TestMethod()]
        public void RetrieveOrderLinesBySupplierIDTest()
        {
            _supplierOrderLine = new List<SpecialOrderLine>();
            _supplierOrderLine = _supplierOrderManager.RetrieveOrderLinesByID(100000);
            Assert.IsNotNull(_supplierOrderLine.FindAll(l => l.SpecialOrderID == 100000));

        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderLineTestInValidOrderQty()
        {
            CompleteSpecialOrder order = new CompleteSpecialOrder()
            {
                SpecialOrderID = 100008,
                EmployeeID = 100005,
                Description = createStringLength(1001),
                DateOrdered = DateTime.Now,
                Supplier = "Rigga meter"
            };

            SpecialOrderLine orderline = new SpecialOrderLine()
            {
                NameID = "Paint",
                Description = createStringLength(1001),
                OrderQty = -1,
                QtyReceived = 0
            };


            _supplierOrderManager.CreateSpecialOrder(order);
            _supplierOrderManager.CreateSpecialOrderLine(orderline);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderLineTestInValidQtyReceived()
        {
            CompleteSpecialOrder order = new CompleteSpecialOrder()
            {
                SpecialOrderID = 100008,
                EmployeeID = 100005,
                Description = "Mighty tests",
                DateOrdered = DateTime.Now,
                Supplier = "SuperMax"
            };

            SpecialOrderLine orderline = new SpecialOrderLine()
            {
                NameID = "Pole",
                Description = createStringLength(1001),
                OrderQty = 10,
                QtyReceived = -10
            };

            _supplierOrderManager.CreateSpecialOrder(order);
            _supplierOrderManager.CreateSpecialOrderLine(orderline);

        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDateValid()
        {
            CompleteSpecialOrder order = new CompleteSpecialOrder()
            {
                SpecialOrderID = 100008,
                EmployeeID = 100005,
                Description = "Mighty tests",
                DateOrdered = DateTime.Now.AddDays(-1),
                Supplier = "Too much"
            };

            SpecialOrderLine orderline = new SpecialOrderLine()
            {
                NameID = "Paviment",
                Description = createStringLength(1001),
                OrderQty = 10,
                QtyReceived = -10
            };

            _supplierOrderManager.CreateSpecialOrder(order);
            _supplierOrderManager.CreateSpecialOrderLine(orderline);

        }



    }
}
