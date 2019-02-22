using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class InternalOrderAccessorMock : IInternalOrderAccessor
    {
        private List<VMInternalOrder> _orders;
        private List<VMInternalOrderLine> _lines;
        public InternalOrderAccessorMock()
        {
            _orders = new List<VMInternalOrder>();
            _lines = new List<VMInternalOrderLine>();
            _orders.Add(new VMInternalOrder()
            {
                InternalOrderID = 100000,
                EmployeeID = 100000,
                FirstName = "Bob",
                LastName = "Trapp",
                DepartmentID = "Events",
                Description = "Food Order",
                OrderComplete = false,
                DateOrdered = DateTime.Now
            });
            _orders.Add(new VMInternalOrder()
            {
                InternalOrderID = 100001,
                EmployeeID = 100000,
                FirstName = "Robert",
                LastName = "Hunt",
                DepartmentID = "Events",
                Description = "Food Order for reasons....",
                OrderComplete = false,
                DateOrdered = DateTime.Now
            });

            _lines.Add(new VMInternalOrderLine()
            {
                InternalOrderId = 100000,
                ItemID = 100003,
                ItemName = "Mango",
                OrderQty = 100,
                QtyReceived = 0
            });
            _lines.Add(new VMInternalOrderLine()
            {
                InternalOrderId = 100000,
                ItemID = 100001,
                ItemName = "Apple",
                OrderQty = 100,
                QtyReceived = 0
            });
            _lines.Add(new VMInternalOrderLine()
            {
                InternalOrderId = 100001,
                ItemID = 100002,
                ItemName = "Tomato",
                OrderQty = 100,
                QtyReceived = 0
            });
            _lines.Add(new VMInternalOrderLine()
            {
                InternalOrderId = 100001,
                ItemID = 100003,
                ItemName = "Mango",
                OrderQty = 1000,
                QtyReceived = 0
            });
            _lines.Add(new VMInternalOrderLine()
            {
                InternalOrderId = 100001,
                ItemID = 100004,
                ItemName = "Forbidden Fruit",
                OrderQty = 100,
                QtyReceived = 0
            });

        }

        public int InsertItemOrder(InternalOrder internalOrder, List<VMInternalOrderLine> lines)
        {
            int count = 0;

            _orders.Add(new VMInternalOrder() {
                InternalOrderID = internalOrder.InternalOrderID,
                EmployeeID = internalOrder.EmployeeID,
                DepartmentID = internalOrder.DepartmentID,
                Description = internalOrder.Description,
                OrderComplete = internalOrder.OrderComplete,
                DateOrdered = internalOrder.DateOrdered
            });
            foreach (var line in lines)
            {
                count++;
                line.InternalOrderId = internalOrder.InternalOrderID;
                _lines.Add(line);
            }


            return count;

        }

        public List<VMInternalOrder> SelectAllInternalOrders()
        {
            return _orders;
        }

        public List<VMInternalOrderLine> SelectOrderLinesByID(int orderID)
        {
            return _lines.FindAll(l => l.InternalOrderId == orderID);
        }

        public int UpdateOrderStatusToComplete(int orderID, bool orderComplete)
        {
            int result = 0;

            foreach (var order in _orders)
            {
                if (order.InternalOrderID == orderID)
                {
                    result = 1;
                }
            }

            return result;
        }
    }
}
