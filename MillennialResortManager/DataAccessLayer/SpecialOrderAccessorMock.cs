using DataObjects;
using System;
using System.Collections.Generic;


namespace DataAccessLayer
{
    public class SpecialOrderAccessorMock : ISpecialOrderAccessor
    {
            private List<int> _orderint;
            private List<int> _orderlineint;
            private List<CompleteSpecialOrder> _order;
            private List<SpecialOrderLine> _orderline;

            /// <summary>
            /// Carlos Arzu
            /// Created: 2019/02/28
            /// 
            /// Creates the new Special order, with the data provided by user.
            /// </summary
        public SpecialOrderAccessorMock()
            {
                _order = new List<CompleteSpecialOrder>();
                _orderline = new List<SpecialOrderLine>();

            _order.Add(new CompleteSpecialOrder()
                {
                    SpecialOrderID = 110056,
                    EmployeeID = 10016,
                    Description = "Sirloin 2% fat",
                    OrderComplete = false,
                    DateOrdered = DateTime.Now,
                    SupplierID = 100024
                });

            _order.Add(new CompleteSpecialOrder()
                {
                    SpecialOrderID = 110000,
                    EmployeeID = 10006,
                    Description = "Full Synthetic Engine Oil",
                    OrderComplete = false,
                    DateOrdered = DateTime.Now,
                    SupplierID = 100010
                });

            _order.Add(new CompleteSpecialOrder()
                    {
                        SpecialOrderID = 100001,
                        EmployeeID = 100005,
                        Description = "Round Table",
                        OrderComplete = false,
                        DateOrdered = DateTime.Now,
                        SupplierID = 100011
                    });

           
            _orderline.Add(new SpecialOrderLine()
                {
                    ItemID = 100003,
                    SpecialOrderID = 100000,
                    Description = "six pack monte carlo beer",
                    OrderQty = 100,
                    QtyReceived = 0
                });

            _orderline.Add(new SpecialOrderLine()
                {
                    ItemID = 100503,
                    SpecialOrderID = 100013,
                    Description = "Box of Matchess",
                    OrderQty = 100,
                    QtyReceived = 100
                });

            _orderline.Add(new SpecialOrderLine()
                {
                    ItemID = 100503,
                    SpecialOrderID = 100008,
                    Description = "Pencil B2",
                    OrderQty = 100,
                    QtyReceived = 0
                });

          
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/28
        /// 
        /// Creates the new Supplier order, with the data provided by user.
        /// </summary
        public int InsertSpecialOrder(CompleteSpecialOrder newSpecialOrder, SpecialOrderLine newSpecialOrderline)
        {
            _order.Add(newSpecialOrder);
            _orderline.Add(newSpecialOrderline);

            return 1;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/28
        /// 
        /// Update the new Special order, with the data provided by user.
        /// </summary
        public int UpdateOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew)
        {
            int iterator = 1;

            return iterator;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/28
        /// 
        /// Retrieves all Browse the new Special order, with the data provided by user.
        /// </summary
        public List<CompleteSpecialOrder> RetrieveSpecialOrder()
        {
          
            return _order;
        }

        public List<int> retrieveitemID()
        {
           List<int> item = new List<int>();

            return item;
        }

        public List<SpecialOrderLine> retrieveSpecialOrderLinebySpecialID(int Item)
        {
           
            return _orderline;
        }

        public List<int> listOfEmployeesID()
        {
            List<int> item = new List<int>();

            return item;
        }

        public int DeactivateSpecialOrder(int specialOrderID)
        {
            throw new NotImplementedException();
        }

        public void UpdateSpecialOrderLine(List<SpecialOrderLine> specialOrderLines)
        {
            throw new NotImplementedException();
        }
    }
}



