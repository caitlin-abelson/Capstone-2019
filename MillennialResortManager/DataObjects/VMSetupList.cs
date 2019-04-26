using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class VMSetupList
    {
        public string EventTitle { get; set; }
        public int SetupListID { get; set; }
        public int SetupID { get; set; }
        public bool Completed { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        
    }
}
