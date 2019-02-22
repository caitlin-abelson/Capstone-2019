/// <summary>
/// Wes Richardson
/// Created: 2019/01/24
/// 
/// Data Object used for Resort Building Infomation
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Building
    {
        public string BuildingID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
