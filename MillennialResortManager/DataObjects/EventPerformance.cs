using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{

    /// <summary>
    /// @Author: Phillip Hansen
    /// @Created 4/10/2019
    /// 
    /// Data Object class for EventPerformance records
    /// </summary>
    public class EventPerformance
    {
        public int EventID { get; set; }
        public string EventTitle { get; set; }
        public string PerformanceTitle { get; set; }
        public int PerformanceID { get; set; }
    }
}
