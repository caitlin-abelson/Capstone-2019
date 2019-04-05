using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Transactions;

/// <summary>
/// Eric Bostwick
/// Created: 2/26/2019
/// 
/// Database methods for managing supplierorder and supplierorderline table
/// </summary>

namespace DataAccessLayer
{
    public class SupplierOrderAccessor : ISupplierOrderAccessor
    {

        public int InsertSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines)
        {
            int rowsAffected = 0;

            var cmdText1 = "sp_insert_supplier_order";
            var cmdText2 = "sp_insert_supplier_order_line";

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var conn = DBConnection.GetDbConnection())
                    {
                        conn.Open();

                        var cmd1 = new SqlCommand(cmdText1, conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@SupplierOrderID", SqlDbType.Int);
                        cmd1.Parameters["@SupplierOrderID"].Direction = ParameterDirection.Output;
                        cmd1.Parameters.AddWithValue("@SupplierID", supplierOrder.SupplierID);
                        cmd1.Parameters.AddWithValue("@EmployeeID", supplierOrder.EmployeeID);
                        cmd1.Parameters.AddWithValue("@Description", supplierOrder.Description);
                        cmd1.ExecuteNonQuery();
                        int supplierOrderID = (int)cmd1.Parameters["@SupplierOrderID"].Value;


                        foreach (var line in supplierOrderLines)
                        {
                            var cmd2 = new SqlCommand(cmdText2, conn);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            line.SupplierOrderID = supplierOrderID;
                            cmd2.Parameters.AddWithValue("@SupplierOrderID", line.SupplierOrderID);
                            cmd2.Parameters.AddWithValue("@ItemID", line.ItemID);
                            cmd2.Parameters.AddWithValue("@Description", line.Description);
                            cmd2.Parameters.AddWithValue("@OrderQty", line.OrderQty);
                            //cmd2.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                            //cmd2.Parameters["@UnitPrice"].Value = line.UnitPrice;
                            //line.UnitPrice = decimal.Round(line.UnitPrice,2);
                            cmd2.Parameters.AddWithValue("@UnitPrice", line.UnitPrice);
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

        public List<SupplierOrder> SelectAllSupplierOrders()
        {
            /// <summary>
            /// Eric Bostwick
            /// Created 3/7/2019
            /// Gets list of All SupplierOrders from SupplierOrder table
            /// </summary>
            /// <returns>
            /// List of SupplierOrder Objects
            /// </returns>            

            List<SupplierOrder> supplierOrders = new List<SupplierOrder>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_supplier_orders";  //sp_retrieve_itemsuppliers_by_itemid
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
                        SupplierOrder supplierOrder = new SupplierOrder();

                        supplierOrder.SupplierOrderID = reader.GetInt32(reader.GetOrdinal("SupplierOrderID"));
                        supplierOrder.DateOrdered = reader.GetDateTime(reader.GetOrdinal("DateOrdered"));
                        supplierOrder.Description = reader["Description"].ToString();
                        supplierOrder.EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                        supplierOrder.FirstName = reader["FirstName"].ToString();
                        supplierOrder.LastName = reader["LastName"].ToString();
                        supplierOrder.OrderComplete = reader.GetBoolean(reader.GetOrdinal("OrderComplete"));
                        supplierOrder.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));
                        supplierOrder.SupplierName = reader["SupplierName"].ToString();
                        supplierOrders.Add(supplierOrder);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }


            return supplierOrders;
        }

        public List<VMItemSupplierItem> SelectItemSuppliersBySupplierID(int supplierID)
        {

            /// <summary>
            /// Eric Bostwick
            /// Created 2/26/2019
            /// Gets list of itemsuppliers from itemsupplier table
            /// using the supplierID
            /// </summary>
            /// <returns>
            /// List of ItemSupplier Objects
            /// </returns> 

            List<VMItemSupplierItem> itemSuppliers = new List<VMItemSupplierItem>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_itemsuppliers_by_supplierid";  //sp_retrieve_itemsuppliers_by_itemid
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierID", supplierID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VMItemSupplierItem itemSupplier = new VMItemSupplierItem();

                        itemSupplier.ItemID = reader.GetInt32(reader.GetOrdinal("ItemID"));
                        itemSupplier.SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID"));
                        itemSupplier.PrimarySupplier = reader.GetBoolean(reader.GetOrdinal("PrimarySupplier"));
                        itemSupplier.LeadTimeDays = reader.GetInt32(reader.GetOrdinal("LeadTimeDays"));
                        itemSupplier.UnitPrice = (decimal)reader.GetSqlMoney(reader.GetOrdinal("UnitPrice"));
                        itemSupplier.Name = reader["Name"].ToString();
                        itemSupplier.Description = reader["Description"].ToString();
                        itemSupplier.ItemSupplierActive = reader.GetBoolean(reader.GetOrdinal("Active"));
                        itemSupplier.ItemType = reader["ItemTypeID"].ToString();
                        itemSupplier.OnHandQty = reader.GetInt32(reader.GetOrdinal("OnHandQuantity"));
                        itemSupplier.ReorderQty = reader.GetInt32(reader.GetOrdinal("ReOrderQuantity"));
                        itemSuppliers.Add(itemSupplier);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return itemSuppliers;
        }


        public List<SupplierOrderLine> SelectSupplierOrderLinesBySupplierOrderID(int supplierOrderID)
        {
            /// <summary>
            /// Eric Bostwick
            /// Created 3/7/2019
            /// Gets list of All SupplierOrderLines from SupplierOrderLine table
            /// </summary>
            /// <returns>
            /// List of SupplierOrderLine Objects
            /// </returns>  

            List<SupplierOrderLine> supplierOrderLines = new List<SupplierOrderLine>();
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_supplier_order_lines";  //sp_retrieve_itemsuppliers_by_itemid
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SupplierOrderID", supplierOrderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierOrderLine supplierOrderLine = new SupplierOrderLine();

                        supplierOrderLine.ItemID = reader.GetInt32(reader.GetOrdinal("ItemID"));
                        supplierOrderLine.Description = reader["Description"].ToString();
                        supplierOrderLine.OrderQty = reader.GetInt32(reader.GetOrdinal("OrderQty"));
                        supplierOrderLine.QtyReceived = reader.GetInt32(reader.GetOrdinal("QtyReceived"));
                        supplierOrderLine.SupplierOrderID = reader.GetInt32(reader.GetOrdinal("SupplierOrderID"));
                        supplierOrderLine.UnitPrice = (decimal)reader.GetSqlMoney(reader.GetOrdinal("UnitPrice"));

                        supplierOrderLines.Add(supplierOrderLine);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return supplierOrderLines;
        }

        public int UpdateSupplierOrder(SupplierOrder supplierOrder, List<SupplierOrderLine> supplierOrderLines)
        {
            int rowsAffected = 0;

            var cmdText1 = "sp_delete_supplier_order_lines";
            var cmdText2 = "sp_update_supplier_order";
            var cmdText3 = "sp_insert_supplier_order_line";
            var cmdText4 = "sp_update_supplier_order_line";

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var conn = DBConnection.GetDbConnection())
                    {
                        conn.Open();

                        var cmd1 = new SqlCommand(cmdText1, conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@SupplierOrderID", supplierOrder.SupplierOrderID);
                        cmd1.ExecuteNonQuery();

                        var cmd2 = new SqlCommand(cmdText2, conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@SupplierOrderID", supplierOrder.SupplierOrderID);
                        cmd2.Parameters.AddWithValue("@Description", supplierOrder.Description);
                        cmd2.ExecuteNonQuery();


                        foreach (var line in supplierOrderLines)
                        {
                            var cmd3 = new SqlCommand(cmdText3, conn);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            var cmd4 = new SqlCommand(cmdText4, conn);
                            cmd4.CommandType = CommandType.StoredProcedure;
                            line.SupplierOrderID = supplierOrder.SupplierOrderID;
                            cmd3.Parameters.AddWithValue("@SupplierOrderID", line.SupplierOrderID);
                            cmd3.Parameters.AddWithValue("@ItemID", line.ItemID);
                            cmd3.Parameters.AddWithValue("@Description", line.Description);
                            cmd3.Parameters.AddWithValue("@OrderQty", line.OrderQty);
                            cmd3.Parameters.AddWithValue("@UnitPrice", line.UnitPrice);
                            rowsAffected += cmd3.ExecuteNonQuery();

                            cmd4.Parameters.AddWithValue("@SupplierOrderID", line.SupplierOrderID);
                            cmd4.Parameters.AddWithValue("@ItemID", line.ItemID);
                            cmd4.Parameters.AddWithValue("@QtyReceived", line.QtyReceived);
                            cmd4.ExecuteNonQuery();
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


        public int DeleteSupplierOrder(int supplierOrderID)
        {
            int rowsAffected = 0;

            var cmdText1 = "sp_delete_supplier_order_lines";
            var cmdText2 = "sp_delete_supplier_order";

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var conn = DBConnection.GetDbConnection())
                    {
                        conn.Open();

                        var cmd1 = new SqlCommand(cmdText1, conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@SupplierOrderID", supplierOrderID);
                        cmd1.ExecuteNonQuery();

                        var cmd2 = new SqlCommand(cmdText2, conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@SupplierOrderID", supplierOrderID);
                        rowsAffected += cmd2.ExecuteNonQuery();
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
        /// Kevin Broskow
        /// Created 4/2/209
        /// Gets a specific supplierOrder by id
        /// </summary>
        /// <returns>
        /// SupplierOrder object
        /// </returns>  
        public SupplierOrder RetrieveSupplierOrderByID(int supplierOrderID)
        {
            SupplierOrder order = null;

            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_supplier_order_by_id";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@SupplierOrderID", supplierOrderID);
            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    order.EmployeeID = reader.GetInt32(0);
                    order.Description = reader.GetString(1);
                    order.OrderComplete = reader.GetBoolean(2);
                    order.DateOrdered = reader.GetDateTime(3);
                    order.SupplierID = reader.GetInt32(4);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return order;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created 4/2/209
        /// Completes a specific SupplierOrder
        /// </summary>
        /// <returns>
        /// void
        /// </returns> 
        public void CompleteSupplierOrder(int supplierOrderID)
        {
            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_complete_order_by_id";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@SupplierOrderID", supplierOrderID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
