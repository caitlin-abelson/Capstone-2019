using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James Heim
    /// Created 2019-02-28
    /// 
    /// View Model object for Shop.
    /// Consists of the data from the Shop table
    /// and fields from the Room table.
    /// </summary>
    public class VMBrowseShop
    {
        public int ShopID { get; set; }
        public int RoomID { get; set; }
        public string BuildingID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoomNumber { get; set; }
        public bool Active { get; set; }
    }
}
