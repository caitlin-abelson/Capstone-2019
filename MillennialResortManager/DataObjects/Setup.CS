using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Caitlin Abelson
    /// Created Date: 2/28/19
    /// 
    /// The data objects for Setup
    /// </summary>
    public class Setup
    {
        public int SetupID { get; set; }
        public int EventID { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateRequired { get; set; }
        public string Comments { get; set; }



    }
}
