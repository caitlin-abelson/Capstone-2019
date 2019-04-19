using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    /// <summary>
    /// James Heim
    /// Created 2019-04-11
    /// </summary>
    public interface IRoomReservationManager
    {
        bool AddGuestAssignment(int guestID, int roomReservationID);

        /// <summary>
        /// James Heim
        /// Created 2019-04-11
        /// 
        /// Retrieve the RoomReservation by an integer id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoomReservation RetrieveRoomReservationByID(int id);

        /// <summary>
        /// James Heim
        /// Created 2019-04-16
        /// 
        /// Retrieve the RoomReservation by integer Guest ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoomReservation RetrieveRoomReservationByGuestID(int id);

        List<VMRoomRoomReservation> RetrieveAvailableVMRoomRoomReservations(int reservationId);


        /// <summary>
        /// James Heim
        /// Created 2019-04-11
        /// 
        /// Set the CheckIn date on the Room Reservation to now.
        /// </summary>
        /// <param name="roomReservation"></param>
        /// <returns></returns>
        bool UpdateCheckInDateToNow(RoomReservation roomReservation);

        bool DeleteGuestAssignment(int guestID, int reservationID);
    }
}