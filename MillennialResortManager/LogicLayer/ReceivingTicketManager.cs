using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class ReceivingTicketManager : IReceivingManager
    {
        ReceivingAccessor _accessor = new ReceivingAccessor();
        public void createReceivingTicket(ReceivingTicket ticket)
        {
            if (ticket.IsValid())
            {
                try
                {
                    _accessor.insertReceivingTicket(ticket);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public void deactivateReceivingTicket(int id)
        {
            throw new NotImplementedException();
        }

        public void deleteReceivingTicket(int id)
        {
            throw new NotImplementedException();
        }

        public List<ReceivingTicket> retrieveAllReceivingTickets()
        {
            throw new NotImplementedException();
        }

        public ReceivingTicket retrieveReceivingTicketByID(int id)
        {
            throw new NotImplementedException();
        }

        public void updateReceivingTicket(ReceivingTicket original, ReceivingTicket updated)
        {
            throw new NotImplementedException();
        }
    }
}
