using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Eric Bostwick
    /// Created 3/26/2019
    /// Used for managing Picking Operations
    /// </summary>
    public class PickSheet
    {
        public string PickSheetID { get; set; }
        public string TempPickSheetID { get; set; }
        public int PickSheetInternalOrderID { get; set; }
        public string PickSheetIDView { get; set; }
        public int PickSheetCreatedBy { get; set; }
        public string PickSheetCreatedByName { get; set; }
        public DateTime CreateDate { get; set; }
        public int PickCompletedBy { get; set; }
        public string PickCompletedByName { get; set; }
        public DateTime PickCompletedDate { get; set; }
        public string PickCompletedDateView { get; set; }
        public DateTime PickDeliveryDate { get; set; }
        public string PickDeliveryDateView { get; set; }
        public int PickDeliveredBy { get; set; }
        public string PickDeliveredByName { get; set; }
        public int PickSheetStatus { get; set; }  //1 = open 2 = closed
        public string PickSheetStatusView { get; set; }
        public int NumberOfOrders { get; set; }

        public bool IsValid()
        {
                return true;
         }

        

    }
}
