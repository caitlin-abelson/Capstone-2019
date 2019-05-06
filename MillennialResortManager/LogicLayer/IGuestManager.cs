using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Alisa Roehr" created="2019/02/14">
	/// The IGuestManager interface that has all CRUD methods for Guests for the Logic Layer
	/// </summary>
	/// <updates>
	/// <update author="Caitlin Abelson" created="2019/04/12">
	/// Added the VMGuest List method in order to pull all of the Guests and 
	/// their associated Members from the VMGuest class. (SelectAllVMGuests)
	/// </update>
	/// <update author="Eduardo Colon" created="2019/03/20">
	/// Added RetrieveAllGuestInfo and RetrieveGuestInfo.
	/// </update>
	/// <update author="Eduardo Colon" created="2019/04/23">
	/// Added RetrieveAllGuestAppointmentInfo and RetrieveGuestAppointmentInfo.
	/// </update>
	/// </updates>
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
        List<Guest> RetrieveAllGuestInfo();
        Guest RetrieveGuestInfo(int guestID);
        List<VMGuest> SelectAllVMGuests();
        Guest RetrieveGuestByEmail(string email);
        List<Guest> RetrieveAllGuestAppointmentInfo();
        Guest RetrieveGuestAppointmentInfo(int guestID);
    }
}