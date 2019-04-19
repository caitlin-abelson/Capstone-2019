using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Alisa Roehr
    /// Created: 2019/02/14
    /// 
    /// The IGuestAccessor interface that has all CRUD methods for Guests
    /// 
    /// Update By: Caitlin Abelson
    /// Date: 2019/04/12
    /// 
    /// Added accessor method to implement the VMSetup class
    /// </summary>
    public interface IGuestAccessor
    {
        int CreateGuest(Guest newGuest);
        List<Guest> SelectAllGuests();
        Guest SelectGuestByGuestID(int guestID);
        List<Guest> RetrieveGuestsSearchedByName(string searchLast, string searchFirst);
        List<string> SelectAllGuestTypes();
        int UpdateGuest(Guest newGuest, Guest oldGuest);
        void DeactivateGuest(int guestID);
        void ReactivateGuest(int guestID);
        void DeleteGuest(int guestID);
        void CheckInGuest(int guestID);
        void CheckOutGuest(int guestID);
        bool isValid(Guest guest);
        List<Guest> SelectGuestNamesAndIds();
        Guest RetrieveGuestInfo(int guestID); //eduardo colon 2019-03-20
        List<Guest> RetrieveAllGuestInfo();   //eduardo colon 2019-03-20

        // VMGuest List method added by Caitlin Abelson
        List<VMGuest> SelectAllVMGuests();
        Guest RetriveGuestByEmail(string email);
    }
}
