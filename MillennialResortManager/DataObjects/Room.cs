/// <summary>
/// Wes Richardson
/// Created: 2019/01/24
/// 
/// Data Object used for Resort Room Infomation
/// </summary>
/// <remarks>
/// Wes Richardson
/// Updated: 2019/02/20
/// Added Price, OfferingID, RoomStatus, and ResortPropertyID
/// To Keep the object inline with the table in the database
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string Building { get; set; }
        public string RoomType { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public int OfferingID { get; set; }
        public string RoomStatus { get; set; }
        public int ResortPropertyID { get; set; }
    }
}
