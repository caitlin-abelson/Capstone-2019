using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
    public class ReceivingTicketManager : IReceivingManager
    {
        public ReceivingTicketManager()
        {
            _accessor = new ReceivingAccessor();
        }

		/// <summary author="Kevin Broskow" created="2019/04/03">
		/// Constructor which allows us to implement which ever Receiving Accessor we need to use
		/// </summary>
		public ReceivingTicketManager(IReservationAccessor reservationAccessor)
        {
            _accessor = new ReceivingAccessorMock();
        }
        IReceivingAccessor _accessor = new ReceivingAccessor();

		/// <summary author="Kevin Broskow" created="2019/04/03">
		/// Allows for the creation of the Receiving Ticket.
		/// </summary>
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
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
        }

		/// <summary author="Kevin Broskow" created="2019/04/03">
		/// Allows for the deactivation of Receiving Tickets
		/// </summary>
		public void deactivateReceivingTicket(int id)
        {
            try
            {
                _accessor.deactivateReceivingTicket(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Kevin Broskow" created="2019/04/03">
		/// Allows for the purging of Receiving tickets
		/// </summary>
		public void deleteReceivingTicket(int id)
        {
            try
            {
                _accessor.deleteReceivingTicket(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Kevin Broskow" created="2019/04/03">
		/// Retrieves all of the receiving tickets that currently exist.
		/// </summary>
		public List<ReceivingTicket> retrieveAllReceivingTickets()
        {
            List<ReceivingTicket> _tickets = new List<ReceivingTicket>();
            try
            {
                _tickets = _accessor.selectAllReceivingTickets();
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return _tickets;
        }

		/// <summary author="Kevin Broskow" created="2019/04/03">
		/// Returns a specific Receiving Ticket
		/// </summary>
		public ReceivingTicket retrieveReceivingTicketByID(int id)
        {
            ReceivingTicket ticket = new ReceivingTicket();
            try
            {
                ticket = _accessor.selectReceivingTicketByID(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return ticket;
        }

		/// <summary author="Kevin Broskow" created="2019/04/03">
		/// Allows for the updating of a Receiving Ticket
		/// </summary>
		public void updateReceivingTicket(ReceivingTicket original, ReceivingTicket updated)
        {
            try
            {
                _accessor.updateReceivingTicket(original, updated);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}
    }
}