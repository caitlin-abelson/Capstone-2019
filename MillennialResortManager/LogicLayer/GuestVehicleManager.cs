using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Author: Richard Carroll
    /// Created Date: 2/28/19
    /// 
    /// This Class contains methods for interacting with the Data Acesss Layer
    /// with GuestVehicle Data Objects and 
    /// returns the results of calling those methods.
    /// Also runs validation on data passed in to increase code reliability.
    /// </summary>
    public class GuestVehicleManager : IGuestVehicleManager
    {
        private IGuestVehicleAccessor _guestVehicleAccessor;

        public GuestVehicleManager()
        {
            _guestVehicleAccessor = new GuestVehicleAccessor();
        }
        public GuestVehicleManager(GuestVehicleAccessorMock guestVehicleAccessorMock)
        {
            _guestVehicleAccessor = guestVehicleAccessorMock;
        }


        /// <summary>
        /// Richard Carroll
        /// Created: 3/1/19
        /// 
        /// Takes a Vehicle argument and passes it to the Data 
        /// Access Layer which attempts to insert it into the 
        /// Database.
        /// Returns the result.
        /// </summary>
        public bool CreateGuestVehicle(GuestVehicle vehicle)
        {
            bool result = false;

            try
            {
                if (vehicle.isValid())
                {
                    result = (1 == _guestVehicleAccessor.InsertGuestVehicle(vehicle));
                } else
                {
                    throw new ArgumentException("Data for this Vehicle is Invalid: \n"
                        + vehicle.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 3/7/19
        /// 
        /// Requests a List of Vehicles from the Data Access
        /// Layer, Returns the result.
        /// </summary>
        public List<VMGuestVehicle> RetrieveAllGuestVehicles()
        {
            List<VMGuestVehicle> vehicles = new List<VMGuestVehicle>();

            try
            {
                vehicles = _guestVehicleAccessor.SelectAllGuestVehicles();
            }
            catch (Exception)
            {

                throw;
            }

            return vehicles;
        }


        /// <summary>
        /// Richard Carroll
        /// Created: 3/8/19
        /// 
        /// Takes two Vehicle arguments and passes them to the 
        /// Data Access Layer which attempts to update the Vehicle.
        /// Returns the result.
        /// </summary>
        public bool UpdateGuestVehicle(GuestVehicle oldVehicle, GuestVehicle vehicle)
        {
            bool result = false;

            try
            {
                if (vehicle.isValid())
                {
                    if (oldVehicle.isValid())
                    {
                        result = 1 == _guestVehicleAccessor.UpdateGuestVehicle(oldVehicle, vehicle);
                    }
                    else
                    {
                        throw new ArgumentException("Data for this Vehicle is Invalid: \n"
                            + oldVehicle.ToString());
                    }
                }
                else
                {
                    throw new ArgumentException("Data for this Vehicle is Invalid: \n"
                        + vehicle.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
