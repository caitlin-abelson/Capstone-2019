using DataObjects;
using System.Collections.Generic;

namespace LogicLayer
{
	/// <summary author="Francis Mingomba" created="2019/04/03">
	/// Resort Vehicle Manager Interface
	/// </summary>
	public interface IResortVehicleManager
    {
        void AddVehicle(ResortVehicle resortVehicle);
        void UpdateVehicle(ResortVehicle oldResortVehicle, ResortVehicle newResortVehicle);
        ResortVehicle RetrieveVehicleById(int id);
        IEnumerable<ResortVehicle> RetrieveVehicles();
        void DeactivateVehicle(ResortVehicle resortVehicle, Employee employee = null);
        void ActivateVehicle(ResortVehicle resortVehicle, Employee employee = null);
        void DeleteVehicle(ResortVehicle resortVehicle, Employee employee = null);
    }
}
