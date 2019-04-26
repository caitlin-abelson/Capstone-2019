using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MillennialResortWebSite.Models
{
    public class AppointmentModel
    {

        [DisplayName("Service")]
        public String AppointmentType { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [DisplayName("Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public String Description { get; set; }
    }
}