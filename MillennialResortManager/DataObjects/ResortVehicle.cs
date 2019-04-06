using System;

namespace DataObjects
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort ResortVehicle Object
    /// </summary>
    [Serializable]
    public class ResortVehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string License { get; set; }
        public int Mileage { get; set; }
        public int Capacity { get; set; }
        public string Color { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public bool Available { get; set; }
        public string ResortVehicleStatusId { get; set; }
        public int ResortPropertyId { get; set; }

        #region For forms

        public ResortVehicle()
        {
            Mileage = -1;  // to signal form to not display 0
            Capacity = -1; // to signal form to not display 0
            Year = -1;     // to signal form to not display 0
            ResortPropertyStr = "";
        }

        public string PurchaseDateStr => PurchaseDate?.ToShortDateString(); // Purchase date will never be null
        public string ActiveStr => Active ? "Active" : "Inactive";
        public string AvailableStr => Available ? "Available" : "Not Available";
        public string ResortPropertyStr { get; set; }
        #endregion
    }
}