using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/2/23
    /// 
    /// Interface for Shuttel Véhicule Manager
    /// </summary>
    public interface IVehicleManager
    {
        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle);
        Vehicle RetrieveVehicleById(int id);
        IEnumerable<Vehicle> RetrieveVehicles();
        void DeactivateVehicle(Vehicle vehicle, User user = null);
        void ActivateVehicle(Vehicle vehicle, User user = null);
        void DeleteVehicle(Vehicle vehicle, User user = null);
    }
}
