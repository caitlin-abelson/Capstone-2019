using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 1/24/2019
    /// ReservationManagerMSSQL Is an implementation of the IReservationManager Interface meant to interact with the MSSQL ReservationAccessor
    /// </summary>
    public class ReservationManagerMSSQL : IReservationManager
    {
        private IReservationAccessor _reservationAccessor;
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// Constructor which allows us to implement the Reservation Accessor methods
        /// </summary>
        public ReservationManagerMSSQL()
        {
           _reservationAccessor = new ReservationAccessorMSSQL();
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// Constructor which allows us to implement which ever Reservation Accessor we need to use
        /// </summary>
        public ReservationManagerMSSQL(IReservationAccessor reservationAccessor)
        {
            _reservationAccessor = reservationAccessor;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/24/2019
        /// Modified : 2/7/2019, 4/24/2019 updated from void to bool.
        /// Passes along a Reservation object to our ReservationAccessorMSSQL to be stored in our database
        /// </summary>
        /// <param name="newReservation">Contains the information for the Reservation which will be added to our system</param>
        public bool AddReservation(Reservation newReservation)
        {
            bool worked = false;
            try
            {
                //Double Check the Reservation is Valid
                if (!newReservation.IsValid() || !ValidateMember(newReservation.MemberID))
                {
                    throw new ArgumentException("Data for this Reservation is not valid");
                }
                _reservationAccessor.CreateReservation(newReservation);
                worked = true;
            }
            catch (Exception)
            {
                throw;
            }
            return worked;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Delete Reservation will determing whether the Reservation needs to be deleted or deactivated and request deactivation or deletion from a Reservation Accessor
        /// </summary>
        /// <param name="ReservationID">The ID of the Reservation which we are deleting or deactivating</param>
        /// <param name="isActive">The active status from the Reservation we are deleting or deactivating</param>
        public void DeleteReservation(int ReservationID, bool isActive)
        {
            if (isActive)
            {
                //Is Active so we just deactivate it
                try
                {
                    _reservationAccessor.DeactivateReservation(ReservationID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                //Is Deactive so we purge it
                try
                {
                    _reservationAccessor.PurgeReservation(ReservationID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Modified : 2/7/2019
        /// Sends Existing Reservation data along with the new Reservation data to Reservation Accessor. Returns an error if update fails 
        /// </summary>
        /// <param name="oldReservation">The old Reservation Data</param>
        /// <param name="newReservation">The new Reservation Data</param>
        public void EditReservation(Reservation oldReservation, Reservation newReservation)
        {
            try
            {
                if (!newReservation.IsValid() || !ValidateMember(newReservation.MemberID))
                {
                    throw new ArgumentException("Data for this new Reservation is not valid");
                }
                _reservationAccessor.UpdateReservation(oldReservation,newReservation);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Author: Jared Greenfield
        /// Created : 2019-04-25
        /// Retrieves active Reservations
        /// </summary>
        public List<VMBrowseReservation> RetrieveAllActiveVMReservations()
        {
            List<VMBrowseReservation> reservations = new List<VMBrowseReservation>();
            try
            {
                reservations = _reservationAccessor.RetrieveAllActiveVMReservations();
            }
            catch (Exception)
            {
                throw;
            }
            return reservations;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Retrieves all the Reservations in our system from a Reservation Accessor or an error if there was a problem
        /// </summary>
        /// <returns> Returns a List of all the Reservations in our System</returns>
        public List<Reservation> RetrieveAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            try
            {
                reservations = _reservationAccessor.RetrieveAllReservations();
            }
            catch (Exception)
            {
                throw;
            }
            return reservations;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Retrieves all the Browse Reservation View Models in our system from a Reservation Accessor or an error if there was a problem
        /// </summary>
        /// <returns>Returns a List of all View Models for the Browse Reservations</returns>
        public List<VMBrowseReservation> RetrieveAllVMReservations()
        {
            List<VMBrowseReservation> reservations = new List<VMBrowseReservation>();
            try
            {
                reservations = _reservationAccessor.RetrieveAllVMReservations();
            }
            catch (Exception)
            {
                throw;
            }
            return reservations;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 1/29/2019
        /// Returns a Reservation from a ReservationAccessor or throws an error if there was a problem
        /// </summary>
        /// <param name="ReservationID">The Reservation ID of the Reservation we wish to retrieve information about</param>
        /// <returns>Returns a Reservation which should contain the same ReservationID which was passed in</returns>
        public Reservation RetrieveReservation(int ReservationID)
        {
            Reservation reservation = new Reservation();
            try
            {
                reservation = _reservationAccessor.RetrieveReservation(ReservationID);
            }
            catch (Exception)
            {
                throw new ArgumentException("ReservationID did not match any Reservations in our System");
            }
            return reservation;      
        }

        /// <summary>
        /// Author: Wes Richardson
        /// Created: 2019/04/18
        /// </summary>
        /// <param name="guestID"></param>
        /// <returns>A Reservation</returns>
        public Reservation RetrieveReservationByGuestID(int guestID)
        {
            Reservation resv = null;
            try
            {
                resv = _reservationAccessor.RetrieveReservationByGuestID(guestID);
                if (resv == null)
                {
                    throw new ApplicationException("No Reservations Found");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resv;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/07/2019
        /// A simple validate method to confirm 
        /// </summary>
        /// <param name="memberID">An ID for a Member which we want to validate</param>
        /// <returns>returns true if there is a valid member with the given memberID in our database</returns>
        private bool ValidateMember(int memberID)
        {
            bool valid = false;
            try
            {
                valid = _reservationAccessor.ValidateMember(memberID);
            }
            catch (Exception)
            {
                throw;
            }
            return valid;
        }
    }
}
