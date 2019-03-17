using System;

namespace DataObjects
{
    /// <summary>
    ///     Francis Mingomba
    ///     Created: 2019/2/23
    ///     Data Object for Shuttel Véhicule
    /// </summary>
    [Serializable]
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int YearOfManufacture { get; set; }
        public string License { get; set; }
        public int Mileage { get; set; }
        public string Vin { get; set; }
        public int Capacity { get; set; }
        public string Color { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime? DeactivationDate { get; set; }

        #region For forms

        public Vehicle()
        {
            Mileage = -1;  // to signal form to not display 0
            Capacity = -1; // to signal form to not display 0
            YearOfManufacture = -1; // to signal form to not display 0
        }

        public string PurchaseDateStr => PurchaseDate?.ToShortDateString(); // Purchase date will never be null
        public string ActiveStr => Active ? "Active" : "Inactive";

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}" +
                    $", {nameof(Make)}: {Make}" +
                    $", {nameof(Model)}: {Model}" +
                    $", {nameof(YearOfManufacture)}: {YearOfManufacture}" +
                    $", {nameof(License)}: {License}" +
                    $", {nameof(Mileage)}: {Mileage}" +
                    $", {nameof(Vin)}: {Vin}" +
                    $", {nameof(Capacity)}: {Capacity}" +
                    $", {nameof(Color)}: {Color}" +
                    $", {nameof(PurchaseDate)}: {PurchaseDate}" +
                    $", {nameof(Description)}: {Description}" +
                    $", {nameof(Active)}: {Active}" +
                    $", {nameof(DeactivationDate)}: {DeactivationDate}";
        }

        #endregion
    }
}