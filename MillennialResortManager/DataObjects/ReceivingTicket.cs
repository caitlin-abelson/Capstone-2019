using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Kevin Broskow
    /// Created 3/25/2019
    /// 
    ///</summary>
    public class ReceivingTicket
    {
        public int ReceivingTicketID { get; set; }
        public string ReceivingTicketExceptions { get; set; }
        public DateTime ReceivingTicketCreationDate { get; set; }
        public int SupplierOrderID { get; set; }
        public bool Active { get; set; }

        public bool IsValid()
        {
            bool valid = false;
            if (validExceptions())
            {
                if (validDate())
                {
                    valid = true;
                }
                else
                {
                throw new ArgumentException("Date must be for today");
                }
            }
            else
            {
            throw new ArgumentException("Exceptions must less than 1001 characters long.");
            }
            return valid;
        }

        private bool validDate()
        {
            bool valid = false;
            if(ReceivingTicketCreationDate.Date == DateTime.Now.Date)
            {
                valid = true;
            }
            return valid;
        }

        private bool validExceptions()
        {
            bool valid = false;
            if(ReceivingTicketExceptions.Length < 1001)
            {
                valid = true;
            }
            return valid;
        }
    }
}
