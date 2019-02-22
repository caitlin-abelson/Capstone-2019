using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

/// <summary>
/// Richard Carroll
/// Created: 2019/01/30
/// 
/// This class runs tests necessary to ensure data validation 
/// runs properly in the InternalOrderManager.
/// </summary>
namespace LogicLayer.Tests
{
    [TestClass()]
    public class InternalOrderManagerTests
    {
        private InternalOrder internalOrder;
        private VMInternalOrder VMInternalOrder;
        private List<VMInternalOrderLine> lines;
        private VMInternalOrderLine _internalOrderLine;
        private IInternalOrderManager _internalOrderManager;
        private List<VMInternalOrder> retrievedOrders;
        private List<VMInternalOrderLine> retrievedLines;
        //Code seems to seriously dislike using the interface to make this a pointer
        // it throws an error saying it's the wrong object type when setting it to a 
        // mock inside the TestInitialize.
        private InternalOrderAccessorMock internalOrderAccessor;
        
    
        [TestInitialize]
        public void TestSetup()
        {
            internalOrderAccessor = new InternalOrderAccessorMock();
            _internalOrderManager = new InternalOrderManager(internalOrderAccessor);

        }
        //Borrowed From Kevin Broskow
        private string createString(int length)
        {
            string testLength = "";
            for (int i = 0; i < length; i++)
            {
                testLength += "*";
            }
            return testLength;
        }
        //Here starts the AddItemOrder Tests
        [TestMethod()]
        public void AddItemOrderTestValidInput()
        {
            internalOrder = new InternalOrder() { InternalOrderID = 8000, EmployeeID = 100000, DepartmentID = "Events",
                Description = "Food Order... maybe", OrderComplete = false, DateOrdered = DateTime.Now };
            _internalOrderLine = new VMInternalOrderLine() { ItemID = 100000, OrderQty = 100,
                                                        QtyReceived = 100};
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

            retrievedOrders = _internalOrderManager.RetrieveAllInternalOrders();
            retrievedLines = _internalOrderManager.RetrieveOrderLinesByID(internalOrder.InternalOrderID);

            Assert.IsNotNull(retrievedOrders.Find(o => o.InternalOrderID == internalOrder.InternalOrderID
            && o.EmployeeID == internalOrder.EmployeeID && o.DepartmentID == internalOrder.DepartmentID
            && o.Description == internalOrder.Description && o.OrderComplete == internalOrder.OrderComplete
             && o.DateOrdered == internalOrder.DateOrdered));
            Assert.IsNotNull(retrievedLines.Find(l => l.ItemID == _internalOrderLine.ItemID 
                && l.OrderQty == _internalOrderLine.OrderQty && l.QtyReceived == 
                _internalOrderLine.QtyReceived));

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderTestInValidDepartmentLength()
        {
            internalOrder = new InternalOrder()
            {
                InternalOrderID = 8000,
                EmployeeID = 100000,
                DepartmentID = createString(51),
                Description = "Food Order... maybe",
                OrderComplete = false,
                DateOrdered = DateTime.Now
            };
            _internalOrderLine = new VMInternalOrderLine()
            {
                ItemID = 100000,
                OrderQty = 100,
                QtyReceived = 100
            };
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderTestInValidDepartmentBlank()
        {
            internalOrder = new InternalOrder()
            {
                InternalOrderID = 8000,
                EmployeeID = 100000,
                DepartmentID = "",
                Description = "Food Order... maybe",
                OrderComplete = false,
                DateOrdered = DateTime.Now
            };
            _internalOrderLine = new VMInternalOrderLine()
            {
                ItemID = 100000,
                OrderQty = 100,
                QtyReceived = 100
            };
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderTestInValidDescriptionNull()
        {
            internalOrder = new InternalOrder()
            {
                InternalOrderID = 8000,
                EmployeeID = 100000,
                DepartmentID = "Events",
                Description = null,
                OrderComplete = false,
                DateOrdered = DateTime.Now
            };
            _internalOrderLine = new VMInternalOrderLine()
            {
                ItemID = 100000,
                OrderQty = 100,
                QtyReceived = 100
            };
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderTestInValidDescriptionBlank()
        {
            internalOrder = new InternalOrder()
            {
                InternalOrderID = 8000,
                EmployeeID = 100000,
                DepartmentID = "Events",
                Description = "",
                OrderComplete = false,
                DateOrdered = DateTime.Now
            };
            _internalOrderLine = new VMInternalOrderLine()
            {
                ItemID = 100000,
                OrderQty = 100,
                QtyReceived = 100
            };
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderTestInValidDescriptionLength()
        {
            internalOrder = new InternalOrder()
            {
                InternalOrderID = 8000,
                EmployeeID = 100000,
                DepartmentID = "Events",
                Description = createString(1001),
                OrderComplete = false,
                DateOrdered = DateTime.Now
            };
            _internalOrderLine = new VMInternalOrderLine()
            {
                ItemID = 100000,
                OrderQty = 100,
                QtyReceived = 100
            };
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderLineTestInValidOrderQty()
        {
            internalOrder = new InternalOrder()
            {
                InternalOrderID = 8000,
                EmployeeID = 100000,
                DepartmentID = "Events",
                Description = "Food Order... Maybe",
                OrderComplete = false,
                DateOrdered = DateTime.Now
            };
            _internalOrderLine = new VMInternalOrderLine()
            {
                ItemID = 100000,
                OrderQty = -1,
                QtyReceived = 100
            };
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemOrderLineTestInValidQtyReceived()
        {
            internalOrder = new InternalOrder()
            {
                InternalOrderID = 8000,
                EmployeeID = 100000,
                DepartmentID = "Events",
                Description = "Food Order... Maybe",
                OrderComplete = false,
                DateOrdered = DateTime.Now
            };
            _internalOrderLine = new VMInternalOrderLine()
            {
                ItemID = 100000,
                OrderQty = 100,
                QtyReceived = -100
            };
            lines = new List<VMInternalOrderLine>();
            lines.Add(_internalOrderLine);
            _internalOrderManager.CreateInternalOrder(internalOrder, lines);

        }


        //Here starts the RetrieveAllInternalOrders Tests
        [TestMethod()]
        public void RetrieveAllInternalOrdersTest()
        {
            retrievedOrders = new List<VMInternalOrder>();
            retrievedOrders = _internalOrderManager.RetrieveAllInternalOrders();
            Assert.IsNotNull(retrievedOrders);
        }

        //Here starts the RetrieveOrderLinesByID Tests
        [TestMethod()]
        public void RetrieveOrderLinesByIDTest()
        {
            retrievedLines = new List<VMInternalOrderLine>();
            retrievedLines = _internalOrderManager.RetrieveOrderLinesByID(100000);
            Assert.IsNotNull(retrievedLines.FindAll(l => l.InternalOrderId == 100000));

        }

        //Here starts the UpdateOrderStatusToComplete Tests
        [TestMethod()]
        public void UpdateOrderStatusToCompleteTest()
        {
            _internalOrderManager.UpdateOrderStatusToComplete(100000, true);
            retrievedOrders = new List<VMInternalOrder>();
            retrievedOrders = _internalOrderManager.RetrieveAllInternalOrders();
            Assert.IsNotNull(retrievedOrders.FindAll(o => o.OrderComplete == true &&
            o.InternalOrderID == 100000));
        }
    }
}