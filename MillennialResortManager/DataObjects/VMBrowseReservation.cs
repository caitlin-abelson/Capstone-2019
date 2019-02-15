using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/31/2019
    /// This is a View Model object for BrowseReservation. It is just a combination of Member and Reservation
    /// </summary>
    public class VMBrowseReservation
    {
        public int ReservationID { get; set; }
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfPets { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }
    }
}
