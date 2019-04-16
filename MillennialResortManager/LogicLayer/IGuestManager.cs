using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Alisa Roehr
    /// Created: 2019/02/14
    /// 
    /// The IGuestManager interface that has all CRUD methods for Guests for the Logic Layer
    /// 
    /// Updated By: Caitlin Abelson
    /// Date: 2019/04/12
    /// 
    /// Added the VMGuest List method in order to pull all of the Guests and 
    /// their associated Members from the VMGuest class.
    /// </summary>
    public interface IGuestManager
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
        List<Guest> RetrieveAllGuestInfo(); //eduardo colon 2019-03-20
        Guest RetrieveGuestInfo(int guestID); //eduardo colon 2019-03-20

        // Added by Caitlin Abelson 2019/04/12
        List<VMGuest> SelectAllVMGuests();
    }
}