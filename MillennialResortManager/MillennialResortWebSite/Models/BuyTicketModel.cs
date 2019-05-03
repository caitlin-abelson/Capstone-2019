using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MillennialResortWebSite.Models
{
    public class BuyTicketModel
    {
        public int EventID { get; set; }
        [DisplayName("Event Title")]
        public string EventTitle { get; set; }
        [DisplayName("Event Price")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OfferingID { get; set; }

        public DateTime Date { get; set; }

    }
}