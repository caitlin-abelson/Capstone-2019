using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillennialResortWebSite.Models
{

    public class ReservationSearchModel
    {

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? ArrivalDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? DepartureDate { get; set; }

        [Required]
        [Display(Name = "Number of Guests")]
        public int NumberOfGuests { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public string RoomType { get; set; }



        public IEnumerable<DataObjects.Room> Rooms { get; set; }

        public IEnumerable<SelectListItem> RoomTypes { get; set; }
    }
}

