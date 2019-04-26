using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-26
    /// 
    /// A ViewModel to show relevant data about a Tab in a 
    /// DataGrid.
    /// </summary>
    public class VMTab
    {
        /// <summary>
        /// The Unique ID of the MemberTab.
        /// </summary>
        public int MemberTabID { get; set; }

        /// <summary>
        /// The MemberID of the Tab.
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// Whether the tab is Active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// The total of all lines on the tab.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// The DateTime of the last item added to the tab.
        /// </summary>
        public DateTime Date { get; set; }

    }
}
