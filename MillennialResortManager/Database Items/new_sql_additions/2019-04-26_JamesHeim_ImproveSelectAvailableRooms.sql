USE [MillennialResort_DB]
GO


print '' print '*** Deleting the old sp_select_room_roomreservation_viewmodel_by_reservation_id'
-- Delete the old procedure and replace it because it was bad.
DROP PROCEDURE [dbo].[sp_select_room_roomreservation_viewmodel_by_reservation_id]
GO

print '' print '*** Creating sp_select_room_roomreservation_viewmodel_by_reservation_id'
-- James Heim
-- Created 2019-04-17
-- Select all rooms and the number of guests assigned to that room so long that they 
-- are on the supplied ReservationID and they have not reached capacity.
CREATE PROCEDURE [dbo].[sp_select_room_roomreservation_viewmodel_by_reservation_id]
(
    @ReservationID [int]
)
AS
    BEGIN
    
    WITH CTE (RoomReservationID, RoomID, RoomNumber, BuildingName, CurrentlyAssigned, Capacity, CheckInDate, CheckOutDate)
    AS
    (
		SELECT RoomReservation.RoomReservationID,
				Room.RoomID, 
				Room.RoomNumber,
				Building.BuildingName, 
				(SELECT COUNT(GuestID) 
				 FROM GuestRoomAssignment GRA
				 WHERE RoomReservationID = RoomReservation.RoomReservationID) 
				 AS CurrentlyAssigned,
				 Room.Capacity,
				RoomReservation.CheckInDate,
				RoomReservation.CheckOutDate

		FROM	RoomReservation 
				LEFT JOIN GuestRoomAssignment ON GuestRoomAssignment.RoomReservationID = RoomReservation.RoomReservationID
				INNER JOIN Room ON RoomReservation.RoomID = Room.RoomID
				INNER JOIN Building ON Room.BuildingID = Building.BuildingID

		WHERE RoomReservation.ReservationID = @ReservationID
    )
    
    SELECT  RoomReservationID,
			RoomNumber,		
			BuildingName, 
			CurrentlyAssigned, 
			CheckInDate, 
			CheckOutDate
	FROM CTE
	GROUP BY RoomReservationID, CurrentlyAssigned, Capacity, RoomID, 
				 RoomNumber, BuildingName,
				 CheckInDate, CheckOutDate
		HAVING CurrentlyAssigned  < Capacity
            
            
    END
GO