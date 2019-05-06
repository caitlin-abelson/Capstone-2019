using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Alisa Roehr" created="2019/02/15">
	/// GuestManager is an implementaion of the IGuestManager Interface meant to interact with the GuestAccessor
	/// </summary>
	public class GuestManager : IGuestManager
	{
		private IGuestAccessor _guestAccessor;

		/// <summary author="Alisa Roehr" created="2019/02/15">
		/// The constructor for the GuestManager class
		/// </summary>
		public GuestManager()
		{
			_guestAccessor = new GuestAccessor();
		}

		/// <summary author="Alisa Roehr" created="2019/02/15">
		/// Constructor for the mock accessor
		/// </summary>
		/// <param name="guestAccessorMock"></param>
		public GuestManager(GuestAccessorMock guestAccessorMock)
		{
			_guestAccessor = guestAccessorMock;
		}

		/// <summary author="Alisa Roehr" created="2019/02/15">
		/// Modified: 2019/03/01
		/// 
		/// Check if guest is valid or not
		/// </summary>
		/// <updates>
		/// <update author = "Alisa Roehr" date="2019/04/16 ">
		/// Made it so that other extentions can be used for email.
		/// </update>
		/// </updates>
		/// <param name="_guest"> guest that is being tested for validation</param>
		/// <returns>whether the guest information is valid</returns>
		public bool isValid(Guest _guest)
		{
			int aNumber;
			if (_guest.MemberID.ToString().Length > 11 || _guest.MemberID == null || _guest.MemberID == 0)
			{
				return false;// for member id
			}
			if (_guest.GuestTypeID.Length > 25 || _guest.GuestTypeID == null || _guest.GuestTypeID.Length == 0)
			{
				return false; // for guest type
			}
			else if (_guest.FirstName.Length > 50 || _guest.FirstName == null || _guest.FirstName.Length == 0 || _guest.FirstName.Any(c => char.IsDigit(c)))
			{
				return false; // for first name
			}
			else if (_guest.LastName.Length > 100 || _guest.LastName == null || _guest.LastName.Length == 0 || _guest.LastName.Any(c => char.IsDigit(c)))
			{
				return false; // for last name
			}
			else if (_guest.PhoneNumber.Length > 11 || _guest.PhoneNumber == null || _guest.PhoneNumber.Length == 0)
			{
				return false;  // for phone number
			}
			else if (_guest.Email.Length > 250 || _guest.Email == null || _guest.Email.Length == 0 || !_guest.Email.Contains("@") || !_guest.Email.Contains("."))
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
			else if (_guest.EmergencyFirstName.Length > 50 || _guest.EmergencyFirstName == null || _guest.EmergencyFirstName.Length == 0 || _guest.EmergencyFirstName.Any(c => char.IsDigit(c)))
			{
				return false; // for EmergencyFirstName
			}
			else if (_guest.EmergencyLastName.Length > 100 || _guest.EmergencyLastName == null || _guest.EmergencyLastName.Length == 0 || _guest.EmergencyLastName.Any(c => char.IsDigit(c)))
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

		/// <summary author="Alisa Roehr" created="2019/01/24">
		/// logic layer between the form and the accessor to the database, for trying to add a guest to the database
		/// </summary>
		/// <param name="newGuest">what is being sent to accessor to be inserted into database</param>
		/// <returns>whether the inserting into the database was successful</returns>
		public bool CreateGuest(Guest newGuest)
		{
			if (!isValid(newGuest))
			{
				throw new ArgumentException("Guest is not filled out correctly.");
			}

			bool result = false;

			try
			{
				result = (1 == _guestAccessor.CreateGuest(newGuest));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Alisa Roehr" created="2019/01/24">
		/// Used to retrieve a list of guest types to the form from the accessor database.
		/// </summary>
		/// <returns>guest types</returns>
		public List<string> RetrieveGuestTypes()
		{
			List<string> guestTypes = null;
			try
			{
				guestTypes = _guestAccessor.SelectAllGuestTypes();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return guestTypes;
		}

		/// <summary author="Alisa Roehr" created="2019/01/31">
		/// logic layer between the form and the accessor to the database, for trying to edit a guest to the database
		/// </summary>
		/// <param name="newGuest">what is being sent to accessor to be edited in the database</param>
		/// <param name="oldGuest">what is being sent to accessor to be compared in the database</param>
		/// <returns>whether the editing in the database was successful</returns>
		public bool EditGuest(Guest newGuest, Guest oldGuest)
		{
			if (!isValid(newGuest))
			{
				throw new ArgumentException("Guest is not filled out correctly.");
			}

			bool result = false;

			try
			{
				result = (1 == _guestAccessor.UpdateGuest(newGuest, oldGuest));
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Alisa Roehr" created="2019/01/31">
		/// logic layer between the form and the accessor to the database, for trying to edit a guest to the database
		/// </summary>
		/// <param name="GuestID">id of guest being looked for.</param>
		/// <returns>whether the editing in the database was successful</returns>
		public Guest ReadGuestByGuestID(int GuestID)
		{
			Guest guest = new Guest();

			try
			{
				guest = _guestAccessor.SelectGuestByGuestID(GuestID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return guest;
		}

		/// <summary author="Alisa Roehr" created="2019/01/31">
		/// logic layer between the form and the accessor to the database, for trying to edit a guest to the database
		/// </summary>
		/// <returns>List of guests</returns>
		public List<Guest> ReadAllGuests()
		{
			List<Guest> guests = new List<Guest>();

			try
			{
				guests = _guestAccessor.SelectAllGuests();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return guests;
		}

		/// <summary author="Alisa Roehr" created="2019/01/31">
		/// logic layer between the form and the accessor to the database
		/// </summary>
		/// <param name="searchFirst">first name of guest</param>
		/// <param name="searchLast">last name of guest</param>
		/// <returns>list of guests matching criteria</returns>
		public List<Guest> RetrieveGuestsSearched(string searchLast, string searchFirst)
		{
			List<Guest> searchedGuests = new List<Guest>();
			try
			{
				searchedGuests = _guestAccessor.RetrieveGuestsSearchedByName(searchLast, searchFirst);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return searchedGuests;
		}

		/// <summary author="Alisa Roehr" created="2019/02/22">
		/// This will check out an existing Guest or throw a new ArgumentException        
		/// </summary>
		/// <param name="guestID">guestId for the searching guest</param>
		public void CheckOutGuest(int guestID)
		{
			try
			{
				_guestAccessor.CheckOutGuest(guestID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Alisa Roehr" created="2019/02/22">
		/// This will check in an existing Guest or throw a new ArgumentException        
		/// </summary>
		/// <param name="guestID">guestId for the searching guest</param>
		public void CheckInGuest(int guestID)
		{
			try
			{
				_guestAccessor.CheckInGuest(guestID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Alisa Roehr" created="2019/02/15">
		/// This will deactivate an existing Guest or throw a new ArgumentException        
		/// </summary>
		/// <param name="guestID">guestId for the searching guest</param>
		public void DeactivateGuest(int guestID)
		{
			try
			{
				_guestAccessor.DeactivateGuest(guestID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Alisa Roehr" created="2019/02/15">
		/// This will reactivate an existing Guest or throw a new ArgumentException        
		/// </summary>
		/// <param name="guestID">guestId for the searching guest</param>
		public void ReactivateGuest(int guestID)
		{
			try
			{
				_guestAccessor.ReactivateGuest(guestID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Alisa Roehr" created="2019/02/15">
		/// This will purge an existing Guest or throw a new ArgumentException        
		/// </summary>
		/// <param name="guestID">guestId for the searching guest</param>
		public void DeleteGuest(int guestID)
		{
			try
			{
				_guestAccessor.DeleteGuest(guestID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Richard Carroll" created="2019/02/28">
		/// Requests a List of Guest names and Ids from the 
		/// Data Access Layer and Returns the Result.
		/// </summary>
		public List<Guest> RetrieveGuestNamesAndIds()
		{
			List<Guest> guests = new List<Guest>();

			try
			{
				guests = _guestAccessor.SelectGuestNamesAndIds();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return guests;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// Method to retrieve all guestinfo
		/// </summary>
		public List<Guest> RetrieveAllGuestInfo()
		{
			var guests = new List<Guest>();

			try
			{
				guests = _guestAccessor.RetrieveAllGuestInfo();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return guests;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// Eduardo Colon
		/// Created: 2019/03/20
		/// 
		/// method to retrieve all guestinfo by guestid
		/// </summary>
		public Guest RetrieveGuestInfo(int guestID)
		{
			Guest guest = new Guest();

			try
			{

				guest = _guestAccessor.RetrieveGuestInfo(guestID);

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return guest;
		}

		public List<VMGuest> SelectAllVMGuests()
		{
			List<VMGuest> vmGuest = new List<VMGuest>();
			try
			{
				vmGuest = _guestAccessor.SelectAllVMGuests();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return vmGuest;
		}

		/// <summary author="Austin Berquam" created="2019/04/17">
		/// Method to retrieve guest info by email
		/// </summary>
		public Guest RetrieveGuestByEmail(string email)
		{
			try
			{
				return _guestAccessor.RetriveGuestByEmail(email);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Eduardo Colon" created="2019/04/23">
		/// method to retrieve all guestappointmentinfo
		/// </summary>
		public List<Guest> RetrieveAllGuestAppointmentInfo()
		{
			var guests = new List<Guest>();
			try
			{
				guests = _guestAccessor.RetrieveAllGuestAppointmentInfo();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return guests;
		}

		/// <summary author="Eduardo Colon" created="2019/04/23">
		/// Method to retrieve all guesappointmentinfo by guestid
		/// </summary>
		public Guest RetrieveGuestAppointmentInfo(int guestID)
		{
			Guest guest = new Guest();

			try
			{

				guest = _guestAccessor.RetrieveGuestAppointmentInfo(guestID);

			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return guest;
		}
	}
}