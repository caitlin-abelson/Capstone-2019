using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class OfferingVM
    {
        public int OfferingID { get; set; }
        public string OfferingTypeID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public string OfferingName { get; set; }

    }
}
