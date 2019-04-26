using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{


    /// <summary>
    /// Eduardo Colon
    /// Created: 2019/03/20
    /// 
    /// ShuttleReservation Data objects
    /// </summary>
    public class ShuttleReservation
    {
        public int ShuttleReservationID { get; set; }
        public int GuestID { get; set; }
        public int EmployeeID { get; set; }

        public Guest Guest { get; set; }
        public Employee Employee { get; set; }

        public string PickupLocation { get; set; }
        public string DropoffDestination { get; set; }
        public DateTime PickupDateTime { get; set; }
        public DateTime? DropoffDateTime { get; set; }

       public bool Active { get; set; }

     


    }
}
