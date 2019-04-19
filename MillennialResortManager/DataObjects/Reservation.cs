using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// The Reservation Object is designed to directly carry information about a Reservation based on the information about Reservations in our Data Dictionary
    /// </summary>
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int MemberID { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfPets { get; set; }
        public DateTime ArrivalDate { get ; set; }
        public DateTime DepartureDate { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }
        
        public bool ValidateDepartureDate()
        {
            if (DepartureDate == null)
            {
                return false;
            }
            //Departure Date must be after today ?????
            if (ArrivalDate.Date < DepartureDate.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateArrivalDate()
        {
            if(ArrivalDate == null)
            {
                return false;
            }

            //Business rule. Jsut saying Arrival date cannot be before the Resort was created 
            if (ArrivalDate.Year < 1900)
            {
                return false;
            }
            return true;
        }

        public bool ValidateNumberOfGuests()
        {
            //Chose a range of 1-100 Guests. Can be changed as needed
            if (NumberOfGuests >= 1 && NumberOfGuests <= 100)
            {
                return true;
            }
            return false;
        }

        public bool ValidateNumberOfPets()
        {
            if (NumberOfPets >=0 && NumberOfPets <=100)
            {
                return true;
            }
            return false;
        }

        public bool ValidateNotes()
        {
            if ((Notes != null && Notes.Length < 1001) || Notes == null)
            {
                return true;
            }
            return false;
        }

        public bool IsValid()
        {
            if (ValidateArrivalDate() && ValidateDepartureDate() && ValidateNumberOfGuests() && ValidateNumberOfPets() && ValidateNotes())
            {
                return true;
            }
            return false;
        }
    }
}
