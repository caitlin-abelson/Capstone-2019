using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IShuttleReservationAccessor
    {
        List<ShuttleReservation> RetrieveAllShuttleReservations();

        int InsertShuttleReservation(ShuttleReservation newShuttleReservation);

        void UpdateShuttleReservation(ShuttleReservation oldShuttleReservation, ShuttleReservation newShuttleReservation);
        ShuttleReservation RetrieveShuttleReservationByShuttleReservationID(int shuttleReservationID);
        List<int> RetrieveGuestIDs();
        List<int> RetrieveEmployeeIDs();
        void DeactivateShuttleReservation(int shuttleReservationID);
        List<ShuttleReservation> RetrieveInactiveShuttleReservations();
        List<ShuttleReservation> RetrieveActiveShuttleReservations();
        void UpdateShuttleReservationDropoff(ShuttleReservation oldShuttleReservation);


    }
}
