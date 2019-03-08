using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SupplierOrder
    {
        public int SupplierOrderID { get; set; }

        public int SupplierID { get; set; }

        public String SupplierName { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Description { get; set; }

        public DateTime DateOrdered { get; set; }

        public bool OrderComplete { get; set; }

        public bool IsValid()
        {
            if (ValidateDescription())
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

    }
}
