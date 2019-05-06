using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IShuttleReservationManager
    {
        List<ShuttleReservation> RetrieveAllShuttleReservations();
        int CreateShuttleReservation(ShuttleReservation newShuttleReservation);
        void UpdateShuttleReservation(ShuttleReservation oldShuttleReservation, ShuttleReservation newShuttleReservation);
        ShuttleReservation RetrieveShutteReservationByShuttleReservationID(int shuttleReservationID);
        List<int> RetrieveAllEmployeeIDs();
        List<int> RetrieveAllGuestIDs();
        void DeactivateShuttleReservation(int shuttleReservationID, bool isActive);
        List<ShuttleReservation> RetrieveAllInActiveShuttleReservations();
        List<ShuttleReservation> RetrieveAllActiveShuttleReservations();
        void UpdateShuttleReservationDropoff(ShuttleReservation oldShuttleReservation);
    }
}
