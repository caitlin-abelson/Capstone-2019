using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Carlos Arzu
    /// Created: 2019/02/06
    /// 
    /// Object of the table SupplierOrderLine
    /// </summary>
    public class SpecialOrderLine
    {

        public int ItemID { get; set; }
        public int SpecialOrderID { get; set; }
        public string Description { get; set; }
        public int OrderQty { get; set; }
        public int QtyReceived { get; set; }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Validate the description entered by user.
        ///
        /// </summary>
        public bool ValidDescription()
        {
            bool isValid = true;

            if (Description == null || Description == "" || Description.Length > 1000)
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Validate the Quantity of items needed in order.
        ///
        /// </summary>
        public bool ValidOrderQty()

        {
            bool isValid = true;

            if (OrderQty< 0)
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Validate the Quantity of items received from the order generated.
        ///
        /// </summary>
        public bool ValidQtyReceived()

        {
             bool isValid = true;

            if (QtyReceived < 0)
            {
                isValid = false;
            }

         return isValid;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/02/20
        /// 
        /// Valid method, that returns if all input in this object is valid
        ///
        /// </summary>
        public bool isValid()
        {
            bool isValid = true;

            if (!ValidOrderQty() || !ValidQtyReceived() || !ValidDescription())
            {
                isValid = false;
            }

            return isValid;
        }
    }

        
    
}
