using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class GuestRoomAssignmentMock : IGuestRoomAssignmentAccessor
    {
        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 2019-04-26
        /// Mock Accessor for Guest Room Assignment operations
        /// </summary>
        private List<GuestRoomAssignmentVM> _assignments;
        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 2019-04-26
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public GuestRoomAssignmentMock()
        {
            _assignments = new List<GuestRoomAssignmentVM>();
            _assignments.Add(new GuestRoomAssignmentVM()
            {
                RoomReservationID = 100000,
                RoomNumber = 100,
                BuildingName = "Shack A",
                CheckinDate = DateTime.Now,
                FirstName = "Frank",
                LastName = "Frankerson",
                CheckOutDate = null,
                GuestID = 100000
            });
            _assignments.Add(new GuestRoomAssignmentVM()
            {
                RoomReservationID = 100000,
                RoomNumber = 100,
                BuildingName = "Shack A",
                CheckinDate = DateTime.Now,
                FirstName = "Dan",
                LastName = "Daniel",
                CheckOutDate = null,
                GuestID = 100001
            });
            _assignments.Add(new GuestRoomAssignmentVM()
            {
                RoomReservationID = 100000,
                RoomNumber = 100,
                BuildingName = "Shack A",
                CheckinDate = DateTime.Now,
                FirstName = "Rickey",
                LastName = "Raccoon",
                CheckOutDate = DateTime.Now.AddDays(1),
                GuestID = 100002
            });
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 2019-04-26
        /// Selects all of the VMS with the given ID, in this case, all of the reservations.
        /// </summary>
        public List<GuestRoomAssignmentVM> SelectGuestRoomAssignmentVMSByReservationID(int reservationID)
        {
            return _assignments;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 2019-04-26
        /// Sets a specific assignment to checked out by adding a checkout date
        /// </summary>
        public int UpdateGuestRoomAssignmentToCheckedOut(int guestID, int roomReservationID)
        {
            int result = 0;
            if(_assignments.Find(x => x.GuestID == guestID && x.RoomReservationID == roomReservationID) != null)
            {
                _assignments.Find(x => x.GuestID == guestID && x.RoomReservationID == roomReservationID).CheckOutDate = DateTime.Now;
                result = 1;
            }

            return result;
        }
    }
}
