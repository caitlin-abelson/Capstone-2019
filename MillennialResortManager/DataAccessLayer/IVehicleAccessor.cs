using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/2/23
    /// 
    /// Interface for accessing Shuttel Véhicule
    /// </summary>
    public interface IVehicleAccessor
    {
        int AddVehicle(Vehicle vehicle);
        Vehicle RetrieveVehicleById(int id);
        IEnumerable<Vehicle> RetrieveVehicles();
        void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle);
        void DeactivateVehicle(int vehicleId);
        void DeleteVehicle(int vehicleId);
    }
}
