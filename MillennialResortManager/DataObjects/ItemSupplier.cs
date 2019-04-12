using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ItemSupplier : Supplier
    {
        public int ItemID { get; set; }
        public int ItemSupplierID { get; set; }
        public new int SupplierID { get; set; }

        public bool PrimarySupplier { get; set; }

        public int LeadTimeDays { get; set; }

        public decimal UnitPrice { get; set; }
        public bool ItemSupplierActive { get; set; }

        public bool IsValid()
        {
            if (ValidateLeadTimeDays() && ValidateUnitPrice())
            {
                return true;
            }
            return false;
        }

        public bool ValidateLeadTimeDays()
        {
            if (LeadTimeDays > 0 && LeadTimeDays <= 365)
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
        

    }
}
