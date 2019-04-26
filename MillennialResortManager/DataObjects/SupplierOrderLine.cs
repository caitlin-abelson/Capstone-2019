using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplierOrderLine
    {
        public int SupplierOrderID { get; set; }

        public int ItemID { get; set; }
        public int SupplierItemID { get; set; }

        public string Description { get; set; }

        public int OrderQty { get; set; }

        public decimal UnitPrice { get; set; }

        public int QtyReceived { get; set; }

        public bool IsValid()
        {
            if (ValidateOrderQty() && ValidateUnitPrice() && ValidateDescription() && ValidateQtyReceived())
            {
                return true;
            }
            return false;
        }

        public bool ValidateOrderQty()
        {
            if (OrderQty > 0 && OrderQty < 100000)
            {
                return true;
            }
            return false;
        }
        public bool ValidateUnitPrice()
        {
            if (UnitPrice > 0.0M && UnitPrice <= 9999.99M)
            {
                return true;
            }

            return false;
        }
        public bool ValidateDescription()
        {
            if (Description.Length < 1001)
            {
                return true;
            }

            return false;
        }

        public bool ValidateQtyReceived()
        {
            if (QtyReceived >= 0 && QtyReceived < 100000)
            {
                return true;
            }
            return false;
        }

    }
}
