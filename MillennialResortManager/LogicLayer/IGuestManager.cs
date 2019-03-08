using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Alisa Roehr
    /// Created: 2019/02/14
    /// 
    /// The IGuestManager interface that has all CRUD methods for Guests for the Logic Layer
    /// </summary>
    interface IGuestManager
    {
        bool CreateGuest(Guest newGuest);
        bool EditGuest(Guest newGuest, Guest oldGuest);
        Guest ReadGuestByGuestID(int GuestID);
        List<Guest> ReadAllGuests();
        List<Guest> RetrieveGuestsSearched(string searchLast, string searchFirst);
        List<string> RetrieveGuestTypes();
        void DeactivateGuest(int guestID);
        void ReactivateGuest(int guestID);
        void CheckOutGuest(int guestID);
        void CheckInGuest(int guestID);
        void DeleteGuest(int guestID);
        List<Guest> RetrieveGuestNamesAndIds();
    }
}