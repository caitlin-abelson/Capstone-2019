/// <summary>
/// Jared Greenfield
/// Created: 2019/01/22
/// 
/// The concrete implementation of IOfferingAccessor. Handles storage and collection of
/// Offering objects to and from the database.
/// </summary>
///
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OfferingAccessor : IOfferingAccessor
    {
        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        ///
        /// Deactivates an Offering based on an ID
        /// </summary>
        /// <param name="offeringID">The ID of the Offering.</param>
        /// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
        /// <returns>Rows affected</returns>
        public int DeactivateOfferingByID(int offeringID)
        {
            int result = 0;
            string cmdText = @"sp_deactivate_offering";
            var conn = DBConnection.GetDbConnection();
            SqlCommand cmd1 = new SqlCommand(cmdText, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@OfferingID", offeringID);
            try
            {
                conn.Open();
                var temp = cmd1.ExecuteNonQuery();
                result = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        ///
        /// Deletes an Offering based on an ID
        /// </summary>
        /// <param name="offeringID">The ID of the Offering.</param>
        /// <exception cref="SQLException">Delete Fails (example of exception tag)</exception>
        /// <returns>Rows affected</returns>
        public int DeleteOfferingByID(int offeringID)
        {
            int result = 0;
            string cmdText = @"sp_delete_offering";
            var conn = DBConnection.GetDbConnection();
            SqlCommand cmd1 = new SqlCommand(cmdText, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@OfferingID", offeringID);
            try
            {
                conn.Open();
                var temp = cmd1.ExecuteNonQuery();
                result = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/29
        ///
        /// Adds an Offering record to the database.
        /// </summary>
        /// <param name="offering">An Offering object to be added to the database.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows affected.</returns>
        public int InsertOffering(Offering offering)
        {

            int returnedID = 0;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_insert_offering";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@OfferingTypeID", offering.OfferingTypeID);
                cmd1.Parameters.AddWithValue("@EmployeeID", offering.EmployeeID);
                cmd1.Parameters.AddWithValue("@Description", offering.Description);
                cmd1.Parameters.AddWithValue("@Price", offering.Price);
                try
                {
                    conn.Open();
                    var temp = cmd1.ExecuteScalar();
                    returnedID = Convert.ToInt32(temp);
                }
                catch (Exception)
                {

                    throw;
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
            return returnedID;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        ///
        /// Reactivates an Offering based on an ID
        /// </summary>
        /// <param name="offeringID">The ID of the Offering.</param>
        /// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
        /// <returns>Rows affected</returns>
        public int ReactivateOfferingByID(int offeringID)
        {
            int result = 0;
            string cmdText = @"sp_reactivate_offering";
            var conn = DBConnection.GetDbConnection();
            SqlCommand cmd1 = new SqlCommand(cmdText, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@OfferingID", offeringID);
            try
            {
                conn.Open();
                var temp = cmd1.ExecuteNonQuery();
                result = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/28
        ///
        /// Retrieves all Offering Types
        /// </summary>
        /// <exception cref="SQLException">Select Fails</exception>
        /// <returns>List of Offeringtypes</returns>
        public List<string> SelectAllOfferingTypes()
        {
            List<string> offeringTypes = new List<string>();
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_all_offeringtypes";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            offeringTypes.Add(reader.GetString(0));
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
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
            return offeringTypes;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/24
        ///
        /// Retrieves all Offering View Models
        /// </summary>
        /// <exception cref="SQLException">Select Fails</exception>
        /// <returns>List of Offering VMs</returns>
        public List<OfferingVM> SelectAllOfferingViewModels()
        {
            List<OfferingVM> offerings = new List<OfferingVM>();
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_all_offeringvms";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            offerings.Add(new OfferingVM()
                            {
                                OfferingID = reader.GetInt32(0),
                                OfferingTypeID = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                Active = reader.GetBoolean(4),
                                OfferingName = reader.GetString(5)
                            });
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
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
            return offerings;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/01/29
        ///
        /// Retrieves an Offering based on an ID of an Offering.
        /// </summary>
        /// <param name="offeringID">The ID of the Offering.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Offering Object</returns>
        public Offering SelectOfferingByID(int offeringID)
        {
            Offering offering = null;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_offering";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@OfferingID", offeringID);
                try
                {
                    conn.Open();
                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string offeringTypeID = reader.GetString(1);
                            int employeeID = reader.GetInt32(2);
                            string description = reader.GetString(3);
                            decimal price = reader.GetDecimal(4);
                            bool active = reader.GetBoolean(5);
                            offering = new Offering(offeringID, offeringTypeID, employeeID, description, price, active);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
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
            return offering;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/04/04
        /// Retrieves a variety of Objects based on the OfferingType and ID
        /// </summary>
        /// <exception cref="SQLException">Select Fails</exception>
        /// /// <param name="id">ID of offering</param>
        /// <param name="offeringType">Type of Offering</param>
        /// <returns>Object object</returns>
        public Object SelectOfferingInternalRecordByIDAndType(int id, string offeringType)
        {
            Object returnValue = null;
            var conn = DBConnection.GetDbConnection();
            string cmdText = @"sp_select_offeringsubitemid_by_idandtype";
            try
            {
                SqlCommand cmd1 = new SqlCommand(cmdText, conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@OfferingID", id);
                cmd1.Parameters.AddWithValue("@OfferingType", offeringType);
                try
                {
                    conn.Open();
                    var reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (offeringType == "Item")
                        {
                            Item item = new Item();
                            item.ItemID = reader.GetInt32(0);
                            item.ItemType = reader.GetString(1);
                            if (reader.IsDBNull(2))
                            {
                                item.RecipeID = null;
                            }
                            else
                            {
                                item.RecipeID = reader.GetInt32(2);
                            }
                            item.CustomerPurchasable = reader.GetBoolean(3);
                            if (reader.IsDBNull(4))
                            {
                                item.Description = null;
                            }
                            else
                            {
                                item.Description = reader.GetString(4);
                            }
                            item.OnHandQty = reader.GetInt32(5);
                            item.Name = reader.GetString(6);
                            item.ReorderQty = reader.GetInt32(7);
                            item.DateActive = reader.GetDateTime(8);
                            item.Active = reader.GetBoolean(9);
                            item.OfferingID = id;
                            returnValue = item;
                        }
                        else if (offeringType == "Service")
                        {
                            // Not Implemented
                        }
                        else if (offeringType == "Event")
                        {
                            Event eventValue = new Event();
                            eventValue.EventID = reader.GetInt32(0);
                            eventValue.EventTypeID = reader.GetString(1);
                            eventValue.EventStartDate = reader.GetDateTime(2);
                            eventValue.NumGuests = reader.GetInt32(3);
                            if (reader.IsDBNull(4))
                            {
                                eventValue.SeatsRemaining = null;
                            }
                            else
                            {
                                eventValue.SeatsRemaining = reader.GetInt32(4);
                            }
                            eventValue.PublicEvent = reader.GetBoolean(5);
                            if (reader.IsDBNull(6))
                            {
                                eventValue.Description = null;
                            }
                            else
                            {
                                eventValue.Description = reader.GetString(6);
                            }
                            eventValue.KidsAllowed = reader.GetBoolean(7);
                            eventValue.Location = reader.GetString(8);
                            if (reader.IsDBNull(9))
                            {
                                //eventValue.EventEndDate = null;
                            }
                            else
                            {
                                eventValue.EventEndDate = reader.GetDateTime(9);
                            }
                            eventValue.EventTitle = reader.GetString(10);
                            eventValue.Sponsored = reader.GetBoolean(11);
                            eventValue.EmployeeID = reader.GetInt32(12);
                            eventValue.Approved = reader.GetBoolean(13);
                            eventValue.Cancelled = reader.GetBoolean(14);
                            eventValue.OfferingID = id;
                            returnValue = eventValue;
                        }
                        if (offeringType == "Room")
                        {
                            Room room = new Room();
                            room.RoomID = reader.GetInt32(0);
                            //room.BuildingID = reader.GetString(1);
                            room.RoomNumber = reader.GetInt32(2).ToString();
                            room.RoomType = reader.GetString(3);
                            if (reader.IsDBNull(4))
                            {
                                room.Description = null;
                            }
                            else
                            {
                                room.Description = reader.GetString(4);
                            }
                            room.Capacity = reader.GetInt32(5);
                            room.RoomStatus = reader.GetString(6);
                            room.ResortPropertyID = reader.GetInt32(7);
                            returnValue = room;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
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
            return returnValue;
        }

        /// <summary>
        /// Jared Greenfield
        /// Created: 2018/02/09
        ///
        /// Updates an Offering with a new Offering.
        /// </summary>
        /// 
        /// <param name="oldOffering">The old Offering.</param>
        /// <param name="newOffering">The updated Offering.</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows affected.</returns>
        public int UpdateOffering(Offering oldOffering, Offering newOffering)
        {
            int result = 0;
            string cmdText = @"sp_update_offering";
            var conn = DBConnection.GetDbConnection();
            SqlCommand cmd1 = new SqlCommand(cmdText, conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@OfferingID", oldOffering.OfferingID);

            cmd1.Parameters.AddWithValue("@OldOfferingTypeID", oldOffering.OfferingTypeID);
            cmd1.Parameters.AddWithValue("@OldEmployeeID", oldOffering.EmployeeID);
            cmd1.Parameters.AddWithValue("@OldDescription", oldOffering.Description);
            cmd1.Parameters.AddWithValue("@OldPrice", oldOffering.Price);
            cmd1.Parameters.AddWithValue("@OldActive", oldOffering.Active);

            cmd1.Parameters.AddWithValue("@NewOfferingTypeID", newOffering.OfferingTypeID);
            cmd1.Parameters.AddWithValue("@NewEmployeeID", newOffering.EmployeeID);
            cmd1.Parameters.AddWithValue("@NewDescription", newOffering.Description);
            cmd1.Parameters.AddWithValue("@NewPrice", newOffering.Price);
            cmd1.Parameters.AddWithValue("@NewActive", newOffering.Active);
            try
            {
                conn.Open();
                var temp = cmd1.ExecuteNonQuery();
                result = Convert.ToInt32(temp);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

    }
}
