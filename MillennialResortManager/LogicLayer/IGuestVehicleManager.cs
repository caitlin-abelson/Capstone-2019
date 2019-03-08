using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    public interface IGuestVehicleManager
    {
        bool CreateGuestVehicle(GuestVehicle vehicle);
        
        List<VMGuestVehicle> RetrieveAllGuestVehicles();
        bool UpdateGuestVehicle(GuestVehicle oldVehicle, GuestVehicle vehicle);
    }
}