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
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// The ReservationAccessorMSSQL is an implementation of the IReservationAccessor and is designed to access a MSSQL database and work with data related to Reservations
    /// </summary>
    public class ReservationAccessorMSSQL : IReservationAccessor
    {
        public ReservationAccessorMSSQL()
        {
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// Creates a Reservation for the Reservation Table in our Microsoft SQL Server
        /// </summary>
        /// <param name="newReservation">Contains the information for the Reservation which will be added to our system</param>
        public void CreateReservation(Reservation newReservation)
        {
            
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_create_reservation";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            //We do not have a ReservationID since we have not created the Reservation in our database yet
            cmd1.Parameters.AddWithValue("@MemberID", newReservation.MemberID);
            cmd1.Parameters.AddWithValue("@NumberOfGuests", newReservation.NumberOfGuests);
            cmd1.Parameters.AddWithValue("@NumberOfPets", newReservation.NumberOfPets);
            //May need to remove the ToString() here. Currently it has the time appended to the end and I do not think we will need it. 
            cmd1.Parameters.AddWithValue("@ArrivalDate", newReservation.ArrivalDate);
            cmd1.Parameters.AddWithValue("@DepartureDate", newReservation.DepartureDate);
            cmd1.Parameters.AddWithValue("@Notes", newReservation.Notes);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd1.ExecuteNonQuery();
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
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Deactivate Reservation will attempt to change the active field to false for the Reservation with a matching ResrvationID in our MSSQL database
        /// </summary>
        /// <param name="ReservationID">The Reservation ID of the Reservation we wish to deactivate from our system</param>
        public void DeactivateReservation(int ReservationID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_deactivate_reservation";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@ReservationID", ReservationID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                if (cmd1.ExecuteNonQuery() < 1)
                {
                    throw new ArgumentException("No Reservation found with that ID");
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
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Purge Reservation will attempt to completely delete the Reservation with a matching ResrvationID in our MSSQL database
        /// </summary>
        /// <param name="ReservationID">The Reservation ID of the Reservation we wish to purge from our system</param>
        public void PurgeReservation(int ReservationID)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_delete_reservation";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@ReservationID", ReservationID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd1.ExecuteNonQuery();
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
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Retrieve All Reservations will attempt to select and return every single Reservation from our MSSQL database in the form of a List
        /// </summary>
        /// <returns>Returns a List of all the Reservations in our System</returns>
        public List<Reservation> RetrieveAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_all_reservations";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.ReservationID = reader.GetInt32(0);
                        reservation.MemberID = reader.GetInt32(1);
                        reservation.NumberOfGuests = reader.GetInt32(2);
                        reservation.NumberOfPets = reader.GetInt32(3);
                        reservation.ArrivalDate = reader.GetDateTime(4);
                        reservation.DepartureDate = reader.GetDateTime(5);
                        reservation.Notes = reader.GetString(6);
                        reservation.Active = reader.GetBoolean(7);
                        reservations.Add(reservation);
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
            return reservations;
        }

        public bool ValidateMember(int memberID)
        {
            bool valid = false;            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_select_member_by_ID";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            //We do not have a ReservationID since we have not created the Reservation in our database yet
            cmd1.Parameters.AddWithValue("@MemberID", memberID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    valid = true;
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
            return valid;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Retrieve All VM Reservations will attempt to select every single Reservation along with the Member which matches the Reservations MemberID from our MSSQL database in the form of a List
        /// </summary>
        /// <returns>Returns a List of all View Models for the Browse Reservations</returns>
        public List<VMBrowseReservation> RetrieveAllVMReservations()
        {
            List<VMBrowseReservation> reservations = new List<VMBrowseReservation>();

            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText = @"sp_retrieve_all_view_model_reservations";

            // command objects
            var cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VMBrowseReservation reservation = new VMBrowseReservation();
                        reservation.ReservationID = reader.GetInt32(0);
                        reservation.MemberID = reader.GetInt32(1);
                        reservation.NumberOfGuests = reader.GetInt32(2);
                        reservation.NumberOfPets = reader.GetInt32(3);
                        reservation.ArrivalDate = reader.GetDateTime(4);
                        reservation.DepartureDate = reader.GetDateTime(5);
                        reservation.Notes = reader.GetString(6);
                        reservation.Active = reader.GetBoolean(7);
                        reservation.FirstName = reader.GetString(8);
                        reservation.LastName = reader.GetString(9);
                        reservation.PhoneNumber = reader.GetString(10);
                        reservation.Email = reader.GetString(11);
                        reservations.Add(reservation);
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
            return reservations;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Retrieve Reservation will attempt to select and return a Reservation from our MSSQL database if it matches the given ReservationID
        /// </summary>
        /// <param name="ReservationID">The Reservation ID of the Reservation we wish to retrieve information about</param>
        /// <returns>Returns a Reservation which should contain the same ReservationID which was passed in</returns>
        public Reservation RetrieveReservation(int ReservationID)
        {
            Reservation reservation = new Reservation();
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_select_reservation";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            //We do not have a ReservationID since we have not created the Reservation in our database yet
            cmd1.Parameters.AddWithValue("@ReservationID", ReservationID);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation.ReservationID = reader.GetInt32(0);
                        reservation.MemberID = reader.GetInt32(1);
                        reservation.NumberOfGuests = reader.GetInt32(2);
                        reservation.NumberOfPets = reader.GetInt32(3);
                        reservation.ArrivalDate = reader.GetDateTime(4);
                        reservation.DepartureDate = reader.GetDateTime(5);
                        reservation.Notes = reader.GetString(6);
                        reservation.Active = reader.GetBoolean(7);
                        break;
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
            return reservation;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Update Reservation will attempt to update the Reservation from our MSSQL database
        /// </summary>
        /// <param name="oldReservation">The old Reservation Data</param>
        /// <param name="newReservation">The new Reservation Data</param>
        public void UpdateReservation(Reservation oldReservation, Reservation newReservation)
        {
            // get a connection
            var conn = DBConnection.GetDbConnection();

            // command text
            string cmdText1 = @"sp_update_reservation";

            // command objects
            var cmd1 = new SqlCommand(cmdText1, conn);

            // set the command type
            cmd1.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd1.Parameters.AddWithValue("@ReservationID", oldReservation.ReservationID);
            cmd1.Parameters.AddWithValue("@oldMemberID", oldReservation.MemberID);
            cmd1.Parameters.AddWithValue("@oldNumberOfGuests", oldReservation.NumberOfGuests);
            cmd1.Parameters.AddWithValue("@oldNumberOfPets", oldReservation.NumberOfPets);
            cmd1.Parameters.AddWithValue("@oldArrivalDate", oldReservation.ArrivalDate);
            cmd1.Parameters.AddWithValue("@oldDepartureDate", oldReservation.DepartureDate);
            cmd1.Parameters.AddWithValue("@oldNotes", oldReservation.Notes);
            cmd1.Parameters.AddWithValue("@oldActive", oldReservation.Active);
            cmd1.Parameters.AddWithValue("@newMemberID", newReservation.MemberID);
            cmd1.Parameters.AddWithValue("@newNumberOfGuests", newReservation.NumberOfGuests);
            cmd1.Parameters.AddWithValue("@newNumberOfPets", newReservation.NumberOfPets);
            cmd1.Parameters.AddWithValue("@newArrivalDate", newReservation.ArrivalDate);
            cmd1.Parameters.AddWithValue("@newDepartureDate", newReservation.DepartureDate);
            cmd1.Parameters.AddWithValue("@newNotes", newReservation.Notes);
            cmd1.Parameters.AddWithValue("@newActive", newReservation.Active);

            try
            {
                // open the connection
                conn.Open();
                // execute the command
                cmd1.ExecuteNonQuery();
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
        /// Author: Wes Richardson
        /// Created: 2019/04/18
        /// </summary>
        /// <remarks>
        /// James Heim
        /// 2019-04-19
        /// Added a check if Notes is null before assigning it.
        /// </remarks>
        /// <param name="guestID"></param>
        /// <returns>A Reservation</returns>
        public Reservation RetrieveReservationByGuestID(int guestID)
        {
            Reservation resv = null;

            var conn = DBConnection.GetDbConnection();

            var cmdText = @"sp_retrieve_reservation_by_guestID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GuestID", guestID);

            try
            {

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    resv = new Reservation()
                    {
                        ReservationID = reader.GetInt32(0),
                        MemberID = reader.GetInt32(1),
                        NumberOfGuests = reader.GetInt32(2),
                        NumberOfPets = reader.GetInt32(3),
                        ArrivalDate = reader.GetDateTime(4),
                        DepartureDate = reader.GetDateTime(5),
                        Active = true
                    };

                    // Notes can be nullable.
                    if (reader.IsDBNull(6))
                    {
                        resv.Notes = "";
                    }
                    else
                    {
                        resv.Notes = reader.GetString(6);
                    }

                }
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return resv;
        }
    }
}
