using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class BrowseSponsor
    {
        public int SponsorID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City{ get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string StatusID { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }
    }
}
