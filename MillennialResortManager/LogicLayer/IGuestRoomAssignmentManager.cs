using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
	/// <summary author="Jared Greenfield" created="2019/04/25">
	/// Class for interaction with database for GuestRoomAssignment operations
	/// </summary>
	public interface IGuestRoomAssignmentManager
	{
		/// <summary author="Jared Greenfield" created="2019/04/25">
		/// Retrieves a list of guest Room Assignment View Models
		/// </summary>
		/// <param name="reservationID">ID of Reservation you want Guests of</param>
		/// <returns>List of Room Assignment View Models</returns>
		List<GuestRoomAssignmentVM> SelectGuestRoomAssignmentVMSByReservationID(int reservationID);

		/// <summary author="Jared Greenfield" created="2019/04/25">
		/// Updates a guest room assignment to checkedout and sets the date
		/// </summary>
		/// <param name="roomReservationID">ID of Room Reservation </param>
		/// <param name="guestID">ID of Guest</param>
		/// <returns>True if succeeded, false otherwise</returns>
		bool UpdateGuestRoomAssignmentToCheckedOut(int guestID, int roomReservationID);
    }
}