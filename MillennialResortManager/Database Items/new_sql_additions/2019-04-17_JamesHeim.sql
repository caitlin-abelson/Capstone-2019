USE [MillennialResort_DB]
GO

print '' print '*** Updating sp_select_building_by_id'
-- James Heim
-- Updated 2019-04-19
-- Updated select list to include ResortPropertyID
DROP PROCEDURE [dbo].[sp_select_building_by_id]
GO
CREATE PROCEDURE [dbo].[sp_select_building_by_id]
	(
		@BuildingID		[nvarchar](50)
	)
AS
	BEGIN
		SELECT	[BuildingID], [BuildingName], [Address], [Description], [BuildingStatusID], 
				[ResortPropertyID]
		FROM	[Building]
		WHERE	[BuildingID] = @BuildingID
	END
GO

-- James Heim
-- Created 2019-04-16
-- Selects the Member's only open reservation.
CREATE PROCEDURE [dbo].[sp_select_active_reservation_by_member_id]
(
	@MemberID	 [int]
)
AS
	BEGIN
		SELECT  [ReservationID], [MemberID], [NumberOfGuests], [NumberOfPets], 
				[ArrivalDate], [DepartureDate], [Notes], [Active]
		FROM 	Reservation
		WHERE 	[MemberID] = @MemberID
		  AND 	[Active] = 1
	END
GO

print '' print '*** Creating sp_select_roomreservation_by_guest_id'
GO
-- James Heim
-- Created 2019-04-04
-- Find the RoomReservation associated with a guest via the GuestRoomAssignment table.
CREATE PROCEDURE [dbo].[sp_select_roomreservation_by_guest_id]
(
	@GuestID		[int]
)
AS
	BEGIN
		SELECT	RoomReservation.RoomReservationID, 
				RoomReservation.RoomID, 
				RoomReservation.ReservationID, 
				RoomReservation.CheckInDate, RoomReservation.CheckOutDate
		FROM	GuestRoomAssignment
			INNER JOIN RoomReservation ON GuestRoomAssignment.RoomReservationID = RoomReservation.RoomReservationID
		WHERE	GuestRoomAssignment.GuestID = @GuestID
	END
GO

print '' print '*** Creating sp_select_room_roomreservation_viewmodel_by_guest_id'
GO
-- James Heim
-- Created 2019-04-17
CREATE PROCEDURE [dbo].[sp_select_room_roomreservation_viewmodel_by_reservation_id]
(
	@ReservationID	[int]
)
AS
	BEGIN
	
		SELECT	RoomReservation.RoomReservationID,
				Room.RoomID, 
				Room.RoomNumber,
				Building.BuildingName, 
				(SELECT COUNT(GuestID) 
				 FROM GuestRoomAssignment GRA
				 WHERE RoomReservationID = RoomReservation.RoomReservationID) 
				 AS CurrentGuests,
				 Room.Capacity,
				RoomReservation.CheckInDate,
				RoomReservation.CheckOutDate

		FROM	RoomReservation 
				LEFT JOIN GuestRoomAssignment ON GuestRoomAssignment.RoomReservationID = RoomReservation.RoomReservationID
				INNER JOIN Room ON RoomReservation.RoomID = Room.RoomID
				INNER JOIN Building ON Room.BuildingID = Building.BuildingID

		WHERE RoomReservation.ReservationID = @ReservationID

		GROUP BY RoomReservation.RoomReservationID, Room.Capacity, Room.RoomID, 
				 Room.RoomNumber, Building.BuildingName,
				 RoomReservation.CheckInDate, RoomReservation.CheckOutDate
		HAVING (SELECT COUNT(GuestID) 
				 FROM GuestRoomAssignment GRA
				 WHERE RoomReservationID = RoomReservation.RoomReservationID)  < Room.Capacity
	END
GO

print '' print '*** Creating sp_assign_room_reservation'
GO
-- James Heim
-- Created 2019-04-04
CREATE PROCEDURE [dbo].[sp_assign_room_reservation]
(
	@GuestID 			[int],
	@RoomReservationID 	[int]
)
AS
	BEGIN
		INSERT INTO GuestRoomAssignment([GuestID], [RoomReservationID])
			VALUES
			(@GuestID, @RoomReservationID)
	END
GO


print '' print '*** Creating sp_unassign_room_reservation'
GO
-- James Heim
-- Created 2019-04-04
CREATE PROCEDURE [dbo].[sp_unassign_room_reservation]
(
	@GuestID 			[int],
	@RoomReservationID 	[int]
)
AS
	BEGIN
		DELETE 
		FROM GuestRoomAssignment
		WHERE GuestID = @GuestID
		  AND RoomReservationID = @RoomReservationID
		  
		RETURN @@ROWCOUNT
	END
GO

-- James Heim
-- Created 2019-04-11
-- Set CheckInDate to now.
print '' print '*** Creating sp_update_checkindate'
GO
CREATE PROCEDURE [dbo].[sp_update_checkindate]
(
	@RoomReservationID [int]
)
AS
	BEGIN
		UPDATE RoomReservation
			SET CheckInDate = GETDATE()
			
			WHERE RoomReservationID = @RoomReservationID
		RETURN @@ROWCOUNT
	END
GO