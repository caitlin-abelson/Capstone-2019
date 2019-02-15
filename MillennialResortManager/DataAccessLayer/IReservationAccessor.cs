using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// The IReservationAccessor is an interface meant to be the standard for interacting with Data in a storage medium regarding Reservations
    /// </summary>
    public interface IReservationAccessor
    {
        void CreateReservation(Reservation newReservation);
        Reservation RetrieveReservation(int ReservationID);
        List<Reservation> RetrieveAllReservations();
        List<VMBrowseReservation> RetrieveAllVMReservations();
        void UpdateReservation(Reservation oldReservation, Reservation newReservation);
        void DeactivateReservation(int ReservationID);
        void PurgeReservation(int ReservationID);
        bool ValidateMember(int memberID);
    }
}
