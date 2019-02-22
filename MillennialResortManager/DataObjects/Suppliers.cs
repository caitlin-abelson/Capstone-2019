/// <summary>
/// Caitlin Abelson
/// Created: 1/21/19
/// 
/// This class holds all of the data objects for the Supplier
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Suppliers
    {
        public Suppliers()
        {

        }

        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string SupplierEmail { get; set; }
        public DateTime DateAdded { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string ZipCode { get; set; }
    }
}
