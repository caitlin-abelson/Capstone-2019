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
        public int InsertSpecialOrder(CompleteSpecialOrder newSpecialOrder)
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
                        cmd1.Parameters.AddWithValue("@DateOrdered", newSpecialOrder.DateOrdered);
                        cmd1.Parameters.AddWithValue("@Supplier", newSpecialOrder.Supplier);
                        row += cmd1.ExecuteNonQuery();
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
        /// Creates the new Special order Line, with the data provided by user.
        /// </summary
        public int InsertSpecialOrderLine(SpecialOrderLine newSpecialOrderline)
        {
            int row = 0;

            try
            {
                var conn = DBConnection.GetDbConnection();
               
                conn.Open();

                var cmd = new SqlCommand("sp_create_specialOrderLine", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NameID", newSpecialOrderline.NameID);
                cmd.Parameters.AddWithValue("@SpecialOrderID", newSpecialOrderline.SpecialOrderID);
                cmd.Parameters.AddWithValue("@Description", newSpecialOrderline.Description);
                cmd.Parameters.AddWithValue("@OrderQty", newSpecialOrderline.OrderQty);
                cmd.Parameters.AddWithValue("@QtyReceived", newSpecialOrderline.QtyReceived);
                row += cmd.ExecuteNonQuery();


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
        public List<CompleteSpecialOrder> SelectSpecialOrder()
        {
            List<CompleteSpecialOrder> list = new List<CompleteSpecialOrder>();
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_all_special_order", conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
                            DateOrdered = reader.GetDateTime(3),
                            Supplier = reader.GetString(4),
                            Authorized = reader.GetString(5)
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
        /// </summary
        public List<SpecialOrderLine> SelectSpecialOrderLinebySpecialID(int orderID)
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
                        items.NameID = reader.GetString(0);
                        items.SpecialOrderID = reader.GetInt32(1);
                        items.Description = reader.GetString(2);
                        items.OrderQty = reader.GetInt32(3);
                        items.QtyReceived = reader.GetInt32(4);
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


        

        // <summary>
        /// Carlos Arzu
        /// Created: 2019/01/31
        /// 
        /// Retrieves the ItemId needed for every form.
        /// </summary
        public int retrieveSpecialOrderIDbyDetails(CompleteSpecialOrder selected)
        {

            List<int> ID = new List<int>();
            int max = 0;

            try
            {
                var conn = DBConnection.GetDbConnection();

                conn.Open();
                var cmd = new SqlCommand("sp_retrieve_last_specialOrderID_created", conn);
                cmd.CommandType = CommandType.StoredProcedure;
            
                cmd.Parameters.AddWithValue("@EmployeeID", selected.EmployeeID);
                cmd.Parameters.AddWithValue("@Description", selected.Description);
                cmd.Parameters.AddWithValue("@DateOrdered", selected.DateOrdered);
                cmd.Parameters.AddWithValue("@Supplier", selected.Supplier);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ID.Add(reader.GetInt32(0));
                    }
                }
            

                conn.Close();

                max = ID.Max();
                
            }
            catch (Exception)
            {

                throw;
            }

            return max;
        
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
                throw ex;
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
            cmd.Parameters.AddWithValue("@Supplier", Ordernew.Supplier);

            cmd.Parameters.AddWithValue("@OldEmployeeID", Order.EmployeeID);
            cmd.Parameters.AddWithValue("@OldDescription", Order.Description);
            cmd.Parameters.AddWithValue("@OldSupplier", Order.Supplier);


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
        /// Carlos Arzu
        /// Created: 2019/04/05
        /// 
        /// With the input user provided for updating, the special order line will be updated.
        /// </summary
        public int UpdateOrderLine(SpecialOrderLine Order, SpecialOrderLine Ordernew)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_update_SpecialOrderLine";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", Order.SpecialOrderID);

            cmd.Parameters.AddWithValue("@NameID", Ordernew.NameID);
            cmd.Parameters.AddWithValue("@Description", Ordernew.Description);
            cmd.Parameters.AddWithValue("@OrderQty", Ordernew.OrderQty);
            cmd.Parameters.AddWithValue("@QtyReceived", Ordernew.QtyReceived);

            cmd.Parameters.AddWithValue("@OldNameID", Order.NameID);
            cmd.Parameters.AddWithValue("@OldDescription", Order.Description);
            cmd.Parameters.AddWithValue("@OldOrderQty", Order.OrderQty);
            cmd.Parameters.AddWithValue("@OldQtyReceived", Order.QtyReceived);


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
        /// Carlos Arzu
        /// Created: 2019/04/06
        /// 
        /// Deletes a record from the DB.
        /// </summary
        public int DeleteItemFromOrder(int ID, string ItemName)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_delete_Item_from_SpecialOrder";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", ID);
            cmd.Parameters.AddWithValue("@NameID", ItemName);
            
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
        /// Carlos Arzu
        /// Created: 2019/04/10
        /// 
        /// Adding the username who authorized this order.
        /// </summary
        public int insertAuthenticateBy(int SpecialOrderID, string Authorized)
        {
            int rows = 0;

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_update_AuthenticatedBy";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SpecialOrderID", SpecialOrderID);

            cmd.Parameters.AddWithValue("@Authorized", Authorized);

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
            

    }
}
