using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IResortVehicleStatusManager
    {
        int AddResortVehicleStatus(ResortVehicleStatus status);

        ResortVehicleStatus RetrieveResortVehicleStatusById(string id);

        IEnumerable<ResortVehicleStatus> RetrieveResortVehicleStatuses();

        void UpdateResortVehicleStatus(ResortVehicleStatus oldStatus, ResortVehicleStatus newStatus);

        void DeleteResortVehicleStatus(string id);
    }
}