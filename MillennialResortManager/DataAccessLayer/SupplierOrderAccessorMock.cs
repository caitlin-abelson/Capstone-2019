using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class SupplierOrderAccessorMock : ISupplierOrderAccessor
    {
        private List<SupplierOrder> _orders;
        private List<SupplierOrderLine> _lines;
        private List<VMItemSupplierItem> _suppliers;

        public SupplierOrderAccessorMock()
        {
            _orders = new List<SupplierOrder>();
            _lines = new List<SupplierOrderLine>();
            _suppliers = new List<VMItemSupplierItem>();

            _orders.Add(new SupplierOrder()
            {
                SupplierOrderID = 100000,
                EmployeeID = 100000,
                SupplierID = 100000,
                DateOrdered = DateTime.Now,
                Description = "Order 1",
                OrderComplete = false
            });
            _orders.Add(new SupplierOrder()
            {
                SupplierOrderID = 100001,
                EmployeeID = 100001,
                SupplierID = 100001,
                DateOrdered = DateTime.Now,
                Description = "Order 2",
                OrderComplete = false
            });
            _orders.Add(new SupplierOrder()
            {
                SupplierOrderID = 100002,
                EmployeeID = 100001,
                SupplierID = 100002,
                DateOrdered = DateTime.Now,
                Description = "Order 3",
                OrderComplete = false
            });

            _lines.Add(new SupplierOrderLine()
            {
                SupplierOrderID = 100000,
                ItemID = 100015,
                Description = "Soap",
                OrderQty = 100,
                UnitPrice = 0.25M,
                QtyReceived = 0
            });
            _lines.Add(new SupplierOrderLine()
            {
                SupplierOrderID = 100000,
                ItemID = 100016,
                Description = "ToothPaste",
                OrderQty = 50,
                UnitPrice = 0.76M,
                QtyReceived = 0
            });
            _lines.Add(new SupplierOrderLine()
            {
                SupplierOrderID = 100001,
                ItemID = 100017,
                Description = "Lotion",
                OrderQty = 50,
                UnitPrice = 0.40M,
                QtyReceived = 0
            });
            _lines.Add(new SupplierOrderLine()
            {
                SupplierOrderID = 100001,
                ItemID = 100018,
                Description = "Bourbon",
                OrderQty = 5,
                UnitPrice = 22.00M,
                QtyReceived = 0
            });
            _lines.Add(new SupplierOrderLine()
            {
                SupplierOrderID = 100002,
                ItemID = 100015,
                Description = "Soap",
                OrderQty = 100,
                UnitPrice = 0.25M,
                QtyReceived = 0
            });
            _lines.Add(new SupplierOrderLine()
            {
                SupplierOrderID = 100002,
                ItemID = 100016,
                Description = "ToothPaste",
                OrderQty = 50,
                UnitPrice = 0.76M,
                QtyReceived = 0
            });

            _suppliers.Add(new VMItemSupplierItem()
            {
                SupplierID = 100000,
                ItemID = 100015,
                Active = true,
                CustomerPurchasable = true,
                DateActive = DateTime.Today,
                Description = "Dog Food",
                ItemSupplierActive = true,
                ItemType = "Pet Supplies",
                Name = "Something",
                LeadTimeDays = 5,
                OnHandQty = 6,
                OfferingID = 0,
                PrimarySupplier = false,
                ProductID = 0,
                RecipeID = 0,
                UnitPrice = 0.10M,
                ReorderQty = 2
            });

            _suppliers.Add(new VMItemSupplierItem()
            {
                SupplierID = 100000,
                ItemID = 100016,
                Active = true,
                CustomerPurchasable = false,
                DateActive = DateTime.Today,
                Description = "Soap",
                ItemSupplierActive = true,
                ItemType = "Hotel",
                Name = "Ivory",
                LeadTimeDays = 12,
                OnHandQty = 50,
                OfferingID = 0,
                PrimarySupplier = false,
                ProductID = 0,
                RecipeID = 0,
                UnitPrice = 0.45M,
                ReorderQty = 2
            });

            _suppliers.Add(new VMItemSupplierItem()
            {
                SupplierID = 100001,
                ItemID = 100017,
                Active = true,
                CustomerPurchasable = false,
                DateActive = DateTime.Today,
                Description = "Onions",
                ItemSupplierActive = true,
                ItemType = "Food",
                Name = "Vadalia",
                LeadTimeDays = 5,
                OnHandQty = 500,
                OfferingID = 0,
                PrimarySupplier = false,
                ProductID = 0,
                RecipeID = 0,
                UnitPrice = 0.50M,
                ReorderQty = 2
            });

            _suppliers.Add(new VMItemSupplierItem()
            {
                SupplierID = 100001,
                ItemID = 100018,
                Active = true,
                CustomerPurchasable = true,
                DateActive = DateTime.Today,
                Description = "Bourbon",
                ItemSupplierActive = true,
                ItemType = "Drink",
                Name = "Old Vagrant",
                LeadTimeDays = 25,
                OnHandQty = 1,
                OfferingID = 0,
                PrimarySupplier = false,
                ProductID = 0,
                RecipeID = 0,
                UnitPrice = 15.00M,
                ReorderQty = 1
            });

            _suppliers.Add(new VMItemSupplierItem()
            {
                SupplierID = 100002,
                ItemID = 100019,
                Active = true,
                CustomerPurchasable = false,
                DateActive = DateTime.Today,
                Description = "Bath Towel",
                ItemSupplierActive = true,
                ItemType = "Hotel",
                Name = "Cannon White",
                LeadTimeDays = 30,
                OnHandQty = 400,
                OfferingID = 0,
                PrimarySupplier = false,
                ProductID = 0,
                RecipeID = 0,
                UnitPrice = 5.00M,
                ReorderQty = 100
            });

            _suppliers.Add(new VMItemSupplierItem()
            {
                SupplierID = 100002,
                ItemID = 100020,
                Active = true,
                CustomerPurchasable = false,
                DateActive = DateTime.Today,
                Description = "Bath Towel",
                ItemSupplierActive = true,
                ItemType = "Hotel",
                Name = "Cannon White",
                LeadTimeDays = 30,
                OnHandQty = 400,
                OfferingID = 0,
                PrimarySupplier = false,
                ProductID = 0,
                RecipeID = 0,
                UnitPrice = 5.00M,
                ReorderQty = 100
            });
        }



        public int InsertSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines)
        {
            int count = 0;

            _orders.Add(new SupplierOrder()
            {
                SupplierOrderID = supplierOrder.SupplierOrderID,
                EmployeeID = supplierOrder.EmployeeID,
                SupplierID = supplierOrder.SupplierID,
                Description = supplierOrder.Description,
                OrderComplete = supplierOrder.OrderComplete,
                DateOrdered = supplierOrder.DateOrdered
            });
            foreach (var line in supplierOrderLines)
            {
                count++;
                line.SupplierOrderID = supplierOrder.SupplierOrderID;
                _lines.Add(line);
            }
            return count;
        }

        public List<SupplierOrder> SelectAllSupplierOrders()
        {
            return _orders;
        }

        public List<VMItemSupplierItem> SelectItemSuppliersBySupplierID(int supplierID)
        {
            return _suppliers.FindAll(s => s.SupplierID == supplierID);
        }

        public List<SupplierOrderLine> SelectSupplierOrderLinesBySupplierOrderID(int supplierOrderID)
        {
            return _lines.FindAll(l => l.SupplierOrderID == supplierOrderID);
        }

        public int UpdateSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines)
        {
            int result = 0;
            SupplierOrder _oldOrder;

            _oldOrder = _orders.Find(s => s.SupplierOrderID == supplierOrder.SupplierOrderID);

            foreach (var order in _orders)
            {
                if (supplierOrder.SupplierOrderID == _oldOrder.SupplierOrderID)
                {
                    _oldOrder.Description = supplierOrder.Description;
                    result = 1;
                }
            }

            return result;
        }
        public int DeleteSupplierOrder(int supplierOrderID)
        {
            if (_orders.Remove(_orders.Find(x => x.SupplierOrderID == supplierOrderID)))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public SupplierOrder RetrieveSupplierOrderByID(int supplierOrderID)
        {
            throw new NotImplementedException();
        }

        public void CompleteSupplierOrder(int supplierOrderID)
        {
            throw new NotImplementedException();
        }
    }
}
