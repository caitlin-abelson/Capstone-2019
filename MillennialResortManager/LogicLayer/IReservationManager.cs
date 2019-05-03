using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// IReservationManager is an interface meant to be the standard for interacting with Reservations in a storage medium
    /// </summary>
    public interface IReservationManager
    {
        bool AddReservation(Reservation newReservation);
        void EditReservation(Reservation oldReservation, Reservation newReservation);
        Reservation RetrieveReservation(int ReservationID);
        List<Reservation> RetrieveAllReservations();
        List<VMBrowseReservation> RetrieveAllVMReservations();
        void DeleteReservation(int ReservationID, bool isActive);
        Reservation RetrieveReservationByGuestID(int guestID);

        List<VMBrowseReservation> RetrieveAllActiveVMReservations();
    }
}
