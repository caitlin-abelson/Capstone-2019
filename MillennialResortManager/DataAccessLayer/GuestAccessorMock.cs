using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    public class GuestAccessorMock : IGuestAccessor
    {
        private List<Guest> _guests;
        private List<VMGuest> _vmGuests;

        /// <summary>
        /// Author: Alisa Roehr
        /// Created : 2019/02/12
        /// This constructor sets up all of the dummy data that will be used
        /// 
        /// Updated By: Caitlin Abelson
        /// Date: 2019/04/13
        /// 
        /// Added VMGuest dummy data so that the mock could return list of VMGuest.
        /// </summary>
        public GuestAccessorMock()
        {
            _guests = new List<Guest>();
            _guests.Add(new Guest()
            {
                GuestID = 100000,
                MemberID = 100000,
                GuestTypeID = "Adult",
                FirstName = "Bill",
                LastName = "Smith",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });
            _guests.Add(new Guest()
            {
                GuestID = 100001,
                MemberID = 100000,
                GuestTypeID = "Adult",
                FirstName = "Bob",
                LastName = "Smith",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });
            _guests.Add(new Guest()
            {
                GuestID = 100002,
                MemberID = 100000,
                GuestTypeID = "Adult",
                FirstName = "Joe",
                LastName = "Smith",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });
            _guests.Add(new Guest()
            {
                GuestID = 100003,
                MemberID = 100000,
                GuestTypeID = "Adult",
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });
            _guests.Add(new Guest()
            {
                GuestID = 100004,
                MemberID = 100000,
                GuestTypeID = "Adult",
                FirstName = "Jacob",
                LastName = "Smith",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });

            _vmGuests = new List<VMGuest>();
            _vmGuests.Add(new VMGuest()
            {
                GuestID = 100000,
                MemberID = 100000,
                GuestTypeID = "Adult",
                FirstName = "Bill",
                LastName = "Smith",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                MemberFirstName = "Joe",
                MemberLastName = "Blow",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });

            _vmGuests.Add(new VMGuest()
            {
                GuestID = 100001,
                MemberID = 100000,
                GuestTypeID = "Adult",
                FirstName = "Will",
                LastName = "Smith",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                MemberFirstName = "Joe",
                MemberLastName = "Blow",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });

            _vmGuests.Add(new VMGuest()
            {
                GuestID = 100003,
                MemberID = 100001,
                GuestTypeID = "Adult",
                FirstName = "Bob",
                LastName = "Johnson",
                PhoneNumber = "3192860018",
                Minor = false,
                Active = true,
                Email = "abc@def.com",
                MemberFirstName = "Joe",
                MemberLastName = "Schmo",
                ReceiveTexts = true,
                EmergencyFirstName = "Albion",
                EmergencyLastName = "Bumblebee",
                EmergencyPhoneNumber = "3192885567",
                EmergencyRelation = "Brother-in-law"
            });


        }
        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/21
        /// This is meant to add a guest to the _guests
        /// </summary>
        /// <param name="newGuest">The new guest</param>
        public int CreateGuest(Guest newGuest)
        {
            _guests.Add(newGuest);
            return 1;
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/21
        /// This will simply return a list with all of our Guest data
        /// </summary>
        /// <returns>a list of type Guest containing all of our Guests</returns>
        public List<Guest> SelectAllGuests()
        {
            return _guests;
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/21
        /// This will simply return a list with all of our Guest data
        /// </summary>
        /// <param name="guestID">guestId for the searching guest</param>
        /// <returns>guest that ID was given for</returns>
        public Guest SelectGuestByGuestID(int guestID)
        {
            Guest g = new Guest();
            g = _guests.Find(x => x.GuestID == guestID);
            if (g == null)
            {
                throw new ArgumentException("GuestID did not match any Guest in our System");
            }

            return g;
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/21
        /// This will simply return a list with all of our Guest data
        /// </summary>
        /// <param name="searchLast">last name of the guest searched for</param>
        /// <param name="searchFirst">first name of the guest searched for</param>
        /// <returns>list of guests that match the name</returns>
        public List<Guest> RetrieveGuestsSearchedByName(string searchLast, string searchFirst)
        {
            List<Guest> g = new List<Guest>();
            if (searchFirst == null && searchLast == null)
            {
                g = _guests;
            }
            else if (searchFirst == null)
            {
                g = _guests.FindAll(x => x.LastName == searchLast);
                if (g == null)
                {
                    throw new ArgumentException("Last name did not match any Guest in our System");
                }
            }
            else if (searchLast == null)
            {
                g = _guests.FindAll(x => x.FirstName == searchFirst);
                if (g == null)
                {
                    throw new ArgumentException("First name did not match any Guest in our System");
                }
            }
            else
            {
                g = _guests.FindAll(x => x.LastName == searchLast && x.FirstName == searchFirst);
                if (g == null)
                {
                    throw new ArgumentException("Name did not match any Guest in our System");
                }
            }

            return g;
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/15
        /// This will update an existing Guest or throw a new ArgumentException
        /// </summary>
        /// <param name="newGuest">The new Reservatin data</param>
        /// <param name="oldGuest">The old Reservation data</param>
        public int UpdateGuest(Guest newGuest, Guest oldGuest)
        {
            bool didUpdate = false;
            foreach (var g in _guests)
            {
                if (g.GuestID == oldGuest.GuestID)
                {
                    g.MemberID = newGuest.MemberID;
                    g.GuestTypeID = newGuest.GuestTypeID;
                    g.FirstName = newGuest.FirstName;
                    g.LastName = newGuest.LastName;
                    g.PhoneNumber = newGuest.PhoneNumber;
                    g.Email = newGuest.Email;
                    g.Minor = newGuest.Minor;
                    g.Active = newGuest.Active;
                    g.ReceiveTexts = newGuest.ReceiveTexts;
                    g.EmergencyFirstName = newGuest.EmergencyFirstName;
                    g.EmergencyLastName = newGuest.EmergencyLastName;
                    g.EmergencyPhoneNumber = newGuest.EmergencyPhoneNumber;
                    g.EmergencyRelation = newGuest.EmergencyRelation;
                    didUpdate = true;
                    break;
                }
            }
            if (!didUpdate)
            {
                throw new ArgumentException("No guest was found to update");
            }
            return 1;
        }

        public List<string> SelectAllGuestTypes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/15
        /// This will deactivate an existing Guest or throw a new ArgumentException        
        /// </summary>
        /// <param name="guestID">guestId for the searching guest</param>
        public void DeactivateGuest(int guestID)
        {
            bool foundGuest = false;
            foreach (var gues in _guests)
            {
                if (gues.GuestID == guestID)
                {
                    gues.Active = false;
                    foundGuest = true;
                    break;
                }
            }
            if (!foundGuest)
            {
                throw new ArgumentException("No Guest was found with that ID");
            }
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/15
        /// This will reactivate an existing Guest or throw a new ArgumentException        
        /// </summary>
        /// <param name="guestID">guestId for the searching guest</param>
        public void ReactivateGuest(int guestID)
        {
            bool foundGuest = false;
            foreach (var gues in _guests)
            {
                if (gues.GuestID == guestID)
                {
                    gues.Active = true;
                    foundGuest = true;
                    break;
                }
            }
            if (!foundGuest)
            {
                throw new ArgumentException("No Guest was found with that ID");
            }
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/22
        /// This will check out an existing Guest or throw a new ArgumentException        
        /// </summary>
        /// <param name="guestID">guestId for the searching guest</param>
        public void CheckOutGuest(int guestID)
        {
            bool foundGuest = false;
            foreach (var gues in _guests)
            {
                if (gues.GuestID == guestID)
                {
                    gues.CheckedIn = false;
                    foundGuest = true;
                    break;
                }
            }
            if (!foundGuest)
            {
                throw new ArgumentException("No Guest was found with that ID");
            }
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/22
        /// This will check in an existing Guest or throw a new ArgumentException        
        /// </summary>
        /// <param name="guestID">guestId for the searching guest</param>
        public void CheckInGuest(int guestID)
        {
            bool foundGuest = false;
            foreach (var gues in _guests)
            {
                if (gues.GuestID == guestID)
                {
                    gues.CheckedIn = true;
                    foundGuest = true;
                    break;
                }
            }
            if (!foundGuest)
            {
                throw new ArgumentException("No Guest was found with that ID");
            }
        }

        /// <summary>
        /// Author: Alisa Roehr
        /// Created: 2019/02/15
        /// This will purge an existing Guest or throw a new ArgumentException        
        /// </summary>
        /// <param name="guestID">guestId for the searching guest</param>
        public void DeleteGuest(int guestID)
        {
            bool active = false;
            try
            {
                Guest g = SelectGuestByGuestID(guestID);
                active = g.Active;
            }
            catch (Exception)
            {
                throw new ArgumentException("No Guest was found with that ID");
            }
            if (!active)
            {
                _guests.Remove(_guests.Find(x => x.GuestID == guestID));
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
            if (_guest.GuestID.ToString().Length != 6 || _guest.GuestID == null || _guest.GuestID == 0)
            {
                return false; // for guest id, check for errors
            }
            else if ( /*_guest.MemberID.ToString().Length > 11 ||*/ _guest.MemberID == null || _guest.MemberID == 0)
            {
                return false;// for member id
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
                return false;  // for phone number
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


        public List<Guest> SelectGuestNamesAndIds() { return null; }
        public Guest RetrieveGuestInfo(int guestID)
        {
            throw new NotImplementedException();
        }
        public List<Guest> RetrieveAllGuestInfo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Author: Caitlin Abelson
        /// Date: 2019/04/13
        /// 
        /// Returns the list of mock VMGuests.
        /// </summary>
        /// <returns></returns>
        public List<VMGuest> SelectAllVMGuests()
        {
            return _vmGuests;
        }
    }
}
