using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects

/// <summary>
/// Eric Bostwick
/// Created: 2019/4/2
/// 
/// Data Object for picking orders
/// extends the InternalOrderLine object
/// </summary>
///
/// <remarks>
{
    public class PickOrder : InternalOrderLine
    {
        public string PickSheetID { get; set; }
        public string PickSheetIDView { get; set; }
        public int EmployeeID { get; set; }
        public string Orderer { get; set; }
        public int InternalOrderID { get; set; }        
        public string ItemDescription { get; set; }
        public string DeptID { get; set; }
        public string DeptDescription { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateView { get; set; }
        public DateTime OrderReceivedDate { get; set; }
        public string OrderReceivedDateView { get; set; }
        public DateTime PickCompleteDate { get; set; }
        public string PickCompleteDateView { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryDateView { get; set; }
        public int OrderStatus { get; set; }
        public string OrderStatusView { get; set; }
        public bool OutOfStock { get; set; }

        public string DepartmentID { get; set; }

        public bool IsValid()
        {
            if (!ValidateQtyReceived())
            {
                return false;
            }
            return true;
            
        }
        


    }
}
