using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/03/2
    /// 
    /// </summary>
    public class MockResortVehicleAccessor : IResortVehicleAccessor
    {
        /// <summary>
        /// To not have mock drive unit test
        /// return zero at all times
        /// </summary>
        /// <param name="resortVehicle"></param>
        /// <returns>0</returns>
        public int AddVehicle(ResortVehicle resortVehicle)
        {
            return 0;
        }

        /// <summary>
        /// Throws no exceptions
        /// </summary>
        /// <param name="vehicleId"></param>
        public void DeactivateVehicle(int vehicleId)
        {
            // do nothing
        }

        /// <summary>
        /// Throws no exceptions
        /// </summary>
        /// <param name="vehicleId"></param>
        public void DeleteVehicle(int vehicleId)
        {
            // do nothing
        }

        /// <summary>
        /// Returns an empty resortVehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResortVehicle RetrieveVehicleById(int id)
        {
            return new ResortVehicle();
        }

        /// <summary>
        /// Returns null to intentionally
        /// have logic layer throw an exception
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ResortVehicle> RetrieveVehicles()
        {
            return null;
        }

        /// <summary>
        /// Throws no exceptions
        /// Intended to do nothing
        /// </summary>
        /// <param name="oldResortVehicle"></param>
        /// <param name="newResortVehicle"></param>
        public void UpdateVehicle(ResortVehicle oldResortVehicle, ResortVehicle newResortVehicle)
        {
            // do nothing
        }
    }
}
