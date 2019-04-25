using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MillennialResortWebSite.Models
{
    public class NewReservation
    {
        
        [Required]
        [Display(Name = "Start Date")]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [Display(Name = "End Date")]       
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Display(Name = "Number of Guests")]
        public int numberOfGuests { get; set; }

        [Required]
        [Display(Name = "Number of Pets")]
        public int numberOfPets { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public string roomType { get; set; }

        public string Notes { get; set; }
    }
}