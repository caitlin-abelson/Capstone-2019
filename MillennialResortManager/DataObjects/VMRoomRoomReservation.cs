using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class VMRoomRoomReservation
    {
        public int ReservationID { get; set; }
        public int RoomReservationID { get; set; }
        public int RoomID { get; set; }
        public int RoomNumber { get; set; }
        public string BuildingID { get; set; }
        public string BuildingName { get; set; }
        public string RoomTypeID { get; set; }
        public string RoomTypeDescription { get; set; }
        public int CurrentlyAssigned { get; set; }
        public int Capacity { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

    }
}
