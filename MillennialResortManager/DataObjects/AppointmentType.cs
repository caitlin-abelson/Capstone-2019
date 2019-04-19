using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class AppointmentType
    {
        [DisplayName("Service")]
        public string AppointmentTypeID { get; set; }
        public string Description { get; set; }
    }
}
