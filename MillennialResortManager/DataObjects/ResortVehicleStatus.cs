using System;

namespace DataObjects
{
    /// <summary>
    /// Francis Mingomba
    /// Created: 2019/04/03
    ///
    /// Resort Vehicle Status Data Object
    /// </summary>
    public class ResortVehicleStatus
    {
        public string Id { get; set; }

        public string Description { get; set; }
    }

    public enum ResortVehicleStatusEnum
    {
        InUse,
        Available,
        Decommissioned
    }
}