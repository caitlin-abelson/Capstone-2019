using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Luggage
    {
        public int LuggageID { get; set; }
        public int GuestID { get; set; }
        public string Status { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
    }
}
