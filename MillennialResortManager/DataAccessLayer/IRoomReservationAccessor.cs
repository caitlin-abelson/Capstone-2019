using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRoomReservationAccessor
    {

        int InsertGuestRoomAssignment(int guestID, int roomReservationID);
        RoomReservation SelectRoomReservationByID(int id);
        RoomReservation SelectRoomReservationByGuestID(int id);
        List<VMRoomRoomReservation> SelectAvailableVMRoomRoomReservations(int reservationId);
        int UpdateCheckInDateToNow(RoomReservation roomReservation);
        int DeleteGuestRoomAssignment(int guestID, int roomReservationID);



    }
}
