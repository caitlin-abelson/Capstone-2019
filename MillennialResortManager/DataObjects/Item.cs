using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Richard Carroll
/// Created: 2019/01/28
/// 
/// This class is only needed because the presentation layer requires small amounts of data
/// about items to insert orders. This class only holds that much data
/// </summary>
///
/// <remarks>

namespace DataObjects
{
    public class Item
    {
        public string Name { get; set; }
        public int ItemID { get; set; }
    }
}
