using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using ExceptionLoggerLogic;

namespace LogicLayer
{
	/// <summary author="Eduardo Colon" created="2019/03/20">
	/// Concrete class for IshuttleReservationManager.
	/// </summary>
	public class ShuttleReservationManager : IShuttleReservationManager
	{
		IShuttleReservationAccessor _shuttleReservationAccessor;
		public ShuttleReservationManager(IShuttleReservationAccessor shuttleReservationAccessor)
		{
			_shuttleReservationAccessor = shuttleReservationAccessor;
		}

		public ShuttleReservationManager()
		{
			_shuttleReservationAccessor = new ShuttleReservationAccessor();
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to create shuttle reservation
		/// </summary>
		public int CreateShuttleReservation(ShuttleReservation newShuttleReservation)
		{
			int result = 0;
			try
			{
				if (!isValid(newShuttleReservation))
				{
					throw new ArgumentException("The data for this shuttle reservation is invalid");
				}
				result = _shuttleReservationAccessor.InsertShuttleReservation(newShuttleReservation);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return result;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method  to retrieve all guestids 
		/// </summary>
		public List<int> RetrieveAllGuestIDs()
		{
			List<int> guestIDs = null;
			try
			{
				guestIDs = _shuttleReservationAccessor.RetrieveGuestIDs();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return guestIDs;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method  to retrieve all employeeids 
		/// </summary>
		public List<int> RetrieveAllEmployeeIDs()
		{
			List<int> employeeIDs = null;
			try
			{
				employeeIDs = _shuttleReservationAccessor.RetrieveEmployeeIDs();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return employeeIDs;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// Retrieves  all  active shuttle reservations
		/// </summary>
		public List<ShuttleReservation> RetrieveAllActiveShuttleReservations()
		{
			List<ShuttleReservation> shuttleReservations = new List<ShuttleReservation>();
			try
			{
				shuttleReservations = _shuttleReservationAccessor.RetrieveActiveShuttleReservations();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return shuttleReservations;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// Retrieves  all  inactive shuttle reservations
		/// </summary>
		public List<ShuttleReservation> RetrieveAllInActiveShuttleReservations()
		{
			List<ShuttleReservation> shuttleReservations = new List<ShuttleReservation>();
			try
			{
				//     shuttleReservations = _shuttleReservationAccessor.RetrieveInactiveRoles();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return shuttleReservations;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to get all shuuttlereservatons
		/// </summary>
		public List<ShuttleReservation> RetrieveAllShuttleReservations()
		{
			List<ShuttleReservation> shuttles = null;
			try
			{
				shuttles = _shuttleReservationAccessor.RetrieveAllShuttleReservations();
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return shuttles;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to retrieve shutlereservation by shuttlereservationid
		/// </summary>
		public ShuttleReservation RetrieveShutteReservationByShuttleReservationID(int shuttleReservationID)
		{
			ShuttleReservation shuttleReservation;
			try
			{
				shuttleReservation = _shuttleReservationAccessor.RetrieveShuttleReservationByShuttleReservationID(shuttleReservationID);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
			return shuttleReservation;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to update shuttle reservation
		/// </summary>
		public void UpdateShuttleReservation(ShuttleReservation oldShuttleReservation, ShuttleReservation newShuttleReservation)
		{
			try
			{
				_shuttleReservationAccessor.UpdateShuttleReservation(oldShuttleReservation, newShuttleReservation);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to update shuttle reservation dropoff
		/// </summary>
		public void UpdateShuttleReservationDropoff(ShuttleReservation oldShuttleReservation)
		{
			try
			{
				_shuttleReservationAccessor.UpdateShuttleReservationDropoff(oldShuttleReservation);
			}
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to  deactivate shuttleReservation
		/// </summary>
		public void DeactivateShuttleReservation(int shuttleReservationID, bool isActive)
		{
			// check to see if shuttlereservation is active to diactivate it.
			if (isActive == true)
			{
				try
				{
					_shuttleReservationAccessor.DeactivateShuttleReservation(shuttleReservationID);
				}
				catch (Exception ex)
				{
					ExceptionLogManager.getInstance().LogException(ex);
					throw ex;
				}
			}
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to validate pickuplocation
		/// </summary>
		public bool validatePickupLocation(string pickupLocation)
		{
			if (pickupLocation.Length < 1 || pickupLocation.Length > 150)
			{
				return false;
			}
			return true;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to validate dropoffdestination
		/// </summary>
		public bool validateDropoffDestination(string dropoffDestination)
		{
			if (dropoffDestination.Length < 1 || dropoffDestination.Length > 150)
			{
				return false;
			}
			return true;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to validate pickupdatetime
		/// </summary>
		public bool validatePickupDateTime(DateTime pickupDateTime)
		{
			if (pickupDateTime == null)
			{
				return false;
			}

			//Business rule. Jsut saying Arrival date cannot be before the Resort was created 
			if (pickupDateTime.Year < 1900)
			{
				return false;
			}
			return true;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to validate first name
		/// </summary>
		public bool validateFirstName(string firstName)
		{
			if (firstName.Length < 1 || firstName.Length > 50)
			{
				return false;
			}
			return true;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to validate last name
		/// </summary>
		public bool validateLastName(string lastName)
		{
			if (lastName.Length < 1 || lastName.Length > 100)
			{
				return false;
			}
			return true;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to validate phone number
		/// </summary>
		public bool validatePhoneNumber(string phoneNumber)
		{
			if (phoneNumber.Length != 11)
			{
				return false;
			}
			return true;
		}

		/// <summary author="Eduardo Colon" created="2019/03/20">
		/// method to check validation for all users inputs methods
		/// </summary>
		public bool isValid(ShuttleReservation shuttleReservation)
		{
			if (validatePickupLocation(shuttleReservation.PickupLocation) && validateDropoffDestination(shuttleReservation.DropoffDestination) &&
				validatePickupDateTime(shuttleReservation.PickupDateTime)
			   )
			//&& ValidatePickupDate(shuttleReservation.PickupDate
			{
				return true;
			}

			return false;
		}
	}
}