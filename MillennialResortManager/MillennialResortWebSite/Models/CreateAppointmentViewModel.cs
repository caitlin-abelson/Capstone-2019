using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MillennialResortWebSite.Models
{
    public class CreateAppointmentViewModel
    {
        [Display(Name = "What day would you like you appointment")]
        [Required(ErrorMessage ="You must select a day")]
        public DateTime StartDay { get; set; }

        [Display(Name = "What time would you like you appointment")]
        [Required(ErrorMessage = "You must select a time")]
        public int StartTime { get; set; }

        [Display(Name = "Anything we should know about your appointment")]
        public String Description { get; set; }
    }
}