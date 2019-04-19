/// <summary>
/// Danielle Russo
/// Created: 2019/01/20
/// 
/// Building object that reflects the database's Building table
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Building
    {
        public string BuildingID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        [DisplayName("Current Status")]
        public string StatusID { get; set; }
        public int ResortPropertyID { get; set; }
    }
}
