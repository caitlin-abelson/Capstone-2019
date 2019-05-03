using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created On: 2019-04-25
    /// View Model for Guest Room Assignments
    /// </summary>
    public class GuestRoomAssignmentVM
    {
        public GuestRoomAssignmentVM()
        {
        }

        public int GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BuildingName { get; set; }
        public int RoomNumber { get; set; }
        public int RoomReservationID { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}
