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
    public class MockVehicleAccessor : IVehicleAccessor
    {
        /// <summary>
        /// To not have mock drive unit test
        /// return zero at all times
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns>0</returns>
        public int AddVehicle(Vehicle vehicle)
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
        /// Returns an empty vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vehicle RetrieveVehicleById(int id)
        {
            return new Vehicle();
        }

        /// <summary>
        /// Returns null to intentionally
        /// have logic layer throw an exception
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vehicle> RetrieveVehicles()
        {
            return null;
        }

        /// <summary>
        /// Throws no exceptions
        /// Intended to do nothing
        /// </summary>
        /// <param name="oldVehicle"></param>
        /// <param name="newVehicle"></param>
        public void UpdateVehicle(Vehicle oldVehicle, Vehicle newVehicle)
        {
            // do nothing
        }
    }
}
