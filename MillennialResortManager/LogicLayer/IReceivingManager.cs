using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IReceivingManager
    {
        void createReceivingTicket(ReceivingTicket ticket);
        List<ReceivingTicket> retrieveAllReceivingTickets();
        ReceivingTicket retrieveReceivingTicketByID(int id);
        void updateReceivingTicket(ReceivingTicket original, ReceivingTicket updated);
        void deactivateReceivingTicket(int id);
        void deleteReceivingTicket(int id);
    }
}
