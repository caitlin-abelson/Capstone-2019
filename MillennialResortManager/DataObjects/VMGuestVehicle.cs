using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Richard Carroll
    /// Created Date: 3/7/19
    /// 
    /// This Class holds the data necessary for displaying
    /// GuestVehicle Data on the Presentation Layer.
    /// </summary>
    public class VMGuestVehicle
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GuestID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public string Color { get; set; }
        public string ParkingLocation { get; set; }
    }
}
