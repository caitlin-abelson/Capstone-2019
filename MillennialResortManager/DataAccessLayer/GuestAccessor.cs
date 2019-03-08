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
    /// Author: Alisa Roehr
    /// Created: 2019/01/24
    /// The GuestAccessor is an implementation of the IGuestAccessor and is designed to access a MSSQL database and work with data related to Guests
    /// </summary>
    public class GuestAccessor : IGuestAccessor
    {
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/24
        ///
        /// connect to database to insert a guest into the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="MemberID">member ID connected to the guest, either their own member Id or the member Id of the person who created their party.</param>
        /// <param name="GuestTypeID">A Guest Type is the type of guest that is with the Member. Guest Types can be child, spouse, etc.</param>
        /// <param name="FirstName">First name of guest</param>
        /// <param name="LastName">Last name of guest</param>
        /// <param name="PhoneNumber">Phone number of the guest</param>
        /// <param name="Email">guests email</param>
        /// <param name="Minor">is the guest a minor or not</param>
        /// <exception cref="SQLException">Insert Fails (example of exception tag)</exception>
        /// <returns>Rows edited</returns>
        public int CreateGuest(Guest newGuest)
        {
            int rows = 0;

            if (!isValid(newGuest))
            {
                return rows;
            }

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_insert_guest";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MemberID", newGuest.MemberID);
            cmd.Parameters.AddWithValue("@GuestTypeID", newGuest.GuestTypeID);
            cmd.Parameters.AddWithValue("@FirstName", newGuest.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newGuest.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", newGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newGuest.Email);
            cmd.Parameters.AddWithValue("@Minor", newGuest.Minor);
            cmd.Parameters.AddWithValue("@ReceiveTexts", newGuest.ReceiveTexts);
            cmd.Parameters.AddWithValue("@EmergencyFirstName", newGuest.EmergencyFirstName);
            cmd.Parameters.AddWithValue("@EmergencyLastName", newGuest.EmergencyLastName);
            cmd.Parameters.AddWithValue("@EmergencyPhoneNumber", newGuest.EmergencyPhoneNumber);
            cmd.Parameters.AddWithValue("@EmergencyRelation", newGuest.EmergencyRelation);
            
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
        /// Alisa Roehr
        /// Created: 2019/01/31
        ///
        /// connect to database to get a guest from the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="GuestID">Guest unique id number</param>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        /// <returns>Guest</returns>
        public Guest SelectGuestByGuestID(int guestID)
        {
            List<Guest> Guests = new List<Guest>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_guests_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", guestID);

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        Guests.Add(new Guest()
                        {
                            GuestID = r.GetInt32(0),
                            MemberID = r.GetInt32(1),
                            GuestTypeID = r.GetString(2),
                            FirstName = r.GetString(3),
                            LastName = r.GetString(4),
                            PhoneNumber = r.GetString(5),
                            Email = r.GetString(6),
                            Minor = r.GetBoolean(7),
                            Active = r.GetBoolean(8),
                            ReceiveTexts = r.GetBoolean(9),
                            EmergencyFirstName = r.GetString(10),
                            EmergencyLastName = r.GetString(11),
                            EmergencyPhoneNumber = r.GetString(12),
                            EmergencyRelation = r.GetString(13),
                            CheckedIn = r.GetBoolean(14)
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

            Guest newGuest = Guests[0];
            return newGuest;
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/01
        ///
        /// connect to database to get guests from the guest table with matching names.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="searchFirst">First name of guest</param>
        /// <param name="searchLast">Last name of guest</param>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        /// <returns>List of Guests</returns>
        public List<Guest> RetrieveGuestsSearchedByName(string searchLast, string searchFirst)
        {
            List<Guest> Guests = new List<Guest>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_guests_by_name";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", searchFirst);
            cmd.Parameters.AddWithValue("@LastName", searchLast);

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        Guests.Add(new Guest()
                        {
                            GuestID = r.GetInt32(0),
                            MemberID = r.GetInt32(1),
                            GuestTypeID = r.GetString(2),
                            FirstName = r.GetString(3),
                            LastName = r.GetString(4),
                            PhoneNumber = r.GetString(5),
                            Email = r.GetString(6),
                            Minor = r.GetBoolean(7),
                            Active = r.GetBoolean(8),
                            ReceiveTexts = r.GetBoolean(9),
                            EmergencyFirstName = r.GetString(10),
                            EmergencyLastName = r.GetString(11),
                            EmergencyPhoneNumber = r.GetString(12),
                            EmergencyRelation = r.GetString(13),
                            CheckedIn = r.GetBoolean(14)
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


            return Guests;
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/24
        ///
        /// connect to database to edit a guest in the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="GuestID">guests unique id number</param>
        /// <param name="MemberID">member ID connected to the guest, either their own member Id or the member Id of the person who created their party.</param>
        /// <param name="GuestTypeID">A Guest Type is the type of guest that is with the Member. Guest Types can be child, spouse, etc.</param>
        /// <param name="FirstName">First name of guest</param>
        /// <param name="LastName">Last name of guest</param>
        /// <param name="PhoneNumber">Phone number of the guest</param>
        /// <param name="Email">guests email</param>
        /// <param name="Minor">is the guest a minor or not</param>
        /// <param name="Active">is the guest a active or not</param>
        /// <param name="ReceiveTexts"></param>
        /// <param name="EmergencyFirstName"></param>
        /// <param name="EmergencyLastName"></param>
        /// <param name="EmergencyPhoneNumber"></param>
        /// <param name="EmergencyRelation"></param>
        /// 
        /// <param name="OldMemberID">member ID connected to the guest, either their own member Id or the member Id of the person who created their party.</param>
        /// <param name="OldGuestTypeID">A Guest Type is the type of guest that is with the Member. Guest Types can be child, spouse, etc.</param>
        /// <param name="OldFirstName">First name of guest</param>
        /// <param name="OldLastName">Last name of guest</param>
        /// <param name="OldPhoneNumber">Phone number of the guest</param>
        /// <param name="OldEmail">guests email</param>
        /// <param name="OldMinor">is the guest a minor or not</param>
        /// <param name="OldActive">is the guest a active or not</param>
        /// <param name="OldReceiveTexts"></param>
        /// <param name="OldEmergencyFirstName"></param>
        /// <param name="OldEmergencyLastName"></param>
        /// <param name="OldEmergencyPhoneNumber"></param>
        /// <param name="OldEmergencyRelation"></param>
        /// 
        /// <exception cref="SQLException">Update Fails (example of exception tag)</exception>
        /// <returns>Rows edited</returns>
        public int UpdateGuest(Guest newGuest, Guest oldGuest)
        {
            int rows = 0;

            if (!isValid(newGuest))
            {
                return rows;
            }
            if (!isValid(oldGuest))
            {
                return rows;
            }

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_update_guest_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", newGuest.GuestID);

            cmd.Parameters.AddWithValue("@MemberID", newGuest.MemberID);
            cmd.Parameters.AddWithValue("@GuestTypeID", newGuest.GuestTypeID);
            cmd.Parameters.AddWithValue("@FirstName", newGuest.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newGuest.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", newGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@Email", newGuest.Email);
            cmd.Parameters.AddWithValue("@Minor", newGuest.Minor);
            cmd.Parameters.AddWithValue("@Active", newGuest.Active);
            cmd.Parameters.AddWithValue("@ReceiveTexts", newGuest.ReceiveTexts);
            cmd.Parameters.AddWithValue("@EmergencyFirstName", newGuest.EmergencyFirstName);
            cmd.Parameters.AddWithValue("@EmergencyLastName", newGuest.EmergencyLastName);
            cmd.Parameters.AddWithValue("@EmergencyPhoneNumber", newGuest.EmergencyPhoneNumber);
            cmd.Parameters.AddWithValue("@EmergencyRelation", newGuest.EmergencyRelation);
            cmd.Parameters.AddWithValue("@CheckedIn", newGuest.CheckedIn);

            cmd.Parameters.AddWithValue("@OldMemberID", oldGuest.MemberID);
            cmd.Parameters.AddWithValue("@OldGuestTypeID", oldGuest.GuestTypeID);
            cmd.Parameters.AddWithValue("@OldFirstName", oldGuest.FirstName);
            cmd.Parameters.AddWithValue("@OldLastName", oldGuest.LastName);
            cmd.Parameters.AddWithValue("@OldPhoneNumber", oldGuest.PhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmail", oldGuest.Email);
            cmd.Parameters.AddWithValue("@OldMinor", oldGuest.Minor);
            cmd.Parameters.AddWithValue("@OldActive", oldGuest.Active);
            cmd.Parameters.AddWithValue("@OldReceiveTexts", oldGuest.ReceiveTexts);
            cmd.Parameters.AddWithValue("@OldEmergencyFirstName", oldGuest.EmergencyFirstName);
            cmd.Parameters.AddWithValue("@OldEmergencyLastName", oldGuest.EmergencyLastName);
            cmd.Parameters.AddWithValue("@OldEmergencyPhoneNumber", oldGuest.EmergencyPhoneNumber);
            cmd.Parameters.AddWithValue("@OldEmergencyRelation", oldGuest.EmergencyRelation);
            cmd.Parameters.AddWithValue("@OldCheckedIn", oldGuest.CheckedIn);

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
        /// Alisa Roehr
        /// Created: 2019/01/24
        ///
        /// gets all the guest types for filling out things, like the combo boxes.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns>all guest types</returns>
        public List<string> SelectAllGuestTypes()
        {
            List<string> guestTypes = new List<string>();
            var conn = DBConnection.GetDbConnection();
            var cmd = new SqlCommand("sp_retrieve_all_guest_types", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        guestTypes.Add(r.GetString(0));
                    }
                }
                r.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return guestTypes;
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/01
        ///
        /// connect to database to get all guests from the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        /// <returns>List of Guests</returns>
        public List<Guest> SelectAllGuests()
        {
            List<Guest> Guests = new List<Guest>();

            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_retrieve_all_guests";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        Guests.Add(new Guest()
                        {
                            GuestID = r.GetInt32(0),
                            MemberID = r.GetInt32(1),
                            GuestTypeID = r.GetString(2),
                            FirstName = r.GetString(3),
                            LastName = r.GetString(4),
                            PhoneNumber = r.GetString(5),
                            Email = r.GetString(6),
                            Minor = r.GetBoolean(7),
                            Active = r.GetBoolean(8),
                            CheckedIn = r.GetBoolean(9)
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

            return Guests;
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/31
        ///
        /// connect to database to deactivate a guest from the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="guestID">Guest unique id number</param>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        public void DeactivateGuest(int guestID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_deactivate_guest_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@GuestID", guestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/31
        ///
        /// connect to database to purge a guest from the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="guestID">Guest unique id number</param>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        public void DeleteGuest(int guestID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_delete_guest_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@GuestID", guestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/01/31
        ///
        /// connect to database to reactivate a guest from the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="guestID">Guest unique id number</param>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        public void ReactivateGuest(int guestID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_activate_guest_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@GuestID", guestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        ///
        /// connect to database to check out a guest from the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="guestID">Guest unique id number</param>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        public void CheckOutGuest(int guestID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_check_out_guest_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@GuestID", guestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        
        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/22
        ///
        /// connect to database to check in a guest from the guest table.
        /// </summary>
        /// 
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="guestID">Guest unique id number</param>
        /// <exception cref="SQLException">Select Fails (example of exception tag)</exception>
        public void CheckInGuest(int guestID)
        {
            var conn = DBConnection.GetDbConnection();
            var cmdText = "sp_check_in_guest_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@GuestID", guestID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Alisa Roehr
        /// Created: 2019/02/15
        /// Modified: 2019/02/21
        /// 
        /// check if guest is valid or not
        /// </summary>
        /// <param name="_guest"> guest that is being tested for validation</param>
        /// <returns>whether the guest information is valid</returns>
        public bool isValid(Guest _guest)
        {
            if ( /*_guest.MemberID.ToString().Length > 11 ||*/ _guest.MemberID == null || _guest.MemberID == 0)
            {
                return false;// for member id, check for if integer
            }
            else if (_guest.GuestTypeID.Length > 25 || _guest.GuestTypeID == null || _guest.GuestTypeID.Length == 0)
            {
                return false; // for guest type
            }
            else if (_guest.FirstName.Length > 50 || _guest.FirstName == null || _guest.FirstName.Length == 0)
            {
                return false; // for first name
            }
            else if (_guest.LastName.Length > 100 || _guest.LastName == null || _guest.LastName.Length == 0)
            {
                return false; // for last name
            }
            else if (_guest.PhoneNumber.Length > 11 || _guest.PhoneNumber == null || _guest.PhoneNumber.Length == 0)
            {
                return false;  // for phone number, check for if integer
            }
            else if (_guest.Email.Length > 250 || _guest.Email == null || _guest.Email.Length == 0)
            {
                return false;  // for email, need greater email validation
            }
            else if (_guest.Minor == null)
            {
                return false; // for minor
            }
            else if (_guest.Active == null)
            {
                return false; // for active
            }
            else if (_guest.ReceiveTexts == null)
            {
                return false; // for ReceiveTexts
            }
            else if (_guest.EmergencyFirstName.Length > 50 || _guest.EmergencyFirstName == null || _guest.EmergencyFirstName.Length == 0)
            {
                return false; // for EmergencyFirstName
            }
            else if (_guest.EmergencyLastName.Length > 100 || _guest.EmergencyLastName == null || _guest.EmergencyLastName.Length == 0)
            {
                return false; // for EmergencyLastName
            }
            else if (_guest.EmergencyPhoneNumber.Length > 11 || _guest.EmergencyPhoneNumber == null || _guest.EmergencyPhoneNumber.Length < 7)
            {
                return false; // for EmergencyPhoneNumber, need to test for if integer
            }
            else if (_guest.EmergencyRelation.Length > 25 || _guest.EmergencyRelation == null || _guest.EmergencyRelation.Length == 0)
            {
                return false; // for EmergencyRelation
            }
            else
            {
                return true;
            }
        }
    }
}
