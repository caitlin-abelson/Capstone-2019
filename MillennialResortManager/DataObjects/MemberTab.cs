using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-18
    /// 
    /// MemberTab object.
    /// </summary>
    public class MemberTab
    {
        public int MemberTabID { get; set; }

        /// <summary>
        /// The Member that this Tab belongs to.
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// If the tab is not active, charges cannot be billed to the tab.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The total of all items on the tab.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// The itemized tab lines.
        /// </summary>
        public List<MemberTabLine> MemberTabLines { get; set; }

        
    }
}
