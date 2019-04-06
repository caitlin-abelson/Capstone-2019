using DataObjects;
using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    /// 
    /// Interface for Resort Vehicle Accessor
    /// </summary>
    public interface IResortVehicleAccessor
    {
        int AddVehicle(ResortVehicle resortVehicle);
        ResortVehicle RetrieveVehicleById(int id);
        IEnumerable<ResortVehicle> RetrieveVehicles();
        void UpdateVehicle(ResortVehicle oldResortVehicle, ResortVehicle newResortVehicle);
        void DeactivateVehicle(int vehicleId);
        void DeleteVehicle(int vehicleId);
    }
}
