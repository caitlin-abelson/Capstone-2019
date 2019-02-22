using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Richard Carroll
    /// Created: 2019/01/28
    /// 
    /// This class contains all the data needed for Database 
    /// Interactions for Lines
    /// </summary>
    ///
    /// <remarks>
    public class InternalOrderLine
    {
        public int ItemID { get; set; }
        public int ItemOrderID { get; set; }
        public int OrderQty { get; set; }
        public decimal UnitPrice { get; set; }
        public int QtyReceived { get; set; }
        public bool ValidateOrderQty()
        {
            bool isValid = true;

            if (OrderQty < 0)
            {
                isValid = false;
            }

            return isValid;
        }
        public bool ValidateQtyReceived()
        {
            bool isValid = true;

            if (QtyReceived < 0)
            {
                isValid = false;
            }

            return isValid;
        }
        public bool isValid()
        {
            bool isValid = true;

            if (!ValidateOrderQty() || !ValidateQtyReceived())
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
