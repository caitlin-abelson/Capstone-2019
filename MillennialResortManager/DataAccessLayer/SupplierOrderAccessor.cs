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
            throw new NotImplementedException();
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
            {
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
        }

        public List<SupplierOrderLine> SelectSupplierOrderLinesBySupplierOrderID(int supplierOrderID)
        {
            throw new NotImplementedException();
        }
    }

}
