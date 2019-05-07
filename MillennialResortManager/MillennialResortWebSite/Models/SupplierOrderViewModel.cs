using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MillennialResortWebSite.Models
{
    public class SupplierOrderViewModel
    {
        public int SupplierOrderID { get; set; }

        public int SupplierID { get; set; }

        public string SupplierName { get; set; }
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Description { get; set; }

        public DateTime DateOrdered { get; set; }

        public bool OrderComplete { get; set; }
        public List<SupplierOrderLine> Lines { get; set; }
        public string Exceptions { get; set; }
        public DateTime DateReceived { get; set; }

    }
}