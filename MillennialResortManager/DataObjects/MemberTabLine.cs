using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataObjects
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-18
    /// 
    /// MemberTabLine object.
    /// </summary>
    public class MemberTabLine
    {
        /// <summary>
        /// The ID of the MemberTabLine.
        /// </summary>
        public int MemberTabLineID { get; set; }

        /// <summary>
        /// The ID of the MemberTab that this line corresponds to.
        /// </summary>
        public int MemberTabID { get; set; }

        /// <summary>
        /// The ID of the Guest who purchased the item.
        /// Nullable if the purchase was done directly by the account (like web.)
        /// </summary>
        public int? GuestID { get; set; } 

        /// <summary>
        /// The ID of the Shop the good or service was purchased from.
        /// Nullable because the purchase may not be from a Shop.
        /// </summary>
        public int? ShopID { get; set; } 

        /// <summary>
        /// The offering ID that corresponds to this line.
        /// </summary>
        public int OfferingID { get; set; }

        /// <summary>
        /// The quantity of the offering.
        /// </summary>
        [DisplayName("Qty")]
        public int Quantity { get; set; }

        /// <summary>
        /// The price of the offering at the time this line was added
        /// to the MemberTab.
        /// </summary>
        [DisplayName("Price")]
        public decimal Price { get; set; }
        
        /// <summary>
        /// The employee that added this line. Optional and nullable as 
        /// items can be added systematically. 
        /// </summary>
        public int? EmployeeID { get; set; } 

        /// <summary>
        /// Discount on the offering.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// The Date the offering/item was purchased.
        /// </summary>
        [DisplayName("Date of Purchase")]
        public DateTime PurchasedDate { get; set; } // added by Matt H. on 4/26/19

        /// <summary>
        /// The purchased offering's item type.
        /// </summary>
        [DisplayName("Purchased Item")]
        public string OfferingTypeID { get; set; } // added by Matt H. on 4/27/19

        /// <summary>
        /// A description of the purchased item.
        /// </summary>
        [DisplayName("Item's Description")]
        public string Description { get; set; } // added by Matt H. on 4/27/19

        /// Date the item was added to the bill.
        /// </summary>
        public DateTime DatePurchased { get; set; }
    }
}
