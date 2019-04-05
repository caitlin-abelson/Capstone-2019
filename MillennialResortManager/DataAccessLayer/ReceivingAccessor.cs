using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Kevin Broskow
    /// 3/27/2019
    /// 
    /// The concrete implementation of IReceivingAccessor. Handles storage and collection of
    /// Receiving objects to and from the database.
    /// </summary>
    public class ReceivingAccessor : IReceivingAccessor
    {
        public void deactivateReceivingTicket(int id)
        {
            throw new NotImplementedException();
        }

        public void deleteReceivingTicket(int id)
        {
            throw new NotImplementedException();
        }

        public void insertReceivingTicket(ReceivingTicket ticket)
        {
            var cmdText = @"sp_insert_receiving";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierOrderID", ticket.SupplierOrderID);
            cmd.Parameters.AddWithValue("@Description", ticket.ReceivingTicketExceptions);
            cmd.Parameters.AddWithValue("@DateDelivered", ticket.ReceivingTicketCreationDate);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<ReceivingTicket> selectAllReceivingTickets()
        {
            throw new NotImplementedException();
        }

        public ReceivingTicket selectReceivingTicketByID(int id)
        {
            throw new NotImplementedException();
        }

        public void updateReceivingTicket(ReceivingTicket original, ReceivingTicket updated)
        {
            throw new NotImplementedException();
        }
    }
}
