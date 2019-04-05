using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class ReceivingAccessorMock : IReceivingAccessor
    {
        List<ReceivingTicket> _tickets = new List<ReceivingTicket>();
        public ReceivingAccessorMock()
        {
            _tickets.Add(new ReceivingTicket(){
                ReceivingTicketID = 1000, ReceivingTicketCreationDate = DateTime.Now, SupplierOrderID = 2000, ReceivingTicketExceptions = "", Active = true
            });
            _tickets.Add(new ReceivingTicket()
            {
                ReceivingTicketID = 1001,
                ReceivingTicketCreationDate = DateTime.Now,
                SupplierOrderID = 2001,
                ReceivingTicketExceptions = "",
                Active = true
            });
            _tickets.Add(new ReceivingTicket()
            {
                ReceivingTicketID = 1002,
                ReceivingTicketCreationDate = DateTime.Now,
                SupplierOrderID = 2002,
                ReceivingTicketExceptions = "",
                Active = true
            });
            _tickets.Add(new ReceivingTicket()
            {
                ReceivingTicketID = 1003,
                ReceivingTicketCreationDate = DateTime.Now,
                SupplierOrderID = 2003,
                ReceivingTicketExceptions = "",
                Active = true
            });
        }
        public void deactivateReceivingTicket(int id)
        {
            _tickets.Find(x => x.ReceivingTicketID == id).Active = false;
        }

        public void deleteReceivingTicket(int id)
        {
            _tickets.Remove(_tickets.Find(x => x.ReceivingTicketID == id));
        }

        public void insertReceivingTicket(ReceivingTicket ticket)
        {
            if (ticket.IsValid())
            {
                _tickets.Add(ticket);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public List<ReceivingTicket> selectAllReceivingTickets()
        {
            return _tickets;
        }

        public ReceivingTicket selectReceivingTicketByID(int id)
        {
            ReceivingTicket _ticket = new ReceivingTicket();
            foreach (var ticket in _tickets)
            {
                if(ticket.ReceivingTicketID == id)
                {
                    _ticket = ticket;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            return _ticket;
        }

        public void updateReceivingTicket(ReceivingTicket original, ReceivingTicket updated)
        {
            if (updated.IsValid())
            {
                foreach (var ticket in _tickets)
                {
                    if(ticket.ReceivingTicketID == original.ReceivingTicketID)
                    {
                        ticket.ReceivingTicketCreationDate = updated.ReceivingTicketCreationDate;
                        ticket.ReceivingTicketExceptions = updated.ReceivingTicketExceptions;
                        ticket.Active = updated.Active;
                        ticket.SupplierOrderID = updated.SupplierOrderID;
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
