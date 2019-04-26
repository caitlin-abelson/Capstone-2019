using System;

namespace DataObjects
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Vehicle Checkout Data Object
    /// </summary>
    [Serializable]
    public class ResortVehicleCheckout
    {
        public int VehicleCheckoutId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime DateCheckedOut { get; set; }

        public DateTime? DateReturned { get; set; }

        public DateTime DateExpectedBack { get; set; }

        public bool Returned { get; set; }

        public int ResortVehicleId { get; set; }

    }
}