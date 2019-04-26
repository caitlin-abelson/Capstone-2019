using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Author: Jared Greenfield
    /// Created On: 2019-04-25
    /// Class for interaction with database for GuestRoomAssignment operations
    /// </summary>
    public class GuestRoomAssignmentManager : IGuestRoomAssignmentManager
    {
        private IGuestRoomAssignmentAccessor _guestRoomAssignmentAccessor;

        public GuestRoomAssignmentManager()
        {
            _guestRoomAssignmentAccessor = new GuestRoomAssignmentAccessor();
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Test Constructor
        /// 
        /// </summary>
        public GuestRoomAssignmentManager(IGuestRoomAssignmentAccessor mock)
        {
            _guestRoomAssignmentAccessor = mock;
        }


        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Retrieves a list of guest Room Assignment View Models
        /// 
        /// </summary>
        /// <param name="reservationID">ID of Reservation you want Guests of</param>
        /// <returns>List of Room Assignment View Models</returns>
        public List<GuestRoomAssignmentVM> SelectGuestRoomAssignmentVMSByReservationID(int reservationID)
        {
            List<GuestRoomAssignmentVM> assignments = null;
            try
            {
                assignments = _guestRoomAssignmentAccessor.SelectGuestRoomAssignmentVMSByReservationID(reservationID);
            }
            catch (Exception)
            {

                throw;
            }
            return assignments;
        }

        /// <summary>
        /// Author: Jared Greenfield
        /// Created On: 2019-04-25
        /// Updates a guest room assignment to checkedout and sets the date
        /// 
        /// </summary>
        /// <param name="roomReservationID">ID of Room Reservation </param>
        /// <param name="guestID">ID of Guest</param>
        /// <returns>True if succeeded, false otherwise</returns>
        public bool UpdateGuestRoomAssignmentToCheckedOut(int guestID, int roomReservationID)
        {
            bool result = false;
            try
            {
                result = 0 < _guestRoomAssignmentAccessor.UpdateGuestRoomAssignmentToCheckedOut(guestID, roomReservationID);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
