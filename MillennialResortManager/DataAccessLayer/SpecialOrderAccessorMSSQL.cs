using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataObjects;
using System.Windows;

namespace DataAccessLayer
{
    public class SpecialOrderAccessorMSSQL : ISpecialOrderAccessor
    {
        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Creates the new Special order, with the data provided by user.
        /// </summary
        public int InsertSpecialOrder(CompleteSpecialOrder newSpecialOrder, SpecialOrderLine newSpecialOrderline)
        {
            int row = 0;
           
            try
            {   
                        var conn = DBConnection.GetDbConnection();
                        conn.Open();
                        var cmd1 = new SqlCommand("sp_create_specialOrder", conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@EmployeeID", newSpecialOrder.EmployeeID);
                        cmd1.Parameters.AddWithValue("@Description", newSpecialOrder.Description);
                        cmd1.Parameters.AddWithValue("@OrderComplete", newSpecialOrder.OrderComplete);
                        cmd1.Parameters.AddWithValue("@DateOrdered", newSpecialOrder.DateOrdered);
                        cmd1.Parameters.AddWithValue("@SupplierID", newSpecialOrder.SupplierID);
                var order = cmd1.ExecuteScalar();
                conn.Close();

                conn.Open();
                        
                        int SpecialOrderID = Convert.ToInt32(order);

                        var cmd2 = new SqlCommand("sp_create_specialOrderLine", conn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@ItemID", newSpecialOrderline.ItemID);
                        cmd2.Parameters.AddWithValue("@SpecialOrderID", SpecialOrderID);
                        cmd2.Parameters.AddWithValue("@Description", newSpecialOrder.Description);
                        cmd2.Parameters.AddWithValue("@OrderQty", newSpecialOrderline.OrderQty);
                        cmd2.Parameters.AddWithValue("@QtyReceived", newSpecialOrderline.QtyReceived);
                        row += cmd2.ExecuteNonQuery();


                conn.Close();
                  
            }
            catch (Exception)
            {

                throw;
            }

            return row;
        }


        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Retrieves the list of Special orders
        /// </summary
        public List<CompleteSpecialOrder> RetrieveSpecialOrder()
        {
            List<CompleteSpecialOrder> list = new List<CompleteSpecialOrder>();
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_all_special_order", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqldata = new SqlDataAdapter(cmd);
            DataTable datatable = new DataTable();
            sqldata.Fill(datatable);

            
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         list.Add(new CompleteSpecialOrder()

                         {
                             SpecialOrderID = reader.GetInt32(0),
                             EmployeeID = reader.GetInt32(1),
                             Description = reader.GetString(2),
                             OrderComplete = reader.GetBoolean(3),
                             DateOrdered = reader.GetDateTime(4),
                             SupplierID = reader.GetInt32(5) 
                         });

                    }
                }
            }catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return list;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Retrieves the ItemId needed for every form.
        /// 
        /// </summary
        public List<int> retrieveitemID()
        {
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_List_of_Item_ID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<int> itemid = new List<int>();

            try
            {
              conn.Open();
              var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {                while (reader.Read())
                                    {
                                      itemid.Add(reader.GetInt32(0));
                                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return itemid;
        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Class that manages data from Presentation Layer to Data Access layer,
        /// and Data Access Layer to Presentation Layer.
        /// </summary
        public List<SpecialOrderLine> retrieveSpecialOrderLinebySpecialID(int orderID)
        {

            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_List_of_SpecialOrderLine_by_SpecialOrderID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<SpecialOrderLine> order = new List<SpecialOrderLine>();
            cmd.Parameters.AddWithValue("@SpecialOrderID", orderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var items = new SpecialOrderLine();
                        items.ItemID = reader.GetInt32(0);
                        items.Description = reader.GetString(1);
                        items.OrderQty = reader.GetInt32(2);
                        items.QtyReceived = reader.GetInt32(3);
                        order.Add(items);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return order;
        }




        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Retrieves List of employeesId to fill a combo box for add/edit order.
        /// </summary
        public List<int> listOfEmployeesID()
        {
            List<int> employee = new List<int>();
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_List_of_EmployeeID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //int ID =0;

            try
            {
                // open the connection
                conn.Open();

                var read = cmd.ExecuteReader();

                if (read.HasRows)
                {
                    while (read.Read())
                    {

                        //  ID = int.Parse(read["Employee ID"].ToString());
                        employee.Add(read.GetInt32(0));
                    };
                }
                read.Close();
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message, "Unable to retrieve Employees ID");
            }
            finally
            {
                // close connection to DB
                conn.Close();
            }

            return employee;



        }

        /// <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// With the input user provided for updating, the order will be updated.
        /// </summary
        public int UpdateOrder(CompleteSpecialOrder Order, CompleteSpecialOrder Ordernew)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_update_SpecialOrder";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", Order.SpecialOrderID);

            cmd.Parameters.AddWithValue("@EmployeeID", Ordernew.EmployeeID);
            cmd.Parameters.AddWithValue("@Description", Ordernew.Description);
            cmd.Parameters.AddWithValue("@OrderComplete", Ordernew.OrderComplete);
            cmd.Parameters.AddWithValue("@SupplierID", Ordernew.SupplierID);

            cmd.Parameters.AddWithValue("@OldEmployeeID", Order.EmployeeID);
            cmd.Parameters.AddWithValue("@OldDescription", Order.Description);
            cmd.Parameters.AddWithValue("@OldOrderComplete", Order.OrderComplete);
            cmd.Parameters.AddWithValue("@OldSupplierID", Order.SupplierID);


            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Eduardo Colon
        /// Created: 2019/01/27
        /// 
        /// Method to run the procedures to diactivate the order supplies.
        /// </summary>

        public int DeactivateSpecialOrder(int specialOrderID)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_deactivate_SpecialOrder";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", specialOrderID);


            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        /// <summary>
        /// Kevin Broskow
        /// Created: 04/11/2019
        /// 
        /// Update a line in a special order.
        /// </summary>
        public void UpdateSpecialOrderLine(List<SpecialOrderLine> specialOrderLines)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_update_special_order_lines";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var line in specialOrderLines)
            {
                cmd.Parameters.AddWithValue("@SpecialOrderID", line.SpecialOrderID);
                cmd.Parameters.AddWithValue("@ItemID", line.ItemID);
                cmd.Parameters.AddWithValue("@QtyReceived", line.QtyReceived);
                cmd.ExecuteNonQuery();
            }
            
        }
    }
}
