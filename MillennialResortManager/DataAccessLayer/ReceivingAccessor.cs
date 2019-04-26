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
        List<ReceivingTicket> _tickets = new List<ReceivingTicket>();
        ReceivingTicket _ticket = new ReceivingTicket();



        /// <summary>
        ///  Author Kevin Broskow
        ///  Created 4/5/201
        /// <param name="id"></param>
        /// 
        /// Deactivates a receiving ticket
        /// </summary>
        public void deactivateReceivingTicket(int id)
        {

            var cmdText = @"sp_deactivate_receiving";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@ReceivingID", id);
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

        /// <summary>
        ///  Author Kevin Broskow
        ///  Created 4/5/201
        /// <param name="id"></param>
        /// 
        /// Deactivates a receiving ticket
        /// </summary>
        public void deleteReceivingTicket(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Author Kevin Broskow
        ///  Created 4/5/201
        /// <param name="ticket"></param>
        /// 
        /// Insert a new receiving ticket
        /// </summary>
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

        /// <summary>
        ///  Author Kevin Broskow
        ///  Created 4/5/201
        /// <returns>List<ReceivingTicket></returns>
        /// 
        /// Returns a list of all receiving tickets
        /// </summary>
        public List<ReceivingTicket> selectAllReceivingTickets()
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_receiving";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _tickets.Add(new ReceivingTicket()
                        {
                            ReceivingTicketID = reader.GetInt32(0),
                            SupplierOrderID = reader.GetInt32(1),
                            ReceivingTicketExceptions = reader.GetString(2),
                            ReceivingTicketCreationDate = reader.GetDateTime(3),
                            Active = reader.GetBoolean(4)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return _tickets;
        }
        /// <summary>
        ///  Author Kevin Broskow
        ///  Created 4/5/201
        /// <returns>ReceivingTicket</returns>
        /// <paramref name="id"/>
        /// Returns a specific receiving ticket.
        /// </summary>
        public ReceivingTicket selectReceivingTicketByID(int id)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_receiving";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _ticket.ReceivingTicketID = reader.GetInt32(0);
                        _ticket.SupplierOrderID = reader.GetInt32(1);
                        _ticket.ReceivingTicketExceptions = reader.GetString(2);
                        _ticket.ReceivingTicketCreationDate = reader.GetDateTime(3);
                        _ticket.Active = reader.GetBoolean(4);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ticket;
        }
        /// <summary>
        ///  Author Kevin Broskow
        ///  Created 4/5/201
        ///  <paramref name="original"/>
        ///  <paramref name="updated"/>
        /// 
        /// Updates a specific receiving ticket.
        /// </summary>
        public void updateReceivingTicket(ReceivingTicket original, ReceivingTicket updated)
        {
            var cmdText = @"sp_update_receiving";
            var conn = DBConnection.GetDbConnection();

            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@ReceivingID", original.ReceivingTicketID);
            cmd.Parameters.AddWithValue("@Description", updated.ReceivingTicketExceptions);
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
    }
}
