using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IGuestVehicleAccessor
    {
        int InsertGuestVehicle(GuestVehicle vehicle);
        List<VMGuestVehicle> SelectAllGuestVehicles();
        int UpdateGuestVehicle(GuestVehicle oldVehicle, GuestVehicle vehicle);
    }
}