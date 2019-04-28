using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DataObjects;

namespace MillennialResortWebSite.Models
{
    /// <summary>
    /// Added by Matt H. on 4/18/19
    /// View Model class for Contact Us page.
    /// </summary>
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be at least 2 characters in length")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be at least 2 characters in length")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Message to Send")]
        [MinLength(20, ErrorMessage = "Your Message must be at least 20 characters in length.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(255, MinimumLength = 15, ErrorMessage = "The Email entered must be at least 15 characters in length")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must select a Subject Header from the drop-down list")]
        [Display(Name = "Subject Header")]
        public string SelectedSubject { get; set; }

        public IEnumerable<SelectListItem> Subjects { get; set; }
    }

    /// <summary>
    /// Added by Matt H. on 4/18/19
    /// View Model class for index page's mailing list form.
    /// </summary>
    public class IndexPageMailingListViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be 2 or more characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be 2 or more characters")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(255, MinimumLength = 15, ErrorMessage = "Email must be at least 15 characters in length")]
        public string Email { get; set; }

    }

    /// <summary>
    /// Added by Matt H. on 4/18/19
    /// View Model class comprised of both Reservation View Model and MailingList View Model
    /// for the index page.
    /// </summary>
    public class HomeViewModelsMixer
    {
        // public List<ReservationSearchModel> Reservations { get; set; }

        // public IEnumerable<Room> Rooms { get; set; }



        public List<NewReservation> Reservation { get; set; }

        /*public List<IndexPageMailingListViewModel> MailingLists { get; set; } */

        /*public ReservationSearchModel Reservation { get; set; } */
        public IndexPageMailingListViewModel MailingList { get; set; } 


       

    }
}