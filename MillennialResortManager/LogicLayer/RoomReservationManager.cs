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
    public class RoomReservationManager : IRoomReservationManager
    {
        RoomReservationAccessor _roomReservationAccessor;

		/// <summary author="James Heim" created="2019/04/11">
		/// The default constructor for the standard MSSQL accessor.
		/// </summary>
		public RoomReservationManager()
        {
            _roomReservationAccessor = new RoomReservationAccessor();
        }

		/// <summary author="James Heim" created="2019/04/11">
		/// Constructor allows a Mock Accessor (or alternative to MSSQL) to be inserted.
		/// </summary>
		/// <param name="roomReservationAccessor"></param>
		public RoomReservationManager(RoomReservationAccessor roomReservationAccessor)
        {
            _roomReservationAccessor = roomReservationAccessor;
        }        

        public List<VMRoomRoomReservation> RetrieveAvailableVMRoomRoomReservations(int reservationId)
        {
            List<VMRoomRoomReservation> roomVMs = null;

            try
            {
                roomVMs = _roomReservationAccessor.SelectAvailableVMRoomRoomReservations(reservationId);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return roomVMs;
        }

		/// <summary author="James Heim" created="2019/04/11">
		/// Retrieve the RoomReservation by integer id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public RoomReservation RetrieveRoomReservationByID(int id)
        {
            RoomReservation roomReservation = null;

            try
            {
                roomReservation = _roomReservationAccessor.SelectRoomReservationByID(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return roomReservation;
        }

		/// <summary author="James Heim" created="2019/04/16">
		/// Retrieve the RoomReservation by integer Guest ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public RoomReservation RetrieveRoomReservationByGuestID(int id)
        {
            RoomReservation roomReservation = null;

            try
            {
                roomReservation = _roomReservationAccessor.SelectRoomReservationByGuestID(id);
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return roomReservation;
        }

		/// <summary author="James Heim" created="2019/04/11">
		/// Set the RoomReservation CheckInDate to now.
		/// </summary>
		/// <param name="roomReservation"></param>
		/// <returns></returns>
		public bool UpdateCheckInDateToNow(RoomReservation roomReservation)
        {
            bool result = false;

            try
            {
                // If the row count is 1 (updated only one) then set result to true.
                result = (1 == _roomReservationAccessor.UpdateCheckInDateToNow(roomReservation));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

		/// <summary author="James Heim" created="2019/04/12">
		/// Assign a Guest to a specific room.
		/// </summary>
		/// <param name="guestID"></param>
		/// <returns></returns>
		public bool AddGuestAssignment(int guestID, int roomReservationID)
        {
            bool result = false;

            try
            {
                result = (1 == _roomReservationAccessor.InsertGuestRoomAssignment(guestID, roomReservationID));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }

        public bool DeleteGuestAssignment(int guestID, int roomReservationID)
        {
            bool result = false;

            try
            {
                result = (1 == _roomReservationAccessor.DeleteGuestRoomAssignment(guestID, roomReservationID));
            }
			catch (Exception ex)
			{
				ExceptionLogManager.getInstance().LogException(ex);
				throw ex;
			}

			return result;
        }
    }
}