using System;
using System.CodeDom;
using System.Security.Policy;

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

        // VEHICLE STATUS AS IN DATABASE
        public string InUse => "In Use";
        public string Available => "Available";
        public string Decommissioned => "Decommissioned";
    }
}