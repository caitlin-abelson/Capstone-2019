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
    /// This class contains all the data needed for presenting Line 
    /// Data on the Presentation Layer and can be used in place of 
    /// InternalOrderLine as needed.    
    /// </summary>
    ///
    /// <remarks>
    public class VMInternalOrderLine
    {
        public int InternalOrderId { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int OrderQty { get; set; }
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
