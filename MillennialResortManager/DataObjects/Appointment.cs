/// <summary>
/// Wes Richardson
/// Created: 2019/03/07
/// 
/// Data object for Appointment Data
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public String AppointmentType { get; set; }
        public int GuestID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Description { get; set; }
    }
}
