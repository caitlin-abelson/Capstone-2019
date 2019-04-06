using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Vehicle Checkout Accessor Interface
    /// </summary>
    public interface IResortVehicleCheckoutAccessor
    {
        int AddVehicleCheckout(ResortVehicleCheckout resortVehicleCheckout);

        ResortVehicleCheckout RetrieveVehicleCheckoutById(int resortVehicleCheckoutId);

        IEnumerable<ResortVehicleCheckout> RetrieveVehicleCheckouts();

        void UpdateVehicleCheckouts(ResortVehicleCheckout old, ResortVehicleCheckout newResortVehicleCheckOut);

        void DeleteVehicleCheckout(int vehicleCheckoutId);
    }
}