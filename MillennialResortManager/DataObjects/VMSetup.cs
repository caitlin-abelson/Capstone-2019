using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class VMSetup
    {
        public int SetupID { get; set; }
        public int EventID { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateRequired { get; set; }
        public string Comments { get; set; }
        public string EventTitle { get; set; }
    }
}
