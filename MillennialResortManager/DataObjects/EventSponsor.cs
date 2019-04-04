using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class EventSponsor
    {
        /// <summary>
        /// @Author: Phillip Hansen
        /// @Created 4/3/2019
        /// 
        /// Creates the Event Sponsor object for the joined table
        /// </summary>
        public int EventID { get; set; }
        public int SponsorID { get; set; }
    }
}
