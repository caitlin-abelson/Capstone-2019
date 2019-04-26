/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Used as a view model for adding a guest for appointments
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class AppointmentGuestViewModel
    {
        public int GuestID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
