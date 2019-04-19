using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
   public class RoomReservationModel
    {
      
        /// <remarks>
        /// Craig Barkley
        /// Updated: 2019/04/18
        ///
        /// </remarks>
        
       
        public string RoomType { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }   
       
        public int NumberOfGuests { get; set; }
        public int NumberOfPets { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Notes { get; set; }
       
        public RoomReservationModel(Reservation reservation, Room room)
        {
            this.RoomType = room.RoomType;
            this.Description = room.Description;
            this.Price = room.Price;
            this.NumberOfGuests = reservation.NumberOfGuests;
            this.ArrivalDate = reservation.ArrivalDate;
            this.DepartureDate = reservation.DepartureDate;
            this.Notes = reservation.Notes; 
        }
        public RoomReservationModel()
        {

        }
    }
}
