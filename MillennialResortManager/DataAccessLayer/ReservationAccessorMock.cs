using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Author: Matt LaMarche
    /// Created : 2/08/2019
    /// This is a mock Data accessor which implements the IReservationAccessor interface. It is used for testing purposes only
    /// </summary>
    public class ReservationAccessorMock: IReservationAccessor
    {
        private List<Reservation> _reservations;
        private List<int> _AllMembers;
        private List<VMBrowseReservation> _vmReservations;
        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This constructor sets up all of our dummy data we will be using
        /// </summary>
        public ReservationAccessorMock()
        {
            _reservations = new List<Reservation>();
            _reservations.Add(new Reservation() { ReservationID = 100000, MemberID = 100000, NumberOfGuests = 1, NumberOfPets = 0, ArrivalDate = new DateTime(2008, 11, 11), DepartureDate = new DateTime(2008, 11, 12), Notes = "test", Active = true});
            _reservations.Add(new Reservation() { ReservationID = 100001, MemberID = 100000, NumberOfGuests = 15, NumberOfPets = 7, ArrivalDate = new DateTime(2017, 11, 11), DepartureDate = new DateTime(2017, 11, 12), Notes = "test", Active = true});
            _reservations.Add(new Reservation() { ReservationID = 100002, MemberID = 100001, NumberOfGuests = 3, NumberOfPets = 4, ArrivalDate = new DateTime(2019, 11, 11), DepartureDate = new DateTime(2020, 11, 12), Notes = "test", Active = true});
            _reservations.Add(new Reservation() { ReservationID = 100003, MemberID = 100002, NumberOfGuests = 4, NumberOfPets = 1, ArrivalDate = new DateTime(2020, 11, 11), DepartureDate = new DateTime(2020, 11, 12), Notes = "test", Active = true });
            _AllMembers = new List<int>();
            foreach (var res in _reservations)
            {
                _AllMembers.Add(res.MemberID);
            }
            _vmReservations = new List<VMBrowseReservation>();
            _vmReservations.Add(new VMBrowseReservation() { ReservationID = 100001, MemberID = 100000, NumberOfGuests = 4, NumberOfPets = 1, ArrivalDate = new DateTime(2020, 11, 11), DepartureDate = new DateTime(2020, 11, 12), Notes = "test", Active = true, FirstName = "Spongebob", LastName = "SquarePants", Email = "swagpants@h.com", PhoneNumber = "123456789" });
            _vmReservations.Add(new VMBrowseReservation() { ReservationID = 100002, MemberID = 100001, NumberOfGuests = 4, NumberOfPets = 1, ArrivalDate = new DateTime(2020, 11, 11), DepartureDate = new DateTime(2020, 11, 12), Notes = "test", Active = true, FirstName = "Patrick", LastName = "Star", Email = "pswag@h.com", PhoneNumber = "223456789" });
            _vmReservations.Add(new VMBrowseReservation() { ReservationID = 100003, MemberID = 100002, NumberOfGuests = 4, NumberOfPets = 1, ArrivalDate = new DateTime(2020, 11, 11), DepartureDate = new DateTime(2020, 11, 12), Notes = "test", Active = true, FirstName = "Sandy", LastName = "Cheeks", Email = "cswag@h.com", PhoneNumber = "323456789" });

        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This will create a reservation using the data provided in the newReservation Reservation
        /// </summary>
        /// <param name="newReservation">The Reservation we wnat added to our mock system</param>
        public void CreateReservation(Reservation newReservation)
        {
            _reservations.Add(newReservation);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This will search for a Reservation with a matching ReservationID from our mock data
        /// </summary>
        /// <param name="ReservationID">The ReservationID we want to search our mock data for</param>
        /// <returns> This will return a Reservation object if we found a match. otherwise we will throw an exception</returns>
        public Reservation RetrieveReservation(int ReservationID)
        {
            Reservation r = new Reservation();
            r = _reservations.Find(x => x.ReservationID == ReservationID);
            if(r == null){
                throw new ArgumentException("ReservationID did not match any Reservations in our System");
            }

            return r;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This will simply return a list with all of our Reservation data
        /// </summary>
        /// <returns>a list of type Reservation containing data on all of our Reservations</returns>
        public List<Reservation> RetrieveAllReservations()
        {
            return _reservations;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This will simply return a list with all of our Reservation view model data
        /// </summary>
        /// <returns>a list of type VMBrowseReservation which contains all of our VMBrowseReservation data</returns>
        public List<VMBrowseReservation> RetrieveAllVMReservations()
        {
            return _vmReservations;
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This will update an existing Reservation or throw a new ArgumentException
        /// </summary>
        /// <param name="oldReservation">The old Reservation data</param>
        /// <param name="newReservation">The new Reservatin data</param>
        public void UpdateReservation(Reservation oldReservation, Reservation newReservation)
        {
            bool didUpdate = false;
            foreach (var res in _reservations)
            {
                if(res.ReservationID == oldReservation.ReservationID){
                    res.MemberID = newReservation.MemberID;
                    res.NumberOfGuests = newReservation.NumberOfGuests;
                    res.NumberOfPets = newReservation.NumberOfPets;
                    res.ArrivalDate = newReservation.ArrivalDate;
                    res.DepartureDate = newReservation.DepartureDate;
                    res.Notes = newReservation.Notes;
                    res.Active = newReservation.Active;
                    didUpdate = true;
                    break;
                }
            }
            if (!didUpdate)
            {
                throw new ArgumentException("No Reservation was found to update");
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This will deactivate the Reservation which has a matching ReservationID to the ReservationID provided 
        /// </summary>
        /// <param name="ReservationID"> The ReservationID of the Reseravation we would like to deactivate</param>
        public void DeactivateReservation(int ReservationID)
        {
            bool foundReservation = false;
            foreach (var res in _reservations)
            {
                if (res.ReservationID == ReservationID)
                {
                    res.Active = false;
                    foundReservation = true;
                    break;
                }
            }
            if(!foundReservation){
                throw new ArgumentException("No Reservation was found with that ID");
            }
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// This will completely purge a Reservation from our system or throw an exception if we cannot find that exception
        /// </summary>
        /// <param name="ReservationID">The ReservationID of the Reservation we are purging</param>
        public void PurgeReservation(int ReservationID)
        {
            try
            {
                RetrieveReservation(ReservationID);
            }
            catch(Exception){
                throw new ArgumentException("No Reservation was found with that ID");
            }
            _reservations.Remove(_reservations.Find(x=>x.ReservationID==ReservationID));
        }

        /// <summary>
        /// Author: Wes Richardson
        /// Created: 2019/04/18
        /// </summary>
        /// <param name="guestID"></param>
        /// <returns>A Mock Reservation</returns>
        public Reservation RetrieveReservationByGuestID(int guestID)
        {
            return _reservations.Find(x => x.MemberID == guestID);
        }

        /// <summary>
        /// Author: Matt LaMarche
        /// Created : 2/08/2019
        /// A simple validate methid which will return true if we have a valid Member whise MemberID matches the MemberID provided
        /// </summary>
        /// <param name="memberID">MemberID which we are searching for</param>
        /// <returns>true if we have a Member with a matching MemberID to the ont provided. false otherwise</returns>
        public bool ValidateMember(int memberID)
        {
            return _AllMembers.Contains(memberID);
        }
    }
}
