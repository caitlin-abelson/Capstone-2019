using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-11
    /// 
    /// Data Object for the RoomReservation table.
    /// </summary>
    public class RoomReservation
    {
        public int RoomReservationID { get; set; }
        public int RoomID { get; set; }
        public int ReservationID { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

    }
}
