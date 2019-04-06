using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/3/23
    /// 
    /// Interface for Resort VehicleStatus Accessor
    /// </summary>
    public interface IResortVehicleStatusAccessor
    {
        int AddResortVehicleStatus(ResortVehicleStatus status);

        ResortVehicleStatus RetrieveResortVehicleStatusById(string id);

        IEnumerable<ResortVehicleStatus> RetrieveResortVehicleStatuses();

        void UpdateResortVehicleStatus(ResortVehicleStatus oldStatus, ResortVehicleStatus newStatus);

        void DeleteResortVehicleStatus(string id);
    }

}
