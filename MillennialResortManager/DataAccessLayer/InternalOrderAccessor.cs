using DataObjects;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

/// <summary>
/// Richard Carroll
/// Created: 2019/01/30
/// 
/// This class is used for Database Interactions for 
/// Order Data
/// </summary>
namespace DataAccessLayer
{
    public class InternalOrderAccessor : IInternalOrderAccessor
    {
        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// This Method inserts Order Data into the database
        /// and returns the result to the 
        /// Logic Layer.
        /// </summary>
        public int InsertItemOrder(InternalOrder internalOrder, List<VMInternalOrderLine> lines)
        {
            int rowsAffected = 0;

            var cmdText1 = "sp_insert_internal_order";
            var cmdText2 = "sp_insert_internal_order_line";

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using( var conn = DBConnection.GetDbConnection())
                    {
                        conn.Open();

                        var cmd1 = new SqlCommand(cmdText1, conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@EmployeeID", internalOrder.EmployeeID);
                        cmd1.Parameters.AddWithValue("@DepartmentID", internalOrder.DepartmentID);
                        cmd1.Parameters.AddWithValue("@Description", internalOrder.Description);
                        cmd1.Parameters.AddWithValue("@OrderComplete", internalOrder.OrderComplete);
                        cmd1.Parameters.AddWithValue("@DateOrdered", internalOrder.DateOrdered);
                        var temp  = cmd1.ExecuteScalar();
                        int internalOrderID = Convert.ToInt32(temp);


                        foreach (var line in lines)
                        {
                            var cmd2 = new SqlCommand(cmdText2, conn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@ItemID", line.ItemID);
                            cmd2.Parameters.AddWithValue("@InternalOrderID", internalOrderID);
                            cmd2.Parameters.AddWithValue("@OrderQty", line.OrderQty);
                            cmd2.Parameters.AddWithValue("@QtyReceived", line.QtyReceived);
                            rowsAffected += cmd2.ExecuteNonQuery();

                        }

                    }
                    scope.Complete();
                }
                


            }
            catch (Exception)
            {
                
                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// This Method Requests Order data from the database 
        /// and returns it to the Logic Layer if Possible.
        /// </summary>
        public List<VMInternalOrder> SelectAllInternalOrders()
        {
            List<VMInternalOrder> orders = new List<VMInternalOrder>();

            var cmdText = "sp_select_all_internal_orders";
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VMInternalOrder order = new VMInternalOrder();
                        order.InternalOrderID = reader.GetInt32(0);
                        order.EmployeeID = reader.GetInt32(1);
                        order.FirstName = reader.GetString(2);
                        order.LastName = reader.GetString(3);
                        order.DepartmentID = reader.GetString(4);
                        order.Description = reader.GetString(5);
                        order.OrderComplete = reader.GetBoolean(6);
                        order.DateOrdered = reader.GetDateTime(7);
                        orders.Add(order);

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return orders;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// This Method Requests Line data from the database by
        /// it's OrderID and returns it to the Logic Layer if Possible.
        /// </summary>
        public List<VMInternalOrderLine> SelectOrderLinesByID(int orderID)
        {
            List<VMInternalOrderLine> lines = new List<VMInternalOrderLine>();

            var cmdText = "sp_select_order_lines_by_id";
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InternalOrderID", orderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var line = new VMInternalOrderLine();
                        line.ItemID = reader.GetInt32(0);
                        line.ItemName = reader.GetString(1);
                        line.OrderQty = reader.GetInt32(2);
                        line.QtyReceived = reader.GetInt32(3);
                        lines.Add(line);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return lines;
        }

        /// <summary>
        /// Richard Carroll
        /// Created: 2019/01/30
        /// 
        /// This Method alters the Order status of an Order in 
        /// the database and returns the result to the 
        /// Logic Layer.
        /// </summary>
        public int UpdateOrderStatusToComplete(int orderID, bool orderComplete)
        {
            int rowsAffected = 0;

            var cmdText = "sp_update_order_status_to_complete";
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InternalOrderID", orderID);
            cmd.Parameters.AddWithValue("@OrderComplete", orderComplete);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

    }
}
