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
                DateOrdered = DateTime.Now,
                Supplier = "Teflon"
            });

            _order.Add(new CompleteSpecialOrder()
            {
                SpecialOrderID = 110000,
                EmployeeID = 10006,
                Description = "Full Synthetic Engine Oil",
                DateOrdered = DateTime.Now,
                Supplier = "jadeo"
            });

            _order.Add(new CompleteSpecialOrder()
            {
                SpecialOrderID = 100001,
                EmployeeID = 100005,
                Description = "Round Table",
                DateOrdered = DateTime.Now,
                Supplier = "Man with a hat"
            });


            _orderline.Add(new SpecialOrderLine()
            {
                NameID = "Camping Knife",
                SpecialOrderID = 100000,
                Description = "six pack monte carlo beer",
                OrderQty = 100,
                QtyReceived = 0
            });

            _orderline.Add(new SpecialOrderLine()
            {
                NameID = "Gold Earrings",
                SpecialOrderID = 100013,
                Description = "Box of Matchess",
                OrderQty = 100,
                QtyReceived = 100
            });

            _orderline.Add(new SpecialOrderLine()
            {
                NameID = "Leather Jacket Calvin Klein",
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
        public int InsertSpecialOrder(CompleteSpecialOrder newSpecialOrder)
        {
            _order.Add(newSpecialOrder);


            return 1;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/03/26
        /// 
        /// Creates the new Supplier order, with the data provided by user.
        /// </summary
        public int InsertSpecialOrderLine(SpecialOrderLine newSpecialOrderline)
        {
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
        /// Update the new Special order, with the data provided by user.
        /// </summary
        public int UpdateOrderLine(SpecialOrderLine Order, SpecialOrderLine Ordernew)
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
        public List<CompleteSpecialOrder> SelectSpecialOrder()
        {

            return _order;
        }

        public List<int> SelectitemID()
        {
            List<int> item = new List<int>();

            return item;
        }

        public List<SpecialOrderLine> SelectSpecialOrderLinebySpecialID(int Item)
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

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/06
        /// 
        /// Deletes a record from the DB.
        /// </summary
        public int DeleteItemFromOrder(int ID, string ItemName)
        {
            int iterator = 1;

            return iterator;
        }

        // <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Retrieves the ItemId needed for every form.
        /// </summary
        public int retrieveSpecialOrderIDbyDetails(CompleteSpecialOrder selected)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/04/10
        /// 
        /// Adding the username who authorized this order.
        /// </summary
        public int insertAuthenticateBy(int SpecialOrderID, string Authorized)
        {
            throw new NotImplementedException();
        }
    }
}



