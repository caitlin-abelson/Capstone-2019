using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public interface IReceivingAccessor
    {
        void insertReceivingTicket(ReceivingTicket ticket);
        List<ReceivingTicket> selectAllReceivingTickets();
        ReceivingTicket selectReceivingTicketByID(int id);
        void updateReceivingTicket(ReceivingTicket original, ReceivingTicket updated);
        void deactivateReceivingTicket(int id);
        void deleteReceivingTicket(int id);
    }
}
