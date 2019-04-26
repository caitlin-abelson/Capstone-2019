USE [MillennialResort_DB]
GO

INSERT INTO [RoomStatus]
	([RoomStatusID] , [Description])
	VALUES
	('Ready for Cleaning', 'The room is ready to be cleaned after a guest left.')

print 'tr_roomassign_update'
GO
-- Created By: James Heim and Jared Greenfield
-- Created On: 2019-04-25
-- Checks out a room Reservation when everryone in the room leaves.
CREATE TRIGGER [dbo].[tr_roomassign_update]
ON [GuestRoomAssignment]
AFTER UPDATE
AS
	BEGIN
		DECLARE @RoomReservationID [int]
	    SELECT @RoomReservationID = [RoomReservationID] FROM Inserted
		IF (SELECT COUNT(GuestID)
			FROM GuestRoomAssignment GRA
					--INNER JOIN Inserted ON GRA.RoomReservationID = GRA.RoomReservationID
			WHERE CheckOutDate IS NULL
			  AND GRA.RoomReservationID = @RoomReservationID
			) = 0

	UPDATE [RoomReservation]
	
	SET CheckOutDate = GETDATE()
	WHERE RoomReservationID = @RoomReservationID

	END
GO
print 'tr_roomreservation_update'
GO
-- Created By: James Heim and Jared Greenfield
-- Created On: 2019-04-25
-- Finalizes the Member's tab
CREATE TRIGGER [dbo].[tr_roomreservation_update]
ON [RoomReservation]
AFTER UPDATE
AS
	BEGIN
		DECLARE @ReservationID	[INT]
		DECLARE @RoomID			[INT]
        DECLARE @MemberID		[INT]
		DECLARE @TabID			[INT]
		DECLARE @TotalPrice		[INT]

	    SELECT @RoomID = [RoomID] FROM Inserted
	    SELECT @ReservationID = [ReservationID] FROM Inserted

	
	UPDATE [Room]
	SET [RoomStatusID] = "Ready for Cleaning"
	WHERE [Room].[RoomID] = @RoomID
	IF (SELECT COUNT(RoomReservationID) 
		FROM RoomReservation
		WHERE ReservationID = @ReservationID
		  AND CheckoutDate IS NULL
		) = 0
		BEGIN
		BEGIN TRANSACTION
			BEGIN TRY
			-- Deactivates the reservation
			UPDATE [dbo].[Reservation]
			SET [Active] = 0
			WHERE [ReservationID] = @ReservationID
			-- Retrieve Member from Reservation and Deactivate Member
			-- Makes it so no one can add items to the tab
			SELECT @MemberID = (SELECT [MemberID] FROM [dbo].[Reservation] WHERE [Reservation].[ReservationID] = @ReservationID)
			UPDATE [dbo].[Member]
			SET Member.Active = 0
			WHERE [Member].[MemberID] = @MemberID

			-- Retrieve TabID with MemberID
			SELECT @TabID = (SELECT MemberTabID FROM MemberTab WHERE MemberTab.Active = 1 AND MemberID = @MemberID)

			-- Sum up all tab items and place it in MemberTab total column
			SELECT @TotalPrice = SUM(Price * Quantity) FROM MemberTabLine WHERE MemberTabID = @TabID
			
			-- Finalize The Tab
			UPDATE [MemberTab]
			SET [Active] = 0,
				[TotalPrice] = @TotalPrice
				WHERE [MemberTabID] = @TabID
			
			END TRY
			
			BEGIN CATCH
				ROLLBACK TRANSACTION
			END CATCH
			
			COMMIT TRANSACTION
			
		END

	END
GO	
print '' print 'Creating sp_select_all_active_reservationvms'
GO
-- Created By: Jared Greenfield
-- Created On: 2019-04-25
-- Selects currently inhabited reservations
CREATE PROCEDURE [dbo].[sp_select_all_active_reservationvms]
AS
	BEGIN
		SELECT DISTINCT [Reservation].[ReservationID],
		[Reservation].[MemberID],
		[Reservation].[NumberOfGuests],
		[Reservation].[NumberOfPets],
		[Reservation].[ArrivalDate],
		[Reservation].[DepartureDate],
		[Reservation].[Notes],
		[Reservation].[Active],
		[Member].[FirstName],
		[Member].[LastName],
		[Member].[PhoneNumber],
		[Member].[Email]
		FROM Reservation INNER JOIN Member ON Reservation.MemberID = Member.MemberID
		INNER JOIN [RoomReservation] ON [Reservation].[ReservationID] = [RoomReservation].[ReservationID]
		WHERE [RoomReservation].[CheckInDate] IS NOT NULL AND 
			[RoomReservation].[CheckOutDate] IS NULL
	END

GO

print '' print 'Creating sp_select_all_guestroomassignvms_by_reservationID'
GO
-- Created By: Jared Greenfield
-- Created On: 2019-04-25
-- Selects room assignment view models by a provided reservation ID
CREATE PROCEDURE [dbo].[sp_select_all_guestroomassignvms_by_reservationID]
(
	@ReservationID [int]
)
AS
	BEGIN
		SELECT DISTINCT
			[Guest].[GuestID],
			[Guest].[FirstName],
			[Guest].[LastName],
			[Building].[BuildingName],
			[Room].[RoomNumber],
			[GuestRoomAssignment].[RoomReservationID],
			[GuestRoomAssignment].[CheckinDate],
			[GuestRoomAssignment].[CheckoutDate]
			FROM [Reservation]
			INNER JOIN [RoomReservation] ON [RoomReservation].[ReservationID] = @ReservationID
			INNER JOIN [Room] ON [Room].[RoomID] = [RoomReservation].[RoomID]
			INNER JOIN [Building] ON [Building].[BuildingID] = [Room].[BuildingID]
			INNER JOIN [GuestRoomAssignment] ON [RoomReservation].[RoomReservationID] = [GuestRoomAssignment].[RoomReservationID]
			INNER JOIN [Guest] ON [GuestRoomAssignment].[GuestID] = [Guest].[GuestID]
	END

GO
	
print '' print 'Creating sp_update_guestroomassignment_checkoutdate'
GO
-- Created By: Jared Greenfield
-- Created On: 2019-04-25
-- Updates a guest room's checkout date
CREATE PROCEDURE [dbo].[sp_update_guestroomassignment_checkoutdate]
(
	@GuestID [int],
	@RoomReservationID [int]
)
AS
	BEGIN
		UPDATE [GuestRoomAssignment]
		SET [CheckoutDate] = CURRENT_TIMESTAMP
		WHERE [GuestID] = @GuestID AND [RoomReservationID] = @RoomReservationID
		RETURN @@ROWCOUNT
	END

GO
	