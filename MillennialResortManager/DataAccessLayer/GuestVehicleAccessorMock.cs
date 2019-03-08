using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Richard Carroll
    /// Created Date: 3/8/19
    /// 
    /// This Class contains methods for feigning database interactions
    /// for testing.
    /// </summary>
    public class GuestVehicleAccessorMock : IGuestVehicleAccessor
    {
        public List<VMGuestVehicle> vehicles;

        public GuestVehicleAccessorMock()
        {
            vehicles = new List<VMGuestVehicle>();
            vehicles.Add( new VMGuestVehicle()
            {
                GuestID = 100001,
                Make = "Ford",
                Model = "Focus",
                PlateNumber = "789 ACE",
                Color = "Silver",
                ParkingLocation = "Over thar, yonder"
            }
                );
        }

        public int InsertGuestVehicle(GuestVehicle vehicle)
        {
            int result = 0;

            vehicles.Add(new VMGuestVehicle()
            {
                GuestID = vehicle.GuestID,
                Make = vehicle.Make,
                Model = vehicle.Model,
                PlateNumber = vehicle.PlateNumber,
                Color = vehicle.Color,
                ParkingLocation = vehicle.ParkingLocation
            });
            ++result;

            return result;
        }

        public List<VMGuestVehicle> SelectAllGuestVehicles()
        {
            return vehicles;
        }

        public int UpdateGuestVehicle(GuestVehicle oldVehicle, GuestVehicle vehicle)
        {
            int result = 0;
            VMGuestVehicle oldVMVehicle = new VMGuestVehicle()
            {
                GuestID = oldVehicle.GuestID,
                Make = oldVehicle.Make,
                Model = oldVehicle.Model,
                PlateNumber = oldVehicle.PlateNumber,
                Color = oldVehicle.Color,
                ParkingLocation = oldVehicle.ParkingLocation
            };
            VMGuestVehicle newVMVehicle = new VMGuestVehicle()
            {
                GuestID = vehicle.GuestID,
                Make = vehicle.Make,
                Model = vehicle.Model,
                PlateNumber = vehicle.PlateNumber,
                Color = vehicle.Color,
                ParkingLocation = vehicle.ParkingLocation
            };

            for (int i = 0; i < vehicles.Count; i++)
            {
                VMGuestVehicle currentVehicle = vehicles[i];
                if (currentVehicle.Equals(oldVMVehicle))
                {
                    vehicles[i] = newVMVehicle;
                    ++result;
                }
            }

            return result;
        }
    }
}
