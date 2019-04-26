using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Eric Bostwick
    /// Created 4/2/2019
    /// Implementation of IPickAccessor
    /// for managing Picking Operations
    /// </summary>
    public class PickAccessor : IPickAccessor
    {
        public List<PickSheet> Select_All_PickSheets()
        {
            List<PickSheet> picksheets = new List<PickSheet>();

            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_picksheets";  //up_Select_Orders_From_Date
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string strPickStatus;
                        int pickStatus;
                        if (!reader.GetDateTime(reader.GetOrdinal("PickCompletedDate")).Equals(null))
                        {
                            strPickStatus = "Open PickSheet";
                            pickStatus = 2;

                        }
                        else
                        {
                            strPickStatus = "Close PickSheet";
                            pickStatus = 1;
                        }

                        picksheets.Add(new PickSheet()
                        {
                            PickSheetID = reader["PickSheetID"].ToString(),
                            PickSheetCreatedBy = reader.GetInt32(reader.GetOrdinal("PickSheetCreatedBy")),
                            PickSheetCreatedByName = reader["PickSheetCreatedByName"].ToString(),
                            CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")),
                            PickCompletedBy = reader.GetInt32(reader.GetOrdinal("PickCompletedBy")),
                            PickCompletedByName = reader["PickCompletedByName"].ToString(),
                            PickCompletedDate = reader.GetDateTime(reader.GetOrdinal("PickCompletedDate")),
                            PickDeliveredBy = reader.GetInt32(reader.GetOrdinal("PickDeliveredBy")),
                            PickDeliveredByName = reader["PickDeliveredName"].ToString(),
                            PickDeliveryDate = reader.GetDateTime(reader.GetOrdinal("PickDeliveryDate")),
                            PickSheetStatusView = strPickStatus,
                            NumberOfOrders = reader.GetInt32(reader.GetOrdinal("NumberOfOrders")),
                            PickSheetStatus = pickStatus
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return picksheets;
        }     

        public int Insert_TmpPickSheet_To_PickSheet(string picksheetID)      
        {
           //Commits orders from tmpPickTable To PickSheet(Moves Orders From tmpPickTable to PickTable,
             //Deletes Orders From TmpPickTable and Updates the order Table
             int result;
            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_tmppicksheet_to_picksheet";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.Add("@OrdersAffected", SqlDbType.Int);
            cmd.Parameters["@OrdersAffected"].Direction = ParameterDirection.Output;
            //cmd.Parameters["@PickSheetNumber"].Value = orderStation;

            cmd.Parameters.Add("@PickSheetID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@PickSheetID"].Value = picksheetID.Trim();

            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                int retValue = (int)cmd.Parameters["@OrdersAffected"].Value;
                result = retValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int Delete_TmpPickSheet_Item(PickOrder pickOrder)
        {
            int result;
            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_tmp_picksheet_item";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.Add("@PickSheetID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@PickSheetID"].Value = pickOrder.PickSheetID;

            cmd.Parameters.Add("@InternalOrderID", SqlDbType.Int);
            cmd.Parameters["@InternalOrderID"].Value = pickOrder.InternalOrderID;

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters["@ItemID"].Value = pickOrder.ItemID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        public  int Delete_TmpPickSheet(string picksheetID)
        {  //Deletes all items from a tmpPickSheet -- Used when cancelling a pick 
            int result;
            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_delete_tmp_picksheet";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.Add("@PickSheetID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@PickSheetID"].Value = picksheetID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result / 2;
        }

        public int Insert_Record_To_TmpPicksheet(PickOrder pickOrder)
        {
            int result;

            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_insert_tmp_picksheet";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;           

            cmd.Parameters.Add("@PickSheetID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@PickSheetID"].Value = pickOrder.PickSheetID;

            cmd.Parameters.Add("@InternalOrderID", SqlDbType.Int);
            cmd.Parameters["@InternalOrderID"].Value = pickOrder.InternalOrderID;

            cmd.Parameters.Add("@ItemID", SqlDbType.Int);
            cmd.Parameters["@ItemID"].Value = pickOrder.ItemID;

            cmd.Parameters.Add("@PickedBy", SqlDbType.Int);
            cmd.Parameters["@PickedBy"].Value = pickOrder.EmployeeID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

       
        public List<PickSheet> Select_All_PickSheets_By_Date(DateTime startDate)
        {
            List<PickSheet> picksheets = new List<PickSheet>();

            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_pickSheets_by_date";  //up_Select_Orders_From_Date
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters["@StartDate"].Value = startDate;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string pickStatusView;
                        int pickSheetStatus;
                        int pickCompletedBy;
                        int pickDeliveredBy;
                        string pickCompletedByName;
                        string pickDeliveredByName;
                        string pickCompletedDateView;
                        string pickDeliveryDateView;
                        DateTime pickCompletedDate = new DateTime(0001, 1, 1);
                        DateTime pickDeliveryDate = new DateTime(0001, 1, 1);

                        if (!reader.IsDBNull(reader.GetOrdinal("PickCompletedDate")))
                        {
                            pickStatusView = "Open PickSheet";
                            pickSheetStatus = 2;
                            
                        }
                        else
                        {
                            pickStatusView = "Close PickSheet";
                            pickSheetStatus = 1;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickCompletedBy")))
                        {
                            pickCompletedBy = reader.GetInt32(reader.GetOrdinal("PickCompletedBy"));
                        }
                        else
                        {
                            pickCompletedBy = 0;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickCompletedByName")))
                        {
                            pickCompletedByName = reader["PickCompletedByName"].ToString();
                        }
                        else
                        {
                            pickCompletedByName = "";
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickCompletedDate")))
                        {
                            pickCompletedDateView = reader.GetDateTime(reader.GetOrdinal("PickCompletedDate")).ToString("MM/dd/yyyy HH:mm");
                            pickCompletedDate = reader.GetDateTime(reader.GetOrdinal("PickCompletedDate"));
                        }
                        else
                        {
                            pickCompletedDateView = "";
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickDeliveredBy")))
                        {
                            pickDeliveredBy = reader.GetInt32(reader.GetOrdinal("PickDeliveredBy"));
                        }
                        else
                        {
                            pickDeliveredBy = 0;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickDeliveredByName")))
                        {
                            pickDeliveredByName = reader["PickDeliveredByName"].ToString();
                        }
                        else
                        {
                            pickDeliveredByName = "";
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickDeliveryDate")))
                        {
                            pickDeliveryDateView = reader.GetDateTime(reader.GetOrdinal("PickDeliveryDate")).ToString("MM/dd/yyyy HH:mm");
                            pickDeliveryDate = reader.GetDateTime(reader.GetOrdinal("PickDeliveryDate"));
                        }
                        else
                        {
                            pickDeliveryDateView = "";
                        }


                        picksheets.Add(new PickSheet()
                        {
                            PickSheetID = reader["PickSheetID"].ToString(),                            
                            PickSheetCreatedByName = reader["PickCreatedByName"].ToString(),
                            CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")),
                            PickCompletedBy = pickCompletedBy,                            
                            PickCompletedByName = pickCompletedByName,
                            PickCompletedDateView = pickCompletedDateView,
                            PickDeliveredBy = pickDeliveredBy,
                            PickDeliveredByName = pickDeliveredByName,
                            PickDeliveryDateView = pickDeliveryDateView,
                            NumberOfOrders = reader.GetInt32(reader.GetOrdinal("NumberOfOrders")),
                            PickSheetStatusView = pickStatusView,
                            PickSheetStatus = pickSheetStatus
                        });
                    }
                 }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return picksheets;
        }
    

        public int UpdatePickSheet(PickSheet pickSheet, PickSheet oldPickSheet)
        {
            //List<PickSheet> pickSheet = new List<PickSheet>();
            int result;
            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_picksheet";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.AddWithValue("@PickSheetID", pickSheet.PickSheetID);
            cmd.Parameters.AddWithValue("@PickCompletedBy", pickSheet.PickCompletedBy);
            if (pickSheet.PickCompletedDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@PickCompletedDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PickCompletedDate", pickSheet.PickCompletedDate); 
            }

            if (pickSheet.PickDeliveryDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@PickDeliveryDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PickDeliveryDate", pickSheet.PickDeliveryDate);
            }
            
            cmd.Parameters.AddWithValue("@PickDeliveredBy", pickSheet.PickDeliveredBy);
            cmd.Parameters.AddWithValue("@NumberofOrders", pickSheet.NumberOfOrders);

            cmd.Parameters.AddWithValue("@OldPickCompletedBy", oldPickSheet.PickCompletedBy);

            if (oldPickSheet.PickCompletedDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@OldPickCompletedDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldPickCompletedDate", oldPickSheet.PickCompletedDate);
            }

            cmd.Parameters.AddWithValue("@OldPickDeliveredBy", oldPickSheet.PickDeliveredBy);

            if (oldPickSheet.PickDeliveryDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@OldPickDeliveryDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldPickDeliveryDate", oldPickSheet.PickDeliveryDate);
            }

            cmd.Parameters.AddWithValue("@OldNumberofOrders", oldPickSheet.NumberOfOrders);

            var x = cmd.Parameters.Count;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public List<PickOrder> Select_Orders_For_Acknowledgement(DateTime startDate, bool hidePicked)
        {
            List<PickOrder> orders = new List<PickOrder>();


            var conn = DBConnection.GetDbConnection();
            string cmdText;
            if (hidePicked == false)
            {
                cmdText = @"sp_select_orders_for_acknowledgement";  //if we want to show picked orders
            }
            else
            {
                cmdText = @"sp_select_orders_for_acknowledgement_hidePicked";  //if we want to show only open orders
            }

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters["@StartDate"].Value = startDate;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(reader.GetOrdinal("OrderStatus")))
                        {
                            //No orders
                            return orders;
                        }
                        string pickSheetID = "";
                        string pickSheetIDView = "";

                        if (!reader.IsDBNull(reader.GetOrdinal("DeliveryDate")))
                        {
                            var DeliveryDate = reader.GetDateTime(reader.GetOrdinal("DeliveryDate"));
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("PickSheetID")))
                        {
                            pickSheetID = reader["PickSheetID"].ToString();
                            pickSheetIDView = reader["PickSheetID"].ToString();
                        }                       

                        orders.Add(new PickOrder()
                        {
                            DeptID = reader["DepartmentID"].ToString(),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                            Orderer = reader["Orderer"].ToString(),
                            ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
                            InternalOrderID = reader.GetInt32(reader.GetOrdinal("InternalOrderID")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            PickSheetID = pickSheetID,
                            PickSheetIDView = pickSheetIDView,
                            OrderStatusView = reader["OrderStatusView"].ToString(),
                            OrderStatus = reader.GetInt32(reader.GetOrdinal("OrderStatus")),
                            ItemDescription = reader["ItemDescription"].ToString(),
                            OrderQty = reader.GetInt32(reader.GetOrdinal("OrderQty")),
                            DepartmentID = reader["DepartmentID"].ToString()

                            //InternalOrderID = reader["InternalOrderID"].ToString(),
                            //OrderReceivedDate = reader.GetDateTime(reader.GetOrdinal("OrderReceivedDate")), 
                            ////PickCompleteDate = reader.GetDateTime(reader.GetOrdinal("PickCompleteDate")),     
                            /// //DeptDescription = reader["DeptDescription"].ToString(),
                        });
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
            return orders;
        }

        public string Select_Pick_Sheet_Number()
        {
            string pickSheetNumber = null;

            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_next_picksheet_number";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.Add("@PickSheetNumber", SqlDbType.NVarChar, 25);
            cmd.Parameters["@PickSheetNumber"].Direction = ParameterDirection.Output;           

            try
            {
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                pickSheetNumber = cmd.Parameters["@PickSheetNumber"].Value.ToString();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return pickSheetNumber;
        }

        public List<PickOrder> Select_PickSheet_By_PickSheetID(string pickSheetID)
        {
            List<PickOrder> pickOrders = new List<PickOrder>();

            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_picksheet_by_picksheetid";  //up_Select_Orders_From_Date
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.Add("@PickSheetID", SqlDbType.NVarChar, 25);
            cmd.Parameters["@PickSheetID"].Value = pickSheetID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pickOrders.Add(new PickOrder()
                        {
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                            OrderDateView = reader["OrderDate"].ToString(),  //"MM/dd/yyyy hh:mm"                           
                            DeptID = reader["DepartmentID"].ToString(),
                            EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                            Orderer = reader["Orderer"].ToString(),
                            ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
                            InternalOrderID = reader.GetInt32(reader.GetOrdinal("InternalOrderID")),
                            ItemDescription = reader["ItemDescription"].ToString(),
                            OrderQty = reader.GetInt32(reader.GetOrdinal("OrderQty")),
                            QtyReceived = reader.GetInt32(reader.GetOrdinal("QtyReceived")),
                            OrderReceivedDate = reader.GetDateTime(reader.GetOrdinal("OrderReceivedDate")),
                            OrderReceivedDateView = reader["OrderReceivedDate"].ToString(),
                            PickSheetID = reader["PickSheetID"].ToString(),
                            //PickCompleteDate = reader.GetDateTime(reader.GetOrdinal("PickCompleteDate")),
                            PickCompleteDateView = reader["PickCompleteDate"].ToString(),
                            //////DeliveryDate = reader.GetDateTime(reader.GetOrdinal("DeliveryDate")),
                            DeliveryDateView = reader["DeliveryDate"].ToString(),
                            OrderStatus = reader.GetInt32(reader.GetOrdinal("OrderStatus")),
                            OrderStatusView = reader["StatusView"].ToString(),
                            OutOfStock = reader.GetBoolean(reader.GetOrdinal("OutOfStock")),
                            DepartmentID = reader["DepartmentID"].ToString()
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return pickOrders;
        }
        public int Update_PickOrder(PickOrder pickOrder, PickOrder oldPickOrder)
        {
            int result;
            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_update_pick_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            
            //@ItemID int,
            cmd.Parameters.AddWithValue("@ItemID", pickOrder.ItemID); //1
            //@InternalOrderID int,
            cmd.Parameters.AddWithValue("@InternalOrderID", pickOrder.InternalOrderID);//2
            //@OrderQty int,
            cmd.Parameters.AddWithValue("@OrderQty", pickOrder.OrderQty);//3
            //@QtyReceived int,
            cmd.Parameters.AddWithValue("@QtyReceived", pickOrder.QtyReceived); //4
            //@OrderReceivedDate datetime,
            cmd.Parameters.AddWithValue("@OrderReceivedDate", pickOrder.OrderReceivedDate);//5
            //@PickSheetID nvarchar(25),
            cmd.Parameters.AddWithValue("@PickSheetID", pickOrder.PickSheetID); //6
            //@PickCompleteDate datetime,
            if (pickOrder.PickCompleteDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@PickCompleteDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PickCompleteDate", pickOrder.PickCompleteDate); //7
            }
            if (pickOrder.DeliveryDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@DeliveryDate", Convert.DBNull);  
            }
            else
            {
                cmd.Parameters.AddWithValue("@DeliveryDate", pickOrder.DeliveryDate); //8
            }
            //@OutOfStock bit,
            cmd.Parameters.AddWithValue("@OutOfStock", pickOrder.OutOfStock); //9
            //@OrderStatus int,
            cmd.Parameters.AddWithValue("@OrderStatus", pickOrder.OrderStatus); //10
            //@OldItemID int,
            cmd.Parameters.AddWithValue("@OldItemID", oldPickOrder.ItemID);  //11
            //@OldInternalOrderID int,
            cmd.Parameters.AddWithValue("@OldInternalOrderID", oldPickOrder.InternalOrderID); //12
            //@OldOrderQty int,
            cmd.Parameters.AddWithValue("@OldOrderQty", oldPickOrder.OrderQty); //13
            //@OldQtyReceived int,
            cmd.Parameters.AddWithValue("@OldQtyReceived", oldPickOrder.QtyReceived); //14
            //@OldOrderReceivedDate datetime,
            if (oldPickOrder.OrderReceivedDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@OldOrderReceivedDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldOrderReceivedDate", oldPickOrder.OrderReceivedDate); //15
            }
            //@OldPickSheetID nvarchar(25),
            cmd.Parameters.AddWithValue("@OldPickSheetID", oldPickOrder.PickSheetID);  //16
            //@OldPickCompleteDate datetime,
            if (oldPickOrder.PickCompleteDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@OldPickCompleteDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldPickCompleteDate", oldPickOrder.PickCompleteDate); //17
            }

            if (oldPickOrder.DeliveryDate.Year < DateTime.Now.Year)
            {
                cmd.Parameters.AddWithValue("@OldDeliveryDate", Convert.DBNull);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldDeliveryDate", oldPickOrder.DeliveryDate); //18
            }
            cmd.Parameters.AddWithValue("@OldOutOfStock", oldPickOrder.OutOfStock); //19
            cmd.Parameters.AddWithValue("@OldOrderStatus", oldPickOrder.OrderStatus); //20

            //var test = cmd.Parameters["@OldDeliveryDate"].Value;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public List<PickSheet> Select_All_Closed_PickSheets_By_Date(DateTime startDate)
        {
            List<PickSheet> pickSheets = new List<PickSheet>();
            string cmdtext;
            var conn = DBConnection.GetDbConnection();
            var cmdText = @"sp_select_all_closed_pickSheets_by_date";  //up_Select_Orders_From_Date
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdtext = cmd.CommandText;

            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters["@StartDate"].Value = startDate;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //string strPickStatus;
                        string pickStatusView;
                        int pickSheetStatus = 0;
                        string pickCompletedByName;
                        int pickCompletedBy;
                        string pickDeliveredByName;
                        int pickDeliveredBy;
                        DateTime pickCompletedDate = new DateTime(0001, 1, 1); ;
                        string pickCompletedDateView;
                        DateTime pickDeliveredDate = new DateTime(0001, 1, 1);
                        string pickDeliveredDateView;

                        if (!reader.IsDBNull(reader.GetOrdinal("PickDeliveryDate")))
                        {
                            //pickStatusView = "UnDeliver PickSheet";
                            //pickSheetStatus = 2;
                        }
                        else
                        {
                            //pickStatusView = "Deliver PickSheet";
                            //pickSheetStatus = 3;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickCompletedBy")))
                        {
                            pickCompletedBy = reader.GetInt32(reader.GetOrdinal("PickCompletedBy"));
                        }
                        else
                        {
                            pickCompletedBy = 0;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickCompletedByName")))
                        {
                            pickCompletedByName = reader["PickCompletedByName"].ToString();
                        }
                        else
                        {
                            pickCompletedByName = "";
                        }

                        if (!(reader.IsDBNull(reader.GetOrdinal("PickDeliveredBy")) || reader.GetInt32(reader.GetOrdinal("PickDeliveredBy")) == 0))
                        {
                            pickDeliveredBy = reader.GetInt32(reader.GetOrdinal("PickDeliveredBy"));
                            pickStatusView = "UnDeliver PickSheet";
                            pickSheetStatus = 3;
                        }
                        else
                        {
                            pickDeliveredBy = 0;
                            pickStatusView = "Deliver PickSheet";
                            pickSheetStatus = 2;
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickDeliveredByName")))
                        {
                            pickDeliveredByName = reader["PickDeliveredByName"].ToString();
                        }
                        else
                        {
                            pickDeliveredByName = "";
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickCompletedDate")))
                        {
                            pickCompletedDateView = reader.GetDateTime(reader.GetOrdinal("PickCompletedDate")).ToString();
                            pickCompletedDate = reader.GetDateTime(reader.GetOrdinal("PickCompletedDate"));
                        }
                        else
                        {                           
                            pickCompletedDateView = "";                            
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("PickDeliveryDate")))
                        {
                            pickDeliveredDateView = reader.GetDateTime(reader.GetOrdinal("PickDeliveryDate")).ToString();
                            pickDeliveredDate = reader.GetDateTime(reader.GetOrdinal("PickDeliveryDate"));
                        }
                        else
                        {
                            pickDeliveredDateView = "";
                        }                        

                        pickSheets.Add(new PickSheet()
                        {
                            PickSheetID = reader["PickSheetID"].ToString(),
                            CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")),                             
                            NumberOfOrders = reader.GetInt32(reader.GetOrdinal("OrderCount")),
                            PickCompletedBy = pickCompletedBy,
                            PickCompletedByName =pickCompletedByName,                            
                            PickCompletedDate = pickCompletedDate,
                            PickCompletedDateView = pickCompletedDateView,
                            PickDeliveredByName = pickDeliveredByName,
                            PickDeliveredBy = pickDeliveredBy,
                            PickDeliveryDate = pickDeliveredDate,
                            PickDeliveryDateView = pickDeliveredDateView,
                            PickSheetStatus = pickSheetStatus,
                            PickSheetStatusView = pickStatusView                      
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return pickSheets;
        }

        public List<PickOrder> Select_All_Temp_PickOrders()
        {
            throw new NotImplementedException();
        }

        public PickSheet Select_TmpPickSheet(string pickSheetID)
        {
            throw new NotImplementedException();
        }
    }
    
}
